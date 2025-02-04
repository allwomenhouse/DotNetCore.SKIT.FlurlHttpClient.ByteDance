﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp
{
    /// <summary>
    /// 为 <see cref="ByteDanceMicroAppClient"/> 提供回调通知事件的扩展方法。
    /// </summary>
    public static class ByteDanceMicroAppClientEventExtensions
    {
        private class InnerEncryptedEvent
        {
            [Newtonsoft.Json.JsonProperty("Encrypt")]
            [System.Text.Json.Serialization.JsonPropertyName("Encrypt")]
            public string EncryptedData { get; set; } = default!;

            [Newtonsoft.Json.JsonProperty("TimeStamp")]
            [System.Text.Json.Serialization.JsonPropertyName("TimeStamp")]
            [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Converters.NumericalStringConverter))]
            public string TimestampString { get; set; } = default!;

            [Newtonsoft.Json.JsonProperty("Nonce")]
            [System.Text.Json.Serialization.JsonPropertyName("Nonce")]
            public string Nonce { get; set; } = default!;

            [Newtonsoft.Json.JsonProperty("MsgSignature")]
            [System.Text.Json.Serialization.JsonPropertyName("MsgSignature")]
            public string Signature { get; set; } = default!;
        }

        private static string InnerDecryptEventData(string sMsgEncrypt, string encodingAESKey)
        {
            if (string.IsNullOrEmpty(sMsgEncrypt))
                throw new Exceptions.ByteDanceMicroAppEventSerializationException("Decrypt event failed, because of empty encrypted data.");

            if (string.IsNullOrEmpty(encodingAESKey))
                throw new Exceptions.ByteDanceMicroAppEventSerializationException("Decrypt event failed, because there is no encoding AES key.");

            byte[] bCipher = Convert.FromBase64String(sMsgEncrypt);
            byte[] bKey = Convert.FromBase64String(encodingAESKey + "=");
            byte[] bIV = new byte[16];
            Array.Copy(bKey, bIV, 16);

            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.None;
            aes.Key = bKey;
            aes.IV = bIV;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
            {
                byte[] bTmp1 = new byte[bCipher.Length + 32 - bCipher.Length % 32];
                Array.Copy(bCipher, bTmp1, bCipher.Length);
                cs.Write(bCipher, 0, bCipher.Length);

                byte[] bTmp2 = ms.ToArray();

                int pad = bTmp2[bTmp2.Length - 1];
                pad = (pad < 1 || pad > 32) ? 0 : pad;
                byte[] bTmp3 = new byte[bTmp2.Length - pad];
                Array.Copy(bTmp2, 0, bTmp3, 0, bTmp2.Length - pad);

                const int RANDOM_BYTES_POS = 32;
                byte[] bPos = new byte[4];
                Array.Copy(bTmp3, RANDOM_BYTES_POS, bPos, 0, 4);

                int len = BitConverter.ToInt32(bPos, 16);
                len = IPAddress.NetworkToHostOrder(len);
                byte[] bMsg = new byte[len];
                Array.Copy(bTmp3, RANDOM_BYTES_POS + 4, bMsg, 0, len);
                return Encoding.UTF8.GetString(bMsg);
            }
        }

        private static bool InnerVerifyEventSignature(string sToken, string sTimestamp, string sNonce, string sMsgEncrypt, string sMsgSign)
        {
            ISet<string> set = new SortedSet<string>(StringComparer.Ordinal);
            set.Add(sToken);
            set.Add(sTimestamp);
            set.Add(sNonce);
            set.Add(sMsgEncrypt);

            string raw = string.Join(string.Empty, set.ToArray());
            string sign = Utilities.SHA1Utility.Hash(raw);
            return string.Equals(sign, sMsgSign, StringComparison.OrdinalIgnoreCase);
        }

        private static TEvent InnerDeserializeEventFromJson<TEvent>(ByteDanceMicroAppClient client, string callbackJson)
            where TEvent : ByteDanceMicroAppEvent
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrEmpty(callbackJson)) throw new ArgumentNullException(callbackJson);

            try
            {
                if (callbackJson.Contains("\"Encrypt\""))
                {
                    InnerEncryptedEvent encryptedEvent = client.JsonSerializer.Deserialize<InnerEncryptedEvent>(callbackJson);
                    callbackJson = InnerDecryptEventData(sMsgEncrypt: encryptedEvent.EncryptedData, encodingAESKey: client.Credentials.PushEncodingAESKey!);
                }

                return client.JsonSerializer.Deserialize<TEvent>(callbackJson);
            }
            catch (ByteDanceMicroAppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exceptions.ByteDanceMicroAppEventSerializationException("Deserialize event failed. Please see the `InnerException` for more details.", ex);
            }
        }

        private static TEvent InnerDeserializeEventFromXml<TEvent>(ByteDanceMicroAppClient client, string callbackXml)
            where TEvent : ByteDanceMicroAppEvent
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (string.IsNullOrEmpty(callbackXml)) throw new ArgumentNullException(callbackXml);

            try
            {
                if (callbackXml.Contains("<Encrypt>") && callbackXml.Contains("</Encrypt>"))
                {
                    string encryptedData = XDocument.Parse(callbackXml).Element("Encrypt").Value;
                    callbackXml = InnerDecryptEventData(sMsgEncrypt: encryptedData, encodingAESKey: client.Credentials.PushEncodingAESKey!);
                }

                return Utilities.XmlUtility.Deserialize<TEvent>(callbackXml);
            }
            catch (ByteDanceMicroAppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exceptions.ByteDanceMicroAppEventSerializationException("Deserialize event failed. Please see the `InnerException` for more details.", ex);
            }
        }

        /// <summary>
        /// <para>从 JSON 反序列化得到 <see cref="ByteDanceMicroAppEvent"/> 对象。</para>
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="client"></param>
        /// <param name="callbackJson"></param>
        /// <returns></returns>
        public static TEvent DeserializeEventFromJson<TEvent>(this ByteDanceMicroAppClient client, string callbackJson)
            where TEvent : ByteDanceMicroAppEvent, ByteDanceMicroAppEvent.Serialization.IJsonSerializable, new()
        {
            return InnerDeserializeEventFromJson<TEvent>(client, callbackJson);
        }

        /// <summary>
        /// <para>从 JSON 反序列化得到 <see cref="ByteDanceMicroAppEvent"/> 对象。</para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callbackJson"></param>
        /// <returns></returns>
        public static ByteDanceMicroAppEvent DeserializeEventFromJson(this ByteDanceMicroAppClient client, string callbackJson)
        {
            return InnerDeserializeEventFromJson<ByteDanceMicroAppEvent>(client, callbackJson);
        }

        /// <summary>
        /// <para>从 XML 反序列化得到 <see cref="ByteDanceMicroAppEvent"/> 对象。</para>
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="client"></param>
        /// <param name="callbackXml"></param>
        /// <returns></returns>
        public static TEvent DeserializeEventFromXml<TEvent>(this ByteDanceMicroAppClient client, string callbackXml)
            where TEvent : ByteDanceMicroAppEvent, ByteDanceMicroAppEvent.Serialization.IXmlSerializable, new()
        {
            return InnerDeserializeEventFromXml<TEvent>(client, callbackXml);
        }

        /// <summary>
        /// <para>从 XML 反序列化得到 <see cref="ByteDanceMicroAppEvent"/> 对象。</para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callbackXml"></param>
        /// <returns></returns>
        public static ByteDanceMicroAppEvent DeserializeEventFromXml(this ByteDanceMicroAppClient client, string callbackXml)
        {
            return InnerDeserializeEventFromXml<ByteDanceMicroAppEvent>(client, callbackXml);
        }

        /// <summary>
        /// <para>验证回调通知事件签名。</para>
        /// <para>REF: https://microapp.bytedance.com/docs/zh-CN/mini-app/develop/component/message-push-customer-service </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callbackTimestamp">头条回调通知中的 timestamp 字段。</param>
        /// <param name="callbackNonce">头条回调通知中的 nonce 字段。</param>
        /// <param name="callbackMessage">头条回调通知中的 msg 字段。</param>
        /// <param name="callbackSignature">头条回调通知中的 signature 字段。</param>
        /// <returns></returns>
        public static bool VerifyEventSignatureForEcho(this ByteDanceMicroAppClient client, string callbackTimestamp, string callbackNonce, string callbackMessage, string callbackSignature)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (callbackTimestamp == null) throw new ArgumentNullException(nameof(callbackTimestamp));
            if (callbackNonce == null) throw new ArgumentNullException(nameof(callbackNonce));
            if (callbackMessage == null) throw new ArgumentNullException(nameof(callbackMessage));
            if (callbackSignature == null) throw new ArgumentNullException(nameof(callbackSignature));

            ISet<string> set = new SortedSet<string>(StringComparer.Ordinal) { client.Credentials.PushToken!, callbackTimestamp, callbackNonce, callbackMessage };
            string sign = Utilities.SHA1Utility.Hash(string.Concat(set));
            return string.Equals(sign, callbackSignature, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// <para>验证回调通知事件签名。</para>
        /// <para>REF: https://microapp.bytedance.com/docs/zh-CN/mini-app/thirdparty/overview-guide/encryption/ </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callbackJson"></param>
        /// <returns></returns>
        public static bool VerifyEventSignatureFromJson(this ByteDanceMicroAppClient client, string callbackJson)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (callbackJson == null) throw new ArgumentNullException(nameof(callbackJson));

            try
            {
                InnerEncryptedEvent encryptedEvent = client.JsonSerializer.Deserialize<InnerEncryptedEvent>(callbackJson);
                return InnerVerifyEventSignature(
                    sToken: client.Credentials.PushToken!,
                    sTimestamp: encryptedEvent.TimestampString,
                    sNonce: encryptedEvent.Nonce,
                    sMsgEncrypt: encryptedEvent.EncryptedData,
                    sMsgSign: encryptedEvent.Signature
                );
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// <para>验证回调通知事件签名。</para>
        /// <para>REF: https://microapp.bytedance.com/docs/zh-CN/mini-app/thirdparty/overview-guide/encryption/ </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="callbackXml"></param>
        /// <returns></returns>
        public static bool VerifyEventSignatureFromXml(this ByteDanceMicroAppClient client, string callbackXml)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (callbackXml == null) throw new ArgumentNullException(nameof(callbackXml));

            try
            {
                InnerEncryptedEvent encryptedEvent = Utilities.XmlUtility.Deserialize<InnerEncryptedEvent>(callbackXml);
                return InnerVerifyEventSignature(
                    sToken: client.Credentials.PushToken!,
                    sTimestamp: encryptedEvent.TimestampString,
                    sNonce: encryptedEvent.Nonce,
                    sMsgEncrypt: encryptedEvent.EncryptedData,
                    sMsgSign: encryptedEvent.Signature
                );
            }
            catch
            {
                return false;
            }
        }
    }
}

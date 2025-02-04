﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Events
{
    /// <summary>
    /// <para>表示 EVENT.AUTHORIZED 事件的数据。</para>
    /// <para>REF: https://microapp.bytedance.com/docs/zh-CN/mini-app/thirdparty/API/authorization/send </para>
    /// </summary>
    public class ComponentAuthorizedEvent : ByteDanceMicroAppEvent, ByteDanceMicroAppEvent.Serialization.IJsonSerializable, ByteDanceMicroAppEvent.Serialization.IXmlSerializable
    {
        /// <summary>
        /// 获取或设置授权码。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("AuthorizationCode")]
        [System.Text.Json.Serialization.JsonPropertyName("AuthorizationCode")]
        [System.Xml.Serialization.XmlElement("AuthorizationCode")]
        public string AuthCode { get; set; } = default!;

        /// <summary>
        /// 获取或设置授权码有效期（单位：秒）。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("AuthorizationCodeExpiresIn")]
        [System.Text.Json.Serialization.JsonPropertyName("AuthorizationCodeExpiresIn")]
        [System.Xml.Serialization.XmlElement("AuthorizationCodeExpiresIn")]
        public long AuthCodeExpiresIn { get; set; }
    }
}

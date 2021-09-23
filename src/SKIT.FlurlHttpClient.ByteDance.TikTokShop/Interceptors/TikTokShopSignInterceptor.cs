﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SKIT.FlurlHttpClient.ByteDance.TikTokShop.Interceptors
{
    internal partial class TikTokShopSignInterceptor : FlurlHttpCallInterceptor
    {
        private readonly string _signMethod;
        private readonly string _appKey;
        private readonly string _appSecret;

        public TikTokShopSignInterceptor(string signMethod, string appKey, string appSecret)
        {
            _signMethod = signMethod;
            _appKey = appKey;
            _appSecret = appSecret;
        }

        public override async Task BeforeCallAsync(FlurlCall flurlCall)
        {
            if (flurlCall == null) throw new ArgumentNullException(nameof(flurlCall));

            var queries = HttpUtility.ParseQueryString(flurlCall.HttpRequestMessage.RequestUri?.Query ?? string.Empty);
            string method = queries.Get("method") ?? string.Empty;
            string version = queries.Get("v") ?? string.Empty;
            string timestamp = DateTimeOffset.Now.ToLocalTime().ToUnixTimeSeconds().ToString();

            string plainText;
            string signText;

            try
            {
                string body = string.Empty;
                if (flurlCall.HttpRequestMessage.Content is MultipartFormDataContent)
                {
                    // NOTICE: multipart/form-data 文件上传请求的待签名参数需特殊处理
                    var formdataContent = (MultipartFormDataContent)flurlCall.HttpRequestMessage.Content;
                    var httpContent = formdataContent.SingleOrDefault(e => Constants.FormDataFields.FORMDATA_PARAM_JSON.Equals(e.Headers.ContentDisposition?.Name?.Trim('\"')));
                    if (httpContent != null)
                    {
                        body = await httpContent.ReadAsStringAsync().ConfigureAwait(false);
                    }
                }
                else
                {
                    body = flurlCall.RequestBody ?? string.Empty;
                }

                plainText = $"{_appSecret}app_key{_appKey}method{method}{Constants.FormDataFields.FORMDATA_PARAM_JSON}{body}timestamp{timestamp}v{version}{_appSecret}";

                switch (_signMethod)
                {
                    case Constants.SignAlgorithms.MD5:
                        {
                            signText = Security.MD5Utility.Hash(plainText);
                        }
                        break;

                    case Constants.SignAlgorithms.HMAC_SHA256:
                        {
                            signText = Security.HMACSHA256Utility.Hash(_appSecret, plainText);
                        }
                        break;

                    default:
                        throw new Exceptions.TikTokShopRequestSignatureException("Unsupported sign method.");
                }
            }
            catch (Exceptions.TikTokShopRequestSignatureException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exceptions.TikTokShopRequestSignatureException("Generate signature of request failed. Please see the `InnerException` for more details.", ex);
            }

            flurlCall.Request.SetQueryParam("app_key", _appKey);
            flurlCall.Request.SetQueryParam("sign_method", _signMethod);
            flurlCall.Request.SetQueryParam("sign", signText);
            flurlCall.Request.SetQueryParam("timestamp", timestamp);
        }
    }
}

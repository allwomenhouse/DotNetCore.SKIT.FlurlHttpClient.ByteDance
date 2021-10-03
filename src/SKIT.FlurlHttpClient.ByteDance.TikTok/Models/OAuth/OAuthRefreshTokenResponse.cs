﻿namespace SKIT.FlurlHttpClient.ByteDance.TikTok.Models
{
    /// <summary>
    /// <para>表示 [POST] /oauth/refresh_token 接口的响应。</para>
    /// </summary>
    public class OAuthRefreshTokenResponse : TikTokResponse<OAuthRefreshTokenResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : OAuthAccessTokenResponse.Types.Data
            {
            }
        }

        /// <summary>
        /// 获取或设置返回信息。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message")]
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}

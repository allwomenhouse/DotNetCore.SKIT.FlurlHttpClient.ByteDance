﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /openapi/v1/microapp/app/modify_server_domain 接口的响应。</para>
    /// </summary>
    public class OpenApiMicroAppAppModifyServerDomainV1Response : ByteDanceMicroAppResponse
    {
        public static class Types
        {
            public class Data
            {
                /// <summary>
                /// 获取或设置 HTTP 域名列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("request")]
                [System.Text.Json.Serialization.JsonPropertyName("request")]
                public string[] RequestDomainList { get; set; } = default!;

                /// <summary>
                /// 获取或设置 WebSocket 域名列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("socket")]
                [System.Text.Json.Serialization.JsonPropertyName("socket")]
                public string[] SocketDomainList { get; set; } = default!;

                /// <summary>
                /// 获取或设置上传域名列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("upload")]
                [System.Text.Json.Serialization.JsonPropertyName("upload")]
                public string[] UploadDomainList { get; set; } = default!;

                /// <summary>
                /// 获取或设置下载域名列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("download")]
                [System.Text.Json.Serialization.JsonPropertyName("download")]
                public string[] DownloadDomainList { get; set; } = default!;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("errno")]
        [System.Text.Json.Serialization.JsonPropertyName("errno")]
        public override long ErrorCode { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty("message")]
        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public override string? ErrorMessage { get; set; }

        /// <summary>
        /// 获取或设置返回数据。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("data")]
        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public Types.Data? Data { get; set; }
    }
}

﻿namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /apps/censor/image 接口的请求。</para>
    /// </summary>
    public class AppsCensorImageRequest : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        [Newtonsoft.Json.JsonProperty("access_token")]
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public override string? AccessToken { get; set; }

        /// <summary>
        /// 获取或设置字节小程序的 AppId。如果不指定将使用构造 <see cref="ByteDanceMicroAppClient"/> 时的 <see cref="ByteDanceMicroAppClientOptions.AppId"/> 参数。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("app_id")]
        [System.Text.Json.Serialization.JsonPropertyName("app_id")]
        public string? AppId { get; set; }

        /// <summary>
        /// 获取或设置图片 URL。与字段 <see cref="ImageData"/> 二选一。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("image")]
        [System.Text.Json.Serialization.JsonPropertyName("image")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 获取或设置经过 Base64 编码的图片数据。与字段 <see cref="ImageUrl"/> 二选一。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("image_data")]
        [System.Text.Json.Serialization.JsonPropertyName("image_data")]
        public string? ImageData { get; set; }
    }
}

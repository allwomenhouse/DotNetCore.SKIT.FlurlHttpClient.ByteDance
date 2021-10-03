﻿namespace SKIT.FlurlHttpClient.ByteDance.TikTok.Models
{
    /// <summary>
    /// <para>表示 [POST] /video/delete 接口的请求。</para>
    /// </summary>
    public class VideoDeleteRequest : TikTokRequest
    {
        /// <summary>
        /// 获取或设置用户的 OpenId。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置视频 ID。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("item_id")]
        [System.Text.Json.Serialization.JsonPropertyName("item_id")]
        public string ItemId { get; set; } = string.Empty;
    }
}

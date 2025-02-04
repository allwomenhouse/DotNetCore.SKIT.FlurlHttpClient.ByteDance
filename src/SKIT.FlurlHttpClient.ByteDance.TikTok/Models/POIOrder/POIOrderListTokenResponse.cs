﻿namespace SKIT.FlurlHttpClient.ByteDance.TikTok.Models
{
    /// <summary>
    /// <para>表示 [GET] /poi/order/list/token 接口的响应。</para>
    /// </summary>
    public class POIOrderListTokenResponse : TikTokResponse<POIOrderListTokenResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : TikTokResposneData
            {
                /// <summary>
                /// 获取或设置订单下载凭证。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("order_file_token")]
                [System.Text.Json.Serialization.JsonPropertyName("order_file_token")]
                public string DownloadToken { get; set; } = default!;
            }
        }
    }
}

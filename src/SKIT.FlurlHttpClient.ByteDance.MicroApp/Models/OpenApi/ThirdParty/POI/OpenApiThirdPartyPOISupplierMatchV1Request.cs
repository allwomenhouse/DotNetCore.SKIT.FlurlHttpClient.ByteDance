﻿using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /openapi/v1/tp/poi/supplier/match 接口的请求。</para>
    /// </summary>
    public class OpenApiThirdPartyPOISupplierMatchV1Request : ByteDanceMicroAppRequest
    {
        public static class Types
        {
            public class MatchData
            {
                /// <summary>
                /// 获取或设置纬度。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("latitude")]
                [System.Text.Json.Serialization.JsonPropertyName("latitude")]
                public double Latitude { get; set; }

                /// <summary>
                /// 获取或设置经度。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("longitude")]
                [System.Text.Json.Serialization.JsonPropertyName("longitude")]
                public double Longitude { get; set; }

                /// <summary>
                /// 获取或设置 POI 名称。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("poi_name")]
                [System.Text.Json.Serialization.JsonPropertyName("poi_name")]
                public string POIName { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置外部商铺 ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("supplier_ext_id")]
                [System.Text.Json.Serialization.JsonPropertyName("supplier_ext_id")]
                public string SupplierExternalId { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置抖音 POI ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("poi_id")]
                [System.Text.Json.Serialization.JsonPropertyName("poi_id")]
                public string? POIId { get; set; }

                /// <summary>
                /// 获取或设置高德 POI ID。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("amap_id")]
                [System.Text.Json.Serialization.JsonPropertyName("amap_id")]
                public string? AMapId { get; set; }

                /// <summary>
                /// 获取或设置省份。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("province")]
                [System.Text.Json.Serialization.JsonPropertyName("province")]
                public string Province { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置城市。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("city")]
                [System.Text.Json.Serialization.JsonPropertyName("city")]
                public string City { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置地址。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("address")]
                [System.Text.Json.Serialization.JsonPropertyName("address")]
                public string Address { get; set; } = string.Empty;

                /// <summary>
                /// 获取或设置其他信息。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("extra")]
                [System.Text.Json.Serialization.JsonPropertyName("extra")]
                public string? Extra { get; set; }
            }
        }

        /// <summary>
        /// 获取或设置第三方应用 AppId。如果不指定将使用构造 <see cref="ByteDanceMicroAppClient"/> 时的 <see cref="ByteDanceMicroAppClientOptions.AppId"/> 参数。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? ComponentAppId { get; set; }

        /// <summary>
        /// 获取或设置第三方应用 AccessToken。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string ComponentAccessToken { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置上传的匹配数据列表。
        /// </summary>
        [Newtonsoft.Json.JsonProperty("match_data_list")]
        [System.Text.Json.Serialization.JsonPropertyName("match_data_list")]
        public IList<Types.MatchData> MatchDataList { get; set; } = new List<Types.MatchData>();
    }
}

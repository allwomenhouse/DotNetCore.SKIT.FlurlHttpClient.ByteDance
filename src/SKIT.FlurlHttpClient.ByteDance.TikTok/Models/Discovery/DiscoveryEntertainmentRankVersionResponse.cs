﻿using System;

namespace SKIT.FlurlHttpClient.ByteDance.TikTok.Models
{
    /// <summary>
    /// <para>表示 [GET] /discovery/ent/rank/version 接口的响应。</para>
    /// </summary>
    public class DiscoveryEntertainmentRankVersionResponse : TikTokResponse<DiscoveryEntertainmentRankVersionResponse.Types.Data>
    {
        public static class Types
        {
            public class Data : TikTokResposneData
            {
                public static class Types
                {
                    public class AlbumVersion
                    {
                        /// <summary>
                        /// 获取或设置榜单类型。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("type")]
                        [System.Text.Json.Serialization.JsonPropertyName("type")]
                        public int Type { get; set; }

                        /// <summary>
                        /// 获取或设置榜单版本。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("version")]
                        [System.Text.Json.Serialization.JsonPropertyName("version")]
                        public int Version { get; set; }

                        /// <summary>
                        /// 获取或设置榜单生成时间。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("active_time")]
                        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.RegularDateTimeOffsetConverter))]
                        [System.Text.Json.Serialization.JsonPropertyName("active_time")]
                        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Converters.RegularDateTimeOffsetConverter))]
                        public DateTimeOffset ActiveTime { get; set; }

                        /// <summary>
                        /// 获取或设置榜单开始时间。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("start_time")]
                        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.RegularDateTimeOffsetConverter))]
                        [System.Text.Json.Serialization.JsonPropertyName("start_time")]
                        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Converters.RegularDateTimeOffsetConverter))]
                        public DateTimeOffset StartTime { get; set; }

                        /// <summary>
                        /// 获取或设置榜单结束时间。
                        /// </summary>
                        [Newtonsoft.Json.JsonProperty("end_time")]
                        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.RegularDateTimeOffsetConverter))]
                        [System.Text.Json.Serialization.JsonPropertyName("end_time")]
                        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Converters.RegularDateTimeOffsetConverter))]
                        public DateTimeOffset EndTime { get; set; }
                    }
                }

                /// <summary>
                /// 获取或设置榜单版本列表。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("list")]
                [System.Text.Json.Serialization.JsonPropertyName("list")]
                public Types.AlbumVersion[] VersionList { get; set; } = default!;

                /// <summary>
                /// 获取或设置分页游标。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("cursor")]
                [System.Text.Json.Serialization.JsonPropertyName("cursor")]
                public long? PageCursor { get; set; }

                /// <summary>
                /// 获取或设置是否还有更多。
                /// </summary>
                [Newtonsoft.Json.JsonProperty("has_more")]
                [System.Text.Json.Serialization.JsonPropertyName("has_more")]
                public bool HasMore { get; set; }
            }
        }
    }
}

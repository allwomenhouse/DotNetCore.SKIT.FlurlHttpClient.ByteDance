﻿using System.Collections.Generic;

namespace SKIT.FlurlHttpClient.ByteDance.MicroApp.Models
{
    /// <summary>
    /// <para>表示 [POST] /openapi/v1/microapp/package/rollback 接口的请求。</para>
    /// </summary>
    public class OpenApiV1MicroAppPackageRollbackRequest : ByteDanceMicroAppRequest
    {
        /// <summary>
        /// 获取或设置第三方应用 AppId。如果不指定将使用构造 <see cref="ByteDanceMicroAppClient"/> 时的 <see cref="ByteDanceMicroAppClientOptions.AppId"/> 参数。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string? ComponentAppId { get; set; }
    }
}

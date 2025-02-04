﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.TikTokShop
{
    public static class TikTokShopClientExecuteFreightTemplateExtensions
    {
        /// <summary>
        /// <para>异步调用 [POST] /freightTemplate/list 接口。</para>
        /// <para>REF: https://op.jinritemai.com/docs/api-docs/14/565 </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.FreightTemplateListResponse> ExecuteFreightTemplateListAsync(this TikTokShopClient client, Models.FreightTemplateListRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Post, "freightTemplate", "list")
                .SetQueryParam("access_token", request.AccessToken);

            return await client.SendRequestWithJsonAsync<Models.FreightTemplateListResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
    }
}

﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SKIT.FlurlHttpClient.ByteDance.TikTok
{
    public static class TikTokClientExecuteHotSearchExtensions
    {
        /// <summary>
        /// <para>异步调用 [GET] /hotsearch/sentences 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/data-open-service/hot-video-data/get-current-hot-words </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.HotSearchSentencesResponse> ExecuteHotSearchSentencesAsync(this TikTokClient client, Models.HotSearchSentencesRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "hotsearch", "sentences")
                .SetQueryParam("access_token", request.AccessToken);

            return await client.SendRequestWithJsonAsync<Models.HotSearchSentencesResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /hotsearch/trending/sentences 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/data-open-service/hot-video-data/get-ascending-words </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.HotSearchTrendingSentencesResponse> ExecuteHotSearchTrendingSentencesAsync(this TikTokClient client, Models.HotSearchTrendingSentencesRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "hotsearch", "trending", "sentences")
                .SetQueryParam("access_token", request.AccessToken)
                .SetQueryParam("cursor", request.PageCursor)
                .SetQueryParam("count", request.PageSize);

            return await client.SendRequestWithJsonAsync<Models.HotSearchTrendingSentencesResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// <para>异步调用 [GET] /hotsearch/videos 接口。</para>
        /// <para>REF: https://developer.open-douyin.com/docs/resource/zh-CN/dop/develop/openapi/data-open-service/hot-video-data/get-hot-words-polymerization-video </para>
        /// </summary>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Models.HotSearchVideosResponse> ExecuteHotSearchVideosAsync(this TikTokClient client, Models.HotSearchVideosRequest request, CancellationToken cancellationToken = default)
        {
            if (client is null) throw new ArgumentNullException(nameof(client));
            if (request is null) throw new ArgumentNullException(nameof(request));

            IFlurlRequest flurlReq = client
                .CreateRequest(request, HttpMethod.Get, "hotsearch", "videos")
                .SetQueryParam("access_token", request.AccessToken)
                .SetQueryParam("hot_sentence", request.HotSentence);

            return await client.SendRequestWithJsonAsync<Models.HotSearchVideosResponse>(flurlReq, data: request, cancellationToken: cancellationToken);
        }
    }
}

﻿namespace SKIT.FlurlHttpClient.ByteDance.TikTokShop.Events
{
    /// <summary>
    /// <para>表示 doudian_refund_ArbitrateCancelled 消息的数据。</para>
    /// <para>REF: https://op.jinritemai.com/docs/message-docs/31/260 </para>
    /// </summary>
    public class RefundArbitrateCancelledEvent : TikTokShopEvent<RefundArbitrateCancelledEvent.Types.Data>
    {
        public static class Types
        {
            public class Data : RefundArbitrateAppliedEvent.Types.Data
            {
            }
        }
    }
}

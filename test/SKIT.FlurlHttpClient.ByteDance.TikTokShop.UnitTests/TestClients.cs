﻿namespace SKIT.FlurlHttpClient.ByteDance.TikTokShop.UnitTests
{
    internal class TestClients
    {
        static TestClients()
        {
            Instance = new TikTokShopClient(new TikTokShopClientOptions()
            {
                AppKey = TestConfigs.TikTokShopAppKey,
                AppSecret = TestConfigs.TikTokShopAppSecret
            });
        }

        public static readonly TikTokShopClient Instance;
    }
}

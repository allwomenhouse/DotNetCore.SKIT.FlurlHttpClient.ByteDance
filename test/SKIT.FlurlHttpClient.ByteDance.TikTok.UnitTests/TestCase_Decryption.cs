﻿using Xunit;

namespace SKIT.FlurlHttpClient.ByteDance.TikTok.UnitTests
{
    public class TestCase_Decryption
    {
        [Fact(DisplayName = "测试用例：解密手机号")]
        public void TestDecryptMobileNumber()
        {
            string secret = "0123456789abcdef0123456789abcdef";
            string enctext = "tyUWQwYuUmVFJtElAL+D7Q==";
            string rawtext = new TikTokClient(new TikTokClientOptions() { ClientSecret = secret }).DecryptMobileNumber(enctext);
            Assert.Equal("12345678910", rawtext);
        }
    }
}

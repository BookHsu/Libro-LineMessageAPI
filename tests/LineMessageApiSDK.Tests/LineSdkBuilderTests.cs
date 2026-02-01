using System.Net.Http;
using LineMessageApiSDK.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class LineSdkBuilderTests
    {
        [TestMethod]
        public void Builder_Default_Should_Enable_Webhook()
        {
            // 預設應該啟用 Webhook 模組
            var sdk = new LineSdkBuilder("token-value").Build();

            Assert.IsNotNull(sdk.Webhook);
            Assert.IsNull(sdk.Messages);
            Assert.IsNull(sdk.Profiles);
            Assert.IsNull(sdk.Groups);
        }

        [TestMethod]
        public void Builder_Should_Disable_Webhook_When_Configured()
        {
            // 停用 Webhook 後應為 null
            var sdk = new LineSdkBuilder("token-value")
                .DisableWebhook()
                .Build();

            Assert.IsNull(sdk.Webhook);
        }

        [TestMethod]
        public void Builder_Should_Enable_Selected_Modules()
        {
            // 指定載入訊息、檔案、群組模組
            var sdk = new LineSdkBuilder("token-value")
                .UseMessages()
                .UseProfiles()
                .UseGroups()
                .Build();

            Assert.IsNotNull(sdk.Webhook);
            Assert.IsNotNull(sdk.Messages);
            Assert.IsNotNull(sdk.Profiles);
            Assert.IsNotNull(sdk.Groups);
        }

        [TestMethod]
        public void Builder_Should_Accept_Custom_Dependencies()
        {
            // 使用自訂序列化器與 HttpClient
            var serializer = new StubSerializer();
            var httpClient = new HttpClient();

            var sdk = new LineSdkBuilder("token-value")
                .WithSerializer(serializer)
                .WithHttpClient(httpClient)
                .UseMessages()
                .Build();

            Assert.IsNotNull(sdk.Messages);
        }

        private class StubSerializer : IJsonSerializer
        {
            public string Serialize<T>(T value)
            {
                // 測試用序列化器回傳空字串
                return string.Empty;
            }

            public T Deserialize<T>(string value)
            {
                // 測試用序列化器回傳預設值
                return default;
            }
        }
    }
}

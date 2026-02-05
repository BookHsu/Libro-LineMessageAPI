using System.Net.Http;
using System.Reflection;
using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libro.LineMessageApi.Tests
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

        [TestMethod]
        public void Builder_Should_Use_Custom_SyncAdapterFactory()
        {
            var factory = new StubSyncAdapterFactory();

            var sdk = new LineSdkBuilder("token-value")
                .WithHttpClientSyncAdapterFactory(factory)
                .UseMessages()
                .Build();

            var messageService = sdk.Messages as MessageService;
            Assert.IsNotNull(messageService);

            var contextField = typeof(MessageService)
                .GetField("context", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(contextField);

            var context = (LineApiContext)contextField.GetValue(messageService);
            Assert.AreSame(factory, context.SyncAdapterFactory);
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

        private sealed class StubSyncAdapterFactory : IHttpClientSyncAdapterFactory
        {
            public IHttpClientSyncAdapter Create(HttpClient client)
            {
                return new HttpClientSyncAdapter(client ?? new HttpClient());
            }
        }
    }
}


using Libro.LineMessageApi.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class HttpClientProviderTests
    {
        [TestMethod]
        public void DefaultProvider_Should_Create_New_Client_When_None_Injected()
        {
            // 建立預設提供者（未注入 HttpClient）
            var provider = new DefaultHttpClientProvider(null);

            bool shouldDispose;
            var client = provider.GetClient("token-value", out shouldDispose);

            // 驗證應回傳新建的 HttpClient
            Assert.IsTrue(shouldDispose);
            Assert.AreEqual("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual("token-value", client.DefaultRequestHeaders.Authorization.Parameter);

            // 釋放資源
            client.Dispose();
        }

        [TestMethod]
        public void DefaultProvider_Should_Use_Injected_Client()
        {
            // 建立並注入 HttpClient
            var injectedClient = new HttpClient();
            var provider = new DefaultHttpClientProvider(injectedClient);

            bool shouldDispose;
            var client = provider.GetClient("token-value", out shouldDispose);

            // 驗證使用同一個 HttpClient
            Assert.IsFalse(shouldDispose);
            Assert.AreSame(injectedClient, client);
            Assert.AreEqual("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual("token-value", client.DefaultRequestHeaders.Authorization.Parameter);

            // 手動釋放注入的 HttpClient
            injectedClient.Dispose();
        }
    }
}


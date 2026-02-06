using Libro.LineMessageApi.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Reflection;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class HttpClientProviderTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ClearSharedClients();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ClearSharedClients();
        }

        [TestMethod]
        public void DefaultProvider_Should_Cache_Client_When_None_Injected()
        {
            // 建立預設提供者（未注入 HttpClient）
            var provider = new DefaultHttpClientProvider(null);

            bool shouldDispose;
            var client = provider.GetClient("token-value", out shouldDispose);

            // 驗證應回傳快取中的 HttpClient
            Assert.IsFalse(shouldDispose);
            Assert.AreEqual("Bearer", client.DefaultRequestHeaders.Authorization.Scheme);
            Assert.AreEqual("token-value", client.DefaultRequestHeaders.Authorization.Parameter);
        }

        [TestMethod]
        public void DefaultProvider_Should_Reuse_Cached_Client_For_Same_Token()
        {
            var provider = new DefaultHttpClientProvider(null);

            bool shouldDispose1;
            var client1 = provider.GetClient("token-value", out shouldDispose1);
            bool shouldDispose2;
            var client2 = provider.GetClient("token-value", out shouldDispose2);

            Assert.IsFalse(shouldDispose1);
            Assert.IsFalse(shouldDispose2);
            Assert.AreSame(client1, client2);
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

        [TestMethod]
        public void DefaultProvider_Should_Return_Disposable_Client_When_Cache_Is_Full()
        {
            var provider = new DefaultHttpClientProvider(null);
            var max = GetMaxSharedClients();

            for (var i = 0; i < max; i++)
            {
                bool shouldDispose;
                var client = provider.GetClient($"token-{i}", out shouldDispose);
                Assert.IsFalse(shouldDispose);
            }

            bool overflowDispose;
            var overflowClient = provider.GetClient("token-overflow", out overflowDispose);

            Assert.IsTrue(overflowDispose);
            overflowClient.Dispose();
        }

        private static void ClearSharedClients()
        {
            var shared = GetSharedClients();
            foreach (var item in shared)
            {
                item.Value.Dispose();
            }
            shared.Clear();
        }

        private static ConcurrentDictionary<string, HttpClient> GetSharedClients()
        {
            var field = typeof(DefaultHttpClientProvider)
                .GetField("SharedClients", BindingFlags.NonPublic | BindingFlags.Static);
            if (field == null)
            {
                throw new InvalidOperationException("SharedClients field not found.");
            }

            return (ConcurrentDictionary<string, HttpClient>)field.GetValue(null);
        }

        private static int GetMaxSharedClients()
        {
            var field = typeof(DefaultHttpClientProvider)
                .GetField("MaxSharedClients", BindingFlags.NonPublic | BindingFlags.Static);
            if (field == null)
            {
                throw new InvalidOperationException("MaxSharedClients field not found.");
            }

            return (int)field.GetRawConstantValue();
        }
    }
}


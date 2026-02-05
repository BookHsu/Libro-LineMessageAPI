using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Method;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class MessageContentApiSyncAdapterTests
    {
        [TestMethod]
        public void GetUserUploadData_Should_Use_SyncAdapterFactory()
        {
            using var client = new HttpClient(new NoopHandler());
            var provider = new StubHttpClientProvider(client);
            var adapter = new StubSyncAdapter(new byte[] { 1, 2, 3 });
            var factory = new TrackingSyncAdapterFactory(adapter);

            var api = new MessageContentApi(provider, factory);
            var result = api.GetUserUploadData("token-value", "message-id");

            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, result);
            Assert.AreSame(client, factory.LastClient);
            Assert.AreEqual(1, factory.CreateCount);
        }

        private sealed class StubHttpClientProvider : IHttpClientProvider
        {
            private readonly HttpClient client;

            public StubHttpClientProvider(HttpClient client)
            {
                this.client = client ?? throw new ArgumentNullException(nameof(client));
            }

            public HttpClient GetClient(string channelAccessToken, out bool shouldDispose)
            {
                shouldDispose = false;
                return client;
            }
        }

        private sealed class TrackingSyncAdapterFactory : IHttpClientSyncAdapterFactory
        {
            private readonly IHttpClientSyncAdapter adapter;

            public TrackingSyncAdapterFactory(IHttpClientSyncAdapter adapter)
            {
                this.adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            }

            public int CreateCount { get; private set; }

            public HttpClient LastClient { get; private set; }

            public IHttpClientSyncAdapter Create(HttpClient client)
            {
                CreateCount++;
                LastClient = client;
                return adapter;
            }
        }

        private sealed class StubSyncAdapter : IHttpClientSyncAdapter
        {
            private readonly byte[] bytes;

            public StubSyncAdapter(byte[] bytes)
            {
                this.bytes = bytes ?? throw new ArgumentNullException(nameof(bytes));
            }

            public string GetString(string url)
            {
                return string.Empty;
            }

            public byte[] GetByteArray(string url)
            {
                return bytes;
            }

            public HttpResponseMessage Post(string url, HttpContent content)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            public HttpResponseMessage Put(string url, HttpContent content)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            public HttpResponseMessage Delete(string url)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        private sealed class NoopHandler : HttpMessageHandler
        {
            protected override HttpResponseMessage Send(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                System.Threading.CancellationToken cancellationToken)
            {
                return System.Threading.Tasks.Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }
    }
}

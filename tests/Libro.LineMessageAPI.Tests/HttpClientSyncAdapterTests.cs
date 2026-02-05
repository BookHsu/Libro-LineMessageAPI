using Libro.LineMessageApi.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class HttpClientSyncAdapterTests
    {
        [TestMethod]
        public void GetString_Should_Return_Response_Content()
        {
            var handler = new RecordingHandler(_ =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("ok", Encoding.UTF8, "text/plain")
                });
            using var client = new HttpClient(handler);
            var adapter = new HttpClientSyncAdapter(client);

            var result = adapter.GetString("http://example.com/test");

            Assert.AreEqual("ok", result);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
        }

        [TestMethod]
        public void GetByteArray_Should_Return_Response_Content()
        {
            var payload = new byte[] { 1, 2, 3, 4 };
            var handler = new RecordingHandler(_ =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                });
            using var client = new HttpClient(handler);
            var adapter = new HttpClientSyncAdapter(client);

            var result = adapter.GetByteArray("http://example.com/bytes");

            CollectionAssert.AreEqual(payload, result);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
        }

        [TestMethod]
        public void Post_Should_Send_Request_With_Content()
        {
            var handler = new RecordingHandler(_ =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var adapter = new HttpClientSyncAdapter(client);
            using var content = new StringContent("payload", Encoding.UTF8, "text/plain");

            using var response = adapter.Post("http://example.com/post", content);

            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.IsNotNull(handler.LastRequest?.Content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void Factory_Should_Reuse_Adapter_For_Same_HttpClient()
        {
            var factory = new DefaultHttpClientSyncAdapterFactory();
            using var client = new HttpClient(new RecordingHandler(_ =>
                new HttpResponseMessage(HttpStatusCode.OK)));

            var first = factory.Create(client);
            var second = factory.Create(client);

            Assert.AreSame(first, second);
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly Func<HttpRequestMessage, HttpResponseMessage> responder;

            public RecordingHandler(Func<HttpRequestMessage, HttpResponseMessage> responder)
            {
                this.responder = responder ?? throw new ArgumentNullException(nameof(responder));
            }

            public HttpRequestMessage LastRequest { get; private set; }

            protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                LastRequest = request;
                return responder(request);
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                LastRequest = request;
                return Task.FromResult(responder(request));
            }
        }
    }
}

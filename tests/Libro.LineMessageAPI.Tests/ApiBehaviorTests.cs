using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.SendMessage;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class ApiBehaviorTests
    {
        [TestMethod]
        public void BroadcastApi_Should_Post_Payload_To_Broadcast_Endpoint()
        {
            var serializer = new StubSerializer { SerializedValue = "{\"ok\":true}" };
            var adapter = new RecordingSyncAdapter();
            var api = new BroadcastApi(
                serializer,
                new StubHttpClientProvider(new HttpClient(new NoopHandler())),
                new StubSyncAdapterFactory(adapter));

            var result = api.SendBroadcast("token-value", new BroadcastMessage());

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Post, adapter.LastMethod);
            Assert.AreEqual(LineApiEndpoints.BuildBroadcastMessage(), adapter.LastUrl);
            Assert.AreEqual(serializer.SerializedValue, adapter.LastContent);
        }

        [TestMethod]
        public void MessageValidationApi_Should_Post_To_Validate_Endpoint()
        {
            var serializer = new StubSerializer { SerializedValue = "{\"messages\":[]}" };
            var adapter = new RecordingSyncAdapter();
            var api = new MessageValidationApi(
                serializer,
                new StubHttpClientProvider(new HttpClient(new NoopHandler())),
                new StubSyncAdapterFactory(adapter));

            var result = api.Validate("token-value", "reply", new ReplyMessage("token"));

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Post, adapter.LastMethod);
            Assert.AreEqual(LineApiEndpoints.BuildValidateMessage("reply"), adapter.LastUrl);
            Assert.AreEqual(serializer.SerializedValue, adapter.LastContent);
        }

        [TestMethod]
        public void InsightApi_Should_Get_And_Deserialize_Followers()
        {
            var expected = new FollowerInsightResponse();
            var serializer = new StubSerializer { NextDeserializeResult = expected };
            var adapter = new RecordingSyncAdapter
            {
                NextGetStringResponse = "{\"followers\":1}"
            };
            var api = new InsightApi(
                serializer,
                new StubHttpClientProvider(new HttpClient(new NoopHandler())),
                new StubSyncAdapterFactory(adapter));

            var result = api.GetFollowers("token-value");

            Assert.AreSame(expected, result);
            Assert.AreEqual(HttpMethod.Get, adapter.LastMethod);
            Assert.AreEqual(LineApiEndpoints.BuildFollowerInsight(), adapter.LastUrl);
            Assert.AreEqual(adapter.NextGetStringResponse, serializer.LastDeserializeInput);
        }

        [TestMethod]
        public void WebhookEndpointApi_Should_Test_Endpoint_And_Deserialize()
        {
            var expected = new WebhookTestResponse();
            var serializer = new StubSerializer { NextDeserializeResult = expected };
            var adapter = new RecordingSyncAdapter
            {
                NextResponseContent = "{\"status\":\"ok\"}"
            };
            var api = new WebhookEndpointApi(
                serializer,
                new StubHttpClientProvider(new HttpClient(new NoopHandler())),
                new StubSyncAdapterFactory(adapter));

            var result = api.TestWebhookEndpoint("token-value");

            Assert.AreSame(expected, result);
            Assert.AreEqual(HttpMethod.Post, adapter.LastMethod);
            Assert.AreEqual(LineApiEndpoints.BuildWebhookTest(), adapter.LastUrl);
            Assert.AreEqual("{}", adapter.LastContent);
            Assert.AreEqual(adapter.NextResponseContent, serializer.LastDeserializeInput);
        }

        [TestMethod]
        public void RichMenuApi_Should_Delete_RichMenu()
        {
            var serializer = new StubSerializer();
            var adapter = new RecordingSyncAdapter();
            var api = new RichMenuApi(
                serializer,
                new StubHttpClientProvider(new HttpClient(new NoopHandler())),
                new StubSyncAdapterFactory(adapter));

            var result = api.DeleteRichMenu("token-value", "rich-id");

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Delete, adapter.LastMethod);
            Assert.AreEqual(LineApiEndpoints.BuildRichMenuId("rich-id"), adapter.LastUrl);
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

        private sealed class StubSyncAdapterFactory : IHttpClientSyncAdapterFactory
        {
            private readonly IHttpClientSyncAdapter adapter;

            public StubSyncAdapterFactory(IHttpClientSyncAdapter adapter)
            {
                this.adapter = adapter ?? throw new ArgumentNullException(nameof(adapter));
            }

            public IHttpClientSyncAdapter Create(HttpClient client)
            {
                return adapter;
            }
        }

        private sealed class RecordingSyncAdapter : IHttpClientSyncAdapter
        {
            public HttpMethod LastMethod { get; private set; }

            public string LastUrl { get; private set; }

            public string LastContent { get; private set; }

            public string NextGetStringResponse { get; set; } = string.Empty;

            public byte[] NextGetBytesResponse { get; set; } = Array.Empty<byte>();

            public HttpStatusCode NextStatusCode { get; set; } = HttpStatusCode.OK;

            public string NextResponseContent { get; set; } = "{}";

            public string GetString(string url)
            {
                LastMethod = HttpMethod.Get;
                LastUrl = url;
                return NextGetStringResponse;
            }

            public byte[] GetByteArray(string url)
            {
                LastMethod = HttpMethod.Get;
                LastUrl = url;
                return NextGetBytesResponse;
            }

            public HttpResponseMessage Post(string url, HttpContent content)
            {
                LastMethod = HttpMethod.Post;
                LastUrl = url;
                LastContent = content?.ReadAsStringAsync().GetAwaiter().GetResult();
                return CreateResponse();
            }

            public HttpResponseMessage Put(string url, HttpContent content)
            {
                LastMethod = HttpMethod.Put;
                LastUrl = url;
                LastContent = content?.ReadAsStringAsync().GetAwaiter().GetResult();
                return CreateResponse();
            }

            public HttpResponseMessage Delete(string url)
            {
                LastMethod = HttpMethod.Delete;
                LastUrl = url;
                return CreateResponse();
            }

            private HttpResponseMessage CreateResponse()
            {
                return new HttpResponseMessage(NextStatusCode)
                {
                    Content = new StringContent(NextResponseContent, Encoding.UTF8, "application/json")
                };
            }
        }

        private sealed class StubSerializer : IJsonSerializer
        {
            public string SerializedValue { get; set; } = "{}";

            public object NextDeserializeResult { get; set; }

            public string LastDeserializeInput { get; private set; }

            public string Serialize<T>(T value)
            {
                return SerializedValue;
            }

            public T Deserialize<T>(string value)
            {
                LastDeserializeInput = value;
                if (NextDeserializeResult is T typed)
                {
                    return typed;
                }

                return default;
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

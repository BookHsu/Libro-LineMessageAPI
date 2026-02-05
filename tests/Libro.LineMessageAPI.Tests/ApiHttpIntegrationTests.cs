using Libro.LineMessageApi;
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
using System.Threading;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class ApiHttpIntegrationTests
    {
        [TestMethod]
        public async Task AccountLinkApi_Should_Post_To_LinkToken_Endpoint()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"linkToken\":\"abc\"}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new AccountLinkApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.IssueLinkTokenAsync("token", "user").ConfigureAwait(false);

            Assert.AreEqual("abc", result.linkToken);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildLinkToken("user"), handler.LastRequest?.RequestUri?.ToString());
            Assert.AreEqual("{}", handler.LastContent);
        }

        [TestMethod]
        public async Task AudienceApi_Should_Post_Upload_Request()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"audienceGroupId\":1}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var serializer = new SystemTextJsonSerializer();
            var api = new AudienceApi(serializer, new DefaultHttpClientProvider(client), null);

            var result = await api.UploadAudienceGroupAsync("token", new { name = "group" }).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildAudienceGroupUpload(), handler.LastRequest?.RequestUri?.ToString());
            StringAssert.Contains(handler.LastContent, "\"name\":\"group\"");
        }

        [TestMethod]
        public async Task BotApi_Should_Get_Bot_Info()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"userId\":\"U1\"}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new BotApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.GetBotInfoAsync("token").ConfigureAwait(false);

            Assert.AreEqual("U1", result.userId);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildBotInfo(), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task BroadcastApi_Should_Post_Broadcast_Message()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK));
            using var client = new HttpClient(handler);
            var serializer = new SystemTextJsonSerializer();
            var api = new BroadcastApi(serializer, new DefaultHttpClientProvider(client), null);

            var result = await api.SendBroadcastAsync("token", new BroadcastMessage()).ConfigureAwait(false);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildBroadcastMessage(), handler.LastRequest?.RequestUri?.ToString());
            StringAssert.Contains(handler.LastContent, "\"messages\"");
        }

        [TestMethod]
        public async Task GroupApi_Should_Post_Leave_Request()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK));
            using var client = new HttpClient(handler);
            var api = new GroupApi(new DefaultHttpClientProvider(client), null);

            var result = await api.LeaveRoomOrGroupAsync("token", "group-id", SourceType.group).ConfigureAwait(false);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildLeaveGroupOrRoom(SourceType.group, "group-id"), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task InsightApi_Should_Get_Message_Delivery()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"status\":\"ready\"}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new InsightApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.GetMessageDeliveryAsync("token", "20250101").ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildMessageDeliveryInsight("20250101"), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task MessageContentApi_Should_Get_Bytes()
        {
            var payload = new byte[] { 1, 2, 3 };
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(payload)
                });
            using var client = new HttpClient(handler);
            var api = new MessageContentApi(new DefaultHttpClientProvider(client), null);

            var result = await api.GetUserUploadDataAsync("token", "message-id").ConfigureAwait(false);

            CollectionAssert.AreEqual(payload, result);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildMessageContent("message-id"), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task MessageSendApi_Should_Post_Reply()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var serializer = new SystemTextJsonSerializer();
            var api = new MessageSendApi(serializer, new DefaultHttpClientProvider(client), null);

            var result = await api.SendMessageActionAsync("token", PostMessageType.Reply, new ReplyMessage("reply-token"))
                .ConfigureAwait(false);

            Assert.AreEqual(string.Empty, result);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildReplyMessage(), handler.LastRequest?.RequestUri?.ToString());
            StringAssert.Contains(handler.LastContent, "\"replyToken\":\"reply-token\"");
        }

        [TestMethod]
        public async Task MessageValidationApi_Should_Post_Validate()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK));
            using var client = new HttpClient(handler);
            var serializer = new SystemTextJsonSerializer();
            var api = new MessageValidationApi(serializer, new DefaultHttpClientProvider(client), null);

            var result = await api.ValidateAsync("token", "reply", new ReplyMessage("reply-token")).ConfigureAwait(false);

            Assert.IsTrue(result);
            Assert.AreEqual(HttpMethod.Post, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildValidateMessage("reply"), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task ProfileApi_Should_Get_User_Profile()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"userId\":\"U1\",\"displayName\":\"User\"}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new ProfileApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.GetUserProfileAsync("token", "U1").ConfigureAwait(false);

            Assert.AreEqual("U1", result.userId);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildUserProfile("U1"), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task RichMenuApi_Should_Get_List()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"richmenus\":[]}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new RichMenuApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.GetRichMenuListAsync("token").ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildRichMenuList(), handler.LastRequest?.RequestUri?.ToString());
        }

        [TestMethod]
        public async Task WebhookEndpointApi_Should_Get_Endpoint()
        {
            var handler = new RecordingHandler(() =>
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"endpoint\":\"https://example.com\"}", Encoding.UTF8, "application/json")
                });
            using var client = new HttpClient(handler);
            var api = new WebhookEndpointApi(new SystemTextJsonSerializer(), new DefaultHttpClientProvider(client), null);

            var result = await api.GetWebhookEndpointAsync("token").ConfigureAwait(false);

            Assert.AreEqual("https://example.com", result.endpoint);
            Assert.AreEqual(HttpMethod.Get, handler.LastRequest?.Method);
            Assert.AreEqual(LineApiEndpoints.BuildWebhookEndpoint(), handler.LastRequest?.RequestUri?.ToString());
        }

        private sealed class RecordingHandler : HttpMessageHandler
        {
            private readonly Func<HttpResponseMessage> responder;

            public RecordingHandler(Func<HttpResponseMessage> responder)
            {
                this.responder = responder ?? throw new ArgumentNullException(nameof(responder));
            }

            public HttpRequestMessage LastRequest { get; private set; }
            public string LastContent { get; private set; }

            protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Capture(request);
                return responder();
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Capture(request);
                return Task.FromResult(responder());
            }

            private void Capture(HttpRequestMessage request)
            {
                LastRequest = request;
                if (request.Content != null)
                {
                    LastContent = request.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }
        }
    }
}

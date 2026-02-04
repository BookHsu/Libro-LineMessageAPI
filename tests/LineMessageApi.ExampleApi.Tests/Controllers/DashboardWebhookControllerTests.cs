using System.Text;
using System.Text.Json;
using LineMessageApi.ExampleApi;
using LineMessageApi.ExampleApi.Controllers;
using LineMessageApi.ExampleApi.Hubs;
using LineMessageApi.ExampleApi.Models;
using LineMessageApi.ExampleApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json.Serialization;

namespace LineMessageApi.ExampleApi.Tests.Controllers;

[TestClass]
public class DashboardWebhookControllerTests
{
    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Config_Missing()
    {
        var store = new LineConfigStore();
        var hub = new TestHubContext();
        var controller = CreateController(store, hub, new JsonSerializerOptions());

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_Unauthorized_When_Signature_Missing()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions());

        var payload = BuildPayload("00000000000000000000000000000000");
        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Store_Event_And_Push_When_Signature_Valid()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions());

        var payload = BuildPayload("00000000000000000000000000000000");
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        Assert.AreEqual(1, store.GetEvents().Count);
        Assert.AreEqual(1, hub.ClientProxy.SendCount);
        Assert.AreEqual("webhookReceived", hub.ClientProxy.LastMethod);
    }

    private static DashboardWebhookController CreateController(
        LineConfigStore store,
        TestHubContext hub,
        JsonSerializerOptions jsonOptions)
    {
        var mvcOptionsValue = new Microsoft.AspNetCore.Mvc.JsonOptions();
        mvcOptionsValue.JsonSerializerOptions.PropertyNameCaseInsensitive = jsonOptions.PropertyNameCaseInsensitive;
        foreach (var converter in jsonOptions.Converters)
        {
            mvcOptionsValue.JsonSerializerOptions.Converters.Add(converter);
        }

        var mvcOptions = Options.Create(mvcOptionsValue);

        return new DashboardWebhookController(
            store,
            hub,
            mvcOptions,
            NullLogger<DashboardWebhookController>.Instance);
    }

    private static JsonSerializerOptions CreateJsonOptions()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        options.Converters.Add(new JsonStringEnumConverter());
        return options;
    }

    private static string BuildPayload(string replyToken)
    {
        return $@"{{
  ""events"": [
    {{
      ""replyToken"": ""{replyToken}"",
      ""type"": ""message"",
      ""timestamp"": 0,
      ""source"": {{ ""type"": ""user"", ""userId"": ""U1"" }},
      ""message"": {{ ""id"": ""1"", ""type"": ""text"", ""text"": ""hi"" }}
    }}
  ]
}}";
    }

    private static string BuildSignature(string payload, string secret)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        return Convert.ToBase64String(hash);
    }

    private sealed class TestHubContext : IHubContext<LineWebhookHub>
    {
        public TestHubContext()
        {
            ClientProxy = new TestClientProxy();
            Clients = new TestHubClients(ClientProxy);
            Groups = new TestGroupManager();
        }

        public IHubClients Clients { get; }
        public IGroupManager Groups { get; }
        public TestClientProxy ClientProxy { get; }
    }

    private sealed class TestHubClients : IHubClients
    {
        private readonly IClientProxy proxy;

        public TestHubClients(IClientProxy proxy)
        {
            this.proxy = proxy;
        }

        public IClientProxy All => proxy;
        public IClientProxy AllExcept(IReadOnlyList<string> excludedConnectionIds) => proxy;
        public IClientProxy Client(string connectionId) => proxy;
        public IClientProxy Clients(IReadOnlyList<string> connectionIds) => proxy;
        public IClientProxy Group(string groupName) => proxy;
        public IClientProxy GroupExcept(string groupName, IReadOnlyList<string> excludedConnectionIds) => proxy;
        public IClientProxy Groups(IReadOnlyList<string> groupNames) => proxy;
        public IClientProxy User(string userId) => proxy;
        public IClientProxy Users(IReadOnlyList<string> userIds) => proxy;
    }

    private sealed class TestGroupManager : IGroupManager
    {
        public Task AddToGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task RemoveFromGroupAsync(string connectionId, string groupName, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }

    private sealed class TestClientProxy : IClientProxy
    {
        public int SendCount { get; private set; }
        public string? LastMethod { get; private set; }

        public Task SendCoreAsync(string method, object?[] args, CancellationToken cancellationToken = default)
        {
            SendCount++;
            LastMethod = method;
            return Task.CompletedTask;
        }
    }
}

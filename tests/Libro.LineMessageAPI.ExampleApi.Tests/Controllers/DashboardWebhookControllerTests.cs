using System.Text;
using System.Text.Json;
using Libro.LineMessageAPI.ExampleApi.Controllers;
using Libro.LineMessageAPI.ExampleApi.Hubs;
using Libro.LineMessageAPI.ExampleApi.Models;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageAPI.ExampleApi.Tests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json.Serialization;

namespace Libro.LineMessageAPI.ExampleApi.Tests.Controllers;

[TestClass]
public class DashboardWebhookControllerTests
{
    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Config_Missing()
    {
        var store = new LineConfigStore();
        var hub = new TestHubContext();
        var controller = CreateController(store, hub, new JsonSerializerOptions(), new StubLineSdkFactory());

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
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

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
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

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

        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var responseJson = JsonSerializer.Serialize(okResult.Value);
        StringAssert.Contains(responseJson, "replyToken is missing or invalid");
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Secret_Empty()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Body_Empty()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Array.Empty<byte>());
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_Unauthorized_When_Signature_Invalid()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var payload = BuildPayload("validtoken12345");
        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = "invalid";
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_Ok_When_Events_Empty()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var payload = "{\"events\":[]}";
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        Assert.AreEqual(0, store.GetEvents().Count);
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Record_Error_When_Event_Null()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var payload = "{\"events\":[null]}";
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook() as OkObjectResult;

        Assert.IsNotNull(result);
        var responseJson = JsonSerializer.Serialize(result.Value);
        StringAssert.Contains(responseJson, "event is null");
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Record_Error_When_Token_Empty()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), new StubLineSdkFactory());

        var payload = BuildPayload("validtoken12345");
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook() as OkObjectResult;

        Assert.IsNotNull(result);
        var responseJson = JsonSerializer.Serialize(result.Value);
        StringAssert.Contains(responseJson, "Channel Access Token is empty");
        Assert.AreEqual(1, store.GetEvents().Count);
        Assert.AreEqual(1, hub.ClientProxy.SendCount);
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Record_Error_When_SendReply_Fails()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var messageService = new StubMessageService
        {
            ThrowOnSendReplyAsync = true
        };
        var factory = new StubLineSdkFactory
        {
            MessageSdk = new StubLineSdkFacade
            {
                Messages = messageService
            }
        };

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), factory);

        var payload = BuildPayload("validtoken12345");
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook() as OkObjectResult;

        Assert.IsNotNull(result);
        var responseJson = JsonSerializer.Serialize(result.Value);
        StringAssert.Contains(responseJson, "boom");
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Send_Reply_When_Valid()
    {
        var store = new LineConfigStore();
        store.Update(new LineConfig
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret",
            WebhookUrl = "https://example.com/dashboard/hook"
        });

        var messageService = new StubMessageService();
        var factory = new StubLineSdkFactory
        {
            MessageSdk = new StubLineSdkFacade
            {
                Messages = messageService
            }
        };

        var hub = new TestHubContext();
        var controller = CreateController(store, hub, CreateJsonOptions(), factory);

        var payload = BuildPayload("validtoken12345");
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        Assert.AreEqual(1, messageService.SendReplyAsyncCallCount);
    }

    private static DashboardWebhookController CreateController(
        LineConfigStore store,
        TestHubContext hub,
        JsonSerializerOptions jsonOptions,
        ILineSdkFactory sdkFactory)
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
            sdkFactory,
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




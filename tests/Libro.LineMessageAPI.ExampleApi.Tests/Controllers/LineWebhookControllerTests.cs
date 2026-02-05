using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Libro.LineMessageAPI.ExampleApi.Controllers;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageAPI.ExampleApi.Tests;
using Libro.LineMessageApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libro.LineMessageAPI.ExampleApi.Tests.Controllers;

[TestClass]
public class LineWebhookControllerTests
{
    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Secret_Empty()
    {
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = ""
        });

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{}"));
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_BadRequest_When_Body_Empty()
    {
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        });

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Array.Empty<byte>());
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_Unauthorized_When_Signature_Missing()
    {
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        });

        var payload = BuildPayload("validtoken12345");
        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Return_Unauthorized_When_Signature_Invalid()
    {
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        });

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
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        });

        var payload = "{\"events\":[]}";
        var signature = BuildSignature(payload, "secret");

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(payload));
        context.Request.Headers["X-Line-Signature"] = signature;
        controller.ControllerContext = new ControllerContext { HttpContext = context };

        var result = await controller.HandleWebhook();

        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Record_Error_When_Event_Null()
    {
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        });

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
        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "",
            ChannelSecret = "secret"
        });

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
    }

    [TestMethod]
    public async Task HandleWebhook_Should_Record_Error_When_SendReply_Fails()
    {
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

        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        }, factory);

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
        var messageService = new StubMessageService();
        var factory = new StubLineSdkFactory
        {
            MessageSdk = new StubLineSdkFacade
            {
                Messages = messageService
            }
        };

        var controller = CreateController(new LineChannelOptions
        {
            ChannelAccessToken = "token",
            ChannelSecret = "secret"
        }, factory);

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

    private static LineWebhookController CreateController(LineChannelOptions options, ILineSdkFactory factory = null)
    {
        var mvcOptionsValue = new Microsoft.AspNetCore.Mvc.JsonOptions();
        mvcOptionsValue.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        mvcOptionsValue.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

        var mvcOptions = Options.Create(mvcOptionsValue);

        return new LineWebhookController(
            Options.Create(options),
            factory ?? new StubLineSdkFactory(),
            mvcOptions,
            NullLogger<LineWebhookController>.Instance);
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
}

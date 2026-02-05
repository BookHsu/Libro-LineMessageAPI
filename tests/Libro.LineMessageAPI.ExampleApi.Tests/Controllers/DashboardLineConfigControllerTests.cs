using Libro.LineMessageAPI.ExampleApi.Controllers;
using Libro.LineMessageAPI.ExampleApi.Models;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageAPI.ExampleApi.Tests;
using Libro.LineMessageApi.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Libro.LineMessageAPI.ExampleApi.Tests.Controllers
{
    [TestClass]
    public class DashboardLineConfigControllerTests
    {
        [TestMethod]
        public void GetConfig_Should_Return_Not_Configured_When_Empty()
        {
            // 準備空設定的控制器
            var store = new LineConfigStore();
            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            // 取得設定
            var result = controller.GetConfig() as OkObjectResult;

            // 驗證回傳內容
            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigState;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Configured);
        }

        [TestMethod]
        public void UpdateConfig_Should_Return_BadRequest_When_Missing_Token()
        {
            // 準備控制器
            var store = new LineConfigStore();
            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            // 缺少 token
            var request = new LineConfigRequest
            {
                ChannelAccessToken = "",
                ChannelSecret = "secret"
            };

            var result = controller.UpdateConfig(request) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
        }

        [TestMethod]
        public void GetConfig_Should_Return_Configured_When_Set()
        {
            var store = new LineConfigStore();
            var config = new LineConfig
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/hook",
                UpdatedAtUtc = new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero)
            };
            store.Update(config);

            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            var result = controller.GetConfig() as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigState;
            Assert.IsNotNull(payload);
            Assert.IsTrue(payload.Configured);
            Assert.AreEqual(config.WebhookUrl, payload.WebhookUrl);
            Assert.AreEqual(config.UpdatedAtUtc.ToString("O"), payload.UpdatedAtUtc);
        }

        [TestMethod]
        public void UpdateConfig_Should_Return_BadRequest_When_Request_Null()
        {
            var store = new LineConfigStore();
            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            var result = controller.UpdateConfig(null) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
        }

        [TestMethod]
        public void UpdateConfig_Should_Return_BadRequest_When_Missing_Secret()
        {
            var store = new LineConfigStore();
            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            var request = new LineConfigRequest
            {
                ChannelAccessToken = "token",
                ChannelSecret = ""
            };

            var result = controller.UpdateConfig(request) as BadRequestObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
        }

        [TestMethod]
        public void UpdateConfig_Should_Default_WebhookUrl_When_Empty()
        {
            var store = new LineConfigStore();
            var botService = new StubBotService
            {
                BotInfo = new BotInfo { userId = "bot" }
            };
            var webhookService = new StubWebhookEndpointService
            {
                WebhookEndpoint = new WebhookEndpointResponse { endpoint = "https://example.com/hook" }
            };
            var factory = new StubLineSdkFactory
            {
                BotWebhookSdk = new StubLineSdkFacade
                {
                    Bot = botService,
                    WebhookEndpoints = webhookService
                }
            };

            var controller = new DashboardLineConfigController(store, factory);
            var context = new DefaultHttpContext();
            context.Request.Scheme = "https";
            context.Request.Host = new HostString("example.com");
            controller.ControllerContext = new ControllerContext { HttpContext = context };

            var result = controller.UpdateConfig(new LineConfigRequest
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "",
                SetEndpoint = false
            }) as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsTrue(payload.Success);
            Assert.AreEqual("https://example.com/dashboard/hook", payload.Config.WebhookUrl);
            Assert.AreEqual("bot", payload.BotInfo?.userId);
        }

        [TestMethod]
        public void UpdateConfig_Should_Warn_When_SetEndpoint_Fails()
        {
            var store = new LineConfigStore();
            var webhookService = new StubWebhookEndpointService
            {
                SetEndpointResult = false
            };
            var factory = new StubLineSdkFactory
            {
                BotWebhookSdk = new StubLineSdkFacade
                {
                    Bot = new StubBotService(),
                    WebhookEndpoints = webhookService
                }
            };

            var controller = new DashboardLineConfigController(store, factory);

            var result = controller.UpdateConfig(new LineConfigRequest
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/hook",
                SetEndpoint = true
            }) as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsTrue(payload.Success);
            Assert.AreEqual("設定完成，但 Webhook Endpoint 更新失敗。", payload.Message);
        }

        [TestMethod]
        public void UpdateConfig_Should_Return_Error_When_Sdk_Throws()
        {
            var store = new LineConfigStore();
            var factory = new StubLineSdkFactory
            {
                BotWebhookException = new InvalidOperationException("boom")
            };

            var controller = new DashboardLineConfigController(store, factory);

            var result = controller.UpdateConfig(new LineConfigRequest
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/hook"
            }) as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
            StringAssert.Contains(payload.Message, "設定失敗");
        }

        [TestMethod]
        public void GetInfo_Should_Return_BadRequest_When_No_Config()
        {
            var store = new LineConfigStore();
            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            var result = controller.GetInfo() as BadRequestObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
        }

        [TestMethod]
        public void GetInfo_Should_Return_Bot_Info_When_Configured()
        {
            var store = new LineConfigStore();
            store.Update(new LineConfig
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/hook",
                UpdatedAtUtc = DateTimeOffset.UtcNow
            });

            var botService = new StubBotService
            {
                BotInfo = new BotInfo { userId = "bot" }
            };
            var webhookService = new StubWebhookEndpointService
            {
                WebhookEndpoint = new WebhookEndpointResponse { endpoint = "https://example.com/hook" }
            };
            var factory = new StubLineSdkFactory
            {
                BotWebhookSdk = new StubLineSdkFacade
                {
                    Bot = botService,
                    WebhookEndpoints = webhookService
                }
            };

            var controller = new DashboardLineConfigController(store, factory);

            var result = controller.GetInfo() as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsTrue(payload.Success);
            Assert.AreEqual("bot", payload.BotInfo?.userId);
            Assert.AreEqual("https://example.com/hook", payload.WebhookEndpoint?.endpoint);
        }

        [TestMethod]
        public void GetInfo_Should_Return_Error_When_Sdk_Throws()
        {
            var store = new LineConfigStore();
            store.Update(new LineConfig
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/hook",
                UpdatedAtUtc = DateTimeOffset.UtcNow
            });

            var factory = new StubLineSdkFactory
            {
                BotWebhookException = new InvalidOperationException("boom")
            };

            var controller = new DashboardLineConfigController(store, factory);

            var result = controller.GetInfo() as OkObjectResult;

            Assert.IsNotNull(result);
            var payload = result.Value as LineConfigResponse;
            Assert.IsNotNull(payload);
            Assert.IsFalse(payload.Success);
        }

        [TestMethod]
        public void GetEvents_Should_Return_Event_List()
        {
            var store = new LineConfigStore();
            store.AddEvent(new WebhookEventRecord
            {
                EventType = "message",
                Summary = "hello"
            });

            var controller = new DashboardLineConfigController(store, new StubLineSdkFactory());

            var result = controller.GetEvents() as OkObjectResult;

            Assert.IsNotNull(result);
            var events = result.Value as System.Collections.Generic.List<WebhookEventRecord>;
            Assert.IsNotNull(events);
            Assert.AreEqual(1, events.Count);
        }
    }
}




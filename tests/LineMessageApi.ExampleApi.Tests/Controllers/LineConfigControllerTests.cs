using LineMessageApi.ExampleApi.Controllers;
using LineMessageApi.ExampleApi.Models;
using LineMessageApi.ExampleApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApi.ExampleApi.Tests.Controllers
{
    [TestClass]
    public class LineConfigControllerTests
    {
        [TestMethod]
        public void GetConfig_Should_Return_Not_Configured_When_Empty()
        {
            // 準備空設定的控制器
            var store = new LineConfigStore();
            var controller = new LineConfigController(store);

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
            var controller = new LineConfigController(store);

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
    }
}

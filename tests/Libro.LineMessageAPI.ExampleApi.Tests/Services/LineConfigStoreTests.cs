using Libro.LineMessageAPI.ExampleApi.Models;
using Libro.LineMessageAPI.ExampleApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Libro.LineMessageAPI.ExampleApi.Tests.Services
{
    [TestClass]
    public class LineConfigStoreTests
    {
        [TestMethod]
        public void Update_Should_Return_Config()
        {
            // 準備測試資料
            var store = new LineConfigStore();
            var config = new LineConfig
            {
                ChannelAccessToken = "token",
                ChannelSecret = "secret",
                WebhookUrl = "https://example.com/dashboard/hook"
            };

            // 更新設定
            store.Update(config);

            // 驗證設定可被讀取
            var result = store.Get();
            Assert.IsNotNull(result);
            Assert.AreEqual(config.ChannelAccessToken, result.ChannelAccessToken);
            Assert.AreEqual(config.ChannelSecret, result.ChannelSecret);
            Assert.AreEqual(config.WebhookUrl, result.WebhookUrl);
        }

        [TestMethod]
        public void AddEvent_Should_Cap_At_200()
        {
            // 建立 205 筆事件
            var store = new LineConfigStore();
            for (var i = 0; i < 205; i++)
            {
                store.AddEvent(new WebhookEventRecord
                {
                    EventType = "message",
                    Summary = $"event-{i}"
                });
            }

            // 驗證最多只保留 200 筆
            var events = store.GetEvents();
            Assert.AreEqual(200, events.Count);
            Assert.IsTrue(events.Any(e => e.Summary == "event-204"));
        }
    }
}




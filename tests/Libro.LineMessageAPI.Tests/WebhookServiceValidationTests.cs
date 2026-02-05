using System;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libro.LineMessageApi.Tests
{
    [TestClass]
    public class WebhookServiceValidationTests
    {
        [TestMethod]
        public void ValidateSignature_Should_Throw_When_Request_Is_Null()
        {
            // 建立 SDK（預設包含 Webhook 模組）
            var sdk = new LineSdkBuilder("token-value").Build();

            Assert.ThrowsException<ArgumentNullException>(() =>
                sdk.Webhook.ValidateSignature(null, "secret"));
        }

        [TestMethod]
        public void ValidateSignature_Should_Throw_When_Secret_Is_Empty()
        {
            // 建立測試用請求
            var request = new HttpRequestMessage();
            request.Content = new StringContent("body");

            var sdk = new LineSdkBuilder("token-value").Build();

            Assert.ThrowsException<ArgumentException>(() =>
                sdk.Webhook.ValidateSignature(request, ""));
        }

        [TestMethod]
        public void ValidateSignature_Should_Throw_When_Header_Missing()
        {
            // 建立測試用請求（未提供 X-Line-Signature）
            var request = new HttpRequestMessage();
            request.Content = new StringContent("body");

            var sdk = new LineSdkBuilder("token-value").Build();

            Assert.ThrowsException<InvalidOperationException>(() =>
                sdk.Webhook.ValidateSignature(request, "secret"));
        }
    }
}


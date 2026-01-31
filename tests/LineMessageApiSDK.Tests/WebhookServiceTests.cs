using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LineMessageApiSDK.Tests
{
    [TestClass]
    public class WebhookServiceTests
    {
        [TestMethod]
        public void ValidateSignature_Should_Match_LineChannel_Result()
        {
            // 準備測試資料
            var secret = "test-secret";
            var body = "hello-line";
            var signature = BuildSignature(secret, body);

            var request = new HttpRequestMessage();
            request.Content = new StringContent(body);
            request.Headers.Add("X-Line-Signature", signature);

            var sdk = new LineSdkBuilder("token-value").Build();
            var serviceResult = sdk.Webhook.ValidateSignature(request, secret);
            var channelResult = LineChannel.VaridateSignature(request, secret);

            Assert.IsTrue(serviceResult);
            Assert.AreEqual(serviceResult, channelResult);
        }

        private static string BuildSignature(string secret, string body)
        {
            // 依照 LINE 規格計算 HMAC SHA256
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
            return Convert.ToBase64String(computeHash);
        }
    }
}

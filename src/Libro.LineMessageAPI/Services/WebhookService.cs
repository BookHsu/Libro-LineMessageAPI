using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Webhook 驗證服務
    /// </summary>
    internal class WebhookService : IWebhookService
    {
        /// <summary>
        /// 驗證是否為 LINE 伺服器簽章
        /// </summary>
        /// <param name="request">HTTP 請求</param>
        /// <param name="channelSecret">Channel Secret</param>
        /// <returns>是否通過驗證</returns>
        public bool ValidateSignature(HttpRequestMessage request, string channelSecret)
        {
            if (request == null)
            {
                // 避免空參數造成例外
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(channelSecret))
            {
                // Channel Secret 不可為空
                throw new ArgumentException("Channel Secret 不可為空", nameof(channelSecret));
            }

            // 讀取請求內容並計算簽章
            var body = request.Content?.ReadAsStringAsync().Result ?? string.Empty;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(channelSecret));
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(body));
            var contentHash = Convert.ToBase64String(computeHash);
            var headerHash = request.Headers.GetValues("X-Line-Signature").First();
            return contentHash == headerHash;
        }
    }
}


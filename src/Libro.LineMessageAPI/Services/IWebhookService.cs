using System.Net.Http;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Webhook 驗證服務介面
    /// </summary>
    public interface IWebhookService
    {
        /// <summary>
        /// 驗證是否為 LINE 伺服器簽章
        /// </summary>
        /// <param name="request">HTTP 請求</param>
        /// <param name="channelSecret">Channel Secret</param>
        /// <returns>是否通過驗證</returns>
        bool ValidateSignature(HttpRequestMessage request, string channelSecret);
    }
}


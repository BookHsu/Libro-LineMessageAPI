using System.Net.Http;

namespace LineMessageApiSDK.Services
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
            // 使用既有的驗證邏輯以維持一致性
            return LineChannel.VaridateSignature(request, channelSecret);
        }
    }
}

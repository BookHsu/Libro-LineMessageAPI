using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Webhook Endpoint 管理服務
    /// </summary>
    public interface IWebhookEndpointService
    {
        /// <summary>
        /// 取得 Webhook Endpoint
        /// </summary>
        /// <returns>Webhook Endpoint 設定</returns>
        WebhookEndpointResponse GetWebhookEndpoint();

        /// <summary>
        /// 取得 Webhook Endpoint（非同步）
        /// </summary>
        /// <returns>Webhook Endpoint 設定</returns>
        Task<WebhookEndpointResponse> GetWebhookEndpointAsync();

        /// <summary>
        /// 更新 Webhook Endpoint
        /// </summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        bool SetWebhookEndpoint(WebhookEndpointRequest request);

        /// <summary>
        /// 更新 Webhook Endpoint（非同步）
        /// </summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        Task<bool> SetWebhookEndpointAsync(WebhookEndpointRequest request);

        /// <summary>
        /// 測試 Webhook Endpoint
        /// </summary>
        /// <returns>測試結果</returns>
        WebhookTestResponse TestWebhookEndpoint();

        /// <summary>
        /// 測試 Webhook Endpoint（非同步）
        /// </summary>
        /// <returns>測試結果</returns>
        Task<WebhookTestResponse> TestWebhookEndpointAsync();
    }
}

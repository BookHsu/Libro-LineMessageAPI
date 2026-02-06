using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Webhook 端點 管理服務
    /// </summary>
    public interface IWebhookEndpointService
    {
        /// <summary>
        /// 取得 Webhook 端點
        /// </summary>
        /// <returns>Webhook 端點 設定</returns>
        WebhookEndpointResponse GetWebhookEndpoint();

        /// <summary>
        /// 取得 Webhook 端點
        /// </summary>
        /// <returns>Webhook 端點 設定</returns>
        Task<WebhookEndpointResponse> GetWebhookEndpointAsync();

        /// <summary>
        /// 更新 Webhook 端點
        /// </summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        bool SetWebhookEndpoint(WebhookEndpointRequest request);

        /// <summary>
        /// 更新 Webhook 端點
        /// </summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        Task<bool> SetWebhookEndpointAsync(WebhookEndpointRequest request);

        /// <summary>
        /// 測試 Webhook 端點
        /// </summary>
        /// <returns>測試結果</returns>
        WebhookTestResponse TestWebhookEndpoint();

        /// <summary>
        /// 測試 Webhook 端點
        /// </summary>
        /// <returns>測試結果</returns>
        Task<WebhookTestResponse> TestWebhookEndpointAsync();
    }
}


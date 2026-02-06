using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Webhook Endpoint 管理服務
    /// </summary>
    internal class WebhookEndpointService : IWebhookEndpointService
    {
        private readonly LineApiContext context;
        private readonly WebhookEndpointApi api;

        /// <summary>
        /// 建立 Webhook Endpoint 管理服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal WebhookEndpointService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClientProvider
            this.context = context;
            // 建立 Webhook Endpoint API
            api = new WebhookEndpointApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <inheritdoc />
        public WebhookEndpointResponse GetWebhookEndpoint()
        {
            return api.GetWebhookEndpoint(context.ChannelAccessToken);
        }

        /// <inheritdoc />
        public Task<WebhookEndpointResponse> GetWebhookEndpointAsync()
        {
            return api.GetWebhookEndpointAsync(context.ChannelAccessToken);
        }

        /// <inheritdoc />
        public bool SetWebhookEndpoint(WebhookEndpointRequest request)
        {
            return api.SetWebhookEndpoint(context.ChannelAccessToken, request);
        }

        /// <inheritdoc />
        public Task<bool> SetWebhookEndpointAsync(WebhookEndpointRequest request)
        {
            return api.SetWebhookEndpointAsync(context.ChannelAccessToken, request);
        }

        /// <inheritdoc />
        public WebhookTestResponse TestWebhookEndpoint()
        {
            return api.TestWebhookEndpoint(context.ChannelAccessToken);
        }

        /// <inheritdoc />
        public Task<WebhookTestResponse> TestWebhookEndpointAsync()
        {
            return api.TestWebhookEndpointAsync(context.ChannelAccessToken);
        }
    }
}


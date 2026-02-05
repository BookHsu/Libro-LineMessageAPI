using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Webhook Endpoint 管理 API
    /// </summary>
    internal class WebhookEndpointApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 建立 Webhook Endpoint API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal WebhookEndpointApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 建立 Webhook Endpoint API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal WebhookEndpointApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        /// <summary>
        /// 取得 Webhook Endpoint
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>Webhook Endpoint 設定</returns>
        internal WebhookEndpointResponse GetWebhookEndpoint(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookEndpoint();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<WebhookEndpointResponse>(result);
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 取得 Webhook Endpoint（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>Webhook Endpoint 設定</returns>
        internal async Task<WebhookEndpointResponse> GetWebhookEndpointAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookEndpoint();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<WebhookEndpointResponse>(result);
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 更新 Webhook Endpoint
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        internal bool SetWebhookEndpoint(string channelAccessToken, WebhookEndpointRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookEndpoint();
                var payload = serializer.Serialize(request);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Put(url, content);
                return result.IsSuccessStatusCode;
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 更新 Webhook Endpoint（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        internal async Task<bool> SetWebhookEndpointAsync(string channelAccessToken, WebhookEndpointRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookEndpoint();
                var payload = serializer.Serialize(request);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var result = await client.PutAsync(url, content).ConfigureAwait(false);
                return result.IsSuccessStatusCode;
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 測試 Webhook Endpoint
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>測試結果</returns>
        internal WebhookTestResponse TestWebhookEndpoint(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookTest();
                using var content = new StringContent("{}");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
                var body = result.Content.ReadAsStringSync();
                return serializer.Deserialize<WebhookTestResponse>(body);
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 測試 Webhook Endpoint（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>測試結果</returns>
        internal async Task<WebhookTestResponse> TestWebhookEndpointAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildWebhookTest();
                using var content = new StringContent("{}");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var body = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return serializer.Deserialize<WebhookTestResponse>(body);
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }
    }
}


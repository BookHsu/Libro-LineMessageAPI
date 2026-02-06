using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 提供 Webhook 端點的查詢、更新與測試功能。
    /// </summary>
    internal class WebhookEndpointApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 初始化 <see cref="WebhookEndpointApi"/> 執行個體。
        /// </summary>
        /// <param name="serializer">JSON 序列化器；若為 <c>null</c> 則使用預設實作。</param>
        /// <param name="httpClient">外部提供的 <see cref="HttpClient"/>；若為 <c>null</c> 則使用預設流程建立。</param>
        internal WebhookEndpointApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 初始化 <see cref="WebhookEndpointApi"/> 執行個體。
        /// </summary>
        /// <param name="serializer">JSON 序列化器；若為 <c>null</c> 則使用預設實作。</param>
        /// <param name="httpClientProvider"><see cref="HttpClient"/> 提供者；若為 <c>null</c> 則使用預設實作。</param>
        /// <param name="syncAdapterFactory">同步 HTTP 轉接器工廠；若為 <c>null</c> 則使用預設實作。</param>
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
        /// 取得目前的 Webhook 端點設定。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <returns>Webhook 端點設定內容。</returns>
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
        /// 取得目前的 Webhook 端點設定。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <returns>Webhook 端點設定內容。</returns>
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
        /// 更新 Webhook 端點設定。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <param name="request">要套用的 Webhook 設定。</param>
        /// <returns>更新是否成功。</returns>
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
        /// 更新 Webhook 端點設定。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <param name="request">要套用的 Webhook 設定。</param>
        /// <returns>更新是否成功。</returns>
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
        /// 觸發 Webhook 端點連線測試。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <returns>Webhook 測試結果。</returns>
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
        /// 觸發 Webhook 端點連線測試。
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token。</param>
        /// <returns>Webhook 測試結果。</returns>
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


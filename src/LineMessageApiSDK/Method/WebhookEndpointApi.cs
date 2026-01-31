using LineMessageApiSDK.Http;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// Webhook Endpoint 管理 API
    /// </summary>
    internal class WebhookEndpointApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;

        /// <summary>
        /// 建立 Webhook Endpoint API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal WebhookEndpointApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立 Webhook Endpoint API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal WebhookEndpointApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
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
                var result = client.GetStringAsync(url).Result;
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
                var result = await client.GetStringAsync(url);
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
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PutAsync(url, content).Result;
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
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(url, content);
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
                var result = client.PostAsync(url, new StringContent("{}")).Result;
                var body = result.Content.ReadAsStringAsync().Result;
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
                var result = await client.PostAsync(url, new StringContent("{}"));
                var body = await result.Content.ReadAsStringAsync();
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

using LineMessageApiSDK.Http;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.SendMessage;
using LineMessageApiSDK.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// Broadcast / Narrowcast API
    /// </summary>
    internal class BroadcastApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;

        /// <summary>
        /// 建立 Broadcast API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal BroadcastApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立 Broadcast API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal BroadcastApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
        }

        /// <summary>
        /// 發送 Broadcast
        /// </summary>
        internal bool SendBroadcast(string channelAccessToken, BroadcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildBroadcastMessage();
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 發送 Broadcast（非同步）
        /// </summary>
        internal async Task<bool> SendBroadcastAsync(string channelAccessToken, BroadcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildBroadcastMessage();
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 發送 Narrowcast
        /// </summary>
        internal bool SendNarrowcast(string channelAccessToken, NarrowcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastMessage();
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 發送 Narrowcast（非同步）
        /// </summary>
        internal async Task<bool> SendNarrowcastAsync(string channelAccessToken, NarrowcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastMessage();
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 取得 Narrowcast 進度
        /// </summary>
        internal NarrowcastProgressResponse GetNarrowcastProgress(string channelAccessToken, string requestId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastProgress(requestId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<NarrowcastProgressResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 取得 Narrowcast 進度（非同步）
        /// </summary>
        internal async Task<NarrowcastProgressResponse> GetNarrowcastProgressAsync(string channelAccessToken, string requestId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastProgress(requestId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<NarrowcastProgressResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }
    }
}

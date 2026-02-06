using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.SendMessage;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Broadcast / Narrowcast API
    /// </summary>
    internal class BroadcastApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 初始化 <see cref="BroadcastApi"/> 執行個體。
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal BroadcastApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 初始化 <see cref="BroadcastApi"/> 執行個體。
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        /// <param name="syncAdapterFactory">同步 HttpClient 轉接器工廠</param>
        internal BroadcastApi(
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
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
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
        /// 發送 Broadcast
        /// </summary>
        internal async Task<bool> SendBroadcastAsync(string channelAccessToken, BroadcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildBroadcastMessage();
                var payload = serializer.Serialize(message);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
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
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
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
        internal async Task<bool> SendNarrowcastAsync(string channelAccessToken, NarrowcastMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastMessage();
                var payload = serializer.Serialize(message);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
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
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
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
        /// 取得 Narrowcast 進度
        /// </summary>
        internal async Task<NarrowcastProgressResponse> GetNarrowcastProgressAsync(string channelAccessToken, string requestId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildNarrowcastProgress(requestId);
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
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

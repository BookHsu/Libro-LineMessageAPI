using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using System.Net.Http;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// SDK 內部共用的 API 設定內容
    /// </summary>
    internal class LineApiContext
    {
        /// <summary>
        /// Channel Access Token
        /// </summary>
        internal string ChannelAccessToken { get; }

        /// <summary>
        /// JSON 序列化器
        /// </summary>
        internal IJsonSerializer Serializer { get; }

        /// <summary>
        /// HttpClient（保留以維持相容）
        /// </summary>
        internal HttpClient HttpClient { get; }

        /// <summary>
        /// HttpClient 提供者
        /// </summary>
        internal IHttpClientProvider HttpClientProvider { get; }

        /// <summary>
        /// Sync HttpClient adapter factory
        /// </summary>
        internal IHttpClientSyncAdapterFactory SyncAdapterFactory { get; }

        /// <summary>
        /// 建立 API Context
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="serializer">序列化器</param>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal LineApiContext(
            string channelAccessToken,
            IJsonSerializer serializer,
            HttpClient httpClient,
            IHttpClientProvider httpClientProvider = null,
            IHttpClientSyncAdapterFactory syncAdapterFactory = null)
        {
            // 設定必要的 Token
            ChannelAccessToken = channelAccessToken;
            // 若未提供序列化器，使用預設 System.Text.Json
            Serializer = serializer ?? new SystemTextJsonSerializer();
            // 保留 HttpClient 以兼容既有建構方式
            HttpClient = httpClient;
            // 若提供 HttpClientProvider，優先使用
            HttpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(httpClient);
            SyncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }
    }
}


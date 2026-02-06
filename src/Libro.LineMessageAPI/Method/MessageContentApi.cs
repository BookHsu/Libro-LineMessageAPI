using Libro.LineMessageApi.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 訊息內容 API（下載使用者上傳的檔案）
    /// </summary>
    internal class MessageContentApi
    {
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 建立訊息內容 API
        /// </summary>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal MessageContentApi(HttpClient httpClient = null)
            : this(new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 建立訊息內容 API
        /// </summary>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        /// <param name="syncAdapterFactory">同步 HttpClient 轉接器工廠</param>
        internal MessageContentApi(
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        /// <summary>
        /// 取得使用者傳送的圖片、影片、聲音、檔案
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="messageId">訊息 ID</param>
        /// <returns>檔案內容</returns>
        internal byte[] GetUserUploadData(string channelAccessToken, string messageId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(messageId);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetByteArray(strUrl);
                return result;
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
        /// 取得使用者傳送的圖片、影片、聲音、檔案
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="messageId">訊息 ID</param>
        /// <returns>檔案內容</returns>
        internal async Task<byte[]> GetUserUploadDataAsync(string channelAccessToken, string messageId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(messageId);
                var result = await client.GetByteArrayAsync(strUrl).ConfigureAwait(false);
                return result;
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


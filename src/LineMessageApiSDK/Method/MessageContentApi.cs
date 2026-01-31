using System.Net.Http;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// 訊息內容 API（下載使用者上傳的檔案）
    /// </summary>
    internal class MessageContentApi
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// 建立訊息內容 API
        /// </summary>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal MessageContentApi(HttpClient httpClient = null)
        {
            // 保存 HttpClient（可透過 DI 注入）
            this.httpClient = httpClient;
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
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(messageId);
                var result = client.GetByteArrayAsync(strUrl).Result;
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
        /// 取得使用者傳送的圖片、影片、聲音、檔案（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="messageId">訊息 ID</param>
        /// <returns>檔案內容</returns>
        internal async Task<byte[]> GetUserUploadDataAsync(string channelAccessToken, string messageId)
        {
            bool shouldDispose;
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(messageId);
                var result = await client.GetByteArrayAsync(strUrl);
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

        private HttpClient GetClientDefault(string channelAccessToken, out bool shouldDispose)
        {
            if (httpClient != null)
            {
                // 使用外部注入的 HttpClient
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", channelAccessToken);
                shouldDispose = false;
                return httpClient;
            }

            // 未注入時，維持舊行為：每次建立新的 HttpClient
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", channelAccessToken);
            shouldDispose = true;
            return client;
        }
    }
}

using System.Net.Http;

namespace LineMessageApiSDK.Http
{
    /// <summary>
    /// 預設 HttpClient 提供者
    /// </summary>
    internal class DefaultHttpClientProvider : IHttpClientProvider
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// 建立預設提供者
        /// </summary>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal DefaultHttpClientProvider(HttpClient httpClient)
        {
            // 保存 HttpClient（可為 null）
            this.httpClient = httpClient;
        }

        /// <inheritdoc />
        public HttpClient GetClient(string channelAccessToken, out bool shouldDispose)
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

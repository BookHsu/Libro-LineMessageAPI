using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// 預設 HttpClient 提供者
    /// </summary>
    internal class DefaultHttpClientProvider : IHttpClientProvider
    {
        private const int MaxSharedClients = 64;
        private readonly HttpClient httpClient;
        private static readonly ConcurrentDictionary<string, HttpClient> SharedClients =
            new ConcurrentDictionary<string, HttpClient>(StringComparer.Ordinal);

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
                var current = httpClient.DefaultRequestHeaders.Authorization;
                if (string.IsNullOrEmpty(channelAccessToken))
                {
                    if (current != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = null;
                    }
                }
                else if (current == null
                    || !string.Equals(current.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(current.Parameter, channelAccessToken, StringComparison.Ordinal))
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", channelAccessToken);
                }
                shouldDispose = false;
                return httpClient;
            }

            // 未注入時：依 token 重用 HttpClient，避免頻繁建立造成效能與連線耗損
            var tokenKey = channelAccessToken ?? string.Empty;
            if (SharedClients.TryGetValue(tokenKey, out var cachedClient))
            {
                shouldDispose = false;
                return cachedClient;
            }

            // 避免快取無上限成長：超過上限時改用一次性 HttpClient
            if (SharedClients.Count >= MaxSharedClients)
            {
                shouldDispose = true;
                return CreateClient(tokenKey);
            }

            var newClient = CreateClient(tokenKey);
            if (!SharedClients.TryAdd(tokenKey, newClient))
            {
                // 可能被其他執行緒加入，避免多留一個
                newClient.Dispose();
                if (SharedClients.TryGetValue(tokenKey, out var existing))
                {
                    shouldDispose = false;
                    return existing;
                }
            }

            shouldDispose = false;
            return newClient;
        }

        private static HttpClient CreateClient(string channelAccessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                string.IsNullOrEmpty(channelAccessToken)
                    ? null
                    : new AuthenticationHeaderValue("Bearer", channelAccessToken);
            return client;
        }
    }
}


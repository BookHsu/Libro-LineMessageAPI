using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Rich Menu API
    /// </summary>
    internal class RichMenuApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 初始化 <see cref="RichMenuApi"/> 執行個體。
        /// </summary>
        internal RichMenuApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 初始化 <see cref="RichMenuApi"/> 執行個體。
        /// </summary>
        internal RichMenuApi(
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
        /// 建立 Rich Menu
        /// </summary>
        internal RichMenuIdResponse CreateRichMenu(string channelAccessToken, object richMenu)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenu();
                var payload = serializer.Serialize(richMenu);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
                var body = result.Content.ReadAsStringSync();
                return serializer.Deserialize<RichMenuIdResponse>(body);
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
        /// 建立 Rich Menu
        /// </summary>
        internal async Task<RichMenuIdResponse> CreateRichMenuAsync(string channelAccessToken, object richMenu)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenu();
                var payload = serializer.Serialize(richMenu);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var body = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return serializer.Deserialize<RichMenuIdResponse>(body);
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
        /// 取得 Rich Menu
        /// </summary>
        internal RichMenuResponse GetRichMenu(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<RichMenuResponse>(result);
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
        /// 取得 Rich Menu
        /// </summary>
        internal async Task<RichMenuResponse> GetRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<RichMenuResponse>(result);
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
        /// 刪除 Rich Menu
        /// </summary>
        internal bool DeleteRichMenu(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Delete(url);
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
        /// 刪除 Rich Menu
        /// </summary>
        internal async Task<bool> DeleteRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                using var result = await client.DeleteAsync(url).ConfigureAwait(false);
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
        /// 取得 Rich Menu 清單
        /// </summary>
        internal RichMenuListResponse GetRichMenuList(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuList();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<RichMenuListResponse>(result);
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
        /// 取得 Rich Menu 清單
        /// </summary>
        internal async Task<RichMenuListResponse> GetRichMenuListAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuList();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<RichMenuListResponse>(result);
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
        /// 上傳 Rich Menu 圖片
        /// </summary>
        internal bool UploadRichMenuImage(string channelAccessToken, string richMenuId, string contentType, byte[] content)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                using var body = new ByteArrayContent(content ?? Array.Empty<byte>());
                body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, body);
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
        /// 上傳 Rich Menu 圖片
        /// </summary>
        internal async Task<bool> UploadRichMenuImageAsync(string channelAccessToken, string richMenuId, string contentType, byte[] content)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                using var body = new ByteArrayContent(content ?? Array.Empty<byte>());
                body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                using var result = await client.PostAsync(url, body).ConfigureAwait(false);
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
        /// 下載 Rich Menu 圖片
        /// </summary>
        internal byte[] DownloadRichMenuImage(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                var adapter = syncAdapterFactory.Create(client);
                return adapter.GetByteArray(url);
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
        /// 下載 Rich Menu 圖片
        /// </summary>
        internal async Task<byte[]> DownloadRichMenuImageAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                return await client.GetByteArrayAsync(url).ConfigureAwait(false);
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
        /// 設定預設 Rich Menu
        /// </summary>
        internal bool SetDefaultRichMenu(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu(richMenuId);
                using var content = new StringContent("{}");
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
        /// 設定預設 Rich Menu
        /// </summary>
        internal async Task<bool> SetDefaultRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu(richMenuId);
                using var content = new StringContent("{}");
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
        /// 取得預設 Rich Menu ID
        /// </summary>
        internal RichMenuIdResponse GetDefaultRichMenuId(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<RichMenuIdResponse>(result);
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
        /// 取得預設 Rich Menu ID
        /// </summary>
        internal async Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<RichMenuIdResponse>(result);
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
        /// 取消預設 Rich Menu
        /// </summary>
        internal bool CancelDefaultRichMenu(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Delete(url);
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
        /// 取消預設 Rich Menu
        /// </summary>
        internal async Task<bool> CancelDefaultRichMenuAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                using var result = await client.DeleteAsync(url).ConfigureAwait(false);
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
        /// 綁定使用者 Rich Menu
        /// </summary>
        internal bool LinkUserRichMenu(string channelAccessToken, string userId, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId, richMenuId);
                using var content = new StringContent("{}");
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
        /// 綁定使用者 Rich Menu
        /// </summary>
        internal async Task<bool> LinkUserRichMenuAsync(string channelAccessToken, string userId, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId, richMenuId);
                using var content = new StringContent("{}");
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
        /// 解除使用者 Rich Menu
        /// </summary>
        internal bool UnlinkUserRichMenu(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId);
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Delete(url);
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
        /// 解除使用者 Rich Menu
        /// </summary>
        internal async Task<bool> UnlinkUserRichMenuAsync(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId);
                using var result = await client.DeleteAsync(url).ConfigureAwait(false);
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
        /// 批次綁定 Rich Menu
        /// </summary>
        internal bool BulkLinkRichMenu(string channelAccessToken, RichMenuBulkLinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkLink();
                var payload = serializer.Serialize(request);
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
        /// 批次綁定 Rich Menu
        /// </summary>
        internal async Task<bool> BulkLinkRichMenuAsync(string channelAccessToken, RichMenuBulkLinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkLink();
                var payload = serializer.Serialize(request);
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
        /// 批次解除綁定 Rich Menu
        /// </summary>
        internal bool BulkUnlinkRichMenu(string channelAccessToken, RichMenuBulkUnlinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkUnlink();
                var payload = serializer.Serialize(request);
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
        /// 批次解除綁定 Rich Menu
        /// </summary>
        internal async Task<bool> BulkUnlinkRichMenuAsync(string channelAccessToken, RichMenuBulkUnlinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkUnlink();
                var payload = serializer.Serialize(request);
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
        /// 建立 Rich Menu Alias
        /// </summary>
        internal bool CreateRichMenuAlias(string channelAccessToken, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias();
                var payload = serializer.Serialize(request);
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
        /// 建立 Rich Menu Alias
        /// </summary>
        internal async Task<bool> CreateRichMenuAliasAsync(string channelAccessToken, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias();
                var payload = serializer.Serialize(request);
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
        /// 更新 Rich Menu Alias
        /// </summary>
        internal bool UpdateRichMenuAlias(string channelAccessToken, string aliasId, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var payload = serializer.Serialize(request);
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
        /// 更新 Rich Menu Alias
        /// </summary>
        internal async Task<bool> UpdateRichMenuAliasAsync(string channelAccessToken, string aliasId, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var payload = serializer.Serialize(request);
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
        /// 取得 Rich Menu Alias
        /// </summary>
        internal RichMenuAliasResponse GetRichMenuAlias(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<RichMenuAliasResponse>(result);
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
        /// 取得 Rich Menu Alias
        /// </summary>
        internal async Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<RichMenuAliasResponse>(result);
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
        /// 取得 Rich Menu Alias 清單
        /// </summary>
        internal RichMenuAliasListResponse GetRichMenuAliasList(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAliasList();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<RichMenuAliasListResponse>(result);
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
        /// 取得 Rich Menu Alias 清單
        /// </summary>
        internal async Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAliasList();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<RichMenuAliasListResponse>(result);
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
        /// 刪除 Rich Menu Alias
        /// </summary>
        internal bool DeleteRichMenuAlias(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Delete(url);
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
        /// 刪除 Rich Menu Alias
        /// </summary>
        internal async Task<bool> DeleteRichMenuAliasAsync(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                using var result = await client.DeleteAsync(url).ConfigureAwait(false);
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
    }
}

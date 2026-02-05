using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
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

        /// <summary>
        /// 建立 Rich Menu API
        /// </summary>
        internal RichMenuApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立 Rich Menu API
        /// </summary>
        internal RichMenuApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
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
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                var body = result.Content.ReadAsStringAsync().Result;
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
        /// 建立 Rich Menu（非同步）
        /// </summary>
        internal async Task<RichMenuIdResponse> CreateRichMenuAsync(string channelAccessToken, object richMenu)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenu();
                var payload = serializer.Serialize(richMenu);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                var body = await result.Content.ReadAsStringAsync();
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
                var result = client.GetStringAsync(url).Result;
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
        /// 取得 Rich Menu（非同步）
        /// </summary>
        internal async Task<RichMenuResponse> GetRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                var result = await client.GetStringAsync(url);
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
                var result = client.DeleteAsync(url).Result;
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
        /// 刪除 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> DeleteRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuId(richMenuId);
                var result = await client.DeleteAsync(url);
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
                var result = client.GetStringAsync(url).Result;
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
        /// 取得 Rich Menu 清單（非同步）
        /// </summary>
        internal async Task<RichMenuListResponse> GetRichMenuListAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuList();
                var result = await client.GetStringAsync(url);
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
                var body = new ByteArrayContent(content ?? new byte[0]);
                body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                var result = client.PostAsync(url, body).Result;
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
        /// 上傳 Rich Menu 圖片（非同步）
        /// </summary>
        internal async Task<bool> UploadRichMenuImageAsync(string channelAccessToken, string richMenuId, string contentType, byte[] content)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                var body = new ByteArrayContent(content ?? new byte[0]);
                body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                var result = await client.PostAsync(url, body);
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
                return client.GetByteArrayAsync(url).Result;
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
        /// 下載 Rich Menu 圖片（非同步）
        /// </summary>
        internal async Task<byte[]> DownloadRichMenuImageAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuContent(richMenuId);
                return await client.GetByteArrayAsync(url);
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
                var result = client.PostAsync(url, new StringContent("{}")).Result;
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
        /// 設定預設 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> SetDefaultRichMenuAsync(string channelAccessToken, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu(richMenuId);
                var result = await client.PostAsync(url, new StringContent("{}"));
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
                var result = client.GetStringAsync(url).Result;
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
        /// 取得預設 Rich Menu ID（非同步）
        /// </summary>
        internal async Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                var result = await client.GetStringAsync(url);
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
                var result = client.DeleteAsync(url).Result;
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
        /// 取消預設 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> CancelDefaultRichMenuAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDefaultRichMenu();
                var result = await client.DeleteAsync(url);
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
                var result = client.PostAsync(url, new StringContent("{}")).Result;
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
        /// 綁定使用者 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> LinkUserRichMenuAsync(string channelAccessToken, string userId, string richMenuId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId, richMenuId);
                var result = await client.PostAsync(url, new StringContent("{}"));
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
                var result = client.DeleteAsync(url).Result;
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
        /// 解除使用者 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> UnlinkUserRichMenuAsync(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildUserRichMenu(userId);
                var result = await client.DeleteAsync(url);
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
        /// 批次綁定 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> BulkLinkRichMenuAsync(string channelAccessToken, RichMenuBulkLinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkLink();
                var payload = serializer.Serialize(request);
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
        /// 批次解除綁定 Rich Menu（非同步）
        /// </summary>
        internal async Task<bool> BulkUnlinkRichMenuAsync(string channelAccessToken, RichMenuBulkUnlinkRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuBulkUnlink();
                var payload = serializer.Serialize(request);
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
        /// 建立 Rich Menu Alias（非同步）
        /// </summary>
        internal async Task<bool> CreateRichMenuAliasAsync(string channelAccessToken, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias();
                var payload = serializer.Serialize(request);
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
        /// 更新 Rich Menu Alias（非同步）
        /// </summary>
        internal async Task<bool> UpdateRichMenuAliasAsync(string channelAccessToken, string aliasId, RichMenuAliasRequest request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var payload = serializer.Serialize(request);
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
        /// 取得 Rich Menu Alias
        /// </summary>
        internal RichMenuAliasResponse GetRichMenuAlias(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var result = client.GetStringAsync(url).Result;
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
        /// 取得 Rich Menu Alias（非同步）
        /// </summary>
        internal async Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var result = await client.GetStringAsync(url);
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
                var result = client.GetStringAsync(url).Result;
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
        /// 取得 Rich Menu Alias 清單（非同步）
        /// </summary>
        internal async Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAliasList();
                var result = await client.GetStringAsync(url);
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
                var result = client.DeleteAsync(url).Result;
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
        /// 刪除 Rich Menu Alias（非同步）
        /// </summary>
        internal async Task<bool> DeleteRichMenuAliasAsync(string channelAccessToken, string aliasId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRichMenuAlias(aliasId);
                var result = await client.DeleteAsync(url);
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


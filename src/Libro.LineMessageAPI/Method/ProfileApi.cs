using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.LineReceivedObject;
using Libro.LineMessageApi.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 使用者與成員檔案 API
    /// </summary>
    internal class ProfileApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 建立檔案 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal ProfileApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 建立檔案 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal ProfileApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        /// <summary>
        /// 取得使用者檔案
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="userId">使用者 ID</param>
        /// <returns>使用者檔案</returns>
        internal UserProfile GetUserProfile(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userId);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(strUrl);
                return serializer.Deserialize<UserProfile>(result);
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
        /// 取得使用者檔案（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="userId">使用者 ID</param>
        /// <returns>使用者檔案</returns>
        internal async Task<UserProfile> GetUserProfileAsync(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userId);
                var result = await client.GetStringAsync(strUrl).ConfigureAwait(false);
                return serializer.Deserialize<UserProfile>(result);
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
        /// 取得群組或對話內成員檔案
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="userId">使用者 ID</param>
        /// <param name="groupId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>使用者檔案</returns>
        internal UserProfile GetGroupMemberProfile(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(strUrl);
                return serializer.Deserialize<UserProfile>(result);
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
        /// 取得群組或對話內成員檔案（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="userId">使用者 ID</param>
        /// <param name="groupId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>使用者檔案</returns>
        internal async Task<UserProfile> GetGroupMemberProfileAsync(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var result = await client.GetStringAsync(strUrl).ConfigureAwait(false);
                return serializer.Deserialize<UserProfile>(result);
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


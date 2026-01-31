using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// 使用者與成員檔案 API
    /// </summary>
    internal class ProfileApi
    {
        private readonly IJsonSerializer serializer;
        private readonly HttpClient httpClient;

        /// <summary>
        /// 建立檔案 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal ProfileApi(IJsonSerializer serializer, HttpClient httpClient = null)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? throw new System.ArgumentNullException(nameof(serializer));
            // 保存 HttpClient（可透過 DI 注入）
            this.httpClient = httpClient;
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
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userId);
                var result = client.GetStringAsync(strUrl).Result;
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
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userId);
                var result = await client.GetStringAsync(strUrl);
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
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var result = client.GetStringAsync(strUrl).Result;
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
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var result = await client.GetStringAsync(strUrl);
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

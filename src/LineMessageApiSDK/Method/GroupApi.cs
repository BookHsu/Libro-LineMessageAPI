using System.Net.Http;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// 群組或多人對話 API
    /// </summary>
    internal class GroupApi
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// 建立群組 API
        /// </summary>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal GroupApi(HttpClient httpClient = null)
        {
            // 保存 HttpClient（可透過 DI 注入）
            this.httpClient = httpClient;
        }

        /// <summary>
        /// 離開群組或多人對話
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="id">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>是否成功</returns>
        internal bool LeaveRoomOrGroup(string channelAccessToken, string id, SourceType type)
        {
            string strUrl = LineApiEndpoints.BuildLeaveGroupOrRoom(type, id);
            bool flag = false;
            bool shouldDispose;
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                var result = client.PostAsync(strUrl, new StringContent("")).Result;
                flag = result.IsSuccessStatusCode;
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
            return flag;
        }

        /// <summary>
        /// 離開群組或多人對話（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="id">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>是否成功</returns>
        internal async Task<bool> LeaveRoomOrGroupAsync(string channelAccessToken, string id, SourceType type)
        {
            string strUrl = LineApiEndpoints.BuildLeaveGroupOrRoom(type, id);
            bool flag = false;
            bool shouldDispose;
            HttpClient client = GetClientDefault(channelAccessToken, out shouldDispose);
            try
            {
                var result = await client.PostAsync(strUrl, new StringContent(""));
                flag = result.IsSuccessStatusCode;
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
            return flag;
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

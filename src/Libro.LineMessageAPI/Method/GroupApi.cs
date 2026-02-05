using Libro.LineMessageApi.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 群組或多人對話 API
    /// </summary>
    internal class GroupApi
    {
        private readonly IHttpClientProvider httpClientProvider;

        /// <summary>
        /// 建立群組 API
        /// </summary>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal GroupApi(HttpClient httpClient = null)
            : this(new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立群組 API
        /// </summary>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal GroupApi(IHttpClientProvider httpClientProvider)
        {
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
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
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
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
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
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
    }
}


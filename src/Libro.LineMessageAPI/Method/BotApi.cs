using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Bot 與群組/對話資訊 API
    /// </summary>
    internal class BotApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;

        /// <summary>
        /// 建立 Bot API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal BotApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立 Bot API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal BotApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
        }

        /// <summary>
        /// 取得 Bot 基本資訊
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>Bot 資訊</returns>
        internal BotInfo GetBotInfo(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildBotInfo();
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<BotInfo>(result);
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
        /// 取得 Bot 基本資訊（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <returns>Bot 資訊</returns>
        internal async Task<BotInfo> GetBotInfoAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildBotInfo();
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<BotInfo>(result);
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
        /// 取得群組摘要
        /// </summary>
        internal GroupSummary GetGroupSummary(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupSummary(groupId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<GroupSummary>(result);
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
        /// 取得群組摘要（非同步）
        /// </summary>
        internal async Task<GroupSummary> GetGroupSummaryAsync(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupSummary(groupId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<GroupSummary>(result);
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
        /// 取得多人對話摘要
        /// </summary>
        internal RoomSummary GetRoomSummary(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomSummary(roomId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<RoomSummary>(result);
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
        /// 取得多人對話摘要（非同步）
        /// </summary>
        internal async Task<RoomSummary> GetRoomSummaryAsync(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomSummary(roomId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<RoomSummary>(result);
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
        /// 取得群組成員 ID 清單
        /// </summary>
        internal MemberIdsResponse GetGroupMemberIds(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupMemberIds(groupId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<MemberIdsResponse>(result);
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
        /// 取得群組成員 ID 清單（非同步）
        /// </summary>
        internal async Task<MemberIdsResponse> GetGroupMemberIdsAsync(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupMemberIds(groupId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<MemberIdsResponse>(result);
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
        /// 取得多人對話成員 ID 清單
        /// </summary>
        internal MemberIdsResponse GetRoomMemberIds(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomMemberIds(roomId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<MemberIdsResponse>(result);
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
        /// 取得多人對話成員 ID 清單（非同步）
        /// </summary>
        internal async Task<MemberIdsResponse> GetRoomMemberIdsAsync(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomMemberIds(roomId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<MemberIdsResponse>(result);
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
        /// 取得群組成員數量
        /// </summary>
        internal MemberCountResponse GetGroupMemberCount(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupMemberCount(groupId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<MemberCountResponse>(result);
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
        /// 取得群組成員數量（非同步）
        /// </summary>
        internal async Task<MemberCountResponse> GetGroupMemberCountAsync(string channelAccessToken, string groupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildGroupMemberCount(groupId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<MemberCountResponse>(result);
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
        /// 取得多人對話成員數量
        /// </summary>
        internal MemberCountResponse GetRoomMemberCount(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomMemberCount(roomId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<MemberCountResponse>(result);
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
        /// 取得多人對話成員數量（非同步）
        /// </summary>
        internal async Task<MemberCountResponse> GetRoomMemberCountAsync(string channelAccessToken, string roomId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildRoomMemberCount(roomId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<MemberCountResponse>(result);
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


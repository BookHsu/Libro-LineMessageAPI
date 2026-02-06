using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Bot 與群組/對話資訊服務
    /// </summary>
    internal class BotService : IBotService
    {
        private readonly LineApiContext context;
        private readonly BotApi api;

        /// <summary>
        /// 建立 Bot 服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal BotService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClientProvider
            this.context = context;
            // 建立 Bot API
            api = new BotApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <inheritdoc />
        public BotInfo GetBotInfo()
        {
            return api.GetBotInfo(context.ChannelAccessToken);
        }

        /// <inheritdoc />
        public Task<BotInfo> GetBotInfoAsync()
        {
            return api.GetBotInfoAsync(context.ChannelAccessToken);
        }

        /// <inheritdoc />
        public GroupSummary GetGroupSummary(string groupId)
        {
            return api.GetGroupSummary(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public Task<GroupSummary> GetGroupSummaryAsync(string groupId)
        {
            return api.GetGroupSummaryAsync(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public RoomSummary GetRoomSummary(string roomId)
        {
            return api.GetRoomSummary(context.ChannelAccessToken, roomId);
        }

        /// <inheritdoc />
        public Task<RoomSummary> GetRoomSummaryAsync(string roomId)
        {
            return api.GetRoomSummaryAsync(context.ChannelAccessToken, roomId);
        }

        /// <inheritdoc />
        public MemberIdsResponse GetGroupMemberIds(string groupId)
        {
            return api.GetGroupMemberIds(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public Task<MemberIdsResponse> GetGroupMemberIdsAsync(string groupId)
        {
            return api.GetGroupMemberIdsAsync(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public MemberIdsResponse GetRoomMemberIds(string roomId)
        {
            return api.GetRoomMemberIds(context.ChannelAccessToken, roomId);
        }

        /// <inheritdoc />
        public Task<MemberIdsResponse> GetRoomMemberIdsAsync(string roomId)
        {
            return api.GetRoomMemberIdsAsync(context.ChannelAccessToken, roomId);
        }

        /// <inheritdoc />
        public MemberCountResponse GetGroupMemberCount(string groupId)
        {
            return api.GetGroupMemberCount(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public Task<MemberCountResponse> GetGroupMemberCountAsync(string groupId)
        {
            return api.GetGroupMemberCountAsync(context.ChannelAccessToken, groupId);
        }

        /// <inheritdoc />
        public MemberCountResponse GetRoomMemberCount(string roomId)
        {
            return api.GetRoomMemberCount(context.ChannelAccessToken, roomId);
        }

        /// <inheritdoc />
        public Task<MemberCountResponse> GetRoomMemberCountAsync(string roomId)
        {
            return api.GetRoomMemberCountAsync(context.ChannelAccessToken, roomId);
        }
    }
}


using LineMessageApiSDK.Method;
using System;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// 群組或多人對話服務
    /// </summary>
    internal class GroupService : IGroupService
    {
        private readonly LineApiContext context;
        private readonly MessageApi messageApi;

        /// <summary>
        /// 建立群組服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal GroupService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClient
            this.context = context;
            // 建立內部 API（支援 HttpClient DI）
            messageApi = new MessageApi(context.Serializer, context.HttpClient);
        }

        /// <inheritdoc />
        public bool LeaveRoomOrGroup(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                // 避免使用者類型觸發不支援的 API
                throw new NotSupportedException("無法使用 SourceType = User");
            }

            return messageApi.LeaveRoomOrGroup(context.ChannelAccessToken, sourceId, type);
        }

        /// <inheritdoc />
        public Task<bool> LeaveRoomOrGroupAsync(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                // 避免使用者類型觸發不支援的 API
                throw new NotSupportedException("無法使用 SourceType = User");
            }

            return messageApi.LeaveRoomOrGroupAsync(context.ChannelAccessToken, sourceId, type);
        }
    }
}

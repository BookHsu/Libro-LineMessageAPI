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
        private readonly GroupApi groupApi;

        /// <summary>
        /// 建立群組服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal GroupService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClient
            this.context = context;
            // 建立群組 API（支援 HttpClient DI）
            groupApi = new GroupApi(context.HttpClient);
        }

        /// <inheritdoc />
        public bool LeaveRoomOrGroup(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                // 避免使用者類型觸發不支援的 API
                throw new NotSupportedException("無法使用 SourceType = User");
            }

            return groupApi.LeaveRoomOrGroup(context.ChannelAccessToken, sourceId, type);
        }

        /// <inheritdoc />
        public Task<bool> LeaveRoomOrGroupAsync(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                // 避免使用者類型觸發不支援的 API
                throw new NotSupportedException("無法使用 SourceType = User");
            }

            return groupApi.LeaveRoomOrGroupAsync(context.ChannelAccessToken, sourceId, type);
        }
    }
}

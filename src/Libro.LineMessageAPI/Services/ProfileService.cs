using Libro.LineMessageApi.LineReceivedObject;
using Libro.LineMessageApi.Method;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 使用者與成員檔案服務
    /// </summary>
    internal class ProfileService : IProfileService
    {
        private readonly LineApiContext context;
        private readonly ProfileApi profileApi;

        /// <summary>
        /// 建立檔案服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal ProfileService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClient
            this.context = context;
            // 建立檔案 API（支援 HttpClient DI）
            profileApi = new ProfileApi(context.Serializer, context.HttpClientProvider);
        }

        /// <inheritdoc />
        public UserProfile GetUserProfile(string userId)
        {
            // 取得使用者檔案
            return profileApi.GetUserProfile(context.ChannelAccessToken, userId);
        }

        /// <inheritdoc />
        public Task<UserProfile> GetUserProfileAsync(string userId)
        {
            // 取得使用者檔案（非同步）
            return profileApi.GetUserProfileAsync(context.ChannelAccessToken, userId);
        }

        /// <inheritdoc />
        public UserProfile GetGroupMemberProfile(string userId, string groupIdOrRoomId, SourceType type)
        {
            // 取得群組或多人對話成員檔案
            return profileApi.GetGroupMemberProfile(context.ChannelAccessToken, userId, groupIdOrRoomId, type);
        }

        /// <inheritdoc />
        public Task<UserProfile> GetGroupMemberProfileAsync(string userId, string groupIdOrRoomId, SourceType type)
        {
            // 取得群組或多人對話成員檔案（非同步）
            return profileApi.GetGroupMemberProfileAsync(context.ChannelAccessToken, userId, groupIdOrRoomId, type);
        }
    }
}


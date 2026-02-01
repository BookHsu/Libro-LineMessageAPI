using LineMessageApiSDK.LineReceivedObject;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// 使用者與成員檔案服務介面
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// 取得使用者檔案
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <returns>使用者檔案</returns>
        UserProfile GetUserProfile(string userId);

        /// <summary>
        /// 取得使用者檔案（非同步）
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <returns>使用者檔案</returns>
        Task<UserProfile> GetUserProfileAsync(string userId);

        /// <summary>
        /// 取得群組或多人對話成員檔案
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <param name="groupIdOrRoomId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>使用者檔案</returns>
        UserProfile GetGroupMemberProfile(string userId, string groupIdOrRoomId, SourceType type);

        /// <summary>
        /// 取得群組或多人對話成員檔案（非同步）
        /// </summary>
        /// <param name="userId">使用者 ID</param>
        /// <param name="groupIdOrRoomId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>使用者檔案</returns>
        Task<UserProfile> GetGroupMemberProfileAsync(string userId, string groupIdOrRoomId, SourceType type);
    }
}

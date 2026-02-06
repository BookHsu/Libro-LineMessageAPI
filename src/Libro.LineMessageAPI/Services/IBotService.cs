using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Bot 與群組/對話資訊服務
    /// </summary>
    public interface IBotService
    {
        /// <summary>
        /// 取得 Bot 資訊
        /// </summary>
        BotInfo GetBotInfo();

        /// <summary>
        /// 取得 Bot 資訊
        /// </summary>
        Task<BotInfo> GetBotInfoAsync();

        /// <summary>
        /// 取得群組摘要
        /// </summary>
        GroupSummary GetGroupSummary(string groupId);

        /// <summary>
        /// 取得群組摘要
        /// </summary>
        Task<GroupSummary> GetGroupSummaryAsync(string groupId);

        /// <summary>
        /// 取得多人對話摘要
        /// </summary>
        RoomSummary GetRoomSummary(string roomId);

        /// <summary>
        /// 取得多人對話摘要
        /// </summary>
        Task<RoomSummary> GetRoomSummaryAsync(string roomId);

        /// <summary>
        /// 取得群組成員 ID 清單
        /// </summary>
        MemberIdsResponse GetGroupMemberIds(string groupId);

        /// <summary>
        /// 取得群組成員 ID 清單
        /// </summary>
        Task<MemberIdsResponse> GetGroupMemberIdsAsync(string groupId);

        /// <summary>
        /// 取得多人對話成員 ID 清單
        /// </summary>
        MemberIdsResponse GetRoomMemberIds(string roomId);

        /// <summary>
        /// 取得多人對話成員 ID 清單
        /// </summary>
        Task<MemberIdsResponse> GetRoomMemberIdsAsync(string roomId);

        /// <summary>
        /// 取得群組成員數量
        /// </summary>
        MemberCountResponse GetGroupMemberCount(string groupId);

        /// <summary>
        /// 取得群組成員數量
        /// </summary>
        Task<MemberCountResponse> GetGroupMemberCountAsync(string groupId);

        /// <summary>
        /// 取得多人對話成員數量
        /// </summary>
        MemberCountResponse GetRoomMemberCount(string roomId);

        /// <summary>
        /// 取得多人對話成員數量
        /// </summary>
        Task<MemberCountResponse> GetRoomMemberCountAsync(string roomId);
    }
}


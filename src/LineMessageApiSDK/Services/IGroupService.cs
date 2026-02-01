using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// 群組或多人對話服務介面
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// 離開群組或多人對話
        /// </summary>
        /// <param name="sourceId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>是否成功</returns>
        bool LeaveRoomOrGroup(string sourceId, SourceType type);

        /// <summary>
        /// 離開群組或多人對話（非同步）
        /// </summary>
        /// <param name="sourceId">群組或對話 ID</param>
        /// <param name="type">來源類型</param>
        /// <returns>是否成功</returns>
        Task<bool> LeaveRoomOrGroupAsync(string sourceId, SourceType type);
    }
}

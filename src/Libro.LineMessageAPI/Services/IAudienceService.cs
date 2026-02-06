using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供受眾群組（Audience）管理服務。
    /// </summary>
    public interface IAudienceService
    {
        /// <summary>
        /// 上傳受眾群組。
        /// </summary>
        /// <param name="request">受眾群組上傳請求內容。</param>
        /// <returns>受眾群組上傳結果。</returns>
        AudienceGroupUploadResponse UploadAudienceGroup(object request);
        /// <summary>
        /// 上傳受眾群組。
        /// </summary>
        /// <param name="request">受眾群組上傳請求內容。</param>
        /// <returns>受眾群組上傳結果。</returns>
        Task<AudienceGroupUploadResponse> UploadAudienceGroupAsync(object request);
        /// <summary>
        /// 取得受眾群組狀態。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>受眾群組狀態。</returns>
        AudienceGroupStatusResponse GetAudienceGroupStatus(long audienceGroupId);
        /// <summary>
        /// 取得受眾群組狀態。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>受眾群組狀態。</returns>
        Task<AudienceGroupStatusResponse> GetAudienceGroupStatusAsync(long audienceGroupId);
        /// <summary>
        /// 刪除受眾群組。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>是否刪除成功。</returns>
        bool DeleteAudienceGroup(long audienceGroupId);
        /// <summary>
        /// 刪除受眾群組。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>是否刪除成功。</returns>
        Task<bool> DeleteAudienceGroupAsync(long audienceGroupId);
        /// <summary>
        /// 取得受眾群組清單。
        /// </summary>
        /// <returns>受眾群組清單。</returns>
        AudienceGroupListResponse GetAudienceGroupList();
        /// <summary>
        /// 取得受眾群組清單。
        /// </summary>
        /// <returns>受眾群組清單。</returns>
        Task<AudienceGroupListResponse> GetAudienceGroupListAsync();
    }
}


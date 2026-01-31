using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Audience 服務
    /// </summary>
    public interface IAudienceService
    {
        AudienceGroupUploadResponse UploadAudienceGroup(object request);
        Task<AudienceGroupUploadResponse> UploadAudienceGroupAsync(object request);
        AudienceGroupStatusResponse GetAudienceGroupStatus(long audienceGroupId);
        Task<AudienceGroupStatusResponse> GetAudienceGroupStatusAsync(long audienceGroupId);
        bool DeleteAudienceGroup(long audienceGroupId);
        Task<bool> DeleteAudienceGroupAsync(long audienceGroupId);
        AudienceGroupListResponse GetAudienceGroupList();
        Task<AudienceGroupListResponse> GetAudienceGroupListAsync();
    }
}

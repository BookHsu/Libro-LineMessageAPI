using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Audience 服務
    /// </summary>
    internal class AudienceService : IAudienceService
    {
        private readonly LineApiContext context;
        private readonly AudienceApi api;

        internal AudienceService(LineApiContext context)
        {
            this.context = context;
            api = new AudienceApi(context.Serializer, context.HttpClientProvider);
        }

        public AudienceGroupUploadResponse UploadAudienceGroup(object request)
        {
            return api.UploadAudienceGroup(context.ChannelAccessToken, request);
        }

        public Task<AudienceGroupUploadResponse> UploadAudienceGroupAsync(object request)
        {
            return api.UploadAudienceGroupAsync(context.ChannelAccessToken, request);
        }

        public AudienceGroupStatusResponse GetAudienceGroupStatus(long audienceGroupId)
        {
            return api.GetAudienceGroupStatus(context.ChannelAccessToken, audienceGroupId);
        }

        public Task<AudienceGroupStatusResponse> GetAudienceGroupStatusAsync(long audienceGroupId)
        {
            return api.GetAudienceGroupStatusAsync(context.ChannelAccessToken, audienceGroupId);
        }

        public bool DeleteAudienceGroup(long audienceGroupId)
        {
            return api.DeleteAudienceGroup(context.ChannelAccessToken, audienceGroupId);
        }

        public Task<bool> DeleteAudienceGroupAsync(long audienceGroupId)
        {
            return api.DeleteAudienceGroupAsync(context.ChannelAccessToken, audienceGroupId);
        }

        public AudienceGroupListResponse GetAudienceGroupList()
        {
            return api.GetAudienceGroupList(context.ChannelAccessToken);
        }

        public Task<AudienceGroupListResponse> GetAudienceGroupListAsync()
        {
            return api.GetAudienceGroupListAsync(context.ChannelAccessToken);
        }
    }
}


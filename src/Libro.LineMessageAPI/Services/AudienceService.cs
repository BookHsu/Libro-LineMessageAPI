using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供受眾群組（Audience）管理服務。
    /// </summary>
    internal class AudienceService : IAudienceService
    {
        private readonly LineApiContext context;
        private readonly AudienceApi api;

        internal AudienceService(LineApiContext context)
        {
            this.context = context;
            api = new AudienceApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <summary>
        /// 上傳受眾群組。
        /// </summary>
        /// <param name="request">受眾群組上傳請求內容。</param>
        /// <returns>受眾群組上傳結果。</returns>
        public AudienceGroupUploadResponse UploadAudienceGroup(object request)
        {
            return api.UploadAudienceGroup(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 上傳受眾群組。
        /// </summary>
        /// <param name="request">受眾群組上傳請求內容。</param>
        /// <returns>受眾群組上傳結果。</returns>
        public Task<AudienceGroupUploadResponse> UploadAudienceGroupAsync(object request)
        {
            return api.UploadAudienceGroupAsync(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 取得受眾群組狀態。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>受眾群組狀態。</returns>
        public AudienceGroupStatusResponse GetAudienceGroupStatus(long audienceGroupId)
        {
            return api.GetAudienceGroupStatus(context.ChannelAccessToken, audienceGroupId);
        }

        /// <summary>
        /// 取得受眾群組狀態。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>受眾群組狀態。</returns>
        public Task<AudienceGroupStatusResponse> GetAudienceGroupStatusAsync(long audienceGroupId)
        {
            return api.GetAudienceGroupStatusAsync(context.ChannelAccessToken, audienceGroupId);
        }

        /// <summary>
        /// 刪除受眾群組。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>是否刪除成功。</returns>
        public bool DeleteAudienceGroup(long audienceGroupId)
        {
            return api.DeleteAudienceGroup(context.ChannelAccessToken, audienceGroupId);
        }

        /// <summary>
        /// 刪除受眾群組。
        /// </summary>
        /// <param name="audienceGroupId">受眾群組 ID。</param>
        /// <returns>是否刪除成功。</returns>
        public Task<bool> DeleteAudienceGroupAsync(long audienceGroupId)
        {
            return api.DeleteAudienceGroupAsync(context.ChannelAccessToken, audienceGroupId);
        }

        /// <summary>
        /// 取得受眾群組清單。
        /// </summary>
        /// <returns>受眾群組清單。</returns>
        public AudienceGroupListResponse GetAudienceGroupList()
        {
            return api.GetAudienceGroupList(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得受眾群組清單。
        /// </summary>
        /// <returns>受眾群組清單。</returns>
        public Task<AudienceGroupListResponse> GetAudienceGroupListAsync()
        {
            return api.GetAudienceGroupListAsync(context.ChannelAccessToken);
        }
    }
}


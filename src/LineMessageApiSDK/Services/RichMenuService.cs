using LineMessageApiSDK.Method;
using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Rich Menu 服務
    /// </summary>
    internal class RichMenuService : IRichMenuService
    {
        private readonly LineApiContext context;
        private readonly RichMenuApi api;

        /// <summary>
        /// 建立 Rich Menu 服務
        /// </summary>
        internal RichMenuService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClientProvider
            this.context = context;
            // 建立 Rich Menu API
            api = new RichMenuApi(context.Serializer, context.HttpClientProvider);
        }

        public RichMenuIdResponse CreateRichMenu(object richMenu)
        {
            return api.CreateRichMenu(context.ChannelAccessToken, richMenu);
        }

        public Task<RichMenuIdResponse> CreateRichMenuAsync(object richMenu)
        {
            return api.CreateRichMenuAsync(context.ChannelAccessToken, richMenu);
        }

        public RichMenuResponse GetRichMenu(string richMenuId)
        {
            return api.GetRichMenu(context.ChannelAccessToken, richMenuId);
        }

        public Task<RichMenuResponse> GetRichMenuAsync(string richMenuId)
        {
            return api.GetRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        public bool DeleteRichMenu(string richMenuId)
        {
            return api.DeleteRichMenu(context.ChannelAccessToken, richMenuId);
        }

        public Task<bool> DeleteRichMenuAsync(string richMenuId)
        {
            return api.DeleteRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        public RichMenuListResponse GetRichMenuList()
        {
            return api.GetRichMenuList(context.ChannelAccessToken);
        }

        public Task<RichMenuListResponse> GetRichMenuListAsync()
        {
            return api.GetRichMenuListAsync(context.ChannelAccessToken);
        }

        public bool UploadRichMenuImage(string richMenuId, string contentType, byte[] content)
        {
            return api.UploadRichMenuImage(context.ChannelAccessToken, richMenuId, contentType, content);
        }

        public Task<bool> UploadRichMenuImageAsync(string richMenuId, string contentType, byte[] content)
        {
            return api.UploadRichMenuImageAsync(context.ChannelAccessToken, richMenuId, contentType, content);
        }

        public byte[] DownloadRichMenuImage(string richMenuId)
        {
            return api.DownloadRichMenuImage(context.ChannelAccessToken, richMenuId);
        }

        public Task<byte[]> DownloadRichMenuImageAsync(string richMenuId)
        {
            return api.DownloadRichMenuImageAsync(context.ChannelAccessToken, richMenuId);
        }

        public bool SetDefaultRichMenu(string richMenuId)
        {
            return api.SetDefaultRichMenu(context.ChannelAccessToken, richMenuId);
        }

        public Task<bool> SetDefaultRichMenuAsync(string richMenuId)
        {
            return api.SetDefaultRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        public RichMenuIdResponse GetDefaultRichMenuId()
        {
            return api.GetDefaultRichMenuId(context.ChannelAccessToken);
        }

        public Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync()
        {
            return api.GetDefaultRichMenuIdAsync(context.ChannelAccessToken);
        }

        public bool CancelDefaultRichMenu()
        {
            return api.CancelDefaultRichMenu(context.ChannelAccessToken);
        }

        public Task<bool> CancelDefaultRichMenuAsync()
        {
            return api.CancelDefaultRichMenuAsync(context.ChannelAccessToken);
        }

        public bool LinkUserRichMenu(string userId, string richMenuId)
        {
            return api.LinkUserRichMenu(context.ChannelAccessToken, userId, richMenuId);
        }

        public Task<bool> LinkUserRichMenuAsync(string userId, string richMenuId)
        {
            return api.LinkUserRichMenuAsync(context.ChannelAccessToken, userId, richMenuId);
        }

        public bool UnlinkUserRichMenu(string userId)
        {
            return api.UnlinkUserRichMenu(context.ChannelAccessToken, userId);
        }

        public Task<bool> UnlinkUserRichMenuAsync(string userId)
        {
            return api.UnlinkUserRichMenuAsync(context.ChannelAccessToken, userId);
        }

        public bool BulkLinkRichMenu(RichMenuBulkLinkRequest request)
        {
            return api.BulkLinkRichMenu(context.ChannelAccessToken, request);
        }

        public Task<bool> BulkLinkRichMenuAsync(RichMenuBulkLinkRequest request)
        {
            return api.BulkLinkRichMenuAsync(context.ChannelAccessToken, request);
        }

        public bool BulkUnlinkRichMenu(RichMenuBulkUnlinkRequest request)
        {
            return api.BulkUnlinkRichMenu(context.ChannelAccessToken, request);
        }

        public Task<bool> BulkUnlinkRichMenuAsync(RichMenuBulkUnlinkRequest request)
        {
            return api.BulkUnlinkRichMenuAsync(context.ChannelAccessToken, request);
        }

        public bool CreateRichMenuAlias(RichMenuAliasRequest request)
        {
            return api.CreateRichMenuAlias(context.ChannelAccessToken, request);
        }

        public Task<bool> CreateRichMenuAliasAsync(RichMenuAliasRequest request)
        {
            return api.CreateRichMenuAliasAsync(context.ChannelAccessToken, request);
        }

        public bool UpdateRichMenuAlias(string aliasId, RichMenuAliasRequest request)
        {
            return api.UpdateRichMenuAlias(context.ChannelAccessToken, aliasId, request);
        }

        public Task<bool> UpdateRichMenuAliasAsync(string aliasId, RichMenuAliasRequest request)
        {
            return api.UpdateRichMenuAliasAsync(context.ChannelAccessToken, aliasId, request);
        }

        public RichMenuAliasResponse GetRichMenuAlias(string aliasId)
        {
            return api.GetRichMenuAlias(context.ChannelAccessToken, aliasId);
        }

        public Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string aliasId)
        {
            return api.GetRichMenuAliasAsync(context.ChannelAccessToken, aliasId);
        }

        public RichMenuAliasListResponse GetRichMenuAliasList()
        {
            return api.GetRichMenuAliasList(context.ChannelAccessToken);
        }

        public Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync()
        {
            return api.GetRichMenuAliasListAsync(context.ChannelAccessToken);
        }

        public bool DeleteRichMenuAlias(string aliasId)
        {
            return api.DeleteRichMenuAlias(context.ChannelAccessToken, aliasId);
        }

        public Task<bool> DeleteRichMenuAliasAsync(string aliasId)
        {
            return api.DeleteRichMenuAliasAsync(context.ChannelAccessToken, aliasId);
        }
    }
}

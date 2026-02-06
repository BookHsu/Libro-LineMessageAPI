using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供 Rich Menu 管理服務。
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
            api = new RichMenuApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <summary>
        /// 建立 Rich Menu。
        /// </summary>
        public RichMenuIdResponse CreateRichMenu(object richMenu)
        {
            return api.CreateRichMenu(context.ChannelAccessToken, richMenu);
        }

        /// <summary>
        /// 建立 Rich Menu。
        /// </summary>
        public Task<RichMenuIdResponse> CreateRichMenuAsync(object richMenu)
        {
            return api.CreateRichMenuAsync(context.ChannelAccessToken, richMenu);
        }

        /// <summary>
        /// 依 Rich Menu ID 取得 Rich Menu 詳細資料。
        /// </summary>
        public RichMenuResponse GetRichMenu(string richMenuId)
        {
            return api.GetRichMenu(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 依 Rich Menu ID 取得 Rich Menu 詳細資料。
        /// </summary>
        public Task<RichMenuResponse> GetRichMenuAsync(string richMenuId)
        {
            return api.GetRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 刪除 Rich Menu。
        /// </summary>
        public bool DeleteRichMenu(string richMenuId)
        {
            return api.DeleteRichMenu(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 刪除 Rich Menu。
        /// </summary>
        public Task<bool> DeleteRichMenuAsync(string richMenuId)
        {
            return api.DeleteRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 取得 Rich Menu 清單。
        /// </summary>
        public RichMenuListResponse GetRichMenuList()
        {
            return api.GetRichMenuList(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得 Rich Menu 清單。
        /// </summary>
        public Task<RichMenuListResponse> GetRichMenuListAsync()
        {
            return api.GetRichMenuListAsync(context.ChannelAccessToken);
        }

        /// <summary>
        /// 上傳 Rich Menu 圖片。
        /// </summary>
        public bool UploadRichMenuImage(string richMenuId, string contentType, byte[] content)
        {
            return api.UploadRichMenuImage(context.ChannelAccessToken, richMenuId, contentType, content);
        }

        /// <summary>
        /// 上傳 Rich Menu 圖片。
        /// </summary>
        public Task<bool> UploadRichMenuImageAsync(string richMenuId, string contentType, byte[] content)
        {
            return api.UploadRichMenuImageAsync(context.ChannelAccessToken, richMenuId, contentType, content);
        }

        /// <summary>
        /// 下載 Rich Menu 圖片。
        /// </summary>
        public byte[] DownloadRichMenuImage(string richMenuId)
        {
            return api.DownloadRichMenuImage(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 下載 Rich Menu 圖片。
        /// </summary>
        public Task<byte[]> DownloadRichMenuImageAsync(string richMenuId)
        {
            return api.DownloadRichMenuImageAsync(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 設定預設 Rich Menu。
        /// </summary>
        public bool SetDefaultRichMenu(string richMenuId)
        {
            return api.SetDefaultRichMenu(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 設定預設 Rich Menu。
        /// </summary>
        public Task<bool> SetDefaultRichMenuAsync(string richMenuId)
        {
            return api.SetDefaultRichMenuAsync(context.ChannelAccessToken, richMenuId);
        }

        /// <summary>
        /// 取得預設 Rich Menu ID。
        /// </summary>
        public RichMenuIdResponse GetDefaultRichMenuId()
        {
            return api.GetDefaultRichMenuId(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得預設 Rich Menu ID。
        /// </summary>
        public Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync()
        {
            return api.GetDefaultRichMenuIdAsync(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取消預設 Rich Menu。
        /// </summary>
        public bool CancelDefaultRichMenu()
        {
            return api.CancelDefaultRichMenu(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取消預設 Rich Menu。
        /// </summary>
        public Task<bool> CancelDefaultRichMenuAsync()
        {
            return api.CancelDefaultRichMenuAsync(context.ChannelAccessToken);
        }

        /// <summary>
        /// 將指定使用者與 Rich Menu 進行綁定。
        /// </summary>
        public bool LinkUserRichMenu(string userId, string richMenuId)
        {
            return api.LinkUserRichMenu(context.ChannelAccessToken, userId, richMenuId);
        }

        /// <summary>
        /// 將指定使用者與 Rich Menu 進行綁定。
        /// </summary>
        public Task<bool> LinkUserRichMenuAsync(string userId, string richMenuId)
        {
            return api.LinkUserRichMenuAsync(context.ChannelAccessToken, userId, richMenuId);
        }

        /// <summary>
        /// 解除指定使用者與 Rich Menu 的綁定。
        /// </summary>
        public bool UnlinkUserRichMenu(string userId)
        {
            return api.UnlinkUserRichMenu(context.ChannelAccessToken, userId);
        }

        /// <summary>
        /// 解除指定使用者與 Rich Menu 的綁定。
        /// </summary>
        public Task<bool> UnlinkUserRichMenuAsync(string userId)
        {
            return api.UnlinkUserRichMenuAsync(context.ChannelAccessToken, userId);
        }

        /// <summary>
        /// 批次綁定 Rich Menu。
        /// </summary>
        public bool BulkLinkRichMenu(RichMenuBulkLinkRequest request)
        {
            return api.BulkLinkRichMenu(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 批次綁定 Rich Menu。
        /// </summary>
        public Task<bool> BulkLinkRichMenuAsync(RichMenuBulkLinkRequest request)
        {
            return api.BulkLinkRichMenuAsync(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 批次解除 Rich Menu 綁定。
        /// </summary>
        public bool BulkUnlinkRichMenu(RichMenuBulkUnlinkRequest request)
        {
            return api.BulkUnlinkRichMenu(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 批次解除 Rich Menu 綁定。
        /// </summary>
        public Task<bool> BulkUnlinkRichMenuAsync(RichMenuBulkUnlinkRequest request)
        {
            return api.BulkUnlinkRichMenuAsync(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 建立 Rich Menu Alias。
        /// </summary>
        public bool CreateRichMenuAlias(RichMenuAliasRequest request)
        {
            return api.CreateRichMenuAlias(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 建立 Rich Menu Alias。
        /// </summary>
        public Task<bool> CreateRichMenuAliasAsync(RichMenuAliasRequest request)
        {
            return api.CreateRichMenuAliasAsync(context.ChannelAccessToken, request);
        }

        /// <summary>
        /// 更新 Rich Menu Alias。
        /// </summary>
        public bool UpdateRichMenuAlias(string aliasId, RichMenuAliasRequest request)
        {
            return api.UpdateRichMenuAlias(context.ChannelAccessToken, aliasId, request);
        }

        /// <summary>
        /// 更新 Rich Menu Alias。
        /// </summary>
        public Task<bool> UpdateRichMenuAliasAsync(string aliasId, RichMenuAliasRequest request)
        {
            return api.UpdateRichMenuAliasAsync(context.ChannelAccessToken, aliasId, request);
        }

        /// <summary>
        /// 依 Alias ID 取得 Rich Menu Alias。
        /// </summary>
        public RichMenuAliasResponse GetRichMenuAlias(string aliasId)
        {
            return api.GetRichMenuAlias(context.ChannelAccessToken, aliasId);
        }

        /// <summary>
        /// 依 Alias ID 取得 Rich Menu Alias。
        /// </summary>
        public Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string aliasId)
        {
            return api.GetRichMenuAliasAsync(context.ChannelAccessToken, aliasId);
        }

        /// <summary>
        /// 取得 Rich Menu Alias 清單。
        /// </summary>
        public RichMenuAliasListResponse GetRichMenuAliasList()
        {
            return api.GetRichMenuAliasList(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得 Rich Menu Alias 清單。
        /// </summary>
        public Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync()
        {
            return api.GetRichMenuAliasListAsync(context.ChannelAccessToken);
        }

        /// <summary>
        /// 刪除 Rich Menu Alias。
        /// </summary>
        public bool DeleteRichMenuAlias(string aliasId)
        {
            return api.DeleteRichMenuAlias(context.ChannelAccessToken, aliasId);
        }

        /// <summary>
        /// 刪除 Rich Menu Alias。
        /// </summary>
        public Task<bool> DeleteRichMenuAliasAsync(string aliasId)
        {
            return api.DeleteRichMenuAliasAsync(context.ChannelAccessToken, aliasId);
        }
    }
}


using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Rich Menu 服務
    /// </summary>
    public interface IRichMenuService
    {
        RichMenuIdResponse CreateRichMenu(object richMenu);
        Task<RichMenuIdResponse> CreateRichMenuAsync(object richMenu);
        RichMenuResponse GetRichMenu(string richMenuId);
        Task<RichMenuResponse> GetRichMenuAsync(string richMenuId);
        bool DeleteRichMenu(string richMenuId);
        Task<bool> DeleteRichMenuAsync(string richMenuId);
        RichMenuListResponse GetRichMenuList();
        Task<RichMenuListResponse> GetRichMenuListAsync();
        bool UploadRichMenuImage(string richMenuId, string contentType, byte[] content);
        Task<bool> UploadRichMenuImageAsync(string richMenuId, string contentType, byte[] content);
        byte[] DownloadRichMenuImage(string richMenuId);
        Task<byte[]> DownloadRichMenuImageAsync(string richMenuId);
        bool SetDefaultRichMenu(string richMenuId);
        Task<bool> SetDefaultRichMenuAsync(string richMenuId);
        RichMenuIdResponse GetDefaultRichMenuId();
        Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync();
        bool CancelDefaultRichMenu();
        Task<bool> CancelDefaultRichMenuAsync();
        bool LinkUserRichMenu(string userId, string richMenuId);
        Task<bool> LinkUserRichMenuAsync(string userId, string richMenuId);
        bool UnlinkUserRichMenu(string userId);
        Task<bool> UnlinkUserRichMenuAsync(string userId);
        bool BulkLinkRichMenu(RichMenuBulkLinkRequest request);
        Task<bool> BulkLinkRichMenuAsync(RichMenuBulkLinkRequest request);
        bool BulkUnlinkRichMenu(RichMenuBulkUnlinkRequest request);
        Task<bool> BulkUnlinkRichMenuAsync(RichMenuBulkUnlinkRequest request);
        bool CreateRichMenuAlias(RichMenuAliasRequest request);
        Task<bool> CreateRichMenuAliasAsync(RichMenuAliasRequest request);
        bool UpdateRichMenuAlias(string aliasId, RichMenuAliasRequest request);
        Task<bool> UpdateRichMenuAliasAsync(string aliasId, RichMenuAliasRequest request);
        RichMenuAliasResponse GetRichMenuAlias(string aliasId);
        Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string aliasId);
        RichMenuAliasListResponse GetRichMenuAliasList();
        Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync();
        bool DeleteRichMenuAlias(string aliasId);
        Task<bool> DeleteRichMenuAliasAsync(string aliasId);
    }
}

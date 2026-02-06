using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// Rich Menu 服務
    /// </summary>
    public interface IRichMenuService
    {
        /// <summary>
        /// 建立 Rich Menu。
        /// </summary>
        RichMenuIdResponse CreateRichMenu(object richMenu);
        /// <summary>
        /// 非同步建立 Rich Menu。
        /// </summary>
        Task<RichMenuIdResponse> CreateRichMenuAsync(object richMenu);
        /// <summary>
        /// 依 Rich Menu ID 取得 Rich Menu 詳細資料。
        /// </summary>
        RichMenuResponse GetRichMenu(string richMenuId);
        /// <summary>
        /// 非同步依 Rich Menu ID 取得 Rich Menu 詳細資料。
        /// </summary>
        Task<RichMenuResponse> GetRichMenuAsync(string richMenuId);
        /// <summary>
        /// 刪除指定的 Rich Menu。
        /// </summary>
        bool DeleteRichMenu(string richMenuId);
        /// <summary>
        /// 非同步刪除指定的 Rich Menu。
        /// </summary>
        Task<bool> DeleteRichMenuAsync(string richMenuId);
        /// <summary>
        /// 取得 Rich Menu 清單。
        /// </summary>
        RichMenuListResponse GetRichMenuList();
        /// <summary>
        /// 非同步取得 Rich Menu 清單。
        /// </summary>
        Task<RichMenuListResponse> GetRichMenuListAsync();
        /// <summary>
        /// 上傳 Rich Menu 圖片。
        /// </summary>
        bool UploadRichMenuImage(string richMenuId, string contentType, byte[] content);
        /// <summary>
        /// 非同步上傳 Rich Menu 圖片。
        /// </summary>
        Task<bool> UploadRichMenuImageAsync(string richMenuId, string contentType, byte[] content);
        /// <summary>
        /// 下載 Rich Menu 圖片內容。
        /// </summary>
        byte[] DownloadRichMenuImage(string richMenuId);
        /// <summary>
        /// 非同步下載 Rich Menu 圖片內容。
        /// </summary>
        Task<byte[]> DownloadRichMenuImageAsync(string richMenuId);
        /// <summary>
        /// 設定預設 Rich Menu。
        /// </summary>
        bool SetDefaultRichMenu(string richMenuId);
        /// <summary>
        /// 非同步設定預設 Rich Menu。
        /// </summary>
        Task<bool> SetDefaultRichMenuAsync(string richMenuId);
        /// <summary>
        /// 取得目前預設 Rich Menu ID。
        /// </summary>
        RichMenuIdResponse GetDefaultRichMenuId();
        /// <summary>
        /// 非同步取得目前預設 Rich Menu ID。
        /// </summary>
        Task<RichMenuIdResponse> GetDefaultRichMenuIdAsync();
        /// <summary>
        /// 取消預設 Rich Menu。
        /// </summary>
        bool CancelDefaultRichMenu();
        /// <summary>
        /// 非同步取消預設 Rich Menu。
        /// </summary>
        Task<bool> CancelDefaultRichMenuAsync();
        /// <summary>
        /// 將指定使用者連結至 Rich Menu。
        /// </summary>
        bool LinkUserRichMenu(string userId, string richMenuId);
        /// <summary>
        /// 非同步將指定使用者連結至 Rich Menu。
        /// </summary>
        Task<bool> LinkUserRichMenuAsync(string userId, string richMenuId);
        /// <summary>
        /// 解除指定使用者的 Rich Menu 連結。
        /// </summary>
        bool UnlinkUserRichMenu(string userId);
        /// <summary>
        /// 非同步解除指定使用者的 Rich Menu 連結。
        /// </summary>
        Task<bool> UnlinkUserRichMenuAsync(string userId);
        /// <summary>
        /// 批次將多位使用者連結至 Rich Menu。
        /// </summary>
        bool BulkLinkRichMenu(RichMenuBulkLinkRequest request);
        /// <summary>
        /// 非同步批次將多位使用者連結至 Rich Menu。
        /// </summary>
        Task<bool> BulkLinkRichMenuAsync(RichMenuBulkLinkRequest request);
        /// <summary>
        /// 批次解除多位使用者的 Rich Menu 連結。
        /// </summary>
        bool BulkUnlinkRichMenu(RichMenuBulkUnlinkRequest request);
        /// <summary>
        /// 非同步批次解除多位使用者的 Rich Menu 連結。
        /// </summary>
        Task<bool> BulkUnlinkRichMenuAsync(RichMenuBulkUnlinkRequest request);
        /// <summary>
        /// 建立 Rich Menu Alias。
        /// </summary>
        bool CreateRichMenuAlias(RichMenuAliasRequest request);
        /// <summary>
        /// 非同步建立 Rich Menu Alias。
        /// </summary>
        Task<bool> CreateRichMenuAliasAsync(RichMenuAliasRequest request);
        /// <summary>
        /// 更新指定的 Rich Menu Alias。
        /// </summary>
        bool UpdateRichMenuAlias(string aliasId, RichMenuAliasRequest request);
        /// <summary>
        /// 非同步更新指定的 Rich Menu Alias。
        /// </summary>
        Task<bool> UpdateRichMenuAliasAsync(string aliasId, RichMenuAliasRequest request);
        /// <summary>
        /// 依 Alias ID 取得 Rich Menu Alias。
        /// </summary>
        RichMenuAliasResponse GetRichMenuAlias(string aliasId);
        /// <summary>
        /// 非同步依 Alias ID 取得 Rich Menu Alias。
        /// </summary>
        Task<RichMenuAliasResponse> GetRichMenuAliasAsync(string aliasId);
        /// <summary>
        /// 取得 Rich Menu Alias 清單。
        /// </summary>
        RichMenuAliasListResponse GetRichMenuAliasList();
        /// <summary>
        /// 非同步取得 Rich Menu Alias 清單。
        /// </summary>
        Task<RichMenuAliasListResponse> GetRichMenuAliasListAsync();
        /// <summary>
        /// 刪除指定的 Rich Menu Alias。
        /// </summary>
        bool DeleteRichMenuAlias(string aliasId);
        /// <summary>
        /// 非同步刪除指定的 Rich Menu Alias。
        /// </summary>
        Task<bool> DeleteRichMenuAliasAsync(string aliasId);
    }
}


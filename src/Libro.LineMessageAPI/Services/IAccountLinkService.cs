using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供 Account Link Token 相關服務。
    /// </summary>
    public interface IAccountLinkService
    {
        /// <summary>
        /// 簽發 Account Link Token。
        /// </summary>
        /// <param name="userId">使用者 ID。</param>
        /// <returns>Account Link Token 回應內容。</returns>
        LinkTokenResponse IssueLinkToken(string userId);
        /// <summary>
        /// 簽發 Account Link Token。
        /// </summary>
        /// <param name="userId">使用者 ID。</param>
        /// <returns>Account Link Token 回應內容。</returns>
        Task<LinkTokenResponse> IssueLinkTokenAsync(string userId);
    }
}


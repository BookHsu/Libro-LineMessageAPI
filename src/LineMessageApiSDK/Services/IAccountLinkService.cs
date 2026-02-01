using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Account Link 服務
    /// </summary>
    public interface IAccountLinkService
    {
        LinkTokenResponse IssueLinkToken(string userId);
        Task<LinkTokenResponse> IssueLinkTokenAsync(string userId);
    }
}

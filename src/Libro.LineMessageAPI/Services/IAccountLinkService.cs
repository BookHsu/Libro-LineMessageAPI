using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
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


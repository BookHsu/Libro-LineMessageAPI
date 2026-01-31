using LineMessageApiSDK.Method;
using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Account Link 服務
    /// </summary>
    internal class AccountLinkService : IAccountLinkService
    {
        private readonly LineApiContext context;
        private readonly AccountLinkApi api;

        internal AccountLinkService(LineApiContext context)
        {
            this.context = context;
            api = new AccountLinkApi(context.Serializer, context.HttpClientProvider);
        }

        public LinkTokenResponse IssueLinkToken(string userId)
        {
            return api.IssueLinkToken(context.ChannelAccessToken, userId);
        }

        public Task<LinkTokenResponse> IssueLinkTokenAsync(string userId)
        {
            return api.IssueLinkTokenAsync(context.ChannelAccessToken, userId);
        }
    }
}

using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供 Account Link Token 相關服務。
    /// </summary>
    internal class AccountLinkService : IAccountLinkService
    {
        private readonly LineApiContext context;
        private readonly AccountLinkApi api;

        internal AccountLinkService(LineApiContext context)
        {
            this.context = context;
            api = new AccountLinkApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <summary>
        /// 簽發 Account Link Token。
        /// </summary>
        /// <param name="userId">使用者 ID。</param>
        /// <returns>Account Link Token 回應內容。</returns>
        public LinkTokenResponse IssueLinkToken(string userId)
        {
            return api.IssueLinkToken(context.ChannelAccessToken, userId);
        }

        /// <summary>
        /// 簽發 Account Link Token。
        /// </summary>
        /// <param name="userId">使用者 ID。</param>
        /// <returns>Account Link Token 回應內容。</returns>
        public Task<LinkTokenResponse> IssueLinkTokenAsync(string userId)
        {
            return api.IssueLinkTokenAsync(context.ChannelAccessToken, userId);
        }
    }
}


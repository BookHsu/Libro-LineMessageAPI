using LineMessageApiSDK.Method;
using LineMessageApiSDK.SendMessage;
using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Broadcast / Narrowcast 服務
    /// </summary>
    internal class BroadcastService : IBroadcastService
    {
        private readonly LineApiContext context;
        private readonly BroadcastApi api;

        /// <summary>
        /// 建立 Broadcast 服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal BroadcastService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClientProvider
            this.context = context;
            // 建立 Broadcast API
            api = new BroadcastApi(context.Serializer, context.HttpClientProvider);
        }

        /// <inheritdoc />
        public bool SendBroadcast(BroadcastMessage message)
        {
            return api.SendBroadcast(context.ChannelAccessToken, message);
        }

        /// <inheritdoc />
        public Task<bool> SendBroadcastAsync(BroadcastMessage message)
        {
            return api.SendBroadcastAsync(context.ChannelAccessToken, message);
        }

        /// <inheritdoc />
        public bool SendNarrowcast(NarrowcastMessage message)
        {
            return api.SendNarrowcast(context.ChannelAccessToken, message);
        }

        /// <inheritdoc />
        public Task<bool> SendNarrowcastAsync(NarrowcastMessage message)
        {
            return api.SendNarrowcastAsync(context.ChannelAccessToken, message);
        }

        /// <inheritdoc />
        public NarrowcastProgressResponse GetNarrowcastProgress(string requestId)
        {
            return api.GetNarrowcastProgress(context.ChannelAccessToken, requestId);
        }

        /// <inheritdoc />
        public Task<NarrowcastProgressResponse> GetNarrowcastProgressAsync(string requestId)
        {
            return api.GetNarrowcastProgressAsync(context.ChannelAccessToken, requestId);
        }
    }
}

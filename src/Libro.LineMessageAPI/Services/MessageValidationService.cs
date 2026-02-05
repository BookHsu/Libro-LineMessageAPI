using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.SendMessage;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 訊息驗證服務
    /// </summary>
    internal class MessageValidationService : IMessageValidationService
    {
        private readonly LineApiContext context;
        private readonly MessageValidationApi api;

        /// <summary>
        /// 建立訊息驗證服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal MessageValidationService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClientProvider
            this.context = context;
            // 建立訊息驗證 API
            api = new MessageValidationApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <inheritdoc />
        public bool ValidateReply(ReplyMessage message)
        {
            return api.Validate(context.ChannelAccessToken, "reply", message);
        }

        /// <inheritdoc />
        public Task<bool> ValidateReplyAsync(ReplyMessage message)
        {
            return api.ValidateAsync(context.ChannelAccessToken, "reply", message);
        }

        /// <inheritdoc />
        public bool ValidatePush(PushMessage message)
        {
            return api.Validate(context.ChannelAccessToken, "push", message);
        }

        /// <inheritdoc />
        public Task<bool> ValidatePushAsync(PushMessage message)
        {
            return api.ValidateAsync(context.ChannelAccessToken, "push", message);
        }

        /// <inheritdoc />
        public bool ValidateMulticast(MulticastMessage message)
        {
            return api.Validate(context.ChannelAccessToken, "multicast", message);
        }

        /// <inheritdoc />
        public Task<bool> ValidateMulticastAsync(MulticastMessage message)
        {
            return api.ValidateAsync(context.ChannelAccessToken, "multicast", message);
        }

        /// <inheritdoc />
        public bool ValidateBroadcast(BroadcastMessage message)
        {
            return api.Validate(context.ChannelAccessToken, "broadcast", message);
        }

        /// <inheritdoc />
        public Task<bool> ValidateBroadcastAsync(BroadcastMessage message)
        {
            return api.ValidateAsync(context.ChannelAccessToken, "broadcast", message);
        }

        /// <inheritdoc />
        public bool ValidateNarrowcast(NarrowcastMessage message)
        {
            return api.Validate(context.ChannelAccessToken, "narrowcast", message);
        }

        /// <inheritdoc />
        public Task<bool> ValidateNarrowcastAsync(NarrowcastMessage message)
        {
            return api.ValidateAsync(context.ChannelAccessToken, "narrowcast", message);
        }
    }
}


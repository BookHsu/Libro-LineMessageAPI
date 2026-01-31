using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.Method;
using LineMessageApiSDK.SendMessage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// 訊息服務
    /// </summary>
    internal class MessageService : IMessageService
    {
        private readonly LineApiContext context;
        private readonly MessageContentApi messageContentApi;
        private readonly MessageSendApi messageSendApi;

        /// <summary>
        /// 建立訊息服務
        /// </summary>
        /// <param name="context">API Context</param>
        internal MessageService(LineApiContext context)
        {
            // 保存 Context 以使用 Token/序列化器/HttpClient
            this.context = context;
            // 建立訊息內容 API（支援 HttpClient DI）
            messageContentApi = new MessageContentApi(context.HttpClientProvider);
            // 建立訊息發送 API（支援 HttpClient DI）
            messageSendApi = new MessageSendApi(context.Serializer, context.HttpClientProvider);
        }

        /// <inheritdoc />
        public byte[] GetMessageContent(string messageId)
        {
            // 取得使用者上傳的檔案
            return messageContentApi.GetUserUploadData(context.ChannelAccessToken, messageId);
        }

        /// <inheritdoc />
        public Task<byte[]> GetMessageContentAsync(string messageId)
        {
            // 取得使用者上傳的檔案（非同步）
            return messageContentApi.GetUserUploadDataAsync(context.ChannelAccessToken, messageId);
        }

        /// <inheritdoc />
        public string SendReplyMessage(string replyToken, params Message[] message)
        {
            // 組合 Reply 訊息
            ReplyMessage model = new ReplyMessage(replyToken, message);
            return messageSendApi.SendMessageAction(context.ChannelAccessToken, PostMessageType.Reply, model);
        }

        /// <inheritdoc />
        public Task<string> SendReplyMessageAsync(string replyToken, params Message[] message)
        {
            // 組合 Reply 訊息（非同步）
            ReplyMessage model = new ReplyMessage(replyToken, message);
            return messageSendApi.SendMessageActionAsync(context.ChannelAccessToken, PostMessageType.Reply, model);
        }

        /// <inheritdoc />
        public string SendPushMessage(string toId, params Message[] message)
        {
            // 組合 Push 訊息
            PushMessage model = new PushMessage(toId, message);
            return messageSendApi.SendMessageAction(context.ChannelAccessToken, PostMessageType.Push, model);
        }

        /// <inheritdoc />
        public Task<string> SendPushMessageAsync(string toId, params Message[] message)
        {
            // 組合 Push 訊息（非同步）
            PushMessage model = new PushMessage(toId, message);
            return messageSendApi.SendMessageActionAsync(context.ChannelAccessToken, PostMessageType.Push, model);
        }

        /// <inheritdoc />
        public string SendMulticastMessage(List<string> toIds, params Message[] message)
        {
            // 組合 Multicast 訊息
            MulticastMessage model = new MulticastMessage
            {
                to = toIds
            };
            model.messages.AddRange(message);
            return messageSendApi.SendMessageAction(context.ChannelAccessToken, PostMessageType.Multicast, model);
        }

        /// <inheritdoc />
        public Task<string> SendMulticastMessageAsync(List<string> toIds, params Message[] message)
        {
            // 組合 Multicast 訊息（非同步）
            MulticastMessage model = new MulticastMessage
            {
                to = toIds
            };
            model.messages.AddRange(message);
            return messageSendApi.SendMessageActionAsync(context.ChannelAccessToken, PostMessageType.Multicast, model);
        }
    }
}

using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.LineReceivedObject;
using Libro.LineMessageApi.SendMessage;
using Libro.LineMessageApi.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 訊息發送 API
    /// </summary>
    internal class MessageSendApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 建立訊息發送 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal MessageSendApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 建立訊息發送 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal MessageSendApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        /// <summary>
        /// 根據傳入種類發送訊息
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="type">發送種類</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        internal string SendMessageAction(string channelAccessToken, PostMessageType type, SendLineMessage message)
        {
            string strUrl = BuildMessageUrl(type);

            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                var payload = NormalizeMessagePayload(message);
                var sJosn = serializer.Serialize(payload);
                using var content = new StringContent(sJosn, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var response = adapter.Post(strUrl, content);
                var s = response.Content.ReadAsStringSync();
                if (s == "{}")
                {
                    return string.Empty;
                }
                else
                {
                    LineErrorResponse err = serializer.Deserialize<LineErrorResponse>(s);
                    throw new Exception($"{BuildErrorMessage(err)} | request={sJosn}");
                }
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 根據傳入種類發送訊息（非同步）
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="type">發送種類</param>
        /// <param name="message">訊息內容</param>
        /// <returns>結果字串</returns>
        internal async Task<string> SendMessageActionAsync(string channelAccessToken, PostMessageType type, SendLineMessage message)
        {
            string strUrl = BuildMessageUrl(type);

            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                var payload = NormalizeMessagePayload(message);
                var sJosn = serializer.Serialize(payload);
                using var content = new StringContent(sJosn, Encoding.UTF8, "application/json");
                using var response = await client.PostAsync(strUrl, content).ConfigureAwait(false);
                var s = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (s == "{}")
                {
                    return string.Empty;
                }
                else
                {
                    LineErrorResponse err = serializer.Deserialize<LineErrorResponse>(s);
                    throw new Exception($"{BuildErrorMessage(err)} | request={sJosn}");
                }
            }
            finally
            {
                // 若為自建 HttpClient，才需要釋放
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        private static string BuildMessageUrl(PostMessageType type)
        {
            switch (type)
            {
                case PostMessageType.Reply:
                    return LineApiEndpoints.BuildReplyMessage();
                case PostMessageType.Push:
                    return LineApiEndpoints.BuildPushMessage();
                case PostMessageType.Multicast:
                    return LineApiEndpoints.BuildMulticastMessage();
            }

            return string.Empty;
        }

        private static object NormalizeMessagePayload(SendLineMessage message)
        {
            if (message == null)
            {
                return null;
            }

            IEnumerable<object> messageList = message.messages?.Cast<object>();

            switch (message)
            {
                case ReplyMessage reply:
                    return new
                    {
                        replyToken = reply.replyToken,
                        messages = messageList
                    };
                case PushMessage push:
                    return new
                    {
                        to = push.to,
                        messages = messageList
                    };
                case MulticastMessage multicast:
                    return new
                    {
                        to = multicast.to,
                        messages = messageList
                    };
                case BroadcastMessage broadcast:
                    return new
                    {
                        messages = messageList,
                        notificationDisabled = broadcast.notificationDisabled
                    };
                case NarrowcastMessage narrowcast:
                    return new
                    {
                        recipient = narrowcast.recipient,
                        messages = messageList,
                        notificationDisabled = narrowcast.notificationDisabled
                    };
                default:
                    return message;
            }
        }

        private static string BuildErrorMessage(LineErrorResponse err)
        {
            if (err == null)
            {
                return "Unknown error.";
            }

            if (err.details == null || err.details.Count == 0)
            {
                return err.message ?? "Unknown error.";
            }

            var detailMessages = string.Join("; ", err.details.Select(d =>
                $"{d.property}: {d.message}"));

            return $"{err.message} ({detailMessages})";
        }
    }
}


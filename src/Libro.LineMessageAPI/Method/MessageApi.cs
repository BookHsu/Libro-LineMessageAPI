using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.LineReceivedObject;
using Libro.LineMessageApi.SendMessage;
using Libro.LineMessageApi.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 訊息 API 入口（對外維持原有介面）
    /// </summary>
    internal class MessageApi
    {
        private readonly MessageContentApi messageContentApi;
        private readonly ProfileApi profileApi;
        private readonly GroupApi groupApi;
        private readonly MessageSendApi messageSendApi;

        internal MessageApi(IJsonSerializer serializer, HttpClient httpClient = null, IHttpClientProvider httpClientProvider = null)
        {
            // 建立 HttpClient 提供者（若外部未提供則使用預設）
            IHttpClientProvider provider = httpClientProvider ?? new DefaultHttpClientProvider(httpClient);
            // 將責任拆分為多個模組 API
            messageContentApi = new MessageContentApi(provider);
            profileApi = new ProfileApi(serializer, provider);
            groupApi = new GroupApi(provider);
            messageSendApi = new MessageSendApi(serializer, provider);
        }

        /// <summary>取得使用者傳送的圖片、影片、聲音、檔案</summary>
        /// <param name="channelAccessToken"></param> 
        /// <param name="messageId"></param>
        /// <returns></returns>
        internal byte[] GetUserUploadData(string channelAccessToken, string messageId)
        {
            // 透過訊息內容 API 取得檔案
            return messageContentApi.GetUserUploadData(channelAccessToken, messageId);
        }

        /// <summary>取得使用者傳送的圖片、影片、聲音、檔案</summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="messageId"></param>
        /// <returns></returns>
        internal async Task<byte[]> GetUserUploadDataAsync(string channelAccessToken, string messageId)
        {
            // 透過訊息內容 API 取得檔案（非同步）
            return await messageContentApi.GetUserUploadDataAsync(channelAccessToken, messageId);
        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal UserProfile GetUserProfile(string channelAccessToken, string userId)
        {
            // 透過檔案 API 取得使用者檔案
            return profileApi.GetUserProfile(channelAccessToken, userId);
        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal async Task<UserProfile> GetUserProfileAsync(string channelAccessToken, string userId)
        {
            // 透過檔案 API 取得使用者檔案（非同步）
            return await profileApi.GetUserProfileAsync(channelAccessToken, userId);
        }

        /// <summary>
        /// 取得群組或對話內指定成員的使用者檔案
        /// </summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal UserProfile GetGroupMemberProfile(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            // 透過檔案 API 取得成員檔案
            return profileApi.GetGroupMemberProfile(channelAccessToken, userId, groupId, type);
        }

        /// <summary>
        /// 取得群組內指定成員的使用者檔案
        /// </summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal async Task<UserProfile> GetGroupMemberProfileAsync(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            // 透過檔案 API 取得成員檔案（非同步）
            return await profileApi.GetGroupMemberProfileAsync(channelAccessToken, userId, groupId, type);
        }

        /// <summary>離開群組或對話</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal bool LeaveRoomOrGroup(string channelAccessToken, string id, SourceType type)
        {
            // 透過群組 API 離開群組或多人對話
            return groupApi.LeaveRoomOrGroup(channelAccessToken, id, type);
        }

        /// <summary>離開群組或對話</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal async Task<bool> LeaveRoomOrGroupAsync(string channelAccessToken, string id, SourceType type)
        {
            // 透過群組 API 離開群組或多人對話（非同步）
            return await groupApi.LeaveRoomOrGroupAsync(channelAccessToken, id, type);
        }

        /// <summary>根據傳入種類發送訊息</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal string SendMessageAction(string channelAccessToken, PostMessageType type, SendLineMessage message)
        {
            // 透過訊息發送 API 發送訊息
            return messageSendApi.SendMessageAction(channelAccessToken, type, message);
        }

        /// <summary>根據傳入種類發送訊息</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal async Task<string> SendMessageActionAsync(string channelAccessToken, PostMessageType type, SendLineMessage message)
        {
            // 透過訊息發送 API 發送訊息（非同步）
            return await messageSendApi.SendMessageActionAsync(channelAccessToken, type, message);
        }
    }
}


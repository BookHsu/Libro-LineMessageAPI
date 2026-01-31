using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.Method;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.SendMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LineMessageApiSDK
{
    /// <summary>主動調用Line物件</summary>
    public class LineChannel
    {
        private readonly MessageApi messageApi;

        /// <summary>驗證是否為Line伺服器傳來的訊息</summary>
        /// <param name="request">Request</param> 
        /// <param name="ChannelSecret">ChannelSecret</param>
        /// <returns></returns>
        public static bool VaridateSignature(HttpRequestMessage request, string ChannelSecret)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ChannelSecret));
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Content.ReadAsStringAsync().Result));
            var contentHash = Convert.ToBase64String(computeHash);
            var headerHash = request.Headers.GetValues("X-Line-Signature").First();
            return contentHash == headerHash;
        }



        /// <summary>傳入api中的ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken)
            : this(ChannelAccessToken, new SystemTextJsonSerializer(), null)
        {
        }

        /// <summary>傳入api中的ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken, IJsonSerializer serializer)
            : this(ChannelAccessToken, serializer, null)
        {
        }

        /// <summary>傳入api中的ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken, IJsonSerializer serializer, HttpClient httpClient)
        {
            channelAccessToken = ChannelAccessToken;
            // 使用外部注入的序列化器與 HttpClient（未提供則使用預設）
            messageApi = new MessageApi(serializer, httpClient);
        }

        /// <summary>channelAccessToken</summary>
        public string channelAccessToken { get; set; }

        /// <summary>
        /// 離開對話或群組
        /// </summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool LeaveRoomOrGroup(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用 SourceType = User");
            }
            else
            {
                // 透過內部 API 執行離開群組或多人對話
                return messageApi.LeaveRoomOrGroup(this.channelAccessToken, sourceId, type);
            }
        }
        /// <summary>
        /// 離開對話或群組
        /// </summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<bool> LeaveRoomOrGroupAsync(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用 SourceType = User");
            }
            else
            {
                // 透過內部 API 執行離開群組或多人對話
                return messageApi.LeaveRoomOrGroupAsync(this.channelAccessToken, sourceId, type);
            }

        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserProfile GetUserProfile(string userId)
        {
            // 取得使用者檔案
            return messageApi.GetUserProfile(this.channelAccessToken, userId);
        }
        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Task<UserProfile> GetUserProfileAsync(string userId)
        {
            // 取得使用者檔案（非同步）
            return messageApi.GetUserProfileAsync(this.channelAccessToken, userId);
        }
        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public List<UserProfile> GetUserProfiles(List<string> userIds)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userId in userIds)
            {
                oModel.Add(messageApi.GetUserProfile(this.channelAccessToken, userId));
            }
            return oModel;
        }
        /// <summary>
        /// 取得群組內指定使用者資料
        /// 
        /// </summary>
        /// <param name="userid">指定使用者Id</param>
        ///<param name="GroupidOrRoomId">群組或對話ID</param>
        ///<param name="type">群組或對話</param>
        /// <returns></returns>
        public UserProfile GetGroupMemberProfile(string userId, string groupIdOrRoomId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用Source = User");
            }
            // 取得群組或多人對話內成員檔案
            return messageApi.GetGroupMemberProfile(this.channelAccessToken, userId, groupIdOrRoomId, type);
        }
        /// <summary>
        /// 取得群組內指定使用者資料
        /// </summary>
        /// <param name="userid">指定使用者Id</param>
        ///<param name="GroupidOrRoomId">群組或對話ID</param>
        ///<param name="type">群組或對話</param>
        /// <returns></returns>
        public Task<UserProfile> GetGroupMemberProfileAsync(string userId, string groupIdOrRoomId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用Source = User");
            }
            // 取得群組或多人對話內成員檔案（非同步）
            return messageApi.GetGroupMemberProfileAsync(this.channelAccessToken, userId, groupIdOrRoomId, type);
        }

        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public async Task<List<UserProfile>> GetUserProfilesAsync(List<string> userIds)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userId in userIds)
            {
                oModel.Add(await messageApi.GetUserProfileAsync(this.channelAccessToken, userId));
            }
            return oModel;
        }



        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public byte[] GetUserUploadContent(string messageId)
        {
            // 取得使用者上傳的檔案
            return messageApi.GetUserUploadData(this.channelAccessToken, messageId);
        }

        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public Task<byte[]> GetUserUploadContentAsync(string messageId)
        {
            // 取得使用者上傳的檔案（非同步）
            return messageApi.GetUserUploadDataAsync(this.channelAccessToken, messageId);
        }



        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMulticastMessage(List<string> toIds, params Message[] message)
        {
            MulticastMessage oModel = new MulticastMessage()
            {
                to = toIds
            };
            oModel.messages.AddRange(message);

            return messageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Multicast, oModel);
        }

        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendMulticastMessageAsync(List<string> toIds, params Message[] message)
        {
            MulticastMessage oModel = new MulticastMessage()
            {
                to = toIds
            };
            oModel.messages.AddRange(message);

            return messageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Multicast, oModel);
        }


        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public string SendPushMessage(string ToId, params Message[] message)
        {
            PushMessage oModel = new PushMessage(ToId, message);

            return messageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Push, oModel);
        }



        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public Task<string> SendPushMessageAsync(string ToId, params Message[] message)
        {
            PushMessage oModel = new PushMessage(ToId, message);

            return messageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Push, oModel);
        }


        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendReplyMessage(string replyToken, params Message[] message)
        {
            ReplyMessage oModel = new ReplyMessage(replyToken, message);
            return messageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Reply, oModel);
        }


        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendReplyMessageAsync(string replyToken, params Message[] message)
        {
            ReplyMessage oModel = new ReplyMessage(replyToken, message);
            return messageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Reply, oModel);
        }

        /// <summary>離開對話或群組（已過時，請改用 LeaveRoomOrGroup）</summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 LeaveRoomOrGroup。")]
        public bool Leave_Room_Or_Group(string sourceId, SourceType type)
        {
            // 保留舊版方法以避免破壞性變更
            return LeaveRoomOrGroup(sourceId, type);
        }

        /// <summary>離開對話或群組（已過時，請改用 LeaveRoomOrGroupAsync）</summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 LeaveRoomOrGroupAsync。")]
        public Task<bool> Leave_Room_Or_GroupAsync(string sourceId, SourceType type)
        {
            // 保留舊版方法以避免破壞性變更
            return LeaveRoomOrGroupAsync(sourceId, type);
        }

        /// <summary>取得使用者檔案（已過時，請改用 GetUserProfile）</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserProfile。")]
        public UserProfile Get_User_Data(string userid)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserProfile(userid);
        }

        /// <summary>取得使用者檔案（已過時，請改用 GetUserProfileAsync）</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserProfileAsync。")]
        public Task<UserProfile> Get_User_DataAsync(string userid)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserProfileAsync(userid);
        }

        /// <summary>取得大量使用者檔案（已過時，請改用 GetUserProfiles）</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserProfiles。")]
        public List<UserProfile> Get_User_datas(List<string> userids)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserProfiles(userids);
        }

        /// <summary>取得大量使用者檔案（已過時，請改用 GetUserProfilesAsync）</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserProfilesAsync。")]
        public Task<List<UserProfile>> Get_User_datasAsync(List<string> userids)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserProfilesAsync(userids);
        }

        /// <summary>取得群組或對話內指定使用者資料（已過時，請改用 GetGroupMemberProfile）</summary>
        /// <param name="userid">指定使用者Id</param>
        ///<param name="GroupidOrRoomId">群組或對話ID</param>
        ///<param name="type">群組或對話</param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetGroupMemberProfile。")]
        public UserProfile Get_Group_UserProfile(string userid, string GroupidOrRoomId, SourceType type)
        {
            // 保留舊版方法以避免破壞性變更
            return GetGroupMemberProfile(userid, GroupidOrRoomId, type);
        }

        /// <summary>取得群組或對話內指定使用者資料（已過時，請改用 GetGroupMemberProfileAsync）</summary>
        /// <param name="userid">指定使用者Id</param>
        ///<param name="GroupidOrRoomId">群組或對話ID</param>
        ///<param name="type">群組或對話</param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetGroupMemberProfileAsync。")]
        public Task<UserProfile> Get_Group_UserProfileAsync(string userid, string GroupidOrRoomId, SourceType type)
        {
            // 保留舊版方法以避免破壞性變更
            return GetGroupMemberProfileAsync(userid, GroupidOrRoomId, type);
        }

        /// <summary>取得使用者上傳的檔案（已過時，請改用 GetUserUploadContent）</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserUploadContent。")]
        public byte[] Get_User_Upload_To_Bot(string message_id)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserUploadContent(message_id);
        }

        /// <summary>取得使用者上傳的檔案（已過時，請改用 GetUserUploadContentAsync）</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 GetUserUploadContentAsync。")]
        public Task<byte[]> Get_User_Upload_To_BotAsync(string message_id)
        {
            // 保留舊版方法以避免破壞性變更
            return GetUserUploadContentAsync(message_id);
        }

        /// <summary>傳送訊息給多位使用者（已過時，請改用 SendMulticastMessage）</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 SendMulticastMessage。")]
        public string SendMuticastMessage(List<string> ToId, params Message[] message)
        {
            // 保留舊版方法以避免破壞性變更
            return SendMulticastMessage(ToId, message);
        }

        /// <summary>傳送訊息給多位使用者（已過時，請改用 SendMulticastMessageAsync）</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Obsolete("此方法已過時，請改用 SendMulticastMessageAsync。")]
        public Task<string> SendMuticastMessageAsync(List<string> ToId, params Message[] message)
        {
            // 保留舊版方法以避免破壞性變更
            return SendMulticastMessageAsync(ToId, message);
        }


    }
}

using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.Method;
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
        {
            channelAccessToken = ChannelAccessToken;
        }

        /// <summary>channelAccessToken</summary>
        public string channelAccessToken { get; set; }

        /// <summary>
        /// 離開對話或群組
        /// </summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Leave_Room_Or_Group(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用 SourceType = User");
            }
            else
            {
                return MessageApi.Leave_Room_Group(this.channelAccessToken, sourceId, type);
            }
        }
        /// <summary>
        /// 離開對話或群組
        /// </summary>
        /// <param name="sourceId">欲離開的對話或群組ID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Task<bool> Leave_Room_Or_GroupAsync(string sourceId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用 SourceType = User");
            }
            else
            {
                return MessageApi.Leave_Room_GroupAsync(this.channelAccessToken, sourceId, type);
            }

        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserProfile Get_User_Data(string userid)
        {
            return MessageApi.GetUserProfile(this.channelAccessToken, userid);
        }
        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Task<UserProfile> Get_User_DataAsync(string userid)
        {
            return MessageApi.GetUserProfileAsync(this.channelAccessToken, userid);
        }
        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public List<UserProfile> Get_User_datas(List<string> userids)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userid in userids)
            {
                oModel.Add(MessageApi.GetUserProfile(this.channelAccessToken, userid));
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
        public UserProfile Get_Group_UserProfile(string userid, string GroupidOrRoomId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用Source = User");
            }
            return MessageApi.Get_Group_UserProfile(this.channelAccessToken, userid, GroupidOrRoomId, type);
        }
        /// <summary>
        /// 取得群組內指定使用者資料
        /// </summary>
        /// <param name="userid">指定使用者Id</param>
        ///<param name="GroupidOrRoomId">群組或對話ID</param>
        ///<param name="type">群組或對話</param>
        /// <returns></returns>
        public Task<UserProfile> Get_Group_UserProfileAsync(string userid, string GroupidOrRoomId, SourceType type)
        {
            if (type == SourceType.user)
            {
                throw new NotSupportedException("無法使用Source = User");
            }
            return MessageApi.Get_Group_UserProfileAsync(this.channelAccessToken, userid, GroupidOrRoomId, type);
        }

        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public async Task<List<UserProfile>> Get_User_datasAsync(List<string> userids)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userid in userids)
            {
                oModel.Add(await MessageApi.GetUserProfileAsync(this.channelAccessToken, userid));
            }
            return oModel;
        }



        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public byte[] Get_User_Upload_To_Bot(string message_id)
        {
            return MessageApi.Get_User_Upload_Data(this.channelAccessToken, message_id);
        }

        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public Task<byte[]> Get_User_Upload_To_BotAsync(string message_id)
        {
            return MessageApi.Get_User_Upload_DataAsync(this.channelAccessToken, message_id);
        }



        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMuticastMessage(List<string> ToId, params Message[] message)
        {
            MulticastMessage oModel = new MulticastMessage()
            {
                to = ToId
            };
            oModel.messages.AddRange(message);

            return MessageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Multicast, oModel);
        }

        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendMuticastMessageAsync(List<string> ToId, params Message[] message)
        {
            MulticastMessage oModel = new MulticastMessage()
            {
                to = ToId
            };
            oModel.messages.AddRange(message);

            return MessageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Multicast, oModel);
        }


        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public string SendPushMessage(string ToId, params Message[] message)
        {
            PushMessage oModel = new PushMessage(ToId, message);

            return MessageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Push, oModel);
        }



        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public Task<string> SendPushMessageAsync(string ToId, params Message[] message)
        {
            PushMessage oModel = new PushMessage(ToId, message);

            return MessageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Push, oModel);
        }


        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendReplyMessage(string replyToken, params Message[] message)
        {
            ReplyMessage oModel = new ReplyMessage(replyToken, message);
            return MessageApi.SendMessageAction(this.channelAccessToken, PostMessageType.Reply, oModel);
        }


        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendReplyMessageAsync(string replyToken, params Message[] message)
        {
            ReplyMessage oModel = new ReplyMessage(replyToken, message);
            return MessageApi.SendMessageActionAsync(this.channelAccessToken, PostMessageType.Reply, oModel);
        }


    }
}
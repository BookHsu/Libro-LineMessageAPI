using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.SendMessage;
using LineMessageApiSDK.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    internal class MessageApi
    {
        private readonly IJsonSerializer serializer;

        internal MessageApi(IJsonSerializer serializer)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        /// <summary>取得使用者傳送的 圖片 影片 聲音 檔案</summary>
        /// <param name="ChannelAccessToken"></param> 
        /// <param name="message_id"></param>
        /// <returns></returns>
        internal byte[] Get_User_Upload_Data(string ChannelAccessToken, string message_id)
        {
            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(message_id);
                var result = client.GetByteArrayAsync(strUrl).Result;
                return result;
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>取得使用者傳送的 圖片 影片 聲音 檔案</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="message_id"></param>
        /// <returns></returns>
        internal async Task<byte[]> Get_User_Upload_DataAsync(string ChannelAccessToken, string message_id)
        {
            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                string strUrl = LineApiEndpoints.BuildMessageContent(message_id);
                var result = await client.GetByteArrayAsync(strUrl);
                return result;
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal UserProfile GetUserProfile(string channelAccessToken, string userid)
        {
            HttpClient client = GetClientDefault(channelAccessToken);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userid);
                var result = client.GetStringAsync(strUrl).Result;
                return serializer.Deserialize<UserProfile>(result);
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal async Task<UserProfile> GetUserProfileAsync(string channelAccessToken, string userid)
        {
            HttpClient client = GetClientDefault(channelAccessToken);
            try
            {
                string strUrl = LineApiEndpoints.BuildUserProfile(userid);
                var result = await client.GetStringAsync(strUrl);
                return serializer.Deserialize<UserProfile>(result);
            }
            finally
            {
                client.Dispose();
            }
        }
        /// <summary>
        /// 取得群組或對話內指定成員的使用者檔案
        /// </summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal UserProfile Get_Group_UserProfile(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            HttpClient client = GetClientDefault(channelAccessToken);
            try
            {

                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var result = client.GetStringAsync(strUrl).Result;
                return serializer.Deserialize<UserProfile>(result);
            }
            finally
            {
                client.Dispose();
            }
        }
        /// <summary>
        /// 取得群組內指定成員的使用者檔案
        /// </summary>
        /// <param name="channelAccessToken"></param>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        internal async Task<UserProfile> Get_Group_UserProfileAsync(string channelAccessToken, string userId, string groupId, SourceType type)
        {
            HttpClient client = GetClientDefault(channelAccessToken);
            try
            {

                string strUrl = LineApiEndpoints.BuildGroupMemberProfile(type, groupId, userId);
                var result = await client.GetStringAsync(strUrl);
                return serializer.Deserialize<UserProfile>(result);
            }
            finally
            {
                client.Dispose();
            }
        }


        /// <summary>離開群組或對話</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal bool Leave_Room_Group(string ChannelAccessToken, string id, SourceType type)
        {
            string strUrl = LineApiEndpoints.BuildLeaveGroupOrRoom(type, id);
            bool flag = false;
            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                var result = client.PostAsync(strUrl, new StringContent("")).Result;
                flag = result.IsSuccessStatusCode;
            }
            finally
            {
                client.Dispose();
            }
            return flag;
        }

        /// <summary>離開群組或對話</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal async Task<bool> Leave_Room_GroupAsync(string ChannelAccessToken, string id, SourceType type)
        {
            string strUrl = LineApiEndpoints.BuildLeaveGroupOrRoom(type, id);
            bool flag = false;
            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                var result = await client.PostAsync(strUrl, new StringContent(""));
                flag = result.IsSuccessStatusCode;
            }
            finally
            {
                client.Dispose();
            }
            return flag;
        }


        /// <summary>根據傳入種類發送訊息</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal string SendMessageAction(string ChannelAccessToken, PostMessageType type, SendLineMessage message)
        {
            string strUrl = string.Empty;
            switch (type)
            {
                case PostMessageType.Reply:
                    strUrl = LineApiEndpoints.BuildReplyMessage();
                    break;

                case PostMessageType.Push:
                    strUrl = LineApiEndpoints.BuildPushMessage();
                    break;

                case PostMessageType.Multicast:
                    strUrl = LineApiEndpoints.BuildMulticastMessage();
                    break;
            }

            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                var sJosn = serializer.Serialize(message);
                var content = new StringContent(sJosn, Encoding.UTF8, "application/json");
                var s = client.PostAsync(strUrl, content).Result.Content.ReadAsStringAsync().Result;
                if (s == "{}")
                {
                    return string.Empty;
                }
                else
                {
                    LineErrorResponse err = serializer.Deserialize<LineErrorResponse>(s);
                    throw new Exception(err.message);
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        /// <summary>根據傳入種類發送訊息</summary>
        /// <param name="ChannelAccessToken"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal async Task<string> SendMessageActionAsync(string ChannelAccessToken, PostMessageType type, SendLineMessage message)
        {
            string strUrl = string.Empty;
            switch (type)
            {
                case PostMessageType.Reply:
                    strUrl = LineApiEndpoints.BuildReplyMessage();
                    break;

                case PostMessageType.Push:
                    strUrl = LineApiEndpoints.BuildPushMessage();
                    break;

                case PostMessageType.Multicast:
                    strUrl = LineApiEndpoints.BuildMulticastMessage();
                    break;
            }

            HttpClient client = GetClientDefault(ChannelAccessToken);
            try
            {
                var sJosn = serializer.Serialize(message);
                var content = new StringContent(sJosn, Encoding.UTF8, "application/json");
                var s = await client.PostAsync(strUrl, content).Result.Content.ReadAsStringAsync();
                if (s == "{}")
                {
                    return string.Empty;
                }
                else
                {
                    LineErrorResponse err = serializer.Deserialize<LineErrorResponse>(s);
                    throw new Exception(err.message);
                }
            }
            finally
            {
                client.Dispose();
            }
        }


        private static HttpClient GetClientDefault(string ChannelAccessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ChannelAccessToken);
            return client;
        }
    }
}

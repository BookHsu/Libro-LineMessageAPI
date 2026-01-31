using LineMessageApiSDK.Http;
using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.Services;
using LineMessageApiSDK.SendMessage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LineMessageApiSDK
{
    /// <summary>主動調用 Line 物件</summary>
    public class LineChannel
    {
        private readonly IMessageService messageService;
        private readonly IProfileService profileService;
        private readonly IGroupService groupService;
        private readonly IWebhookService webhookService;
        private readonly IWebhookEndpointService webhookEndpointService;
        private readonly IBotService botService;
        private readonly IBroadcastService broadcastService;
        private readonly IMessageValidationService messageValidationService;
        private readonly IRichMenuService richMenuService;

        /// <summary>驗證是否為 Line 伺服器傳來的訊息</summary>
        /// <param name="request">Request</param> 
        /// <param name="ChannelSecret">ChannelSecret</param>
        /// <returns></returns>
        public static bool VaridateSignature(HttpRequestMessage request, string ChannelSecret)
        {
            // 交由 Webhook 服務執行驗證
            IWebhookService service = new WebhookService();
            return service.ValidateSignature(request, ChannelSecret);
        }

        /// <summary>取得 Webhook Endpoint</summary>
        /// <returns>Webhook Endpoint 設定</returns>
        public Types.WebhookEndpointResponse GetWebhookEndpoint()
        {
            // 透過 Webhook Endpoint 服務取得設定
            return webhookEndpointService.GetWebhookEndpoint();
        }

        /// <summary>取得 Webhook Endpoint（非同步）</summary>
        /// <returns>Webhook Endpoint 設定</returns>
        public Task<Types.WebhookEndpointResponse> GetWebhookEndpointAsync()
        {
            // 透過 Webhook Endpoint 服務取得設定（非同步）
            return webhookEndpointService.GetWebhookEndpointAsync();
        }

        /// <summary>更新 Webhook Endpoint</summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        public bool SetWebhookEndpoint(Types.WebhookEndpointRequest request)
        {
            // 透過 Webhook Endpoint 服務更新設定
            return webhookEndpointService.SetWebhookEndpoint(request);
        }

        /// <summary>更新 Webhook Endpoint（非同步）</summary>
        /// <param name="request">Webhook 設定</param>
        /// <returns>是否成功</returns>
        public Task<bool> SetWebhookEndpointAsync(Types.WebhookEndpointRequest request)
        {
            // 透過 Webhook Endpoint 服務更新設定（非同步）
            return webhookEndpointService.SetWebhookEndpointAsync(request);
        }

        /// <summary>測試 Webhook Endpoint</summary>
        /// <returns>測試結果</returns>
        public Types.WebhookTestResponse TestWebhookEndpoint()
        {
            // 透過 Webhook Endpoint 服務測試設定
            return webhookEndpointService.TestWebhookEndpoint();
        }

        /// <summary>測試 Webhook Endpoint（非同步）</summary>
        /// <returns>測試結果</returns>
        public Task<Types.WebhookTestResponse> TestWebhookEndpointAsync()
        {
            // 透過 Webhook Endpoint 服務測試設定（非同步）
            return webhookEndpointService.TestWebhookEndpointAsync();
        }

        /// <summary>取得 Bot 基本資訊</summary>
        /// <returns>Bot 資訊</returns>
        public Types.BotInfo GetBotInfo()
        {
            // 透過 Bot 服務取得基本資訊
            return botService.GetBotInfo();
        }

        /// <summary>取得 Bot 基本資訊（非同步）</summary>
        /// <returns>Bot 資訊</returns>
        public Task<Types.BotInfo> GetBotInfoAsync()
        {
            // 透過 Bot 服務取得基本資訊（非同步）
            return botService.GetBotInfoAsync();
        }

        /// <summary>取得群組摘要</summary>
        public Types.GroupSummary GetGroupSummary(string groupId)
        {
            return botService.GetGroupSummary(groupId);
        }

        /// <summary>取得群組摘要（非同步）</summary>
        public Task<Types.GroupSummary> GetGroupSummaryAsync(string groupId)
        {
            return botService.GetGroupSummaryAsync(groupId);
        }

        /// <summary>取得多人對話摘要</summary>
        public Types.RoomSummary GetRoomSummary(string roomId)
        {
            return botService.GetRoomSummary(roomId);
        }

        /// <summary>取得多人對話摘要（非同步）</summary>
        public Task<Types.RoomSummary> GetRoomSummaryAsync(string roomId)
        {
            return botService.GetRoomSummaryAsync(roomId);
        }

        /// <summary>取得群組成員 ID 清單</summary>
        public Types.MemberIdsResponse GetGroupMemberIds(string groupId)
        {
            return botService.GetGroupMemberIds(groupId);
        }

        /// <summary>取得群組成員 ID 清單（非同步）</summary>
        public Task<Types.MemberIdsResponse> GetGroupMemberIdsAsync(string groupId)
        {
            return botService.GetGroupMemberIdsAsync(groupId);
        }

        /// <summary>取得多人對話成員 ID 清單</summary>
        public Types.MemberIdsResponse GetRoomMemberIds(string roomId)
        {
            return botService.GetRoomMemberIds(roomId);
        }

        /// <summary>取得多人對話成員 ID 清單（非同步）</summary>
        public Task<Types.MemberIdsResponse> GetRoomMemberIdsAsync(string roomId)
        {
            return botService.GetRoomMemberIdsAsync(roomId);
        }

        /// <summary>取得群組成員數量</summary>
        public Types.MemberCountResponse GetGroupMemberCount(string groupId)
        {
            return botService.GetGroupMemberCount(groupId);
        }

        /// <summary>取得群組成員數量（非同步）</summary>
        public Task<Types.MemberCountResponse> GetGroupMemberCountAsync(string groupId)
        {
            return botService.GetGroupMemberCountAsync(groupId);
        }

        /// <summary>取得多人對話成員數量</summary>
        public Types.MemberCountResponse GetRoomMemberCount(string roomId)
        {
            return botService.GetRoomMemberCount(roomId);
        }

        /// <summary>取得多人對話成員數量（非同步）</summary>
        public Task<Types.MemberCountResponse> GetRoomMemberCountAsync(string roomId)
        {
            return botService.GetRoomMemberCountAsync(roomId);
        }

        /// <summary>傳入 api 中的 ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken)
            : this(ChannelAccessToken, new SystemTextJsonSerializer(), null, null)
        {
        }

        /// <summary>傳入 api 中的 ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken, IJsonSerializer serializer)
            : this(ChannelAccessToken, serializer, null, null)
        {
        }

        /// <summary>傳入 api 中的 ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken, IJsonSerializer serializer, HttpClient httpClient)
            : this(ChannelAccessToken, serializer, httpClient, null)
        {
        }

        /// <summary>傳入 api 中的 ChannelAccessToken</summary>
        public LineChannel(string ChannelAccessToken, IJsonSerializer serializer, HttpClient httpClient, IHttpClientProvider httpClientProvider)
        {
            channelAccessToken = ChannelAccessToken;
            // 建立共用 Context
            var context = new LineApiContext(ChannelAccessToken, serializer, httpClient, httpClientProvider);
            // 建立各模組服務
            messageService = new MessageService(context);
            profileService = new ProfileService(context);
            groupService = new GroupService(context);
            webhookService = new WebhookService();
            webhookEndpointService = new WebhookEndpointService(context);
            botService = new BotService(context);
            broadcastService = new BroadcastService(context);
            messageValidationService = new MessageValidationService(context);
            richMenuService = new RichMenuService(context);
        }

        /// <summary>channelAccessToken</summary>
        public string channelAccessToken { get; private set; }

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
                // 透過群組服務執行離開群組或多人對話
                return groupService.LeaveRoomOrGroup(sourceId, type);
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
                // 透過群組服務執行離開群組或多人對話
                return groupService.LeaveRoomOrGroupAsync(sourceId, type);
            }

        }

        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserProfile GetUserProfile(string userId)
        {
            // 取得使用者檔案
            return profileService.GetUserProfile(userId);
        }
        /// <summary>取得使用者檔案</summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Task<UserProfile> GetUserProfileAsync(string userId)
        {
            // 取得使用者檔案（非同步）
            return profileService.GetUserProfileAsync(userId);
        }
        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public List<UserProfile> GetUserProfiles(List<string> userIds)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userId in userIds)
            {
                oModel.Add(profileService.GetUserProfile(userId));
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
            return profileService.GetGroupMemberProfile(userId, groupIdOrRoomId, type);
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
            return profileService.GetGroupMemberProfileAsync(userId, groupIdOrRoomId, type);
        }

        /// <summary>取得大量使用者檔案</summary>
        /// <param name="userids"></param>
        /// <returns></returns>
        public async Task<List<UserProfile>> GetUserProfilesAsync(List<string> userIds)
        {
            List<UserProfile> oModel = new List<UserProfile>();
            foreach (var userId in userIds)
            {
                oModel.Add(await profileService.GetUserProfileAsync(userId));
            }
            return oModel;
        }

        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public byte[] GetUserUploadContent(string messageId)
        {
            // 取得使用者上傳的檔案
            return messageService.GetMessageContent(messageId);
        }

        /// <summary>取得使用者上傳的檔案</summary>
        /// <param name="message_id"></param>
        /// <returns></returns>
        public Task<byte[]> GetUserUploadContentAsync(string messageId)
        {
            // 取得使用者上傳的檔案（非同步）
            return messageService.GetMessageContentAsync(messageId);
        }

        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendMulticastMessage(List<string> toIds, params Message[] message)
        {
            return messageService.SendMulticastMessage(toIds, message);
        }

        /// <summary>傳送訊息給多位使用者</summary>
        /// <param name="ToId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendMulticastMessageAsync(List<string> toIds, params Message[] message)
        {
            return messageService.SendMulticastMessageAsync(toIds, message);
        }

        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public string SendPushMessage(string ToId, params Message[] message)
        {
            return messageService.SendPushMessage(ToId, message);
        }

        /// <summary>主動傳送訊息</summary>
        /// <param name="ToId">id</param>
        /// <param name="message">訊息</param>
        /// <returns></returns>
        public Task<string> SendPushMessageAsync(string ToId, params Message[] message)
        {
            return messageService.SendPushMessageAsync(ToId, message);
        }

        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string SendReplyMessage(string replyToken, params Message[] message)
        {
            return messageService.SendReplyMessage(replyToken, message);
        }

        /// <summary>被動回復訊息</summary>
        /// <param name="replyToken"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<string> SendReplyMessageAsync(string replyToken, params Message[] message)
        {
            return messageService.SendReplyMessageAsync(replyToken, message);
        }

        /// <summary>發送 Broadcast 訊息</summary>
        public bool SendBroadcastMessage(BroadcastMessage message)
        {
            return broadcastService.SendBroadcast(message);
        }

        /// <summary>發送 Broadcast 訊息（非同步）</summary>
        public Task<bool> SendBroadcastMessageAsync(BroadcastMessage message)
        {
            return broadcastService.SendBroadcastAsync(message);
        }

        /// <summary>發送 Narrowcast 訊息</summary>
        public bool SendNarrowcastMessage(NarrowcastMessage message)
        {
            return broadcastService.SendNarrowcast(message);
        }

        /// <summary>發送 Narrowcast 訊息（非同步）</summary>
        public Task<bool> SendNarrowcastMessageAsync(NarrowcastMessage message)
        {
            return broadcastService.SendNarrowcastAsync(message);
        }

        /// <summary>取得 Narrowcast 進度</summary>
        public Types.NarrowcastProgressResponse GetNarrowcastProgress(string requestId)
        {
            return broadcastService.GetNarrowcastProgress(requestId);
        }

        /// <summary>取得 Narrowcast 進度（非同步）</summary>
        public Task<Types.NarrowcastProgressResponse> GetNarrowcastProgressAsync(string requestId)
        {
            return broadcastService.GetNarrowcastProgressAsync(requestId);
        }

        /// <summary>驗證 Reply 訊息</summary>
        public bool ValidateReplyMessage(ReplyMessage message)
        {
            return messageValidationService.ValidateReply(message);
        }

        /// <summary>驗證 Reply 訊息（非同步）</summary>
        public Task<bool> ValidateReplyMessageAsync(ReplyMessage message)
        {
            return messageValidationService.ValidateReplyAsync(message);
        }

        /// <summary>驗證 Push 訊息</summary>
        public bool ValidatePushMessage(PushMessage message)
        {
            return messageValidationService.ValidatePush(message);
        }

        /// <summary>驗證 Push 訊息（非同步）</summary>
        public Task<bool> ValidatePushMessageAsync(PushMessage message)
        {
            return messageValidationService.ValidatePushAsync(message);
        }

        /// <summary>驗證 Multicast 訊息</summary>
        public bool ValidateMulticastMessage(MulticastMessage message)
        {
            return messageValidationService.ValidateMulticast(message);
        }

        /// <summary>驗證 Multicast 訊息（非同步）</summary>
        public Task<bool> ValidateMulticastMessageAsync(MulticastMessage message)
        {
            return messageValidationService.ValidateMulticastAsync(message);
        }

        /// <summary>驗證 Broadcast 訊息</summary>
        public bool ValidateBroadcastMessage(BroadcastMessage message)
        {
            return messageValidationService.ValidateBroadcast(message);
        }

        /// <summary>驗證 Broadcast 訊息（非同步）</summary>
        public Task<bool> ValidateBroadcastMessageAsync(BroadcastMessage message)
        {
            return messageValidationService.ValidateBroadcastAsync(message);
        }

        /// <summary>驗證 Narrowcast 訊息</summary>
        public bool ValidateNarrowcastMessage(NarrowcastMessage message)
        {
            return messageValidationService.ValidateNarrowcast(message);
        }

        /// <summary>驗證 Narrowcast 訊息（非同步）</summary>
        public Task<bool> ValidateNarrowcastMessageAsync(NarrowcastMessage message)
        {
            return messageValidationService.ValidateNarrowcastAsync(message);
        }

        /// <summary>建立 Rich Menu</summary>
        public Types.RichMenuIdResponse CreateRichMenu(object richMenu)
        {
            return richMenuService.CreateRichMenu(richMenu);
        }

        /// <summary>建立 Rich Menu（非同步）</summary>
        public Task<Types.RichMenuIdResponse> CreateRichMenuAsync(object richMenu)
        {
            return richMenuService.CreateRichMenuAsync(richMenu);
        }

        /// <summary>取得 Rich Menu</summary>
        public Types.RichMenuResponse GetRichMenu(string richMenuId)
        {
            return richMenuService.GetRichMenu(richMenuId);
        }

        /// <summary>取得 Rich Menu（非同步）</summary>
        public Task<Types.RichMenuResponse> GetRichMenuAsync(string richMenuId)
        {
            return richMenuService.GetRichMenuAsync(richMenuId);
        }

        /// <summary>刪除 Rich Menu</summary>
        public bool DeleteRichMenu(string richMenuId)
        {
            return richMenuService.DeleteRichMenu(richMenuId);
        }

        /// <summary>刪除 Rich Menu（非同步）</summary>
        public Task<bool> DeleteRichMenuAsync(string richMenuId)
        {
            return richMenuService.DeleteRichMenuAsync(richMenuId);
        }

        /// <summary>取得 Rich Menu 清單</summary>
        public Types.RichMenuListResponse GetRichMenuList()
        {
            return richMenuService.GetRichMenuList();
        }

        /// <summary>取得 Rich Menu 清單（非同步）</summary>
        public Task<Types.RichMenuListResponse> GetRichMenuListAsync()
        {
            return richMenuService.GetRichMenuListAsync();
        }

        /// <summary>上傳 Rich Menu 圖片</summary>
        public bool UploadRichMenuImage(string richMenuId, string contentType, byte[] content)
        {
            return richMenuService.UploadRichMenuImage(richMenuId, contentType, content);
        }

        /// <summary>上傳 Rich Menu 圖片（非同步）</summary>
        public Task<bool> UploadRichMenuImageAsync(string richMenuId, string contentType, byte[] content)
        {
            return richMenuService.UploadRichMenuImageAsync(richMenuId, contentType, content);
        }

        /// <summary>下載 Rich Menu 圖片</summary>
        public byte[] DownloadRichMenuImage(string richMenuId)
        {
            return richMenuService.DownloadRichMenuImage(richMenuId);
        }

        /// <summary>下載 Rich Menu 圖片（非同步）</summary>
        public Task<byte[]> DownloadRichMenuImageAsync(string richMenuId)
        {
            return richMenuService.DownloadRichMenuImageAsync(richMenuId);
        }

        /// <summary>設定預設 Rich Menu</summary>
        public bool SetDefaultRichMenu(string richMenuId)
        {
            return richMenuService.SetDefaultRichMenu(richMenuId);
        }

        /// <summary>設定預設 Rich Menu（非同步）</summary>
        public Task<bool> SetDefaultRichMenuAsync(string richMenuId)
        {
            return richMenuService.SetDefaultRichMenuAsync(richMenuId);
        }

        /// <summary>取得預設 Rich Menu ID</summary>
        public Types.RichMenuIdResponse GetDefaultRichMenuId()
        {
            return richMenuService.GetDefaultRichMenuId();
        }

        /// <summary>取得預設 Rich Menu ID（非同步）</summary>
        public Task<Types.RichMenuIdResponse> GetDefaultRichMenuIdAsync()
        {
            return richMenuService.GetDefaultRichMenuIdAsync();
        }

        /// <summary>取消預設 Rich Menu</summary>
        public bool CancelDefaultRichMenu()
        {
            return richMenuService.CancelDefaultRichMenu();
        }

        /// <summary>取消預設 Rich Menu（非同步）</summary>
        public Task<bool> CancelDefaultRichMenuAsync()
        {
            return richMenuService.CancelDefaultRichMenuAsync();
        }

        /// <summary>綁定使用者 Rich Menu</summary>
        public bool LinkUserRichMenu(string userId, string richMenuId)
        {
            return richMenuService.LinkUserRichMenu(userId, richMenuId);
        }

        /// <summary>綁定使用者 Rich Menu（非同步）</summary>
        public Task<bool> LinkUserRichMenuAsync(string userId, string richMenuId)
        {
            return richMenuService.LinkUserRichMenuAsync(userId, richMenuId);
        }

        /// <summary>解除使用者 Rich Menu</summary>
        public bool UnlinkUserRichMenu(string userId)
        {
            return richMenuService.UnlinkUserRichMenu(userId);
        }

        /// <summary>解除使用者 Rich Menu（非同步）</summary>
        public Task<bool> UnlinkUserRichMenuAsync(string userId)
        {
            return richMenuService.UnlinkUserRichMenuAsync(userId);
        }

        /// <summary>批次綁定 Rich Menu</summary>
        public bool BulkLinkRichMenu(Types.RichMenuBulkLinkRequest request)
        {
            return richMenuService.BulkLinkRichMenu(request);
        }

        /// <summary>批次綁定 Rich Menu（非同步）</summary>
        public Task<bool> BulkLinkRichMenuAsync(Types.RichMenuBulkLinkRequest request)
        {
            return richMenuService.BulkLinkRichMenuAsync(request);
        }

        /// <summary>批次解除綁定 Rich Menu</summary>
        public bool BulkUnlinkRichMenu(Types.RichMenuBulkUnlinkRequest request)
        {
            return richMenuService.BulkUnlinkRichMenu(request);
        }

        /// <summary>批次解除綁定 Rich Menu（非同步）</summary>
        public Task<bool> BulkUnlinkRichMenuAsync(Types.RichMenuBulkUnlinkRequest request)
        {
            return richMenuService.BulkUnlinkRichMenuAsync(request);
        }

        /// <summary>建立 Rich Menu Alias</summary>
        public bool CreateRichMenuAlias(Types.RichMenuAliasRequest request)
        {
            return richMenuService.CreateRichMenuAlias(request);
        }

        /// <summary>建立 Rich Menu Alias（非同步）</summary>
        public Task<bool> CreateRichMenuAliasAsync(Types.RichMenuAliasRequest request)
        {
            return richMenuService.CreateRichMenuAliasAsync(request);
        }

        /// <summary>更新 Rich Menu Alias</summary>
        public bool UpdateRichMenuAlias(string aliasId, Types.RichMenuAliasRequest request)
        {
            return richMenuService.UpdateRichMenuAlias(aliasId, request);
        }

        /// <summary>更新 Rich Menu Alias（非同步）</summary>
        public Task<bool> UpdateRichMenuAliasAsync(string aliasId, Types.RichMenuAliasRequest request)
        {
            return richMenuService.UpdateRichMenuAliasAsync(aliasId, request);
        }

        /// <summary>取得 Rich Menu Alias</summary>
        public Types.RichMenuAliasResponse GetRichMenuAlias(string aliasId)
        {
            return richMenuService.GetRichMenuAlias(aliasId);
        }

        /// <summary>取得 Rich Menu Alias（非同步）</summary>
        public Task<Types.RichMenuAliasResponse> GetRichMenuAliasAsync(string aliasId)
        {
            return richMenuService.GetRichMenuAliasAsync(aliasId);
        }

        /// <summary>取得 Rich Menu Alias 清單</summary>
        public Types.RichMenuAliasListResponse GetRichMenuAliasList()
        {
            return richMenuService.GetRichMenuAliasList();
        }

        /// <summary>取得 Rich Menu Alias 清單（非同步）</summary>
        public Task<Types.RichMenuAliasListResponse> GetRichMenuAliasListAsync()
        {
            return richMenuService.GetRichMenuAliasListAsync();
        }

        /// <summary>刪除 Rich Menu Alias</summary>
        public bool DeleteRichMenuAlias(string aliasId)
        {
            return richMenuService.DeleteRichMenuAlias(aliasId);
        }

        /// <summary>刪除 Rich Menu Alias（非同步）</summary>
        public Task<bool> DeleteRichMenuAliasAsync(string aliasId)
        {
            return richMenuService.DeleteRichMenuAliasAsync(aliasId);
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

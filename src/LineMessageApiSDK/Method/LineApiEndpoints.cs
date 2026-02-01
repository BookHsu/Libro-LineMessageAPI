namespace LineMessageApiSDK.Method
{
    internal static class LineApiEndpoints
    {
        internal const string ApiBaseUrl = "https://api.line.me";
        internal const string ApiDataBaseUrl = "https://api-data.line.me";

        internal static string BuildMessageContent(string messageId)
        {
            return $"{ApiDataBaseUrl}/v2/bot/message/{messageId}/content";
        }

        internal static string BuildUserProfile(string userId)
        {
            return $"{ApiBaseUrl}/v2/bot/profile/{userId}";
        }

        internal static string BuildGroupMemberProfile(SourceType type, string groupId, string userId)
        {
            return $"{ApiBaseUrl}/v2/bot/{type}/{groupId}/member/{userId}";
        }

        internal static string BuildLeaveGroupOrRoom(SourceType type, string id)
        {
            return $"{ApiBaseUrl}/v2/bot/{type}/{id}/leave";
        }

        internal static string BuildReplyMessage()
        {
            return $"{ApiBaseUrl}/v2/bot/message/reply";
        }

        internal static string BuildPushMessage()
        {
            return $"{ApiBaseUrl}/v2/bot/message/push";
        }

        internal static string BuildMulticastMessage()
        {
            return $"{ApiBaseUrl}/v2/bot/message/multicast";
        }

        internal static string BuildBroadcastMessage()
        {
            return $"{ApiBaseUrl}/v2/bot/message/broadcast";
        }

        internal static string BuildNarrowcastMessage()
        {
            return $"{ApiBaseUrl}/v2/bot/message/narrowcast";
        }

        internal static string BuildNarrowcastProgress(string requestId)
        {
            return $"{ApiBaseUrl}/v2/bot/message/progress/{requestId}";
        }

        internal static string BuildWebhookEndpoint()
        {
            return $"{ApiBaseUrl}/v2/bot/channel/webhook/endpoint";
        }

        internal static string BuildWebhookTest()
        {
            return $"{ApiBaseUrl}/v2/bot/channel/webhook/test";
        }

        internal static string BuildBotInfo()
        {
            return $"{ApiBaseUrl}/v2/bot/info";
        }

        internal static string BuildGroupSummary(string groupId)
        {
            return $"{ApiBaseUrl}/v2/bot/group/{groupId}/summary";
        }

        internal static string BuildRoomSummary(string roomId)
        {
            return $"{ApiBaseUrl}/v2/bot/room/{roomId}/summary";
        }

        internal static string BuildGroupMemberIds(string groupId)
        {
            return $"{ApiBaseUrl}/v2/bot/group/{groupId}/members/ids";
        }

        internal static string BuildRoomMemberIds(string roomId)
        {
            return $"{ApiBaseUrl}/v2/bot/room/{roomId}/members/ids";
        }

        internal static string BuildGroupMemberCount(string groupId)
        {
            return $"{ApiBaseUrl}/v2/bot/group/{groupId}/members/count";
        }

        internal static string BuildRoomMemberCount(string roomId)
        {
            return $"{ApiBaseUrl}/v2/bot/room/{roomId}/members/count";
        }

        internal static string BuildRichMenu()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu";
        }

        internal static string BuildRichMenuId(string richMenuId)
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/{richMenuId}";
        }

        internal static string BuildRichMenuList()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/list";
        }

        internal static string BuildRichMenuContent(string richMenuId)
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/{richMenuId}/content";
        }

        internal static string BuildDefaultRichMenu()
        {
            return $"{ApiBaseUrl}/v2/bot/user/all/richmenu";
        }

        internal static string BuildDefaultRichMenu(string richMenuId)
        {
            return $"{ApiBaseUrl}/v2/bot/user/all/richmenu/{richMenuId}";
        }

        internal static string BuildUserRichMenu(string userId)
        {
            return $"{ApiBaseUrl}/v2/bot/user/{userId}/richmenu";
        }

        internal static string BuildUserRichMenu(string userId, string richMenuId)
        {
            return $"{ApiBaseUrl}/v2/bot/user/{userId}/richmenu/{richMenuId}";
        }

        internal static string BuildRichMenuBulkLink()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/bulk/link";
        }

        internal static string BuildRichMenuBulkUnlink()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/bulk/unlink";
        }

        internal static string BuildRichMenuAlias()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/alias";
        }

        internal static string BuildRichMenuAlias(string aliasId)
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/alias/{aliasId}";
        }

        internal static string BuildRichMenuAliasList()
        {
            return $"{ApiBaseUrl}/v2/bot/richmenu/alias/list";
        }

        internal static string BuildValidateMessage(string type)
        {
            return $"{ApiBaseUrl}/v2/bot/message/validate/{type}";
        }

        internal static string BuildMessageDeliveryInsight(string date)
        {
            return $"{ApiBaseUrl}/v2/bot/insight/message/delivery?date={date}";
        }

        internal static string BuildFollowerInsight()
        {
            return $"{ApiBaseUrl}/v2/bot/insight/followers";
        }

        internal static string BuildDemographicInsight()
        {
            return $"{ApiBaseUrl}/v2/bot/insight/demographic";
        }

        internal static string BuildAudienceGroupUpload()
        {
            return $"{ApiBaseUrl}/v2/bot/audienceGroup/upload";
        }

        internal static string BuildAudienceGroupStatus(long audienceGroupId)
        {
            return $"{ApiBaseUrl}/v2/bot/audienceGroup/upload/{audienceGroupId}";
        }

        internal static string BuildAudienceGroup(long audienceGroupId)
        {
            return $"{ApiBaseUrl}/v2/bot/audienceGroup/{audienceGroupId}";
        }

        internal static string BuildAudienceGroupList()
        {
            return $"{ApiBaseUrl}/v2/bot/audienceGroup/list";
        }

        internal static string BuildLinkToken(string userId)
        {
            return $"{ApiBaseUrl}/v2/bot/user/{userId}/linkToken";
        }
    }
}

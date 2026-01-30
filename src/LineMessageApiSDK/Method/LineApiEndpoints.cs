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
    }
}

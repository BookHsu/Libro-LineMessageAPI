using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// 訊息投遞統計回應
    /// </summary>
    public class MessageDeliveryInsightResponse
    {
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string status { get; set; }

        /// <summary>
        /// Broadcast 成功數量
        /// </summary>
        [JsonPropertyName("broadcast")]
        public long? broadcast { get; set; }

        /// <summary>
        /// Targeting 成功數量
        /// </summary>
        [JsonPropertyName("targeting")]
        public long? targeting { get; set; }

        /// <summary>
        /// Step message 成功數量
        /// </summary>
        [JsonPropertyName("stepMessage")]
        public long? stepMessage { get; set; }

        /// <summary>
        /// Auto response 成功數量
        /// </summary>
        [JsonPropertyName("autoResponse")]
        public long? autoResponse { get; set; }

        /// <summary>
        /// Welcome response 成功數量
        /// </summary>
        [JsonPropertyName("welcomeResponse")]
        public long? welcomeResponse { get; set; }

        /// <summary>
        /// Chat 成功數量
        /// </summary>
        [JsonPropertyName("chat")]
        public long? chat { get; set; }

        /// <summary>
        /// API broadcast 成功數量
        /// </summary>
        [JsonPropertyName("apiBroadcast")]
        public long? apiBroadcast { get; set; }

        /// <summary>
        /// API push 成功數量
        /// </summary>
        [JsonPropertyName("apiPush")]
        public long? apiPush { get; set; }

        /// <summary>
        /// API multicast 成功數量
        /// </summary>
        [JsonPropertyName("apiMulticast")]
        public long? apiMulticast { get; set; }

        /// <summary>
        /// API narrowcast 成功數量
        /// </summary>
        [JsonPropertyName("apiNarrowcast")]
        public long? apiNarrowcast { get; set; }

        /// <summary>
        /// API reply 成功數量
        /// </summary>
        [JsonPropertyName("apiReply")]
        public long? apiReply { get; set; }

        /// <summary>
        /// Chat control auto reply 成功數量
        /// </summary>
        [JsonPropertyName("ccAutoReply")]
        public long? ccAutoReply { get; set; }

        /// <summary>
        /// Chat control manual reply 成功數量
        /// </summary>
        [JsonPropertyName("ccManualReply")]
        public long? ccManualReply { get; set; }

        /// <summary>
        /// Phone Number Portability notice message 成功數量
        /// </summary>
        [JsonPropertyName("pnpNoticeMessage")]
        public long? pnpNoticeMessage { get; set; }

        /// <summary>
        /// Phone Number Portability call to LINE 成功數量
        /// </summary>
        [JsonPropertyName("pnpCallToLine")]
        public long? pnpCallToLine { get; set; }

        /// <summary>
        /// Third party chat tool 成功數量
        /// </summary>
        [JsonPropertyName("thirdPartyChatTool")]
        public long? thirdPartyChatTool { get; set; }
    }
}


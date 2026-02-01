using System.Text.Json.Serialization;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>LINE Webhook 事件</summary>
    public class LineEvents
    {
        /// <summary>事件模式（active / standby）</summary>
        [JsonPropertyName("mode")]
        public string mode { get; set; }

        /// <summary>事件類型</summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EventType type { get; set; }

        /// <summary>事件時間（Unix time in milliseconds）</summary>
        [JsonPropertyName("timestamp")]
        public long timestamp { get; set; }

        /// <summary>事件來源</summary>
        [JsonPropertyName("source")]
        public LineSource source { get; set; }

        /// <summary>Webhook 事件 ID</summary>
        [JsonPropertyName("webhookEventId")]
        public string webhookEventId { get; set; }

        /// <summary>傳遞資訊</summary>
        [JsonPropertyName("deliveryContext")]
        public LineDeliveryContext deliveryContext { get; set; }

        /// <summary>Reply Token（僅限可回覆事件）</summary>
        [JsonPropertyName("replyToken")]
        public string replyToken { get; set; }

        /// <summary>訊息物件</summary>
        [JsonPropertyName("message")]
        public LineMessage message { get; set; }

        /// <summary>Postback 物件</summary>
        [JsonPropertyName("postback")]
        public LinePostBack postback { get; set; }

        /// <summary>Beacon 物件</summary>
        [JsonPropertyName("beacon")]
        public LineBeacon beacon { get; set; }

        /// <summary>Account Link 物件</summary>
        [JsonPropertyName("link")]
        public LineAccountLink link { get; set; }

        /// <summary>加入成員資訊</summary>
        [JsonPropertyName("joined")]
        public LineMembers joined { get; set; }

        /// <summary>離開成員資訊</summary>
        [JsonPropertyName("left")]
        public LineMembers left { get; set; }

        /// <summary>LINE Things 物件</summary>
        [JsonPropertyName("things")]
        public LineThings things { get; set; }

        /// <summary>收回訊息物件</summary>
        [JsonPropertyName("unsend")]
        public LineUnsend unsend { get; set; }

        /// <summary>影片播放完成物件</summary>
        [JsonPropertyName("videoPlayComplete")]
        public LineVideoPlayComplete videoPlayComplete { get; set; }
    }
}

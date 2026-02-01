using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>Webhook 事件的傳遞資訊</summary>
    public class LineDeliveryContext
    {
        /// <summary>是否為重送事件</summary>
        [JsonPropertyName("isRedelivery")]
        public bool isRedelivery { get; set; }
    }

    /// <summary>Account link 事件</summary>
    public class LineAccountLink
    {
        /// <summary>結果（ok / failed）</summary>
        [JsonPropertyName("result")]
        public string result { get; set; }

        /// <summary>Nonce</summary>
        [JsonPropertyName("nonce")]
        public string nonce { get; set; }
    }

    /// <summary>收回訊息事件</summary>
    public class LineUnsend
    {
        /// <summary>被收回的訊息 ID</summary>
        [JsonPropertyName("messageId")]
        public string messageId { get; set; }
    }

    /// <summary>影片播放完成事件</summary>
    public class LineVideoPlayComplete
    {
        /// <summary>追蹤 ID</summary>
        [JsonPropertyName("trackingId")]
        public string trackingId { get; set; }
    }

    /// <summary>成員清單</summary>
    public class LineMembers
    {
        /// <summary>成員</summary>
        [JsonPropertyName("members")]
        public List<LineMember> members { get; set; }
    }

    /// <summary>成員資訊</summary>
    public class LineMember
    {
        /// <summary>來源類型（user）</summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SourceType type { get; set; }

        /// <summary>使用者 ID</summary>
        [JsonPropertyName("userId")]
        public string userId { get; set; }
    }

    /// <summary>LINE Things 事件</summary>
    public class LineThings
    {
        /// <summary>事件類型</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>裝置 ID</summary>
        [JsonPropertyName("deviceId")]
        public string deviceId { get; set; }

        /// <summary>結果</summary>
        [JsonPropertyName("result")]
        public LineThingsResult result { get; set; }
    }

    /// <summary>LINE Things 事件結果</summary>
    public class LineThingsResult
    {
        /// <summary>情境結果</summary>
        [JsonPropertyName("scenarioResult")]
        public LineThingsScenarioResult scenarioResult { get; set; }

        /// <summary>動作結果</summary>
        [JsonPropertyName("actionResults")]
        public List<LineThingsActionResult> actionResults { get; set; }
    }

    /// <summary>情境結果</summary>
    public class LineThingsScenarioResult
    {
        /// <summary>情境 ID</summary>
        [JsonPropertyName("scenarioId")]
        public string scenarioId { get; set; }

        /// <summary>修訂版</summary>
        [JsonPropertyName("revision")]
        public int? revision { get; set; }
    }

    /// <summary>動作結果</summary>
    public class LineThingsActionResult
    {
        /// <summary>動作類型</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>動作資料</summary>
        [JsonPropertyName("data")]
        public object data { get; set; }
    }

    /// <summary>表情符號</summary>
    public class LineEmoji
    {
        /// <summary>索引</summary>
        [JsonPropertyName("index")]
        public int index { get; set; }

        /// <summary>長度</summary>
        [JsonPropertyName("length")]
        public int length { get; set; }

        /// <summary>產品 ID</summary>
        [JsonPropertyName("productId")]
        public string productId { get; set; }

        /// <summary>表情符號 ID</summary>
        [JsonPropertyName("emojiId")]
        public string emojiId { get; set; }
    }

    /// <summary>提及資訊</summary>
    public class LineMention
    {
        /// <summary>被提及者</summary>
        [JsonPropertyName("mentionees")]
        public List<LineMentionee> mentionees { get; set; }
    }

    /// <summary>被提及者</summary>
    public class LineMentionee
    {
        /// <summary>索引</summary>
        [JsonPropertyName("index")]
        public int index { get; set; }

        /// <summary>長度</summary>
        [JsonPropertyName("length")]
        public int length { get; set; }

        /// <summary>類型（user）</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>使用者 ID</summary>
        [JsonPropertyName("userId")]
        public string userId { get; set; }
    }

    /// <summary>內容提供者</summary>
    public class LineMessageContentProvider
    {
        /// <summary>提供者類型（line / external）</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>原始內容 URL</summary>
        [JsonPropertyName("originalContentUrl")]
        public string originalContentUrl { get; set; }

        /// <summary>預覽圖片 URL</summary>
        [JsonPropertyName("previewImageUrl")]
        public string previewImageUrl { get; set; }
    }

    /// <summary>多張圖片集合資訊</summary>
    public class LineImageSet
    {
        /// <summary>集合 ID</summary>
        [JsonPropertyName("id")]
        public string id { get; set; }

        /// <summary>索引</summary>
        [JsonPropertyName("index")]
        public int? index { get; set; }

        /// <summary>總數</summary>
        [JsonPropertyName("total")]
        public int? total { get; set; }
    }
}

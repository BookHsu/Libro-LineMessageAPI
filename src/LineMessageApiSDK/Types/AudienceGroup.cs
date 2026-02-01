using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Audience Group 資料
    /// </summary>
    public class AudienceGroup
    {
        /// <summary>Audience Group ID</summary>
        [JsonPropertyName("audienceGroupId")]
        public long? audienceGroupId { get; set; }

        /// <summary>群組類型</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>描述</summary>
        [JsonPropertyName("description")]
        public string description { get; set; }

        /// <summary>狀態</summary>
        [JsonPropertyName("status")]
        public string status { get; set; }

        /// <summary>失敗原因</summary>
        [JsonPropertyName("failedType")]
        public string failedType { get; set; }

        /// <summary>請求 ID</summary>
        [JsonPropertyName("requestId")]
        public string requestId { get; set; }

        /// <summary>群組人數</summary>
        [JsonPropertyName("audienceGroupCount")]
        public long? audienceGroupCount { get; set; }

        /// <summary>建立時間（Unix time in milliseconds）</summary>
        [JsonPropertyName("created")]
        public long? created { get; set; }

        /// <summary>權限</summary>
        [JsonPropertyName("permission")]
        public string permission { get; set; }

        /// <summary>是否為 IFA Audience</summary>
        [JsonPropertyName("isIfaAudience")]
        public bool? isIfaAudience { get; set; }
    }
}

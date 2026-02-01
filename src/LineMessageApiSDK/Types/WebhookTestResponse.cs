using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Webhook 測試回應
    /// </summary>
    public class WebhookTestResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonPropertyName("success")]
        public bool success { get; set; }

        /// <summary>
        /// 測試時間（RFC3339）
        /// </summary>
        [JsonPropertyName("timestamp")]
        public string timestamp { get; set; }

        /// <summary>
        /// 回應狀態碼
        /// </summary>
        [JsonPropertyName("statusCode")]
        public int? statusCode { get; set; }

        /// <summary>
        /// 回應原因
        /// </summary>
        [JsonPropertyName("reason")]
        public string reason { get; set; }

        /// <summary>
        /// 詳細訊息
        /// </summary>
        [JsonPropertyName("detail")]
        public string detail { get; set; }
    }
}

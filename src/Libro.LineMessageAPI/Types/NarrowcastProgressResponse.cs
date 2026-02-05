using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Narrowcast 進度回應
    /// </summary>
    public class NarrowcastProgressResponse
    {
        /// <summary>
        /// 狀態階段
        /// </summary>
        [JsonPropertyName("phase")]
        public string phase { get; set; }

        /// <summary>
        /// 成功數量
        /// </summary>
        [JsonPropertyName("successCount")]
        public long? successCount { get; set; }

        /// <summary>
        /// 失敗數量
        /// </summary>
        [JsonPropertyName("failureCount")]
        public long? failureCount { get; set; }

        /// <summary>
        /// 目標數量
        /// </summary>
        [JsonPropertyName("targetCount")]
        public long? targetCount { get; set; }

        /// <summary>
        /// 失敗原因
        /// </summary>
        [JsonPropertyName("failedDescription")]
        public string failedDescription { get; set; }

        /// <summary>
        /// 錯誤代碼
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string errorCode { get; set; }

        /// <summary>
        /// 接受時間（RFC3339）
        /// </summary>
        [JsonPropertyName("acceptedTime")]
        public string acceptedTime { get; set; }

        /// <summary>
        /// 完成時間（RFC3339）
        /// </summary>
        [JsonPropertyName("completedTime")]
        public string completedTime { get; set; }
    }
}


using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// 追蹤者統計回應
    /// </summary>
    public class FollowerInsightResponse
    {
        /// <summary>
        /// 狀態
        /// </summary>
        [JsonPropertyName("status")]
        public string status { get; set; }

        /// <summary>
        /// 追蹤者數量
        /// </summary>
        [JsonPropertyName("followers")]
        public long? followers { get; set; }

        /// <summary>
        /// 目標觸及數量
        /// </summary>
        [JsonPropertyName("targetedReaches")]
        public long? targetedReaches { get; set; }

        /// <summary>
        /// 封鎖數量
        /// </summary>
        [JsonPropertyName("blocks")]
        public long? blocks { get; set; }
    }
}


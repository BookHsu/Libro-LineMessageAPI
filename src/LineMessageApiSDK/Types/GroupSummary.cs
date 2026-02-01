using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 群組摘要
    /// </summary>
    public class GroupSummary
    {
        /// <summary>
        /// 群組 ID
        /// </summary>
        [JsonPropertyName("groupId")]
        public string groupId { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        [JsonPropertyName("groupName")]
        public string groupName { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        [JsonPropertyName("pictureUrl")]
        public string pictureUrl { get; set; }
    }
}

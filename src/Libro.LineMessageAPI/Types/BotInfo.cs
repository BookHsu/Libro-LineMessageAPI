using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Bot 基本資訊
    /// </summary>
    public class BotInfo
    {
        /// <summary>
        /// 使用者 ID
        /// </summary>
        [JsonPropertyName("userId")]
        public string userId { get; set; }

        /// <summary>
        /// LINE 基本 ID
        /// </summary>
        [JsonPropertyName("basicId")]
        public string basicId { get; set; }

        /// <summary>
        /// LINE Premium ID
        /// </summary>
        [JsonPropertyName("premiumId")]
        public string premiumId { get; set; }

        /// <summary>
        /// 顯示名稱
        /// </summary>
        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        [JsonPropertyName("pictureUrl")]
        public string pictureUrl { get; set; }

        /// <summary>
        /// Chat 模式
        /// </summary>
        [JsonPropertyName("chatMode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChatMode chatMode { get; set; }

        /// <summary>
        /// 已讀模式
        /// </summary>
        [JsonPropertyName("markAsReadMode")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MarkAsReadMode markAsReadMode { get; set; }
    }
}


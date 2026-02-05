using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>
    /// 訊息來源主要物件
    /// 使用者直接對談：userId
    /// 群組對話：groupId
    /// 房間對話：roomId
    /// </summary>
    public class LineSource
    {
        /// <summary>訊息來源類型</summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SourceType type { get; set; }

        /// <summary>使用者 ID</summary>
        [JsonPropertyName("userId")]
        public string userId { get; set; }

        /// <summary>群組 ID</summary>
        [JsonPropertyName("groupId")]
        public string groupId { get; set; }

        /// <summary>房間 ID</summary>
        [JsonPropertyName("roomId")]
        public string roomId { get; set; }
    }
}


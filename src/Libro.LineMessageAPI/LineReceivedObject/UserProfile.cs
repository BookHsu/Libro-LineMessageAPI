using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>使用者檔案</summary>
    public class UserProfile
    {
        /// <summary>暱稱</summary>
        [JsonPropertyName("displayName")]
        public string displayName { get; set; }

        /// <summary>大頭貼</summary>
        [JsonPropertyName("pictureUrl")]
        public string pictureUrl { get; set; }

        /// <summary>心情狀態</summary>
        [JsonPropertyName("statusMessage")]
        public string statusMessage { get; set; }

        /// <summary>語系</summary>
        [JsonPropertyName("language")]
        public string language { get; set; }

        /// <summary>LINE 使用者 ID</summary>
        [JsonPropertyName("userId")]
        public string userId { get; set; }
    }
}


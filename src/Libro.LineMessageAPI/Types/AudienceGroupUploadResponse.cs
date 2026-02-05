using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Audience 上傳回應
    /// </summary>
    public class AudienceGroupUploadResponse
    {
        /// <summary>
        /// Audience Group ID
        /// </summary>
        [JsonPropertyName("audienceGroupId")]
        public long? audienceGroupId { get; set; }

        /// <summary>
        /// 上傳 ID
        /// </summary>
        [JsonPropertyName("uploadId")]
        public string uploadId { get; set; }
    }
}


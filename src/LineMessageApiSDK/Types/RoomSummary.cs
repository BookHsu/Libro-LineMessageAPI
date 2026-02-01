using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 多人對話摘要
    /// </summary>
    public class RoomSummary
    {
        /// <summary>
        /// 對話 ID
        /// </summary>
        [JsonPropertyName("roomId")]
        public string roomId { get; set; }

        /// <summary>
        /// 對話名稱
        /// </summary>
        [JsonPropertyName("roomName")]
        public string roomName { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        [JsonPropertyName("pictureUrl")]
        public string pictureUrl { get; set; }
    }
}

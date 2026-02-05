using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>Postback 參數</summary>
    public class LineParams
    {
        /// <summary>
        /// Datetime 模式（yyyy-MM-ddTHH:mm）
        /// </summary>
        [JsonPropertyName("datetime")]
        public string datetime { get; set; }

        /// <summary>
        /// Date 模式（yyyy-MM-dd）
        /// </summary>
        [JsonPropertyName("date")]
        public string date { get; set; }

        /// <summary>
        /// Time 模式（HH:mm）
        /// </summary>
        [JsonPropertyName("time")]
        public string time { get; set; }
    }
}


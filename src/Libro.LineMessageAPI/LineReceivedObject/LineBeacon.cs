using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>Beacon 事件</summary>
    public class LineBeacon
    {
        /// <summary>Device message</summary>
        [JsonPropertyName("dm")]
        public string dm { get; set; }

        /// <summary>Hardware ID</summary>
        [JsonPropertyName("hwid")]
        public string hwid { get; set; }

        /// <summary>Beacon 類型</summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BeaconType type { get; set; }
    }
}


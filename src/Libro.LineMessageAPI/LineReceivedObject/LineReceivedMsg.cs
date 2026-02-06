using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>Webhook 事件集合。</summary>
    public class LineReceivedMsg
    {
        /// <summary>Webhook 目標 Bot ID。</summary>
        [JsonPropertyName("destination")]
        public string destination { get; set; }

        /// <summary>事件集合。</summary>
        [JsonPropertyName("events")]
        public List<LineEvents> events { get; set; }
    }
}


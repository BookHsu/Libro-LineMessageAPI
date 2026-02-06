using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>LINE 訊息基底類型。</summary>
    public class Message
    {
        /// <summary>類型。</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SendMessageType type { get; set; } 
    }
}


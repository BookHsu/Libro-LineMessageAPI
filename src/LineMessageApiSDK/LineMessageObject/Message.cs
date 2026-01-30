using System.Text.Json.Serialization;

namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>Line 訊息基底</summary>
    public class Message
    {
        /// <summary>類型</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SendMessageType type { get; set; } 
    }
}

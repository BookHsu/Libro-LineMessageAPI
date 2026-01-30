namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>Line 訊息基底</summary>
    public class Message
    {
        /// <summary>類型</summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public SendMessageType type { get; set; } 
    }
}
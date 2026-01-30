namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>Line Message Api EventObject</summary>
    public class LineEvents
    {
        /// <summary></summary>
        public LineBeacon beacon { get; set; }

        /// <summary>訊息物件資訊</summary>
        public LineMessage message { get; set; }

        /// <summary></summary>
        public LinePostBack postback { get; set; }

        /// <summary>使用reply message 憑證 只可使用一次</summary>
        public string replyToken { get; set; }

        /// <summary>訊息來源物件資訊</summary> 
        public LineSource source { get; set; }

        /// <summary>時間整型</summary>
        public long timestamp { get; set; }

        /// <summary>事件類型</summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public EventType type { get; set; }
    }
}
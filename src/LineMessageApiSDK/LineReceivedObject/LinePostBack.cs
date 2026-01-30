namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>樣板訊息點選後回復訊息</summary>
    public class LinePostBack
    {
        /// <summary>回傳訊息</summary>
        public string data { get; set; }
        /// <summary>DateTime Picker回傳的日期參數</summary>
        public LineParams Params { get; set; }
    }
}
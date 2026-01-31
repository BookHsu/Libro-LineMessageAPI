namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 訊息投遞統計回應
    /// </summary>
    public class MessageDeliveryInsightResponse
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 成功數量
        /// </summary>
        public long? success { get; set; }

        /// <summary>
        /// 失敗數量
        /// </summary>
        public long? failure { get; set; }

        /// <summary>
        /// 目標數量
        /// </summary>
        public long? targeted { get; set; }
    }
}

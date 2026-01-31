namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Narrowcast 進度回應
    /// </summary>
    public class NarrowcastProgressResponse
    {
        /// <summary>
        /// 狀態階段
        /// </summary>
        public string phase { get; set; }

        /// <summary>
        /// 成功數量
        /// </summary>
        public long? succeeded { get; set; }

        /// <summary>
        /// 失敗數量
        /// </summary>
        public long? failed { get; set; }

        /// <summary>
        /// 接收數量
        /// </summary>
        public long? accepted { get; set; }

        /// <summary>
        /// 請求 ID
        /// </summary>
        public string requestId { get; set; }
    }
}

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Bot 基本資訊
    /// </summary>
    public class BotInfo
    {
        /// <summary>
        /// 使用者 ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string displayName { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        public string pictureUrl { get; set; }

        /// <summary>
        /// Chat 模式
        /// </summary>
        public string chatMode { get; set; }

        /// <summary>
        /// 標記功能
        /// </summary>
        public bool markAsReadMode { get; set; }
    }
}

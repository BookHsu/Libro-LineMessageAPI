namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 多人對話摘要
    /// </summary>
    public class RoomSummary
    {
        /// <summary>
        /// 對話 ID
        /// </summary>
        public string roomId { get; set; }

        /// <summary>
        /// 群組名稱
        /// </summary>
        public string roomName { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        public string pictureUrl { get; set; }
    }
}

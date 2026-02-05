namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>
    /// Narrowcast 目標設定
    /// </summary>
    public class NarrowcastRecipient
    {
        /// <summary>
        /// 目標類型（例如 audience）
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 目標群組 ID
        /// </summary>
        public long? audienceGroupId { get; set; }
    }
}


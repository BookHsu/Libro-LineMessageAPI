namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 追蹤者統計回應
    /// </summary>
    public class FollowerInsightResponse
    {
        /// <summary>
        /// 追蹤者數量
        /// </summary>
        public long? followers { get; set; }

        /// <summary>
        /// 目標數量
        /// </summary>
        public long? targetedReaches { get; set; }

        /// <summary>
        /// 區塊數量
        /// </summary>
        public long? blocks { get; set; }
    }
}

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Audience 上傳狀態回應
    /// </summary>
    public class AudienceGroupStatusResponse
    {
        /// <summary>
        /// Audience Group ID
        /// </summary>
        public long? audienceGroupId { get; set; }

        /// <summary>
        /// 狀態
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 失敗原因
        /// </summary>
        public string failedType { get; set; }
    }
}

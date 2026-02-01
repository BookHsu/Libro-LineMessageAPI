using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 成員數量回應
    /// </summary>
    public class MemberCountResponse
    {
        /// <summary>
        /// 成員數量
        /// </summary>
        [JsonPropertyName("count")]
        public long count { get; set; }
    }
}

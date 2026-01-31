using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Audience 清單回應
    /// </summary>
    public class AudienceGroupListResponse
    {
        /// <summary>
        /// Audience 清單
        /// </summary>
        public List<object> audienceGroups { get; set; }
    }
}

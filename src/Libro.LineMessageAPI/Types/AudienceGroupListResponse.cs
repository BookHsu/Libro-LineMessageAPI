using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Audience 清單回應
    /// </summary>
    public class AudienceGroupListResponse
    {
        /// <summary>
        /// Audience 清單
        /// </summary>
        [JsonPropertyName("audienceGroups")]
        public List<AudienceGroup> audienceGroups { get; set; }

        /// <summary>
        /// 是否有下一頁
        /// </summary>
        [JsonPropertyName("hasNextPage")]
        public bool hasNextPage { get; set; }

        /// <summary>
        /// 總筆數
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int? totalCount { get; set; }
    }
}


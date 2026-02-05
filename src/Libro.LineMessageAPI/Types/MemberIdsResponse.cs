using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// 成員 ID 清單回應
    /// </summary>
    public class MemberIdsResponse
    {
        /// <summary>
        /// 成員 ID 清單
        /// </summary>
        [JsonPropertyName("memberIds")]
        public List<string> memberIds { get; set; }

        /// <summary>
        /// 下一頁 Token
        /// </summary>
        [JsonPropertyName("next")]
        public string next { get; set; }
    }
}


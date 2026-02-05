using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu 批次綁定
    /// </summary>
    public class RichMenuBulkLinkRequest
    {
        /// <summary>
        /// Rich Menu ID
        /// </summary>
        [JsonPropertyName("richMenuId")]
        public string richMenuId { get; set; }

        /// <summary>
        /// 使用者 ID 清單
        /// </summary>
        [JsonPropertyName("userIds")]
        public List<string> userIds { get; set; }
    }
}


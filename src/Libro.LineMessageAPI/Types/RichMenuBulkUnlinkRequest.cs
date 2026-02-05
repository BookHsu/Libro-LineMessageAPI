using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu 批次解除綁定
    /// </summary>
    public class RichMenuBulkUnlinkRequest
    {
        /// <summary>
        /// 使用者 ID 清單
        /// </summary>
        [JsonPropertyName("userIds")]
        public List<string> userIds { get; set; }
    }
}


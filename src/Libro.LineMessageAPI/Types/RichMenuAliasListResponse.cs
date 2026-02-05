using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu Alias 清單回應
    /// </summary>
    public class RichMenuAliasListResponse
    {
        /// <summary>
        /// Alias 清單
        /// </summary>
        [JsonPropertyName("aliases")]
        public List<RichMenuAliasResponse> aliases { get; set; }
    }
}


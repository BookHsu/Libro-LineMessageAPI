using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu 清單回應
    /// </summary>
    public class RichMenuListResponse
    {
        /// <summary>
        /// Rich Menu 清單
        /// </summary>
        [JsonPropertyName("richmenus")]
        public List<RichMenuResponse> richmenus { get; set; }
    }
}


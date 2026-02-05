using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu 建立回應
    /// </summary>
    public class RichMenuIdResponse
    {
        /// <summary>
        /// Rich Menu ID
        /// </summary>
        [JsonPropertyName("richMenuId")]
        public string richMenuId { get; set; }
    }
}


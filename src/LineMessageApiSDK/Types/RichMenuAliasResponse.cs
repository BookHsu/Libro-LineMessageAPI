using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu Alias 回應
    /// </summary>
    public class RichMenuAliasResponse
    {
        /// <summary>
        /// Alias ID
        /// </summary>
        [JsonPropertyName("richMenuAliasId")]
        public string richMenuAliasId { get; set; }

        /// <summary>
        /// Rich Menu ID
        /// </summary>
        [JsonPropertyName("richMenuId")]
        public string richMenuId { get; set; }
    }
}

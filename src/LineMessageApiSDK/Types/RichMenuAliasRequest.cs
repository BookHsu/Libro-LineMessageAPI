using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu Alias 設定
    /// </summary>
    public class RichMenuAliasRequest
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

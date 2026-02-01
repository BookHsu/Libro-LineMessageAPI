using System.Text.Json.Serialization;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 連結 Token 回應
    /// </summary>
    public class LinkTokenResponse
    {
        /// <summary>
        /// Link Token
        /// </summary>
        [JsonPropertyName("linkToken")]
        public string linkToken { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
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


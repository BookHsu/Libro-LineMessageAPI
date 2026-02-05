using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Webhook Endpoint 設定
    /// </summary>
    public class WebhookEndpointRequest
    {
        /// <summary>
        /// Webhook Endpoint
        /// </summary>
        [JsonPropertyName("endpoint")]
        public string endpoint { get; set; }

        /// <summary>
        /// 是否啟用
        /// </summary>
        [JsonPropertyName("active")]
        public bool active { get; set; }
    }
}


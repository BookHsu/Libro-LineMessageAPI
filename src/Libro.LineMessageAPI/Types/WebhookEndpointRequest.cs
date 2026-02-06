using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Webhook 端點 設定
    /// </summary>
    public class WebhookEndpointRequest
    {
        /// <summary>
        /// Webhook 端點
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


namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Webhook Endpoint 回應
    /// </summary>
    public class WebhookEndpointResponse
    {
        /// <summary>
        /// Webhook Endpoint
        /// </summary>
        public string endpoint { get; set; }

        /// <summary>
        /// 是否啟用
        /// </summary>
        public bool active { get; set; }
    }
}

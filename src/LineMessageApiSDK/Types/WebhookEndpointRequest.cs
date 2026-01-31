namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Webhook Endpoint 設定
    /// </summary>
    public class WebhookEndpointRequest
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

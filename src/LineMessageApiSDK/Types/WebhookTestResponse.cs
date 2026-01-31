namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Webhook 測試回應
    /// </summary>
    public class WebhookTestResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 錯誤或訊息
        /// </summary>
        public string message { get; set; }
    }
}

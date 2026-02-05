using Libro.LineMessageApi.Types;

namespace Libro.LineMessageAPI.ExampleApi.Models
{
    /// <summary>
    /// 設定請求
    /// </summary>
    public sealed class LineConfigRequest
    {
        /// <summary>
        /// Channel Access Token
        /// </summary>
        public string ChannelAccessToken { get; set; } = string.Empty;

        /// <summary>
        /// Channel Secret
        /// </summary>
        public string ChannelSecret { get; set; } = string.Empty;

        /// <summary>
        /// Webhook URL
        /// </summary>
        public string WebhookUrl { get; set; } = string.Empty;

        /// <summary>
        /// 是否設定 Webhook Endpoint
        /// </summary>
        public bool SetEndpoint { get; set; } = true;
    }

    /// <summary>
    /// 設定回應
    /// </summary>
    public sealed class LineConfigResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 回應訊息
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 設定狀態
        /// </summary>
        public LineConfigState? Config { get; set; }

        /// <summary>
        /// Bot 基本資訊
        /// </summary>
        public BotInfo? BotInfo { get; set; }

        /// <summary>
        /// Webhook Endpoint 設定
        /// </summary>
        public WebhookEndpointResponse? WebhookEndpoint { get; set; }
    }

    /// <summary>
    /// 設定狀態
    /// </summary>
    public sealed class LineConfigState
    {
        /// <summary>
        /// 是否已設定
        /// </summary>
        public bool Configured { get; set; }

        /// <summary>
        /// Webhook URL
        /// </summary>
        public string WebhookUrl { get; set; } = string.Empty;

        /// <summary>
        /// 最後更新時間 (UTC)
        /// </summary>
        public string UpdatedAtUtc { get; set; } = string.Empty;
    }
}





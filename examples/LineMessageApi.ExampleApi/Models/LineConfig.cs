using System;

namespace LineMessageApi.ExampleApi.Models
{
    /// <summary>
    /// Line 設定資料
    /// </summary>
    public sealed class LineConfig
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
        /// 最後更新時間 (UTC)
        /// </summary>
        public DateTimeOffset UpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
    }
}

using System;

namespace Libro.LineMessageAPI.ExampleApi.Models
{
    /// <summary>
    /// Webhook 事件記錄
    /// </summary>
    public sealed class WebhookEventRecord
    {
        /// <summary>
        /// 事件識別
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 事件類型
        /// </summary>
        public string EventType { get; set; } = string.Empty;

        /// <summary>
        /// 訊息類型
        /// </summary>
        public string MessageType { get; set; } = string.Empty;

        /// <summary>
        /// 來源類型
        /// </summary>
        public string SourceType { get; set; } = string.Empty;

        /// <summary>
        /// 事件摘要
        /// </summary>
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// 接收時間 (UTC)
        /// </summary>
        public DateTimeOffset ReceivedAtUtc { get; set; } = DateTimeOffset.UtcNow;

        /// <summary>
        /// 原始內容
        /// </summary>
        public string RawJson { get; set; } = string.Empty;
    }
}




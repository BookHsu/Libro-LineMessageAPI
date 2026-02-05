using Libro.LineMessageApi.LineMessageObject;

namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>
    /// Broadcast 訊息
    /// </summary>
    public class BroadcastMessage : SendLineMessage
    {
        /// <summary>
        /// 是否關閉通知
        /// </summary>
        public bool notificationDisabled { get; set; }

        /// <summary>
        /// 建立 Broadcast 訊息
        /// </summary>
        public BroadcastMessage(params Message[] msg) : base()
        {
            if (msg != null && msg.Length > 0)
            {
                messages.AddRange(msg);
            }
        }
    }
}


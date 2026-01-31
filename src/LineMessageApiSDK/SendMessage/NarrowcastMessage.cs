using LineMessageApiSDK.LineMessageObject;

namespace LineMessageApiSDK.SendMessage
{
    /// <summary>
    /// Narrowcast 訊息
    /// </summary>
    public class NarrowcastMessage : SendLineMessage
    {
        /// <summary>
        /// 目標設定
        /// </summary>
        public NarrowcastRecipient recipient { get; set; }

        /// <summary>
        /// 是否關閉通知
        /// </summary>
        public bool notificationDisabled { get; set; }

        /// <summary>
        /// 建立 Narrowcast 訊息
        /// </summary>
        public NarrowcastMessage(NarrowcastRecipient recipient, params Message[] msg) : base()
        {
            this.recipient = recipient;
            if (msg != null && msg.Length > 0)
            {
                messages.AddRange(msg);
            }
        }
    }
}

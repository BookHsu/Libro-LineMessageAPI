using Libro.LineMessageApi.LineMessageObject;
using System;

namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>主動推播訊息。</summary>
    public class PushMessage : SendLineMessage
    {
        /// <summary>
        /// 初始化 PushMessage 的新執行個體。
        /// </summary>
        public PushMessage(string ToId) : base()
        {
            to = ToId;
        }

        /// <summary>
        /// 初始化 PushMessage 的新執行個體。
        /// </summary>
        public PushMessage(string ToId, params Message[] msg) : this(ToId)
        {
            if (msg.Length <= 5)
            {
                messages.AddRange(msg); 
            }
            else
            {
                throw new Exception("推播訊息不可大於五");
            }
        }

        /// <summary>接收者 ID。</summary>
        public string to { get; set; }
    }
}

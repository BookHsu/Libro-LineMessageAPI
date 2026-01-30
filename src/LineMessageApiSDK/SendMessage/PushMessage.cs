using LineMessageApiSDK.LineMessageObject;
using System;

namespace LineMessageApiSDK.SendMessage
{
    /// <summary>主動推播訊息</summary>
    public class PushMessage : SendLineMessage
    {
        /// <summary></summary>
        public PushMessage(string ToId) : base()
        {
            to = ToId;
        }

        /// <summary></summary>
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

        /// <summary>to id</summary>
        public string to { get; set; }
    }
}
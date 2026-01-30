using LineMessageApiSDK.LineMessageObject;

namespace LineMessageApiSDK.SendMessage
{
    /// <summary>被動回傳訊息</summary>
    public class ReplyMessage : SendLineMessage
    {
        /// <summary></summary>
        public ReplyMessage(string ReplyToken) : base()
        {
            replyToken = ReplyToken;
        }

        /// <summary></summary>
        public ReplyMessage(string ReplyToken, params Message[] msg) : this(ReplyToken)
        {
            messages.AddRange(msg);
        }

        /// <summary>被動回復Token</summary>
        public string replyToken { get; set; } 
    }
}
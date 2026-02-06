using Libro.LineMessageApi.LineMessageObject;

namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>被動回傳訊息。</summary>
    public class ReplyMessage : SendLineMessage
    {
        /// <summary>
        /// 初始化 ReplyMessage 的新執行個體。
        /// </summary>
        public ReplyMessage(string ReplyToken) : base()
        {
            replyToken = ReplyToken;
        }

        /// <summary>
        /// 初始化 ReplyMessage 的新執行個體。
        /// </summary>
        public ReplyMessage(string ReplyToken, params Message[] msg) : this(ReplyToken)
        {
            messages.AddRange(msg);
        }

        /// <summary>被動回覆Token。</summary>
        public string replyToken { get; set; } 
    }
}

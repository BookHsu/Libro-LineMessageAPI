using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>文字訊息。</summary>
    public class TextMessage : Message
    {
        /// <summary>
        /// 初始化 TextMessage 的新執行個體。
        /// </summary>
        public TextMessage()
        {
            base.type = SendMessageType.text;
        }

        /// <summary>直接傳入文字訊息。</summary>
        /// <param name="message"></param>
        public TextMessage(string message) : this() 
        {
            text = message;
        }

        /// <summary>想說的話，長度不可超過2000。</summary>
        [StringLength(2000, ErrorMessage = "訊息過長")]
        public string text { get; set; }
    }
}

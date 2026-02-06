using Libro.LineMessageApi.LineMessageObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>
    /// 表示 SendLineMessage 類別。
    /// </summary>
    public abstract class SendLineMessage
    {
        /// <summary>
        /// 初始化 SendLineMessage 的新執行個體。
        /// </summary>
        public SendLineMessage()
        {
            this.messages = new List<Message>();
        }

        /// <summary>傳送訊息 最多 5 則。</summary>
        [MaxLength(5, ErrorMessage = "被動回複訊息不可大於五")]
        public List<Message> messages { get; set; }
    } 
}

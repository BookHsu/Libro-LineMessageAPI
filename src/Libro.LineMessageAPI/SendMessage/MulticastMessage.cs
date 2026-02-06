using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.SendMessage
{
    /// <summary>傳送訊息給大量使用者。</summary>
    public class MulticastMessage : SendLineMessage
    {
        /// <summary>
        /// 初始化 MulticastMessage 的新執行個體。
        /// </summary>
        public MulticastMessage() : base()
        {
            to = new List<string>(); 
        }

        /// <summary>上限 150 個 ID，且不可推送至 Room ID 與 Group ID。</summary>
        [MaxLength(150, ErrorMessage = "傳送人數過多")]
        public List<string> to { get; set; }
    }
}

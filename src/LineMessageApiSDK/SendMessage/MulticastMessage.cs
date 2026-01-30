using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.SendMessage
{
    /// <summary>傳送訊息給大量使用者</summary>
    public class MulticastMessage : SendLineMessage
    {
        /// <summary></summary>
        public MulticastMessage() : base()
        {
            to = new List<string>(); 
        }

        /// <summary>上限150個id 不可推送roomid與groupid</summary>
        [MaxLength(150, ErrorMessage = "傳送人數過多")]
        public List<string> to { get; set; }
    }
}
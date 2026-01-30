using LineMessageApiSDK.LineMessageObject;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.SendMessage
{
    /// <summary> </summary>
    public abstract class SendLineMessage
    {
        /// <summary></summary>
        public SendLineMessage()
        {
            this.messages = new List<Message>();
        }

        /// <summary>傳送訊息 max 5</summary>
        [MaxLength(5, ErrorMessage = "被動回複訊息不可大於五")]
        public List<Message> messages { get; set; }
    } 
}
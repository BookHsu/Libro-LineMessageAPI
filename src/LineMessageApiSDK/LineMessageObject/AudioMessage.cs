using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>聲音訊息</summary>
    public class AudioMessage : Message
    {
        private string _originalContentUrl;

        /// <summary></summary>
        public AudioMessage()
        {
            base.type = SendMessageType.audio; 
        }

        /// <summary>毫秒(milliseconds)</summary>
        public int duration { get; set; }

        /// <summary>聲音網址 長度不可超過1000需以https開頭，格式限制m4a檔案大小不可超過10mb，長度必須小於一分鐘</summary>
        [StringLength(1000, ErrorMessage = "網址過長")]
        public string originalContentUrl
        {
            get { return _originalContentUrl; }
            set
            {
                bool flag = value.ToLower().StartsWith("https:");
                if (!flag)
                {
                    throw new Exception("網址需以https開頭");
                }
                else
                {
                    _originalContentUrl = value;
                }
            }
        }
    }
}
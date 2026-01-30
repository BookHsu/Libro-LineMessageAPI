using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>圖片訊息</summary>
    public class ImageMessage : Message
    {
        private string _originalContentUrl;

        private string _previewImageUrl; 

        /// <summary></summary>
        public ImageMessage()
        {
            base.type = SendMessageType.image;
        }

        /// <summary>圖片網址 長度不可超過1000需以https開頭，格式限制jpeg檔案大小不可超過1mb，圖片大小最大1024*1024</summary>
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

        /// <summary>預覽圖片 長度不可超過1000需以https開頭，格式限制jpeg檔案大小不可超過1mb，240*240</summary>
        [StringLength(1000, ErrorMessage = "網址過長")]
        public string previewImageUrl
        {
            get { return _previewImageUrl; }
            set
            {
                bool flag = value.ToLower().StartsWith("https:");
                if (!flag)
                {
                    throw new Exception("網址需以https開頭");
                }
                else
                {
                    _previewImageUrl = value;
                }
            }
        }
    }
}
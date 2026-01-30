using System;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>影片訊息</summary>
    public class VideoMessage : Message
    {
        private string _originalContentUrl;

        private string _previewImageUrl;

        /// <summary></summary>
        public VideoMessage()
        {
            base.type = SendMessageType.video;
        }

        /// <summary>影片網址 長度不可超過1000需以https開頭，格式限制mp4檔案大小不可超過10mb，影片長度不可超過一分鐘</summary>
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
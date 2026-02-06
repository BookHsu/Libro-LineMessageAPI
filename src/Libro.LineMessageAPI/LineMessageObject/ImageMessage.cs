using System;
using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>圖片訊息。</summary>
    public class ImageMessage : Message
    {
        private string _originalContentUrl;

        private string _previewImageUrl; 

        /// <summary>
        /// 初始化 ImageMessage 的新執行個體。
        /// </summary>
        public ImageMessage()
        {
            base.type = SendMessageType.image;
        }

        /// <summary>圖片網址（長度不可超過 1000，需以 HTTPS 開頭；格式為 JPEG，檔案大小不可超過 1 MB，尺寸上限 1024x1024）。</summary>
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

        /// <summary>預覽圖片網址（長度不可超過 1000，需以 HTTPS 開頭；格式為 JPEG，檔案大小不可超過 1 MB，建議尺寸 240x240）。</summary>
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

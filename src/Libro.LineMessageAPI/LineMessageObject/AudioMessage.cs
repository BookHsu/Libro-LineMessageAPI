using System;
using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>聲音訊息。</summary>
    public class AudioMessage : Message
    {
        private string _originalContentUrl;

        /// <summary>
        /// 初始化 AudioMessage 的新執行個體。
        /// </summary>
        public AudioMessage()
        {
            base.type = SendMessageType.audio; 
        }

        /// <summary>毫秒(milliseconds)。</summary>
        public int duration { get; set; }

        /// <summary>聲音網址（長度不可超過 1000，需以 HTTPS 開頭；格式為 M4A，檔案大小不可超過 10 MB，長度需小於 1 分鐘）。</summary>
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.LineMessageObject
{
    public class Area
    {
        /// <summary></summary>
        public int height { get; set; }

        /// <summary></summary>
        public int width { get; set; } 

        /// <summary></summary>
        public int x { get; set; }

        /// <summary></summary>
        public int y { get; set; }
    }

    /// <summary></summary>
    public class BaseSize
    {
        /// <summary></summary>
        public BaseSize()
        {
            height = 1024;
            width = 1024;
        }

        /// <summary></summary>
        public int height { get; set; }

        /// <summary></summary>
        public int width { get; set; }
    }

    /// <summary>新聞訊息</summary>
    public class ImagemapMessage : Message
    {
        /// <summary></summary>
        public ImagemapMessage()
        {
            base.type = SendMessageType.imagemap;
            actions = new List<LineAction>();
            baseSize = new BaseSize();
        }

        /// <summary></summary>
        public List<LineAction> actions { get; set; }

        /// <summary>浮水印文字</summary>
        [StringLength(400, ErrorMessage = "訊息過長")]
        public string altText { get; set; }

        /// <summary></summary>
        public BaseSize baseSize { get; set; }

        /// <summary>顯示圖片連結</summary>
        [StringLength(1000, ErrorMessage = "網址過長")]
        public string baseUrl { get; set; }
    }

    /// <summary></summary>
}
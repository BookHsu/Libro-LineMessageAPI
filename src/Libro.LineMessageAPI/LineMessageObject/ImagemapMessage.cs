using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>
    /// 表示 Area 類別。
    /// </summary>
    public class Area
    {
        /// <summary>
        /// 取得或設定 height。
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 取得或設定 width。
        /// </summary>
        public int width { get; set; } 

        /// <summary>
        /// 取得或設定 x。
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// 取得或設定 y。
        /// </summary>
        public int y { get; set; }
    }

    /// <summary>
    /// 表示 BaseSize 類別。
    /// </summary>
    public class BaseSize
    {
        /// <summary>
        /// 初始化 BaseSize 的新執行個體。
        /// </summary>
        public BaseSize()
        {
            height = 1024;
            width = 1024;
        }

        /// <summary>
        /// 取得或設定 height。
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 取得或設定 width。
        /// </summary>
        public int width { get; set; }
    }

    /// <summary>Imagemap 訊息。</summary>
    public class ImagemapMessage : Message
    {
        /// <summary>
        /// 初始化 ImagemapMessage 的新執行個體。
        /// </summary>
        public ImagemapMessage()
        {
            base.type = SendMessageType.imagemap;
            actions = new List<LineAction>();
            baseSize = new BaseSize();
        }

        /// <summary>
        /// 取得或設定 actions。
        /// </summary>
        public List<LineAction> actions { get; set; }

        /// <summary>浮水印文字。</summary>
        [StringLength(400, ErrorMessage = "訊息過長")]
        public string altText { get; set; }

        /// <summary>
        /// 取得或設定 baseSize。
        /// </summary>
        public BaseSize baseSize { get; set; }

        /// <summary>顯示圖片連結。</summary>
        [StringLength(1000, ErrorMessage = "網址過長")]
        public string baseUrl { get; set; }
    }
}

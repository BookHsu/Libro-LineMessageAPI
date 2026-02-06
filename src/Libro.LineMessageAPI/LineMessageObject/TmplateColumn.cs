using System.Collections.Generic;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>
    /// 表示 TmplateColumn class。
    /// </summary>
    /// <summary>
    /// 表示 TmplateColumn 類別。
    /// </summary>
    public class TmplateColumn
    {
        /// <summary>樣板訊息 Carousel 使用。</summary>
        public TmplateColumn()
        {
            actions = new List<LineAction>();
        }

        /// <summary>最多 3 筆。</summary>
        public List<LineAction> actions { get; set; }

        /// <summary>文字訊息(必填)。</summary> 
        public string text { get; set; }

        /// <summary>縮圖網址（需為 HTTPS，支援 JPEG 或 PNG，檔案大小上限 1 MB，非必填）。</summary>
        public string thumbnailImageUrl { get; set; }

        /// <summary>標題(非必填)。</summary>
        public string title { get; set; }
    }
}

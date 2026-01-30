using System.Collections.Generic;

namespace LineMessageApiSDK.LineMessageObject
{/// <summary></summary>
    public class TmplateColumn
    {
        /// <summary>樣板訊息 Carousel 使用</summary>
        public TmplateColumn()
        {
            actions = new List<LineAction>();
        }

        /// <summary>MAX 3 </summary>
        public List<LineAction> actions { get; set; }

        /// <summary>文字訊息(必填)</summary> 
        public string text { get; set; }

        /// <summary>縮圖網址 https jpeg or png max 1mb (非必填)</summary>
        public string thumbnailImageUrl { get; set; }

        /// <summary>標題(非必填)</summary>
        public string title { get; set; }
    }
}
using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu 資料
    /// </summary>
    public class RichMenuResponse
    {
        /// <summary>
        /// Rich Menu ID
        /// </summary>
        public string richMenuId { get; set; }

        /// <summary>
        /// Rich Menu 名稱
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 是否選取
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// Chat bar 文字
        /// </summary>
        public string chatBarText { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public object size { get; set; }

        /// <summary>
        /// 區域設定
        /// </summary>
        public List<object> areas { get; set; }
    }
}

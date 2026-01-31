using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu 批次綁定
    /// </summary>
    public class RichMenuBulkLinkRequest
    {
        /// <summary>
        /// Rich Menu ID
        /// </summary>
        public string richMenuId { get; set; }

        /// <summary>
        /// 使用者 ID 清單
        /// </summary>
        public List<string> userIds { get; set; }
    }
}

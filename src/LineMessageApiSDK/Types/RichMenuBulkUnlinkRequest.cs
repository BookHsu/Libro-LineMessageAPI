using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu 批次解除綁定
    /// </summary>
    public class RichMenuBulkUnlinkRequest
    {
        /// <summary>
        /// 使用者 ID 清單
        /// </summary>
        public List<string> userIds { get; set; }
    }
}

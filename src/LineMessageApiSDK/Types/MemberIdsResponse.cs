using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// 成員 ID 清單回應
    /// </summary>
    public class MemberIdsResponse
    {
        /// <summary>
        /// 成員 ID 清單
        /// </summary>
        public List<string> memberIds { get; set; }

        /// <summary>
        /// 下一頁 Token
        /// </summary>
        public string next { get; set; }
    }
}

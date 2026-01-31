using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu Alias 清單回應
    /// </summary>
    public class RichMenuAliasListResponse
    {
        /// <summary>
        /// Alias 清單
        /// </summary>
        public List<RichMenuAliasResponse> aliases { get; set; }
    }
}

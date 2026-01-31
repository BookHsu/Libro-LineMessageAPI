using System.Collections.Generic;

namespace LineMessageApiSDK.Types
{
    /// <summary>
    /// Rich Menu 清單回應
    /// </summary>
    public class RichMenuListResponse
    {
        /// <summary>
        /// Rich Menu 清單
        /// </summary>
        public List<RichMenuResponse> richmenus { get; set; }
    }
}

using System.Collections.Generic;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>Line物件集合</summary>
    public class LineReceivedMsg
    {
        /// <summary>事件集合</summary>
        public List<LineEvents> events { get; set; } 
    }
}
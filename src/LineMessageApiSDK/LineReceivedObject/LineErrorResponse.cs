using System.Collections.Generic;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary></summary>
    public class LineErrorResponse
    {
        /// <summary></summary>
        public List<ErrorDetail> details { get; set; }

        /// <summary></summary>
        public string message { get; set; } 
    }
}
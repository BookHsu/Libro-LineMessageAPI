using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>LINE 回傳錯誤回應</summary>
    public class LineErrorResponse
    {
        /// <summary>錯誤細節</summary>
        [JsonPropertyName("details")]
        public List<ErrorDetail> details { get; set; }

        /// <summary>錯誤訊息</summary>
        [JsonPropertyName("message")]
        public string message { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>Line 回傳錯誤訊息</summary>
    public class ErrorDetail
    {
        /// <summary>錯誤訊息</summary>
        [JsonPropertyName("message")]
        public string message { get; set; }

        /// <summary>對應欄位</summary>
        [JsonPropertyName("property")]
        public string property { get; set; }
    }
}

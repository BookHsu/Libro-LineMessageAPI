using System;
namespace LineMessageApiSDK.LineReceivedObject
{
    public class LineParams
    {
        /// <summary>
        /// 只有在Datetime模式下出現
        /// 格式： yyyy-MM-ddTHH:mm
        /// </summary>
        public DateTime? datetime { get; set; }
        /// <summary>
        /// 只有在date模式下出現
        /// 格式： yyyy-MM-dd
        /// </summary>
        public DateTime? date { get; set; }
        /// <summary>
        /// 只有在時間模式下出現
        /// 格式： HH:mm
        /// </summary>
        public string time { get; set; }
    }
}
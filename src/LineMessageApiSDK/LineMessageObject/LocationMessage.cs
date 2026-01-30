using System.ComponentModel.DataAnnotations;

namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>地理位置訊息</summary>
    public class LocationMessage : Message
    {
        /// <summary></summary>
        public LocationMessage()
        {
            base.type = SendMessageType.location;
        }

        /// <summary>地址</summary> 
        [StringLength(1000, ErrorMessage = "訊息過長")]
        public string address { get; set; }

        /// <summary>緯度</summary>
        public double latitude { get; set; }

        /// <summary>經度</summary>
        public double longitude { get; set; }

        /// <summary>標題</summary>
        [StringLength(1000, ErrorMessage = "訊息過長")]
        public string title { get; set; }
    }
}
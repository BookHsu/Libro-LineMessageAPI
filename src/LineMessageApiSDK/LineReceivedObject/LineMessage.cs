namespace LineMessageApiSDK.LineReceivedObject
{
    /// <summary>訊息基底類別</summary>
    public class LineMessage
    {
        /// <summary>傳送地理位置 地址</summary>
        public string address { get; set; }

        /// <summary>傳送檔案 檔案名稱</summary>
        public string fileName { get; set; }

        /// <summary>傳送檔案 檔案大小(bytes)</summary>
        public long fileSize { get; set; }

        /// <summary>訊息編號</summary>
        public string id { get; set; } 

        /// <summary>傳送地理位置 緯度</summary>
        public double latitude { get; set; }

        /// <summary>傳送地理位置 經度</summary>
        public double longitude { get; set; }

        /// <summary>傳送貼圖 對應https://devdocs.line.me/files/sticker_list.pdf 中的STKPKGID</summary>
        public string packageId { get; set; }

        /// <summary>傳送貼圖 對應https://devdocs.line.me/files/sticker_list.pdf 中的STKGID</summary>
        public string stickerId { get; set; }

        /// <summary>文字訊息內容</summary>
        public string text { get; set; }

        /// <summary>傳送地理位置 標題</summary>
        public string title { get; set; }

        /// <summary>訊息類型</summary>
        public MessageType type { get; set; }
    }
}
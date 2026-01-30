namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>貼圖</summary>
    public class StickerMessage : Message
    {
        /// <summary></summary>
        public StickerMessage()
        {
            base.type = SendMessageType.sticker;
        }

        /// <summary></summary>
        public StickerMessage(int IpackageId, int IstickerId) : this()
        {
            packageId = IpackageId.ToString(); 
            stickerId = IstickerId.ToString();
        }

        /// <summary></summary>
        public StickerMessage(string spackageId, string sstickerId) : this()
        {
            packageId = spackageId.ToString();
            stickerId = sstickerId.ToString();
        }

        /// <summary>傳送貼圖 對應https://devdocs.line.me/files/sticker_list.pdf 中的STKPKGID</summary>
        public string packageId { get; set; }

        /// <summary>傳送貼圖 對應https://devdocs.line.me/files/sticker_list.pdf 中的STKGID</summary>
        public string stickerId { get; set; }
    }
}
namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>貼圖。</summary>
    public class StickerMessage : Message
    {
        /// <summary>
        /// 初始化 StickerMessage 的新執行個體。
        /// </summary>
        public StickerMessage()
        {
            base.type = SendMessageType.sticker;
        }

        /// <summary>
        /// 初始化 StickerMessage 的新執行個體。
        /// </summary>
        public StickerMessage(int IpackageId, int IstickerId) : this()
        {
            packageId = IpackageId.ToString(); 
            stickerId = IstickerId.ToString();
        }

        /// <summary>
        /// 初始化 StickerMessage 的新執行個體。
        /// </summary>
        public StickerMessage(string spackageId, string sstickerId) : this()
        {
            packageId = spackageId.ToString();
            stickerId = sstickerId.ToString();
        }

        /// <summary>傳送貼圖時對應 https://devdocs.line.me/files/sticker_list.pdf 中的 STKPKGID。</summary>
        public string packageId { get; set; }

        /// <summary>傳送貼圖時對應 https://devdocs.line.me/files/sticker_list.pdf 中的 STKGID。</summary>
        public string stickerId { get; set; }
    }
}

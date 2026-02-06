using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.LineReceivedObject
{
    /// <summary>訊息物件。</summary>
    public class LineMessage
    {
        /// <summary>訊息 ID。</summary>
        [JsonPropertyName("id")]
        public string id { get; set; }

        /// <summary>訊息類型。</summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MessageType type { get; set; }

        /// <summary>文字訊息內容。</summary>
        [JsonPropertyName("text")]
        public string text { get; set; }

        /// <summary>表情符號資訊。</summary>
        [JsonPropertyName("emojis")]
        public List<LineEmoji> emojis { get; set; }

        /// <summary>提及資訊。</summary>
        [JsonPropertyName("mention")]
        public LineMention mention { get; set; }

        /// <summary>引用用 Token。</summary>
        [JsonPropertyName("quoteToken")]
        public string quoteToken { get; set; }

        /// <summary>引用的訊息 ID。</summary>
        [JsonPropertyName("quotedMessageId")]
        public string quotedMessageId { get; set; }

        /// <summary>內容提供者。</summary>
        [JsonPropertyName("contentProvider")]
        public LineMessageContentProvider contentProvider { get; set; }

        /// <summary>多張圖片集合資訊。</summary>
        [JsonPropertyName("imageSet")]
        public LineImageSet imageSet { get; set; }

        /// <summary>影片/音訊長度（毫秒）</summary>
        [JsonPropertyName("duration")]
        public long? duration { get; set; }

        /// <summary>檔案名稱。</summary>
        [JsonPropertyName("fileName")]
        public string fileName { get; set; }

        /// <summary>檔案大小（bytes）</summary>
        [JsonPropertyName("fileSize")]
        public long? fileSize { get; set; }

        /// <summary>地點標題。</summary>
        [JsonPropertyName("title")]
        public string title { get; set; }

        /// <summary>地點地址。</summary>
        [JsonPropertyName("address")]
        public string address { get; set; }

        /// <summary>緯度。</summary>
        [JsonPropertyName("latitude")]
        public double? latitude { get; set; }

        /// <summary>經度。</summary>
        [JsonPropertyName("longitude")]
        public double? longitude { get; set; }

        /// <summary>貼圖包 ID。</summary>
        [JsonPropertyName("packageId")]
        public string packageId { get; set; }

        /// <summary>貼圖 ID。</summary>
        [JsonPropertyName("stickerId")]
        public string stickerId { get; set; }

        /// <summary>貼圖資源類型。</summary>
        [JsonPropertyName("stickerResourceType")]
        public string stickerResourceType { get; set; }

        /// <summary>貼圖關鍵字。</summary>
        [JsonPropertyName("keywords")]
        public List<string> keywords { get; set; }
    }
}


using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// Rich Menu 資料
    /// </summary>
    public class RichMenuResponse
    {
        /// <summary>
        /// Rich Menu ID
        /// </summary>
        [JsonPropertyName("richMenuId")]
        public string richMenuId { get; set; }

        /// <summary>
        /// Rich Menu 名稱
        /// </summary>
        [JsonPropertyName("name")]
        public string name { get; set; }

        /// <summary>
        /// 是否選取
        /// </summary>
        [JsonPropertyName("selected")]
        public bool selected { get; set; }

        /// <summary>
        /// Chat bar 文字
        /// </summary>
        [JsonPropertyName("chatBarText")]
        public string chatBarText { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        [JsonPropertyName("size")]
        public RichMenuSize size { get; set; }

        /// <summary>
        /// 區域設定
        /// </summary>
        [JsonPropertyName("areas")]
        public List<RichMenuArea> areas { get; set; }
    }

    /// <summary>
    /// Rich Menu 大小
    /// </summary>
    public class RichMenuSize
    {
        /// <summary>寬度</summary>
        [JsonPropertyName("width")]
        public int width { get; set; }

        /// <summary>高度</summary>
        [JsonPropertyName("height")]
        public int height { get; set; }
    }

    /// <summary>
    /// Rich Menu 區域
    /// </summary>
    public class RichMenuArea
    {
        /// <summary>區域範圍</summary>
        [JsonPropertyName("bounds")]
        public RichMenuBounds bounds { get; set; }

        /// <summary>動作</summary>
        [JsonPropertyName("action")]
        public RichMenuAction action { get; set; }
    }

    /// <summary>
    /// Rich Menu 區域範圍
    /// </summary>
    public class RichMenuBounds
    {
        /// <summary>X 座標</summary>
        [JsonPropertyName("x")]
        public int x { get; set; }

        /// <summary>Y 座標</summary>
        [JsonPropertyName("y")]
        public int y { get; set; }

        /// <summary>寬度</summary>
        [JsonPropertyName("width")]
        public int width { get; set; }

        /// <summary>高度</summary>
        [JsonPropertyName("height")]
        public int height { get; set; }
    }

    /// <summary>
    /// Rich Menu 動作
    /// </summary>
    public class RichMenuAction
    {
        /// <summary>動作類型</summary>
        [JsonPropertyName("type")]
        public string type { get; set; }

        /// <summary>標籤</summary>
        [JsonPropertyName("label")]
        public string label { get; set; }

        /// <summary>文字</summary>
        [JsonPropertyName("text")]
        public string text { get; set; }

        /// <summary>URI</summary>
        [JsonPropertyName("uri")]
        public string uri { get; set; }

        /// <summary>Postback data</summary>
        [JsonPropertyName("data")]
        public string data { get; set; }

        /// <summary>顯示文字</summary>
        [JsonPropertyName("displayText")]
        public string displayText { get; set; }

        /// <summary>輸入選項</summary>
        [JsonPropertyName("inputOption")]
        public string inputOption { get; set; }

        /// <summary>填入文字</summary>
        [JsonPropertyName("fillInText")]
        public string fillInText { get; set; }

        /// <summary>Rich Menu Alias ID</summary>
        [JsonPropertyName("richMenuAliasId")]
        public string richMenuAliasId { get; set; }
    }
}


using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Libro.LineMessageApi.LineMessageObject
{
    /// <summary>
    /// 表示 樣板 類別。
    /// </summary>
    public class Template
    {
        /// <summary>樣板訊息。</summary>
        public Template()
        {
            actions = new List<LineAction>();
            columns = new List<TmplateColumn>(); 
        }

        /// <summary>樣板訊息。</summary>
        /// <param name="templateType">要傳送的樣板訊息類型</param>
        public Template(TemplateType templateType) : this()
        {
            type = templateType;
        }

        /// <summary>用於type button(最多 4 筆) confirm(最多 2 筆)。</summary>
        public List<LineAction> actions { get; set; }

        /// <summary>用於type Column(最多 5 筆)。</summary>
        public List<TmplateColumn> columns { get; set; }

        /// <summary>訊息(用於 type button confirm)。</summary>
        public string text { get; set; }

        /// <summary>縮圖網址(用於type button)。</summary>
        public string thumbnailImageUrl { get; set; }

        /// <summary>標題(用於type button)。</summary>
        public string title { get; set; }

        /// <summary>樣板訊息類型。</summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TemplateType type { get; set; }
    }
}


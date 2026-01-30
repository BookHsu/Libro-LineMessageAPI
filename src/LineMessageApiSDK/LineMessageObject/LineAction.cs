namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary>用於imagempa 與 template物件</summary>
    public class LineAction
    {
        /// <summary></summary>
        public LineAction()
        {
            area = new Area();
        }

        /// <summary></summary>
        /// <param name="actionType"></param>
        public LineAction(ActionType actionType) 
        {
            type = actionType;
        }

        /// <summary>imagemap</summary>
        public Area area { get; set; }

        /// <summary>Template</summary>
        public string data { get; set; }

        /// <summary>Template</summary>
        public string label { get; set; }

        /// <summary>連結網址 imagemap</summary>
        public string linkUri { get; set; }

        /// <summary>imagemap Template</summary>
        public string text { get; set; }

        /// <summary></summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ActionType type { get; set; }

        /// <summary>Template</summary>
        public string uri { get; set; }

        /// <summary>Datetime Picker</summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DateTimePickerType mode { get; set; }

        /// <summary>
        /// 日期或時間的初始值
        /// </summary>
        public string initial { get; set; }
        /// <summary>
        /// 日期或時間的最大值
        /// </summary>
        public string max { get; set; }
        /// <summary>
        /// 日期或時間的最小值
        /// </summary>
        public string min { get; set; }
    }
}
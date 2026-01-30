namespace LineMessageApiSDK.LineMessageObject
{
    /// <summary></summary>
    public class TemplateMessage : Message
    {
        /// <summary>樣板訊息物件</summary>
        public TemplateMessage()
        {
            base.type = SendMessageType.template;
        }

        /// <summary>發送樣板訊息物件</summary>
        /// <param name="TemplateType">直接指定樣板訊息類型</param>
        public TemplateMessage(TemplateType TemplateType) : this() 
        {
            template = new Template(TemplateType);
        }

        /// <summary>替代訊息(必填)</summary>
        public string altText { get; set; }

        /// <summary>樣板訊息(必填)</summary>
        public Template template { get; set; }
    }
}
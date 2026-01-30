namespace LineMessageApiSDK
{
    /// <summary>動作類型</summary>
    public enum ActionType
    {
        /// <summary>使用者點擊後打開瀏覽器</summary>
        uri,

        /// <summary>使用者點及後傳送設定好的文字訊息視為使用者傳送的訊息</summary>
        message,

        /// <summary>點擊後傳送postback event</summary>
        postback,
        /// <summary>dateTimePucker Action</summary>
        datetimepicker
    }

    /// <summary></summary>
    public enum BeaconType
    {
        /// <summary></summary>
        enter, 

        /// <summary></summary>
        leave,

        /// <summary></summary>
        banner
    }

    /// <summary>line 主動推播 事件類型</summary>
    public enum EventType
    {
        /// <summary>訊息</summary>
        message,

        /// <summary>加入好友</summary>
        follow,

        /// <summary>刪除或封鎖好友</summary>
        unfollow,

        /// <summary>加入群組或多方對話</summary>
        join,

        /// <summary>離開群組或多方對話</summary>
        leave,

        /// <summary>發送樣板訊息後使用者動作推播，可以使用reply message回復</summary>
        postback,

        /// <summary>使用者點選廣告Banner後推播訊息，可以使用reply message回復</summary>
        beacon,
    }

    /// <summary>訊息類型</summary>
    public enum MessageType
    {
        /// <summary>文字訊息</summary>
        text,

        /// <summary>圖片訊息</summary>
        image,

        /// <summary>影片訊息</summary>
        video,

        /// <summary>聲音</summary>
        audio,

        /// <summary>檔案</summary>
        file,

        /// <summary>所在位置</summary>
        location,

        /// <summary>貼圖</summary>
        sticker,
    }

    /// <summary></summary>
    public enum PostMessageType
    {
        /// <summary>回復訊息</summary>
        Reply,

        /// <summary>主動推播</summary>
        Push,

        /// <summary>主動堆播給多位使用者</summary>
        Multicast
    }

  
    /// <summary>發送訊息類型</summary>
    public enum SendMessageType
    {
        /// <summary>文字</summary>
        text,

        /// <summary>圖片</summary>
        image,

        /// <summary>影片</summary>
        video,

        /// <summary>聲音</summary>
        audio,

        /// <summary>地理位置</summary>
        location,

        /// <summary>貼圖</summary>
        sticker,

        /// <summary>新聞訊息</summary>
        imagemap,

        /// <summary>樣板訊息</summary>
        template
    }

    /// <summary>訊息來源</summary>
    public enum SourceType
    {
        /// <summary>使用者</summary>
        user,

        /// <summary>群組</summary>
        group,

        /// <summary>多方對談</summary>
        room
    }

    /// <summary> </summary>
    public enum TemplateType
    {
        /// <summary>按鈕</summary>
        buttons,

        /// <summary>確認</summary>
        confirm,

        /// <summary></summary>
        carousel
    }
    /// <summary>
     /// 日期ACTION使用 templtetype應為Button
     /// </summary>
    public enum DateTimePickerType
    {
        /// <summary>
        /// date mode
        /// </summary>
        date,
        /// <summary>
        /// time mode
        /// </summary>
        time,
        /// <summary>
        /// datetime mode
        /// </summary>
        datetime
    }
}
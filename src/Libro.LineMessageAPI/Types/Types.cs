namespace Libro.LineMessageApi
{
    /// <summary>動作類型。</summary>
    public enum ActionType
    {
        /// <summary>使用者點擊後打開瀏覽器。</summary>
        uri,

        /// <summary>使用者點擊後傳送設定好的文字訊息視為使用者傳送的訊息。</summary>
        message,

        /// <summary>點擊後傳送postback event。</summary>
        postback,
        /// <summary>DateTime Picker 動作。</summary>
        datetimepicker
    }

    /// <summary>
    /// 表示 BeaconType 列舉。
    /// </summary>
    public enum BeaconType
    {
        /// <summary>
        /// 列舉成員。
        /// </summary>
        enter, 

        /// <summary>
        /// 列舉成員。
        /// </summary>
        leave,

        /// <summary>
        /// 列舉成員。
        /// </summary>
        banner,

        /// <summary>
        /// 列舉成員。
        /// </summary>
        stay
    }

    /// <summary>LINE 主動推播事件類型。</summary>
    public enum EventType
    {
        /// <summary>訊息。</summary>
        message,

        /// <summary>加入好友。</summary>
        follow,

        /// <summary>刪除或封鎖好友。</summary>
        unfollow,

        /// <summary>加入群組或多方對話。</summary>
        join,

        /// <summary>離開群組或多方對話。</summary>
        leave,

        /// <summary>群組或多方對話成員加入。</summary>
        memberJoined,

        /// <summary>群組或多方對話成員離開。</summary>
        memberLeft,

        /// <summary>發送樣板訊息後的使用者動作推播事件，可使用 Reply Message 回覆。</summary>
        postback,

        /// <summary>影片播放完成事件。</summary>
        videoPlayComplete,

        /// <summary>使用者點選廣告 Banner 後的推播事件，可使用 Reply Message 回覆。</summary>
        beacon,

        /// <summary>帳號連結事件。</summary>
        accountLink,

        /// <summary>LINE Things 事件。</summary>
        things,

        /// <summary>使用者收回訊息事件。</summary>
        unsend,
    }

    /// <summary>訊息類型。</summary>
    public enum MessageType
    {
        /// <summary>文字訊息。</summary>
        text,

        /// <summary>圖片訊息。</summary>
        image,

        /// <summary>影片訊息。</summary>
        video,

        /// <summary>聲音。</summary>
        audio,

        /// <summary>檔案。</summary>
        file,

        /// <summary>所在位置。</summary>
        location,

        /// <summary>貼圖。</summary>
        sticker,
    }

    /// <summary>
    /// 表示 PostMessageType 列舉。
    /// </summary>
    public enum PostMessageType
    {
        /// <summary>回覆訊息。</summary>
        Reply,

        /// <summary>主動推播。</summary>
        Push,

        /// <summary>主動推播給多位使用者。</summary>
        Multicast
    }

  
    /// <summary>發送訊息類型。</summary>
    public enum SendMessageType
    {
        /// <summary>文字。</summary>
        text,

        /// <summary>圖片。</summary>
        image,

        /// <summary>影片。</summary>
        video,

        /// <summary>聲音。</summary>
        audio,

        /// <summary>地理位置。</summary>
        location,

        /// <summary>貼圖。</summary>
        sticker,

        /// <summary>Imagemap 訊息。</summary>
        imagemap,

        /// <summary>樣板訊息。</summary>
        template
    }

    /// <summary>訊息來源。</summary>
    public enum SourceType
    {
        /// <summary>使用者。</summary>
        user,

        /// <summary>群組。</summary>
        group,

        /// <summary>多人對話。</summary>
        room
    }

    /// <summary>
    /// 表示 樣板Type 列舉。
    /// </summary>
    public enum TemplateType
    {
        /// <summary>按鈕。</summary>
        buttons,

        /// <summary>確認。</summary>
        confirm,

        /// <summary>
        /// 列舉成員。
        /// </summary>
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

    /// <summary>Bot 聊天模式。</summary>
    public enum ChatMode
    {
        /// <summary>聊天模式。</summary>
        chat,

        /// <summary>Bot 模式。</summary>
        bot
    }

    /// <summary>已讀模式。</summary>
    public enum MarkAsReadMode
    {
        /// <summary>自動已讀。</summary>
        auto,

        /// <summary>手動已讀。</summary>
        manual
    }
}


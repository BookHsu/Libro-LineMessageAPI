using LineMessageApiSDK.Services;

namespace LineMessageApiSDK
{
    /// <summary>
    /// 可選模組化 SDK 容器
    /// </summary>
    public class LineSdk
    {
        /// <summary>
        /// Webhook 驗證模組（未啟用時為 null）
        /// </summary>
        public IWebhookService Webhook { get; }

        /// <summary>
        /// 訊息模組（未啟用時為 null）
        /// </summary>
        public IMessageService Messages { get; }

        /// <summary>
        /// 使用者與成員檔案模組（未啟用時為 null）
        /// </summary>
        public IProfileService Profiles { get; }

        /// <summary>
        /// 群組或多人對話模組（未啟用時為 null）
        /// </summary>
        public IGroupService Groups { get; }

        /// <summary>
        /// 建立 LineSdk
        /// </summary>
        /// <param name="webhook">Webhook 模組</param>
        /// <param name="messages">訊息模組</param>
        /// <param name="profiles">檔案模組</param>
        /// <param name="groups">群組模組</param>
        internal LineSdk(
            IWebhookService webhook,
            IMessageService messages,
            IProfileService profiles,
            IGroupService groups)
        {
            // 指定啟用的模組
            Webhook = webhook;
            Messages = messages;
            Profiles = profiles;
            Groups = groups;
        }
    }
}

using Libro.LineMessageApi.Services;

namespace Libro.LineMessageApi
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
        /// Webhook 端點 管理模組（未啟用時為 null）
        /// </summary>
        public IWebhookEndpointService WebhookEndpoints { get; }

        /// <summary>
        /// Bot 與群組/對話資訊模組（未啟用時為 null）
        /// </summary>
        public IBotService Bot { get; }

        /// <summary>
        /// Broadcast / Narrowcast 模組（未啟用時為 null）
        /// </summary>
        public IBroadcastService Broadcast { get; }

        /// <summary>
        /// 訊息驗證模組（未啟用時為 null）
        /// </summary>
        public IMessageValidationService MessageValidation { get; }

        /// <summary>
        /// Rich Menu 模組（未啟用時為 null）
        /// </summary>
        public IRichMenuService RichMenu { get; }

        /// <summary>
        /// Insights 模組（未啟用時為 null）
        /// </summary>
        public IInsightService Insight { get; }

        /// <summary>
        /// Audience 模組（未啟用時為 null）
        /// </summary>
        public IAudienceService Audience { get; }

        /// <summary>
        /// Account Link 模組（未啟用時為 null）
        /// </summary>
        public IAccountLinkService AccountLink { get; }

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
        /// <param name="webhookEndpoints">Webhook 端點 模組</param>
        /// <param name="bot">Bot 模組</param>
        /// <param name="broadcast">Broadcast/Narrowcast 模組</param>
        /// <param name="messageValidation">訊息驗證模組</param>
        /// <param name="richMenu">Rich Menu 模組</param>
        /// <param name="insight">Insights 模組</param>
        /// <param name="audience">Audience 模組</param>
        /// <param name="accountLink">Account Link 模組</param>
        /// <param name="messages">訊息模組</param>
        /// <param name="profiles">檔案模組</param>
        /// <param name="groups">群組模組</param>
        internal LineSdk(
            IWebhookService webhook,
            IWebhookEndpointService webhookEndpoints,
            IBotService bot,
            IBroadcastService broadcast,
            IMessageValidationService messageValidation,
            IRichMenuService richMenu,
            IInsightService insight,
            IAudienceService audience,
            IAccountLinkService accountLink,
            IMessageService messages,
            IProfileService profiles,
            IGroupService groups)
        {
            // 指定啟用的模組
            Webhook = webhook;
            WebhookEndpoints = webhookEndpoints;
            Bot = bot;
            Broadcast = broadcast;
            MessageValidation = messageValidation;
            RichMenu = richMenu;
            Insight = insight;
            Audience = audience;
            AccountLink = accountLink;
            Messages = messages;
            Profiles = profiles;
            Groups = groups;
        }
    }
}


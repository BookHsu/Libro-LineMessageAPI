using LineMessageApiSDK.Http;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.Services;
using System.Net.Http;

namespace LineMessageApiSDK
{
    /// <summary>
    /// Line SDK 模組化建構器
    /// </summary>
    public class LineSdkBuilder
    {
        private readonly string channelAccessToken;
        private IJsonSerializer serializer;
        private HttpClient httpClient;
        private IHttpClientProvider httpClientProvider;
        private bool useWebhook = true;
        private bool useWebhookEndpoints;
        private bool useBot;
        private bool useBroadcast;
        private bool useMessageValidation;
        private bool useRichMenu;
        private bool useInsight;
        private bool useAudience;
        private bool useAccountLink;
        private bool useMessages;
        private bool useProfiles;
        private bool useGroups;

        /// <summary>
        /// 建立 Builder
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        public LineSdkBuilder(string channelAccessToken)
        {
            // 設定必要的 Token
            this.channelAccessToken = channelAccessToken;
        }

        /// <summary>
        /// 指定 JSON 序列化器
        /// </summary>
        /// <param name="serializer">序列化器</param>
        /// <returns>Builder</returns>
        public LineSdkBuilder WithSerializer(IJsonSerializer serializer)
        {
            // 設定外部注入的序列化器
            this.serializer = serializer;
            return this;
        }

        /// <summary>
        /// 指定 HttpClient
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <returns>Builder</returns>
        public LineSdkBuilder WithHttpClient(HttpClient httpClient)
        {
            // 設定外部注入的 HttpClient
            this.httpClient = httpClient;
            return this;
        }

        /// <summary>
        /// 指定 HttpClient 提供者
        /// </summary>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        /// <returns>Builder</returns>
        public LineSdkBuilder WithHttpClientProvider(IHttpClientProvider httpClientProvider)
        {
            // 設定外部注入的 HttpClient 提供者
            this.httpClientProvider = httpClientProvider;
            return this;
        }

        /// <summary>
        /// 啟用 Webhook 模組（預設已啟用）
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseWebhook()
        {
            // 僅載入 Webhook 功能
            useWebhook = true;
            return this;
        }

        /// <summary>
        /// 關閉 Webhook 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder DisableWebhook()
        {
            // 停用 Webhook 功能
            useWebhook = false;
            return this;
        }

        /// <summary>
        /// 啟用 Webhook Endpoint 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseWebhookEndpoints()
        {
            // 啟用 Webhook Endpoint 管理功能
            useWebhookEndpoints = true;
            return this;
        }

        /// <summary>
        /// 啟用 Bot 與群組/對話資訊模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseBot()
        {
            // 啟用 Bot 與群組/對話資訊功能
            useBot = true;
            return this;
        }

        /// <summary>
        /// 啟用 Broadcast / Narrowcast 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseBroadcast()
        {
            // 啟用 Broadcast / Narrowcast 功能
            useBroadcast = true;
            return this;
        }

        /// <summary>
        /// 啟用訊息驗證模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseMessageValidation()
        {
            // 啟用訊息驗證功能
            useMessageValidation = true;
            return this;
        }

        /// <summary>
        /// 啟用 Rich Menu 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseRichMenu()
        {
            // 啟用 Rich Menu 功能
            useRichMenu = true;
            return this;
        }

        /// <summary>
        /// 啟用 Insights 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseInsight()
        {
            // 啟用 Insights 功能
            useInsight = true;
            return this;
        }

        /// <summary>
        /// 啟用 Audience 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseAudience()
        {
            // 啟用 Audience 功能
            useAudience = true;
            return this;
        }

        /// <summary>
        /// 啟用 Account Link 模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseAccountLink()
        {
            // 啟用 Account Link 功能
            useAccountLink = true;
            return this;
        }

        /// <summary>
        /// 啟用訊息模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseMessages()
        {
            // 僅載入訊息功能
            useMessages = true;
            return this;
        }

        /// <summary>
        /// 啟用檔案模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseProfiles()
        {
            // 僅載入使用者/成員檔案功能
            useProfiles = true;
            return this;
        }

        /// <summary>
        /// 啟用群組模組
        /// </summary>
        /// <returns>Builder</returns>
        public LineSdkBuilder UseGroups()
        {
            // 僅載入群組功能
            useGroups = true;
            return this;
        }

        /// <summary>
        /// 建立 LineSdk
        /// </summary>
        /// <returns>LineSdk</returns>
        public LineSdk Build()
        {
            // 建立共用 Context（序列化器預設為 System.Text.Json）
            var context = new LineApiContext(
                channelAccessToken,
                serializer ?? new SystemTextJsonSerializer(),
                httpClient,
                httpClientProvider);

            // 依需求載入模組
            IWebhookService webhook = useWebhook ? new WebhookService() : null;
            IWebhookEndpointService webhookEndpoints = useWebhookEndpoints ? new WebhookEndpointService(context) : null;
            IBotService bot = useBot ? new BotService(context) : null;
            IBroadcastService broadcast = useBroadcast ? new BroadcastService(context) : null;
            IMessageValidationService messageValidation = useMessageValidation ? new MessageValidationService(context) : null;
            IRichMenuService richMenu = useRichMenu ? new RichMenuService(context) : null;
            IInsightService insight = useInsight ? new InsightService(context) : null;
            IAudienceService audience = useAudience ? new AudienceService(context) : null;
            IAccountLinkService accountLink = useAccountLink ? new AccountLinkService(context) : null;
            IMessageService messages = useMessages ? new MessageService(context) : null;
            IProfileService profiles = useProfiles ? new ProfileService(context) : null;
            IGroupService groups = useGroups ? new GroupService(context) : null;

            return new LineSdk(webhook, webhookEndpoints, bot, broadcast, messageValidation, richMenu, insight, audience, accountLink, messages, profiles, groups);
        }
    }
}

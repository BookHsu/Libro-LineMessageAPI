using LineMessageApiSDK.Method;
using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Insights 服務
    /// </summary>
    internal class InsightService : IInsightService
    {
        private readonly LineApiContext context;
        private readonly InsightApi api;

        internal InsightService(LineApiContext context)
        {
            this.context = context;
            api = new InsightApi(context.Serializer, context.HttpClientProvider);
        }

        public MessageDeliveryInsightResponse GetMessageDelivery(string date)
        {
            return api.GetMessageDelivery(context.ChannelAccessToken, date);
        }

        public Task<MessageDeliveryInsightResponse> GetMessageDeliveryAsync(string date)
        {
            return api.GetMessageDeliveryAsync(context.ChannelAccessToken, date);
        }

        public FollowerInsightResponse GetFollowers()
        {
            return api.GetFollowers(context.ChannelAccessToken);
        }

        public Task<FollowerInsightResponse> GetFollowersAsync()
        {
            return api.GetFollowersAsync(context.ChannelAccessToken);
        }

        public DemographicInsightResponse GetDemographic()
        {
            return api.GetDemographic(context.ChannelAccessToken);
        }

        public Task<DemographicInsightResponse> GetDemographicAsync()
        {
            return api.GetDemographicAsync(context.ChannelAccessToken);
        }
    }
}

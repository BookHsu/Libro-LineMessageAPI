using LineMessageApiSDK.Types;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// Insights 服務
    /// </summary>
    public interface IInsightService
    {
        MessageDeliveryInsightResponse GetMessageDelivery(string date);
        Task<MessageDeliveryInsightResponse> GetMessageDeliveryAsync(string date);
        FollowerInsightResponse GetFollowers();
        Task<FollowerInsightResponse> GetFollowersAsync();
        DemographicInsightResponse GetDemographic();
        Task<DemographicInsightResponse> GetDemographicAsync();
    }
}

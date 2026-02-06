using Libro.LineMessageApi.Method;
using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供 Insight 統計資料查詢服務。
    /// </summary>
    internal class InsightService : IInsightService
    {
        private readonly LineApiContext context;
        private readonly InsightApi api;

        internal InsightService(LineApiContext context)
        {
            this.context = context;
            api = new InsightApi(context.Serializer, context.HttpClientProvider, context.SyncAdapterFactory);
        }

        /// <summary>
        /// 取得訊息投遞統計資料。
        /// </summary>
        /// <param name="date">查詢日期（格式：yyyyMMdd）。</param>
        /// <returns>訊息投遞統計資料。</returns>
        public MessageDeliveryInsightResponse GetMessageDelivery(string date)
        {
            return api.GetMessageDelivery(context.ChannelAccessToken, date);
        }

        /// <summary>
        /// 取得訊息投遞統計資料。
        /// </summary>
        /// <param name="date">查詢日期（格式：yyyyMMdd）。</param>
        /// <returns>訊息投遞統計資料。</returns>
        public Task<MessageDeliveryInsightResponse> GetMessageDeliveryAsync(string date)
        {
            return api.GetMessageDeliveryAsync(context.ChannelAccessToken, date);
        }

        /// <summary>
        /// 取得追蹤者統計資料。
        /// </summary>
        /// <returns>追蹤者統計資料。</returns>
        public FollowerInsightResponse GetFollowers()
        {
            return api.GetFollowers(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得追蹤者統計資料。
        /// </summary>
        /// <returns>追蹤者統計資料。</returns>
        public Task<FollowerInsightResponse> GetFollowersAsync()
        {
            return api.GetFollowersAsync(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得人口統計資料。
        /// </summary>
        /// <returns>人口統計資料。</returns>
        public DemographicInsightResponse GetDemographic()
        {
            return api.GetDemographic(context.ChannelAccessToken);
        }

        /// <summary>
        /// 取得人口統計資料。
        /// </summary>
        /// <returns>人口統計資料。</returns>
        public Task<DemographicInsightResponse> GetDemographicAsync()
        {
            return api.GetDemographicAsync(context.ChannelAccessToken);
        }
    }
}


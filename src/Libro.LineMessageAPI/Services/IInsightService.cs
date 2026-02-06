using Libro.LineMessageApi.Types;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Services
{
    /// <summary>
    /// 提供 Insight 統計資料查詢服務。
    /// </summary>
    public interface IInsightService
    {
        /// <summary>
        /// 取得訊息投遞統計資料。
        /// </summary>
        /// <param name="date">查詢日期（格式：yyyyMMdd）。</param>
        /// <returns>訊息投遞統計資料。</returns>
        MessageDeliveryInsightResponse GetMessageDelivery(string date);
        /// <summary>
        /// 取得訊息投遞統計資料。
        /// </summary>
        /// <param name="date">查詢日期（格式：yyyyMMdd）。</param>
        /// <returns>訊息投遞統計資料。</returns>
        Task<MessageDeliveryInsightResponse> GetMessageDeliveryAsync(string date);
        /// <summary>
        /// 取得追蹤者統計資料。
        /// </summary>
        /// <returns>追蹤者統計資料。</returns>
        FollowerInsightResponse GetFollowers();
        /// <summary>
        /// 取得追蹤者統計資料。
        /// </summary>
        /// <returns>追蹤者統計資料。</returns>
        Task<FollowerInsightResponse> GetFollowersAsync();
        /// <summary>
        /// 取得人口統計資料。
        /// </summary>
        /// <returns>人口統計資料。</returns>
        DemographicInsightResponse GetDemographic();
        /// <summary>
        /// 取得人口統計資料。
        /// </summary>
        /// <returns>人口統計資料。</returns>
        Task<DemographicInsightResponse> GetDemographicAsync();
    }
}


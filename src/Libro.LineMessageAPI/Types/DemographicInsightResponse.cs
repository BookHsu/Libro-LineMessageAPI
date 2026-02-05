using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Libro.LineMessageApi.Types
{
    /// <summary>
    /// 人口統計回應
    /// </summary>
    public class DemographicInsightResponse
    {
        /// <summary>
        /// 是否可取得資料
        /// </summary>
        [JsonPropertyName("available")]
        public bool available { get; set; }

        /// <summary>
        /// 性別分佈
        /// </summary>
        [JsonPropertyName("genders")]
        public List<DemographicGender> genders { get; set; }

        /// <summary>
        /// 年齡分佈
        /// </summary>
        [JsonPropertyName("ages")]
        public List<DemographicAge> ages { get; set; }

        /// <summary>
        /// 地區分佈
        /// </summary>
        [JsonPropertyName("areas")]
        public List<DemographicArea> areas { get; set; }

        /// <summary>
        /// App 類型分佈
        /// </summary>
        [JsonPropertyName("appTypes")]
        public List<DemographicAppType> appTypes { get; set; }

        /// <summary>
        /// 追蹤期間分佈
        /// </summary>
        [JsonPropertyName("subscriptionPeriods")]
        public List<DemographicSubscriptionPeriod> subscriptionPeriods { get; set; }
    }

    /// <summary>
    /// 性別分佈項目
    /// </summary>
    public class DemographicGender
    {
        /// <summary>
        /// 性別
        /// </summary>
        [JsonPropertyName("gender")]
        public string gender { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        [JsonPropertyName("percentage")]
        public double? percentage { get; set; }
    }

    /// <summary>
    /// 年齡分佈項目
    /// </summary>
    public class DemographicAge
    {
        /// <summary>
        /// 年齡區間
        /// </summary>
        [JsonPropertyName("age")]
        public string age { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        [JsonPropertyName("percentage")]
        public double? percentage { get; set; }
    }

    /// <summary>
    /// 地區分佈項目
    /// </summary>
    public class DemographicArea
    {
        /// <summary>
        /// 地區
        /// </summary>
        [JsonPropertyName("area")]
        public string area { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        [JsonPropertyName("percentage")]
        public double? percentage { get; set; }
    }

    /// <summary>
    /// App 類型分佈項目
    /// </summary>
    public class DemographicAppType
    {
        /// <summary>
        /// App 類型
        /// </summary>
        [JsonPropertyName("appType")]
        public string appType { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        [JsonPropertyName("percentage")]
        public double? percentage { get; set; }
    }

    /// <summary>
    /// 追蹤期間分佈項目
    /// </summary>
    public class DemographicSubscriptionPeriod
    {
        /// <summary>
        /// 追蹤期間
        /// </summary>
        [JsonPropertyName("subscriptionPeriod")]
        public string subscriptionPeriod { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        [JsonPropertyName("percentage")]
        public double? percentage { get; set; }
    }
}


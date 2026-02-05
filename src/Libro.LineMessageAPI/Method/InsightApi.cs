using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Insights API
    /// </summary>
    internal class InsightApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        /// <summary>
        /// 建立 Insights API
        /// </summary>
        internal InsightApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        /// <summary>
        /// 建立 Insights API
        /// </summary>
        internal InsightApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        internal MessageDeliveryInsightResponse GetMessageDelivery(string channelAccessToken, string date)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildMessageDeliveryInsight(date);
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<MessageDeliveryInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<MessageDeliveryInsightResponse> GetMessageDeliveryAsync(string channelAccessToken, string date)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildMessageDeliveryInsight(date);
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<MessageDeliveryInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal FollowerInsightResponse GetFollowers(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildFollowerInsight();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<FollowerInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<FollowerInsightResponse> GetFollowersAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildFollowerInsight();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<FollowerInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal DemographicInsightResponse GetDemographic(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDemographicInsight();
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
                return serializer.Deserialize<DemographicInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<DemographicInsightResponse> GetDemographicAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildDemographicInsight();
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
                return serializer.Deserialize<DemographicInsightResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }
    }
}


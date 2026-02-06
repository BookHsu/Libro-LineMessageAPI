using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Account Link API
    /// </summary>
    internal class AccountLinkApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        internal AccountLinkApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        internal AccountLinkApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        internal LinkTokenResponse IssueLinkToken(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildLinkToken(userId);
                using var content = new StringContent("{}");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
                var body = result.Content.ReadAsStringSync();
                return serializer.Deserialize<LinkTokenResponse>(body);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<LinkTokenResponse> IssueLinkTokenAsync(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildLinkToken(userId);
                using var content = new StringContent("{}");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var body = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                return serializer.Deserialize<LinkTokenResponse>(body);
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


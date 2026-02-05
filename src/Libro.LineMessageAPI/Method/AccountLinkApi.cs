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

        internal AccountLinkApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        internal AccountLinkApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
        }

        internal LinkTokenResponse IssueLinkToken(string channelAccessToken, string userId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildLinkToken(userId);
                var result = client.PostAsync(url, new StringContent("{}")).Result;
                var body = result.Content.ReadAsStringAsync().Result;
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
                var result = await client.PostAsync(url, new StringContent("{}"));
                var body = await result.Content.ReadAsStringAsync();
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


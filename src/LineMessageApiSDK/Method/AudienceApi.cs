using LineMessageApiSDK.Http;
using LineMessageApiSDK.Serialization;
using LineMessageApiSDK.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LineMessageApiSDK.Method
{
    /// <summary>
    /// Audience API
    /// </summary>
    internal class AudienceApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;

        internal AudienceApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        internal AudienceApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
        }

        internal AudienceGroupUploadResponse UploadAudienceGroup(string channelAccessToken, object request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupUpload();
                var payload = serializer.Serialize(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
                var body = result.Content.ReadAsStringAsync().Result;
                return serializer.Deserialize<AudienceGroupUploadResponse>(body);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<AudienceGroupUploadResponse> UploadAudienceGroupAsync(string channelAccessToken, object request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupUpload();
                var payload = serializer.Serialize(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                var body = await result.Content.ReadAsStringAsync();
                return serializer.Deserialize<AudienceGroupUploadResponse>(body);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal AudienceGroupStatusResponse GetAudienceGroupStatus(string channelAccessToken, long audienceGroupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupStatus(audienceGroupId);
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<AudienceGroupStatusResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<AudienceGroupStatusResponse> GetAudienceGroupStatusAsync(string channelAccessToken, long audienceGroupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupStatus(audienceGroupId);
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<AudienceGroupStatusResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal bool DeleteAudienceGroup(string channelAccessToken, long audienceGroupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroup(audienceGroupId);
                var result = client.DeleteAsync(url).Result;
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<bool> DeleteAudienceGroupAsync(string channelAccessToken, long audienceGroupId)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroup(audienceGroupId);
                var result = await client.DeleteAsync(url);
                return result.IsSuccessStatusCode;
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal AudienceGroupListResponse GetAudienceGroupList(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupList();
                var result = client.GetStringAsync(url).Result;
                return serializer.Deserialize<AudienceGroupListResponse>(result);
            }
            finally
            {
                if (shouldDispose)
                {
                    client.Dispose();
                }
            }
        }

        internal async Task<AudienceGroupListResponse> GetAudienceGroupListAsync(string channelAccessToken)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupList();
                var result = await client.GetStringAsync(url);
                return serializer.Deserialize<AudienceGroupListResponse>(result);
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

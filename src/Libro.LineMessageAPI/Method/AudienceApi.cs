using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.Types;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// Audience API
    /// </summary>
    internal class AudienceApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;
        private readonly IHttpClientSyncAdapterFactory syncAdapterFactory;

        internal AudienceApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient), null)
        {
        }

        internal AudienceApi(
            IJsonSerializer serializer,
            IHttpClientProvider httpClientProvider,
            IHttpClientSyncAdapterFactory syncAdapterFactory)
        {
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
            this.syncAdapterFactory = syncAdapterFactory ?? new DefaultHttpClientSyncAdapterFactory();
        }

        internal AudienceGroupUploadResponse UploadAudienceGroup(string channelAccessToken, object request)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildAudienceGroupUpload();
                var payload = serializer.Serialize(request);
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Post(url, content);
                var body = result.Content.ReadAsStringSync();
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
                using var content = new StringContent(payload, Encoding.UTF8, "application/json");
                using var result = await client.PostAsync(url, content).ConfigureAwait(false);
                var body = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
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
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
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
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
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
                var adapter = syncAdapterFactory.Create(client);
                using var result = adapter.Delete(url);
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
                using var result = await client.DeleteAsync(url).ConfigureAwait(false);
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
                var adapter = syncAdapterFactory.Create(client);
                var result = adapter.GetString(url);
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
                var result = await client.GetStringAsync(url).ConfigureAwait(false);
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


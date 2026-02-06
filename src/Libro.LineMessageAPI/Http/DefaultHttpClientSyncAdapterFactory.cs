using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// Default factory that reuses adapters per HttpClient
    /// </summary>
    public sealed class DefaultHttpClientSyncAdapterFactory : IHttpClientSyncAdapterFactory
    {
        private readonly ConditionalWeakTable<HttpClient, HttpClientSyncAdapter> cache = new();

        public IHttpClientSyncAdapter Create(HttpClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return cache.GetValue(client, key => new HttpClientSyncAdapter(key));
        }
    }
}

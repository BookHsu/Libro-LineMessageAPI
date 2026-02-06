using System;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// 提供預設的同步 HTTP 轉接器工廠，並依 <see cref="HttpClient"/> 重用轉接器執行個體。
    /// </summary>
    public sealed class DefaultHttpClientSyncAdapterFactory : IHttpClientSyncAdapterFactory
    {
        private readonly ConditionalWeakTable<HttpClient, HttpClientSyncAdapter> cache = new();

        /// <summary>
        /// 建立同步 HTTP 呼叫轉接器。
        /// </summary>
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

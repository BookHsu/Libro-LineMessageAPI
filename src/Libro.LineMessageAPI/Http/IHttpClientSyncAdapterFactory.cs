using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// 定義建立同步 <see cref="HttpClient"/> 轉接器的工廠介面。
    /// </summary>
    public interface IHttpClientSyncAdapterFactory
    {
        /// <summary>
        /// 建立同步 HTTP 呼叫轉接器。
        /// </summary>
        IHttpClientSyncAdapter Create(HttpClient client);
    }
}

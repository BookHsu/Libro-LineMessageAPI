using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// Factory for creating sync HttpClient adapters
    /// </summary>
    public interface IHttpClientSyncAdapterFactory
    {
        IHttpClientSyncAdapter Create(HttpClient client);
    }
}

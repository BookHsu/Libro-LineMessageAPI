using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// Sync HttpClient adapter (for testability and sync call sites)
    /// </summary>
    public interface IHttpClientSyncAdapter
    {
        string GetString(string url);
        byte[] GetByteArray(string url);
        HttpResponseMessage Post(string url, HttpContent content);
        HttpResponseMessage Put(string url, HttpContent content);
        HttpResponseMessage Delete(string url);
    }
}

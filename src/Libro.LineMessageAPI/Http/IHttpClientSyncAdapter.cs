using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// 提供同步 HTTP 呼叫能力的 <see cref="HttpClient"/> 轉接器。
    /// </summary>
    public interface IHttpClientSyncAdapter
    {
        /// <summary>
        /// 送出 GET 要求並以字串形式取得回應內容。
        /// </summary>
        string GetString(string url);
        /// <summary>
        /// 送出 GET 要求並以位元組陣列形式取得回應內容。
        /// </summary>
        byte[] GetByteArray(string url);
        /// <summary>
        /// 送出 POST 要求。
        /// </summary>
        HttpResponseMessage Post(string url, HttpContent content);
        /// <summary>
        /// 送出 PUT 要求。
        /// </summary>
        HttpResponseMessage Put(string url, HttpContent content);
        /// <summary>
        /// 送出 DELETE 要求。
        /// </summary>
        HttpResponseMessage Delete(string url);
    }
}

using System;
using System.IO;
using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// 提供基於 <see cref="HttpClient"/> 的同步呼叫轉接實作。
    /// </summary>
    public sealed class HttpClientSyncAdapter : IHttpClientSyncAdapter
    {
        private readonly HttpClient client;

        /// <summary>
        /// 初始化 HttpClientSyncAdapter 的新執行個體。
        /// </summary>
        public HttpClientSyncAdapter(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// 送出 GET 要求並以字串形式取得回應內容。
        /// </summary>
        public string GetString(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = response.Content.ReadAsStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 送出 GET 要求並以位元組陣列形式取得回應內容。
        /// </summary>
        public byte[] GetByteArray(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = response.Content.ReadAsStream();
            using var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            return buffer.ToArray();
        }

        /// <summary>
        /// 送出 POST 要求。
        /// </summary>
        public HttpResponseMessage Post(string url, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content
            };
            var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            request.Dispose();
            return response;
        }

        /// <summary>
        /// 送出 PUT 要求。
        /// </summary>
        public HttpResponseMessage Put(string url, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = content
            };
            var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            request.Dispose();
            return response;
        }

        /// <summary>
        /// 送出 DELETE 要求。
        /// </summary>
        public HttpResponseMessage Delete(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            request.Dispose();
            return response;
        }
    }
}

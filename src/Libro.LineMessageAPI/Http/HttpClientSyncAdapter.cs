using System;
using System.IO;
using System.Net.Http;

namespace Libro.LineMessageApi.Http
{
    /// <summary>
    /// Default sync adapter over HttpClient
    /// </summary>
    public sealed class HttpClientSyncAdapter : IHttpClientSyncAdapter
    {
        private readonly HttpClient client;

        public HttpClientSyncAdapter(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public string GetString(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = response.Content.ReadAsStream();
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public byte[] GetByteArray(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            using var stream = response.Content.ReadAsStream();
            using var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            return buffer.ToArray();
        }

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

        public HttpResponseMessage Delete(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            var response = client.Send(request, HttpCompletionOption.ResponseHeadersRead);
            request.Dispose();
            return response;
        }
    }
}

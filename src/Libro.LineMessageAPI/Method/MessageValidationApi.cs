using Libro.LineMessageApi.Http;
using Libro.LineMessageApi.Serialization;
using Libro.LineMessageApi.SendMessage;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Libro.LineMessageApi.Method
{
    /// <summary>
    /// 訊息驗證 API
    /// </summary>
    internal class MessageValidationApi
    {
        private readonly IJsonSerializer serializer;
        private readonly IHttpClientProvider httpClientProvider;

        /// <summary>
        /// 建立訊息驗證 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClient">外部注入的 HttpClient</param>
        internal MessageValidationApi(IJsonSerializer serializer, HttpClient httpClient = null)
            : this(serializer, new DefaultHttpClientProvider(httpClient))
        {
        }

        /// <summary>
        /// 建立訊息驗證 API
        /// </summary>
        /// <param name="serializer">JSON 序列化器</param>
        /// <param name="httpClientProvider">HttpClient 提供者</param>
        internal MessageValidationApi(IJsonSerializer serializer, IHttpClientProvider httpClientProvider)
        {
            // 設定序列化器（可透過 DI 注入）
            this.serializer = serializer ?? new SystemTextJsonSerializer();
            // 建立 HttpClient 提供者
            this.httpClientProvider = httpClientProvider ?? new DefaultHttpClientProvider(null);
        }

        /// <summary>
        /// 驗證訊息格式
        /// </summary>
        internal bool Validate(string channelAccessToken, string type, SendLineMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildValidateMessage(type);
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;
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

        /// <summary>
        /// 驗證訊息格式（非同步）
        /// </summary>
        internal async Task<bool> ValidateAsync(string channelAccessToken, string type, SendLineMessage message)
        {
            bool shouldDispose;
            HttpClient client = httpClientProvider.GetClient(channelAccessToken, out shouldDispose);
            try
            {
                string url = LineApiEndpoints.BuildValidateMessage(type);
                var payload = serializer.Serialize(message);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
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
    }
}



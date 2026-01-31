using LineMessageApiSDK.Serialization;
using System.Net.Http;

namespace LineMessageApiSDK.Services
{
    /// <summary>
    /// SDK 內部共用的 API 設定內容
    /// </summary>
    internal class LineApiContext
    {
        /// <summary>
        /// Channel Access Token
        /// </summary>
        internal string ChannelAccessToken { get; }

        /// <summary>
        /// JSON 序列化器
        /// </summary>
        internal IJsonSerializer Serializer { get; }

        /// <summary>
        /// HttpClient
        /// </summary>
        internal HttpClient HttpClient { get; }

        /// <summary>
        /// 建立 API Context
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="serializer">序列化器</param>
        /// <param name="httpClient">HttpClient</param>
        internal LineApiContext(string channelAccessToken, IJsonSerializer serializer, HttpClient httpClient)
        {
            // 設定必要的 Token
            ChannelAccessToken = channelAccessToken;
            // 若未提供序列化器，使用預設 System.Text.Json
            Serializer = serializer ?? new SystemTextJsonSerializer();
            // HttpClient 可透過 DI 注入
            HttpClient = httpClient;
        }
    }
}

using System.Net.Http;

namespace LineMessageApiSDK.Http
{
    /// <summary>
    /// HttpClient 提供者介面
    /// </summary>
    internal interface IHttpClientProvider
    {
        /// <summary>
        /// 取得已設定完成的 HttpClient
        /// </summary>
        /// <param name="channelAccessToken">Channel Access Token</param>
        /// <param name="shouldDispose">是否需要釋放</param>
        /// <returns>HttpClient</returns>
        HttpClient GetClient(string channelAccessToken, out bool shouldDispose);
    }
}

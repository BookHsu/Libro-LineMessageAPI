using Libro.LineMessageApi;

namespace Libro.LineMessageAPI.ExampleApi.Services
{
    internal sealed class LineSdkFactory : ILineSdkFactory
    {
        public ILineSdkFacade CreateBotWebhookSdk(string channelAccessToken)
        {
            var sdk = new LineSdkBuilder(channelAccessToken)
                .UseBot()
                .UseWebhookEndpoints()
                .Build();

            return new LineSdkFacade(sdk);
        }

        public ILineSdkFacade CreateMessageSdk(string channelAccessToken)
        {
            var sdk = new LineSdkBuilder(channelAccessToken)
                .UseMessages()
                .Build();

            return new LineSdkFacade(sdk);
        }
    }
}

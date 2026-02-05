namespace Libro.LineMessageAPI.ExampleApi.Services
{
    public interface ILineSdkFactory
    {
        ILineSdkFacade CreateBotWebhookSdk(string channelAccessToken);
        ILineSdkFacade CreateMessageSdk(string channelAccessToken);
    }
}

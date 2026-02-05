using Libro.LineMessageApi.Services;

namespace Libro.LineMessageAPI.ExampleApi.Services
{
    public interface ILineSdkFacade
    {
        IBotService Bot { get; }
        IWebhookEndpointService WebhookEndpoints { get; }
        IMessageService Messages { get; }
    }
}

using Libro.LineMessageApi;
using Libro.LineMessageApi.Services;

namespace Libro.LineMessageAPI.ExampleApi.Services
{
    internal sealed class LineSdkFacade : ILineSdkFacade
    {
        private readonly LineSdk sdk;

        public LineSdkFacade(LineSdk sdk)
        {
            this.sdk = sdk;
        }

        public IBotService Bot => sdk.Bot;

        public IWebhookEndpointService WebhookEndpoints => sdk.WebhookEndpoints;

        public IMessageService Messages => sdk.Messages;
    }
}

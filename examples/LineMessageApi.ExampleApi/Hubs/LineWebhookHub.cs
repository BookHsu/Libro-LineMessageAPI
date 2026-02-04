using Microsoft.AspNetCore.SignalR;

namespace LineMessageApi.ExampleApi.Hubs
{
    /// <summary>
    /// Line Webhook 事件推送 Hub
    /// </summary>
    public sealed class LineWebhookHub : Hub
    {
        // Hub 目前僅用於伺服器推送事件給前端
    }
}

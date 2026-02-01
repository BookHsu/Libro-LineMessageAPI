using LineMessageApiSDK;
using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LineMessageApi.ExampleApi.Controllers;

/// <summary>
/// Line Webhook 相關範例 API
/// </summary>
[ApiController]
[Route("line")]
public sealed class LineWebhookController : ControllerBase
{
    private readonly LineChannelOptions options;
    private readonly JsonSerializerOptions jsonOptions;
    private readonly LineSdk sdk;

    /// <summary>
    /// 建立 Line Webhook Controller
    /// </summary>
    public LineWebhookController(
        IOptions<LineChannelOptions> options,
        IOptions<Microsoft.AspNetCore.Mvc.JsonOptions> jsonOptions,
        LineSdk sdk)
    {
        this.options = options.Value;
        this.jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        this.sdk = sdk;
    }

    /// <summary>
    /// Line Webhook 接收入口（會驗證簽章並回覆示範訊息）
    /// </summary>
    [HttpPost("webhook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var channelSecret = options.ChannelSecret;
        if (string.IsNullOrWhiteSpace(channelSecret))
        {
            return BadRequest(new
            {
                error = "LineChannel:ChannelSecret is not configured."
            });
        }

        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        if (string.IsNullOrWhiteSpace(body))
        {
            return BadRequest(new { error = "Request body is empty." });
        }

        if (!Request.Headers.TryGetValue("X-Line-Signature", out var signature))
        {
            return Unauthorized();
        }

        if (!LineWebhookSignature.Verify(body, channelSecret, signature.ToString()))
        {
            return Unauthorized();
        }

        var token = options.ChannelAccessToken;
        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest(new
            {
                error = "LineChannel:ChannelAccessToken is not configured."
            });
        }

        var payload = JsonSerializer.Deserialize<LineReceivedMsg>(body, jsonOptions);

        if (payload?.events == null || payload.events.Count == 0)
        {
            return Ok(new { received = true, events = 0 });
        }

        foreach (var evt in payload.events)
        {
            if (string.IsNullOrWhiteSpace(evt?.replyToken))
            {
                continue;
            }

            var replyText = BuildReplyText(evt);
            if (string.IsNullOrWhiteSpace(replyText))
            {
                continue;
            }

            await sdk.Messages!.SendReplyMessageAsync(
                evt.replyToken,
                new TextMessage(replyText));
        }

        return Ok(new { received = true, events = payload.events.Count });
    }

    /// <summary>
    /// 依事件類型組合回覆文字
    /// </summary>
    private static string BuildReplyText(LineEvents evt)
    {
        switch (evt.type)
        {
            case EventType.message:
                return BuildMessageReply(evt.message);
            case EventType.follow:
                return "收到 follow 事件";
            case EventType.join:
                return "收到 join 事件";
            case EventType.postback:
                return BuildPostbackReply(evt.postback);
            case EventType.beacon:
                return BuildBeaconReply(evt.beacon);
            case EventType.unfollow:
                return string.Empty;
            case EventType.leave:
                return string.Empty;
            default:
                return $"收到 {evt.type} 事件";
        }
    }

    /// <summary>
    /// 依訊息類型組合回覆文字
    /// </summary>
    private static string BuildMessageReply(LineMessage message)
    {
        if (message == null)
        {
            return "收到訊息事件";
        }

        switch (message.type)
        {
            case MessageType.text:
                return $"收到文字訊息: {message.text}";
            case MessageType.image:
                return $"收到圖片訊息 (id: {message.id})";
            case MessageType.video:
                return $"收到影片訊息 (id: {message.id})";
            case MessageType.audio:
                return $"收到音訊訊息 (id: {message.id})";
            case MessageType.file:
                return $"收到檔案: {message.fileName} ({message.fileSize} bytes)";
            case MessageType.location:
                return $"收到位置: {message.title} {message.address} ({message.latitude}, {message.longitude})";
            case MessageType.sticker:
                return $"收到貼圖: package {message.packageId}, sticker {message.stickerId}";
            default:
                return $"收到訊息: {message.type}";
        }
    }

    /// <summary>
    /// 依 Postback 內容組合回覆文字
    /// </summary>
    private static string BuildPostbackReply(LinePostBack postback)
    {
        if (postback == null)
        {
            return "收到 postback 事件";
        }

        if (!string.IsNullOrWhiteSpace(postback.data))
        {
            return $"收到 postback: {postback.data}";
        }

        if (postback.Params?.datetime != null)
        {
            return $"收到 postback datetime: {postback.Params.datetime:O}";
        }

        if (postback.Params?.date != null)
        {
            return $"收到 postback date: {postback.Params.date:yyyy-MM-dd}";
        }

        if (!string.IsNullOrWhiteSpace(postback.Params?.time))
        {
            return $"收到 postback time: {postback.Params.time}";
        }

        return "收到 postback 事件";
    }

    /// <summary>
    /// 依 Beacon 內容組合回覆文字
    /// </summary>
    private static string BuildBeaconReply(LineBeacon beacon)
    {
        if (beacon == null)
        {
            return "收到 beacon 事件";
        }

        return $"收到 beacon: {beacon.type} {beacon.hwid}";
    }
}

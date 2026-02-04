using System;
using System.Collections.Generic;
using LineMessageApi.ExampleApi.Hubs;
using LineMessageApi.ExampleApi.Models;
using LineMessageApi.ExampleApi.Services;
using LineMessageApiSDK;
using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LineMessageApi.ExampleApi.Controllers;

/// <summary>
/// Dashboard Webhook 接收 API
/// </summary>
[ApiController]
public sealed class DashboardWebhookController : ControllerBase
{
    private readonly LineConfigStore store;
    private readonly JsonSerializerOptions jsonOptions;
    private readonly IHubContext<LineWebhookHub> hubContext;
    private readonly ILogger<DashboardWebhookController> logger;

    /// <summary>
    /// 建立 Dashboard Webhook Controller
    /// </summary>
    public DashboardWebhookController(
        LineConfigStore store,
        IHubContext<LineWebhookHub> hubContext,
        IOptions<Microsoft.AspNetCore.Mvc.JsonOptions> jsonOptions,
        ILogger<DashboardWebhookController> logger)
    {
        this.store = store;
        this.hubContext = hubContext;
        this.jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        this.logger = logger;
    }

    /// <summary>
    /// Dashboard Webhook 入口，驗證簽章並回覆訊息
    /// </summary>
    [HttpPost("/dashboard/hook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var config = store.Get();
        if (config == null)
        {
            return BadRequest(new
            {
                error = "尚未設定 Line 參數。"
            });
        }

        var channelSecret = config.ChannelSecret;
        if (string.IsNullOrWhiteSpace(channelSecret))
        {
            return BadRequest(new
            {
                error = "Channel Secret is empty."
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

        var payload = JsonSerializer.Deserialize<LineReceivedMsg>(body, jsonOptions);

        if (payload?.events == null || payload.events.Count == 0)
        {
            return Ok(new { received = true, events = 0 });
        }

        var errors = new List<object>();

        foreach (var evt in payload.events)
        {
            if (evt == null)
            {
                errors.Add(new
                {
                    eventType = "unknown",
                    message = "event is null."
                });
                continue;
            }

            var record = BuildEventRecord(evt, body);
            store.AddEvent(record);

            // 推送事件到前端
            await hubContext.Clients.All.SendAsync("webhookReceived", record);

            if (!IsValidReplyToken(evt.replyToken))
            {
                errors.Add(new
                {
                    eventType = evt.type.ToString(),
                    message = "replyToken is missing or invalid."
                });
                continue;
            }

            var replyText = BuildReplyText(evt);
            if (string.IsNullOrWhiteSpace(replyText))
            {
                errors.Add(new
                {
                    eventType = evt.type.ToString(),
                    message = "reply text is empty."
                });
                continue;
            }

            try
            {
                var token = config.ChannelAccessToken;
                if (string.IsNullOrWhiteSpace(token))
                {
                    errors.Add(new
                    {
                        eventType = evt.type.ToString(),
                        message = "Channel Access Token is empty."
                    });
                    continue;
                }

                var sdk = new LineSdkBuilder(token)
                    .UseMessages()
                    .Build();

                await sdk.Messages!.SendReplyMessageAsync(
                    evt.replyToken,
                    new TextMessage(replyText));
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "LINE reply failed for event type {EventType}.", evt.type);
                errors.Add(new
                {
                    eventType = evt.type.ToString(),
                    message = ex.Message
                });
            }
        }

        return Ok(new
        {
            received = true,
            events = payload.events.Count,
            errors
        });
    }

    private static bool IsValidReplyToken(string? replyToken)
    {
        if (string.IsNullOrWhiteSpace(replyToken))
        {
            return false;
        }

        // LINE replyToken 通常不會太短
        if (replyToken.Length < 10)
        {
            return false;
        }

        // 已知的測試用假 token 不應回覆
        if (string.Equals(replyToken, "00000000000000000000000000000000", StringComparison.Ordinal))
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 依事件類型產生回覆文字
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
            case EventType.memberJoined:
                return BuildMemberEventReply("memberJoined", evt.joined);
            case EventType.memberLeft:
                return BuildMemberEventReply("memberLeft", evt.left);
            case EventType.postback:
                return BuildPostbackReply(evt.postback);
            case EventType.videoPlayComplete:
                return BuildVideoPlayCompleteReply(evt.videoPlayComplete);
            case EventType.beacon:
                return BuildBeaconReply(evt.beacon);
            case EventType.accountLink:
                return BuildAccountLinkReply(evt.link);
            case EventType.things:
                return BuildThingsReply(evt.things);
            case EventType.unsend:
                return BuildUnsendReply(evt.unsend);
            case EventType.unfollow:
                return "收到 unfollow 事件";
            case EventType.leave:
                return "收到 leave 事件";
            default:
                return $"收到 {evt.type} 事件";
        }
    }

    /// <summary>
    /// 依訊息類型產生回覆文字
    /// </summary>
    private static string BuildMessageReply(LineMessage message)
    {
        if (message == null)
        {
            return "收到訊息，但內容為空";
        }

        switch (message.type)
        {
            case MessageType.text:
                return string.IsNullOrWhiteSpace(message.text)
                    ? "收到文字訊息 (text)，但內容為空"
                    : $"收到文字訊息 (text): {message.text}";
            case MessageType.image:
                return $"收到圖片訊息 (image)，id: {message.id ?? "-"}";
            case MessageType.video:
                return $"收到影片訊息 (video)，id: {message.id ?? "-"}";
            case MessageType.audio:
                return $"收到音訊訊息 (audio)，id: {message.id ?? "-"}";
            case MessageType.file:
                return $"收到檔案訊息 (file): {message.fileName ?? "-"} ({message.fileSize ?? 0} bytes)";
            case MessageType.location:
                return $"收到位置訊息 (location): {message.title ?? "-"} {message.address ?? "-"} ({message.latitude ?? 0}, {message.longitude ?? 0})";
            case MessageType.sticker:
                return $"收到貼圖訊息 (sticker): package {message.packageId ?? "-"}, sticker {message.stickerId ?? "-"}";
            default:
                return $"收到訊息: {message.type}";
        }
    }

    /// <summary>
    /// 依 Postback 內容產生回覆文字
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
    /// 依 Beacon 內容產生回覆文字
    /// </summary>
    private static string BuildBeaconReply(LineBeacon beacon)
    {
        if (beacon == null)
        {
            return "收到 beacon 事件";
        }

        return $"收到 beacon: {beacon.type} {beacon.hwid}";
    }

    private static string BuildMemberEventReply(string eventType, LineMembers members)
    {
        if (members?.members == null || members.members.Count == 0)
        {
            return $"收到 {eventType} 事件";
        }

        return $"收到 {eventType} 事件，共 {members.members.Count} 位成員";
    }

    private static string BuildVideoPlayCompleteReply(LineVideoPlayComplete info)
    {
        if (info == null)
        {
            return "收到 videoPlayComplete 事件";
        }

        return $"收到 videoPlayComplete: {info.trackingId}";
    }

    private static string BuildAccountLinkReply(LineAccountLink link)
    {
        if (link == null)
        {
            return "收到 accountLink 事件";
        }

        return $"收到 accountLink: {link.result}";
    }

    private static string BuildThingsReply(LineThings things)
    {
        if (things == null)
        {
            return "收到 things 事件";
        }

        return $"收到 things: {things.type} device={things.deviceId}";
    }

    private static string BuildUnsendReply(LineUnsend unsend)
    {
        if (unsend == null)
        {
            return "收到 unsend 事件";
        }

        return $"收到 unsend: {unsend.messageId}";
    }

    private static WebhookEventRecord BuildEventRecord(LineEvents evt, string rawJson)
    {
        // 建立事件摘要
        var summary = evt.type.ToString();
        var messageType = evt.message?.type.ToString() ?? string.Empty;
        var sourceType = evt.source?.type.ToString() ?? string.Empty;

        if (evt.type == EventType.message && evt.message is LineMessage message)
        {
            summary = message.type == MessageType.text && !string.IsNullOrWhiteSpace(message.text)
                ? message.text
                : message.type.ToString();
        }

        return new WebhookEventRecord
        {
            EventType = evt.type.ToString(),
            MessageType = messageType,
            SourceType = sourceType,
            Summary = summary,
            RawJson = rawJson
        };
    }
}

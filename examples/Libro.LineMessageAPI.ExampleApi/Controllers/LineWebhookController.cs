using System;
using System.Collections.Generic;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageApi;
using Libro.LineMessageApi.LineMessageObject;
using Libro.LineMessageApi.LineReceivedObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Libro.LineMessageAPI.ExampleApi.Controllers;

/// <summary>
/// Line Webhook 接收 API（API 範例）
/// </summary>
[ApiController]
public sealed class LineWebhookController : ControllerBase
{
    private readonly LineChannelOptions channelOptions;
    private readonly JsonSerializerOptions jsonOptions;
    private readonly ILineSdkFactory sdkFactory;
    private readonly ILogger<LineWebhookController> logger;

    /// <summary>
    /// 建立 Line Webhook Controller
    /// </summary>
    public LineWebhookController(
        IOptions<LineChannelOptions> channelOptions,
        ILineSdkFactory sdkFactory,
        IOptions<Microsoft.AspNetCore.Mvc.JsonOptions> jsonOptions,
        ILogger<LineWebhookController> logger)
    {
        this.channelOptions = channelOptions.Value;
        this.sdkFactory = sdkFactory;
        this.jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        this.logger = logger;
    }

    /// <summary>
    /// Line Webhook 入口，驗證簽章並回覆訊息
    /// </summary>
    [HttpPost("/line/hook")]
    public async Task<IActionResult> HandleWebhook()
    {
        var channelSecret = channelOptions.ChannelSecret;
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
                var token = channelOptions.ChannelAccessToken;
                if (string.IsNullOrWhiteSpace(token))
                {
                    errors.Add(new
                    {
                        eventType = evt.type.ToString(),
                        message = "Channel Access Token is empty."
                    });
                    continue;
                }

                var sdk = sdkFactory.CreateMessageSdk(token);

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
                    ? "收到文字訊息，但內容為空"
                    : $"收到文字訊息: {message.text}";
            case MessageType.image:
                return $"收到圖片訊息 (id: {message.id ?? "-"})";
            case MessageType.video:
                return $"收到影片訊息 (id: {message.id ?? "-"})";
            case MessageType.audio:
                return $"收到音訊訊息 (id: {message.id ?? "-"})";
            case MessageType.file:
                return $"收到檔案: {message.fileName ?? "-"} ({message.fileSize ?? 0} bytes)";
            case MessageType.location:
                return $"收到位置: {message.title ?? "-"} {message.address ?? "-"} ({message.latitude ?? 0}, {message.longitude ?? 0})";
            case MessageType.sticker:
                return $"收到貼圖: package {message.packageId ?? "-"}, sticker {message.stickerId ?? "-"}";
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

}





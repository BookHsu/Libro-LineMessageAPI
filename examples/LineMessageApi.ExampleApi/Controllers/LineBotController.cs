using LineMessageApiSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LineMessageApi.ExampleApi.Controllers;

/// <summary>
/// Line Bot 相關範例 API
/// </summary>
[ApiController]
[Route("line")]
public sealed class LineBotController : ControllerBase
{
    private readonly LineChannelOptions options;
    private readonly LineSdk sdk;

    /// <summary>
    /// 建立 Line Bot Controller
    /// </summary>
    public LineBotController(IOptions<LineChannelOptions> options, LineSdk sdk)
    {
        this.options = options.Value;
        this.sdk = sdk;
    }

    /// <summary>
    /// 取得 Bot 基本資訊
    /// </summary>
    [HttpGet("bot-info")]
    public async Task<IActionResult> GetBotInfo()
    {
        var token = options.ChannelAccessToken;
        if (string.IsNullOrWhiteSpace(token))
        {
            return BadRequest(new
            {
                error = "LineChannel:ChannelAccessToken is not configured."
            });
        }

        var info = await sdk.Bot!.GetBotInfoAsync();
        return Ok(info);
    }
}

using LineMessageApiSDK;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LineMessageApi.ExampleApi.Controllers;

[ApiController]
[Route("line")]
public sealed class LineBotController : ControllerBase
{
    private readonly LineChannelOptions options;

    public LineBotController(IOptions<LineChannelOptions> options)
    {
        this.options = options.Value;
    }

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

        var sdk = new LineSdkBuilder(token)
            .UseBot()
            .Build();

        var info = await sdk.Bot!.GetBotInfoAsync();
        return Ok(info);
    }
}

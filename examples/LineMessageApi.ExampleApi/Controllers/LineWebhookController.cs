using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LineMessageApi.ExampleApi.Controllers;

[ApiController]
[Route("line")]
public sealed class LineWebhookController : ControllerBase
{
    private readonly LineChannelOptions options;

    public LineWebhookController(IOptions<LineChannelOptions> options)
    {
        this.options = options.Value;
    }

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

        return Ok(new { received = true });
    }
}

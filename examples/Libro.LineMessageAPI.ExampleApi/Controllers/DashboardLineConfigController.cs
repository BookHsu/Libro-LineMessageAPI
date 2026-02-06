using System;
using Libro.LineMessageAPI.ExampleApi.Models;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageApi.Types;
using Microsoft.AspNetCore.Mvc;

namespace Libro.LineMessageAPI.ExampleApi.Controllers
{
    /// <summary>
    /// Line 設定與資訊 API
    /// </summary>
    [ApiController]
    [Route("dashboard/api/line")]
    public sealed class DashboardLineConfigController : ControllerBase
    {
        private readonly LineConfigStore store;
        private readonly ILineSdkFactory sdkFactory;

        /// <summary>
        /// 建立控制器
        /// </summary>
        /// <param name="store">設定存放</param>
        public DashboardLineConfigController(LineConfigStore store, ILineSdkFactory sdkFactory)
        {
            this.store = store;
            this.sdkFactory = sdkFactory;
        }

        /// <summary>
        /// 取得目前設定狀態
        /// </summary>
        [HttpGet("config")]
        public IActionResult GetConfig()
        {
            // 讀取記憶體設定
            var config = store.Get();
            if (config == null)
            {
                return Ok(new LineConfigState
                {
                    Configured = false
                });
            }

            return Ok(new LineConfigState
            {
                Configured = true,
                WebhookUrl = config.WebhookUrl,
                UpdatedAtUtc = config.UpdatedAtUtc.ToString("O")
            });
        }

        /// <summary>
        /// 更新設定並可同步設定 Webhook Endpoint
        /// </summary>
        [HttpPost("config")]
        public IActionResult UpdateConfig([FromBody] LineConfigRequest request)
        {
            if (request == null)
            {
                return BadRequest(new LineConfigResponse
                {
                    Success = false,
                    Message = "請提供設定內容。"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ChannelAccessToken) ||
                string.IsNullOrWhiteSpace(request.ChannelSecret))
            {
                return BadRequest(new LineConfigResponse
                {
                    Success = false,
                    Message = "Channel Access Token 與 Channel Secret 為必填。"
                });
            }

            var webhookUrl = request.WebhookUrl;
            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                webhookUrl = BuildDefaultWebhookUrl();
            }

            var config = new LineConfig
            {
                ChannelAccessToken = request.ChannelAccessToken.Trim(),
                ChannelSecret = request.ChannelSecret.Trim(),
                WebhookUrl = webhookUrl.Trim(),
                UpdatedAtUtc = DateTimeOffset.UtcNow
            };

            // 更新記憶體設定
            store.Update(config);

            // 讀取 Bot 資訊與 Webhook 設定
            var response = new LineConfigResponse
            {
                Success = true,
                Message = "設定完成。",
                Config = new LineConfigState
                {
                    Configured = true,
                    WebhookUrl = config.WebhookUrl,
                    UpdatedAtUtc = config.UpdatedAtUtc.ToString("O")
                }
            };

            try
            {
                var sdk = sdkFactory.CreateBotWebhookSdk(config.ChannelAccessToken);

                response.BotInfo = sdk.Bot?.GetBotInfo();

                if (request.SetEndpoint)
                {
                    var setResult = sdk.WebhookEndpoints?.SetWebhookEndpoint(new WebhookEndpointRequest
                    {
                        endpoint = config.WebhookUrl
                    });

                    if (setResult == false)
                    {
                        response.Message = "設定完成，但 Webhook Endpoint 更新失敗。";
                    }
                }

                response.WebhookEndpoint = sdk.WebhookEndpoints?.GetWebhookEndpoint();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"設定失敗: {ex.Message}";
            }

            return Ok(response);
        }

        /// <summary>
        /// 取得 Line Bot 資訊與 Webhook Endpoint
        /// </summary>
        [HttpGet("info")]
        public IActionResult GetInfo()
        {
            var config = store.Get();
            if (config == null)
            {
                return BadRequest(new LineConfigResponse
                {
                    Success = false,
                    Message = "尚未設定 Line 參數。"
                });
            }

            try
            {
                var sdk = sdkFactory.CreateBotWebhookSdk(config.ChannelAccessToken);

                return Ok(new LineConfigResponse
                {
                    Success = true,
                    Message = "取得成功。",
                    Config = new LineConfigState
                    {
                        Configured = true,
                        WebhookUrl = config.WebhookUrl,
                        UpdatedAtUtc = config.UpdatedAtUtc.ToString("O")
                    },
                    BotInfo = sdk.Bot?.GetBotInfo(),
                    WebhookEndpoint = sdk.WebhookEndpoints?.GetWebhookEndpoint()
                });
            }
            catch (Exception ex)
            {
                return Ok(new LineConfigResponse
                {
                    Success = false,
                    Message = $"取得失敗: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// 取得最近的 webhook 事件
        /// </summary>
        [HttpGet("events")]
        public IActionResult GetEvents()
        {
            // 回傳事件清單
            return Ok(store.GetEvents());
        }

        private string BuildDefaultWebhookUrl()
        {
            // 使用目前請求的 Host 組合預設 webhook URL
            return $"{Request.Scheme}://{Request.Host}/dashboard/hook";
        }
    }
}





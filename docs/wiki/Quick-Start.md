# 快速開始

這份快速開始聚焦在「安裝 → 建立 SDK → 回覆訊息」。

## 1) 安裝

```bash
dotnet add package LibroLineMessageSDK
```

> 若要使用 DI/Options，請加裝擴充套件：

```bash
dotnet add package LibroLineMessageSDK.Extensions
```

## 2) 建立 SDK

```csharp
using LineMessageApiSDK;

var channelAccessToken = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");
if (string.IsNullOrWhiteSpace(channelAccessToken))
{
    throw new InvalidOperationException("缺少 LINE_CHANNEL_ACCESS_TOKEN");
}

var sdk = new LineSdkBuilder(channelAccessToken)
    .UseBot()
    .UseMessages()
    .Build();
```

## 3) Webhook 驗證 + 回覆訊息

```csharp
using LineMessageApiSDK;
using LineMessageApiSDK.LineMessageObject;
using LineMessageApiSDK.LineReceivedObject;
using System.Text.Json;

public async Task<IActionResult> Webhook(HttpRequestMessage request, string channelSecret, string channelAccessToken)
{
    if (!LineChannel.VaridateSignature(request, channelSecret))
    {
        return new UnauthorizedResult();
    }

    var body = await request.Content.ReadAsStringAsync();
    var payload = JsonSerializer.Deserialize<LineReceivedMsg>(body);
    var replyToken = payload?.events?[0]?.replyToken;

    if (!string.IsNullOrWhiteSpace(replyToken))
    {
        var sdk = new LineSdkBuilder(channelAccessToken)
            .UseMessages()
            .Build();

        await sdk.Messages!.SendReplyMessageAsync(replyToken, new TextMessage("收到！"));
    }

    return new OkResult();
}
```

## 4) 透過 DI 注入（可選）

```csharp
using LineMessageApiSDK;
using LineMessageApiSDK.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

services.AddLineSdk(
    configuration,
    sdkBuilder => sdkBuilder
        .UseBot()
        .UseMessages());

// 設定環境變數（LineChannel 區段）
// - LineChannel__ChannelAccessToken
// - LineChannel__ChannelSecret

var serviceProvider = services.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<LineSdk>();
```

## 5) 範例路徑（API / Dashboard）

此範例提供兩條路徑：

1. API 範例：透過設定或環境變數注入 Channel Access Token / Secret
2. Dashboard 範例：由頁面輸入並存於記憶體，提供快速驗證與即時事件流

對應端點：

- API Webhook 入口：`POST /line/hook`
- Dashboard Webhook 入口：`POST /dashboard/hook`
- Dashboard API：`/dashboard/api/line/*`

## 下一步

- 參考頁：常用類別與功能入口
- 範例頁：可直接執行的完整專案

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

var serviceProvider = services.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<LineSdk>();
```

## 下一步

- 參考頁：常用類別與功能入口
- 範例頁：可直接執行的完整專案

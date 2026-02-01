# LineMessageApiSDK

![GitHub release](https://img.shields.io/github/v/release/BookHsu/LibroLineMessageApi?sort=semver)
![CI](https://github.com/BookHsu/LibroLineMessageApi/actions/workflows/ci.yml/badge.svg)
![CodeQL](https://github.com/BookHsu/LibroLineMessageApi/actions/workflows/codeql.yml/badge.svg)
![NuGet](https://img.shields.io/nuget/v/LibroLineMessageSDK)
![NuGet downloads](https://img.shields.io/nuget/dt/LibroLineMessageSDK)
![License](https://img.shields.io/github/license/BookHsu/LibroLineMessageApi)
![Issues](https://img.shields.io/github/issues/BookHsu/LibroLineMessageApi)
![PRs](https://img.shields.io/github/issues-pr/BookHsu/LibroLineMessageApi)
![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)

此專案為 LINE Messaging API 的 C# SDK，並以開源專案結構進行整理，方便維護與擴充。

## NuGet

```bash
dotnet add package LibroLineMessageSDK
```

## 快速使用範例

### 1) 直接建立實例

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

### 2) 透過 DI 注入

```csharp
using LineMessageApiSDK;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton(sp =>
{
    var channelAccessToken = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");
    if (string.IsNullOrWhiteSpace(channelAccessToken))
    {
        throw new InvalidOperationException("缺少 LINE_CHANNEL_ACCESS_TOKEN");
    }

    return new LineSdkBuilder(channelAccessToken)
        .UseBot()
        .UseMessages()
        .Build();
});

var serviceProvider = services.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<LineSdk>();
```

### 3) Webhook 驗證與回覆訊息（最短流程）

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

## LINE Messaging API 2.0 快速使用

請先閱讀「LINE Messaging API 2.0 規格速覽與快速上手」，內含支援端點與最短上手流程。  
[docs/line-message-api-2.0.md](docs/line-message-api-2.0.md)

## 授權

本專案採用 MIT License，詳見 [LICENSE](LICENSE)。

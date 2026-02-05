# Libro.LineMessageAPI

![GitHub release](https://img.shields.io/github/v/release/BookHsu/Libro-LineMessageAPI?sort=semver)
![CI](https://github.com/BookHsu/Libro-LineMessageAPI/actions/workflows/ci.yml/badge.svg)
![CodeQL](https://github.com/BookHsu/Libro-LineMessageAPI/actions/workflows/codeql.yml/badge.svg)
![NuGet](https://img.shields.io/nuget/v/Libro.LineMessageAPI)
![NuGet downloads](https://img.shields.io/nuget/dt/Libro.LineMessageAPI)
![License](https://img.shields.io/github/license/BookHsu/Libro-LineMessageAPI)
![Issues](https://img.shields.io/github/issues/BookHsu/Libro-LineMessageAPI)
![PRs](https://img.shields.io/github/issues-pr/BookHsu/Libro-LineMessageAPI)
![.NET](https://img.shields.io/badge/.NET-8%20%7C%209%20%7C%2010-512BD4)

此專案為 LINE Messaging API 的 C# SDK，並以開源專案結構進行整理，方便維護與擴充。

## NuGet

```bash
dotnet add package Libro.LineMessageAPI
```

> 若要使用 DI/Options 方便註冊，請加裝擴充套件：

```bash
dotnet add package Libro.LineMessageAPI.Extensions
```

## 快速使用範例

### 1) 直接建立實例

```csharp
using Libro.LineMessageApi;

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
using Libro.LineMessageApi;
using Libro.LineMessageApi.Extensions;
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

### 3) Webhook 驗證與回覆訊息（最短流程）

```csharp
using Libro.LineMessageApi;
using Libro.LineMessageApi.LineMessageObject;
using Libro.LineMessageApi.LineReceivedObject;
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

## Wiki 同步

本 repo 以 `docs/wiki/` 作為文件來源，可用腳本同步到 GitHub Wiki repo：

```bash
pwsh scripts/sync-wiki.ps1
pwsh scripts/sync-wiki.ps1 -Commit
pwsh scripts/sync-wiki.ps1 -Commit -Push
```

## 授權

本專案採用 MIT License，詳見 [LICENSE](https://github.com/BookHsu/Libro-LineMessageAPI/blob/main/LICENSE)。

## 範例：API 與 Dashboard 兩種流程

此範例提供兩條路徑：

1. API 範例：透過設定或環境變數注入 Channel Access Token / Secret
2. Dashboard 範例：由頁面輸入並存於記憶體，提供快速驗證與即時事件流

### API 範例（注入或環境變數）

設定 `LineChannel`：

```json
{
  "LineChannel": {
    "ChannelAccessToken": "YOUR_CHANNEL_ACCESS_TOKEN",
    "ChannelSecret": "YOUR_CHANNEL_SECRET"
  }
}
```

或使用環境變數：

- `LineChannel__ChannelAccessToken`
- `LineChannel__ChannelSecret`

Webhook 入口：

- `POST /line/hook`

### Dashboard 範例（頁面設定）

新增 Bootstrap 5 + Vue 的 Web UI 範例，支援輸入 Token/Secret、設定 Webhook Endpoint，並即時顯示 webhook 事件。

詳細說明請見：
[docs/example-line-webhook-dashboard.md](docs/example-line-webhook-dashboard.md)






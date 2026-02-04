# LINE Messaging API 2.0 規格速覽與快速上手

本文件彙整 SDK 支援的 LINE Messaging API 2.0 功能，提供標準化的設定流程與可直接套用的範例程式碼，便於快速導入與維護。

## 1. 快速上手流程

1. **建立 LINE Channel**
   - 於 LINE Developers 建立 Provider 與 Messaging API Channel。
   - 取得 **Channel Secret** 與 **Channel Access Token**。
2. **設定 Webhook**
   - 在 LINE Developers 後台設定 HTTPS Webhook URL。
   - 啟用 `Use webhook`。
3. **導入 SDK**
   - 以 NuGet 套件或方案參考方式導入（依部署方式選擇）。
4. **驗證簽章與解析事件**
   - 使用 `LineChannel.VaridateSignature` 驗證 `X-Line-Signature`。
   - 將 webhook body 反序列化為 `LineReceivedMsg`。
5. **使用 Reply Token 回覆**
   - 以 `LineSdkBuilder(...).UseMessages()` 建立後呼叫 `SendReplyMessage`/`SendReplyMessageAsync`。

## 1.1 範例專案（API / Dashboard）

`examples/LineMessageApi.ExampleApi` 提供兩條路徑：

1. **API 範例**
   - 來源：設定檔或環境變數注入
   - Webhook 入口：`POST /line/hook`
   - 設定節點：`LineChannel`（`LineChannel__ChannelAccessToken` / `LineChannel__ChannelSecret`）
2. **Dashboard 範例**
   - 來源：頁面輸入（記憶體保存，重啟即清除）
   - 目的：快速驗證 Webhook 與事件流
   - Webhook 入口：`POST /dashboard/hook`
   - API 端點：`/dashboard/api/line/*`

## 2. 設定方式（必要參數與環境）

### 2.1 LINE Developers 後台設定

1. 在 LINE Developers 建立 Provider 與 Messaging API Channel。
2. 取得以下兩個必要參數：
   - `Channel Secret`
   - `Channel Access Token`
3. 設定 Webhook URL（HTTPS），並啟用 `Use webhook`。
4. 若需在群組/聊天室使用功能，請確認 Bot 已被加入對應群組/聊天室。

### 2.2 服務端設定建議

- **環境變數**（建議）
  - 若直接讀取環境變數（手動建立 `LineSdkBuilder`）：
    - `LINE_CHANNEL_SECRET`
    - `LINE_CHANNEL_ACCESS_TOKEN`
  - 若使用 `LineChannelOptions` / `AddLineSdk(...)`（綁定 `LineChannel` 區段）：
    - `LineChannel__ChannelSecret`
    - `LineChannel__ChannelAccessToken`
- **設定檔**（可選）
  - 以 `appsettings.json` 或秘密管理服務保存，避免硬編碼。
  - SDK 提供 `LineChannelOptions`，預設讀取 `LineChannel` 區段。

### 2.3 SDK 初始化範例（直接建立實例）

```csharp
using LineMessageApiSDK;

var channelSecret = Environment.GetEnvironmentVariable("LINE_CHANNEL_SECRET");
var channelAccessToken = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");

if (string.IsNullOrWhiteSpace(channelSecret) || string.IsNullOrWhiteSpace(channelAccessToken))
{
    throw new InvalidOperationException("缺少 LINE Channel 設定資訊。");
}

var sdk = new LineSdkBuilder(channelAccessToken)
    .UseMessages()
    .Build();
```

### 2.4 SDK 初始化範例（DI 注入 / LineChannelOptions）

> 需安裝 `LibroLineMessageSDK.Extensions`。

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
    sdkBuilder => sdkBuilder.UseMessages());

var serviceProvider = services.BuildServiceProvider();
var sdk = serviceProvider.GetRequiredService<LineSdk>();
```

設定環境變數（綁定 `LineChannel` 區段）：

- `LineChannel__ChannelAccessToken`
- `LineChannel__ChannelSecret`

## 3. SDK 支援的 API 端點（2.0）

| 功能 | 對應方法 | API 端點 |
| --- | --- | --- |
| 回覆訊息 | `SendReplyMessage` / `SendReplyMessageAsync` | `POST /v2/bot/message/reply` |
| 推播訊息 | `SendPushMessage` / `SendPushMessageAsync` | `POST /v2/bot/message/push` |
| 多播訊息 | `SendMulticastMessage` / `SendMulticastMessageAsync` | `POST /v2/bot/message/multicast` |
| 取得使用者檔案 | `Get_User_Data` / `Get_User_DataAsync` | `GET /v2/bot/profile/{userId}` |
| 取得群組/聊天室使用者檔案 | `Get_Group_UserProfile` / `Get_Group_UserProfileAsync` | `GET /v2/bot/{group|room}/{groupId|roomId}/member/{userId}` |
| 取得使用者上傳的內容 | `Get_User_Upload_To_Bot` / `Get_User_Upload_To_BotAsync` | `GET /v2/bot/message/{messageId}/content` |
| 離開群組/聊天室 | `Leave_Room_Or_Group` / `Leave_Room_Or_GroupAsync` | `POST /v2/bot/{group|room}/{groupId|roomId}/leave` |

> SDK 內部已統一呼叫 `https://api.line.me` 與 `https://api-data.line.me` 的 2.0 端點，無需額外設定。

## 4. Webhook 驗證與解析範例

```csharp
using LineMessageApiSDK;
using LineMessageApiSDK.LineReceivedObject;
using System.Net.Http;
using System.Text.Json;

public async Task<IActionResult> Webhook(HttpRequestMessage request)
{
    // 1) 驗證簽章
    if (!LineChannel.VaridateSignature(request, channelSecret))
    {
        return new UnauthorizedResult();
    }

    // 2) 讀取與解析事件
    var body = await request.Content.ReadAsStringAsync();
    var payload = JsonSerializer.Deserialize<LineReceivedMsg>(body);

    // 3) 取出 replyToken 後回覆
    var replyToken = payload?.events?[0]?.replyToken;
    if (!string.IsNullOrEmpty(replyToken))
    {
        var sdk = new LineSdkBuilder(channelAccessToken)
            .UseMessages()
            .Build();

        await sdk.Messages!.SendReplyMessageAsync(
            replyToken,
            new LineMessageApiSDK.LineMessageObject.TextMessage("收到！"));
    }

    return new OkResult();
}
```

## 5. 常用訊息類型範例

```csharp
using LineMessageApiSDK.LineMessageObject;

var text = new TextMessage("Hello LINE 2.0");
var image = new ImageMessage
{
    originalContentUrl = "https://example.com/original.jpg",
    previewImageUrl = "https://example.com/preview.jpg"
};
```

> 各訊息型別的欄位限制（長度、格式）已於對應類別註解中標示，請依註解限制設定。

## 6. 常見錯誤排查

- **401 Unauthorized / 403 Forbidden**
  - 檢查 `Channel Access Token` 是否有效且已更新。
- **驗證失敗**
  - 確認 `X-Line-Signature` 與 request body 完全一致（含空白與換行）。
- **訊息無回應**
  - `replyToken` 只有一次性且有時間限制，請在 webhook 立即使用。

## 7. 建議實作流程

1. 在 Webhook 入口先做 **簽章驗證**。
2. 解析事件後分流為 **訊息處理服務**（方便日後擴充）。
3. 回覆訊息與主動推播分離，以符合 **單一責任原則**。
4. 將外部呼叫集中於 SDK（`LineSdkBuilder` / `LineChannel`），以利測試與替換。

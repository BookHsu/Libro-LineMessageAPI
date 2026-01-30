# LINE Messaging API 2.0 規格速覽與快速上手

本文件整理 SDK 目前支援的 LINE Messaging API 2.0 功能，並提供最快速的上手流程與範例程式碼，方便直接套用到專案中。

## 1. 快速上手流程（最短路徑）

1. **建立 LINE Channel**
   - 在 LINE Developers 建立 Provider 與 Messaging API Channel。
   - 取得 **Channel Secret** 與 **Channel Access Token**。
2. **Webhook 設定**
   - 於 LINE Developers 後台設定 webhook URL（HTTPS）。
   - 開啟 `Use webhook`。
3. **在專案中使用 SDK**
   - 建議透過方案參考或 NuGet 套件引用（依你的部署方式）。
4. **驗證簽章並解析事件**
   - 使用 `LineChannel.VaridateSignature` 驗證 `X-Line-Signature`。
   - 將 webhook body 反序列化為 `LineReceivedMsg`。
5. **使用 Reply Token 回覆訊息**
   - `LineChannel.SendReplyMessage` 直接回覆事件訊息。

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
  - `LINE_CHANNEL_SECRET`
  - `LINE_CHANNEL_ACCESS_TOKEN`
- **設定檔**（可選）
  - 以 `appsettings.json` 或秘密管理服務保存，避免硬編碼。

### 2.3 SDK 初始化範例

```csharp
using LineMessageApiSDK;

var channelSecret = Environment.GetEnvironmentVariable("LINE_CHANNEL_SECRET");
var channelAccessToken = Environment.GetEnvironmentVariable("LINE_CHANNEL_ACCESS_TOKEN");

if (string.IsNullOrWhiteSpace(channelSecret) || string.IsNullOrWhiteSpace(channelAccessToken))
{
    throw new InvalidOperationException("缺少 LINE Channel 設定資訊。");
}

var channel = new LineChannel(channelAccessToken);
```

## 3. SDK 支援的 API 端點（2.0）

| 功能 | 對應方法 | API 端點 |
| --- | --- | --- |
| 回覆訊息 | `SendReplyMessage` / `SendReplyMessageAsync` | `POST /v2/bot/message/reply` |
| 推播訊息 | `SendPushMessage` / `SendPushMessageAsync` | `POST /v2/bot/message/push` |
| 多播訊息 | `SendMuticastMessage` / `SendMuticastMessageAsync` | `POST /v2/bot/message/multicast` |
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
        var channel = new LineChannel(channelAccessToken);
        channel.SendReplyMessage(replyToken, new LineMessageApiSDK.LineMessageObject.TextMessage("收到！"));
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
4. 所有外部呼叫集中在 `LineChannel`，讓測試與替換更容易。

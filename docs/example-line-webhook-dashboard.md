# LINE Webhook 控制台範例

此範例位於 `examples/LineMessageApi.ExampleApi`，提供一個簡易的 Web UI，讓使用者輸入 LINE Channel 資訊後：

- 自動設定 Webhook Endpoint
- 取得 Bot 基本資訊與 Webhook 設定
- 即時顯示 webhook 事件（SignalR 推送）
- 設定資料僅保存在記憶體，重啟即清除
- 主要用於快速驗證 Webhook 與事件流

> 注意：本範例僅供開發與示範用途，請勿直接用於正式環境。

## 先備條件

- 公開可連線的 HTTPS 網址（部署後提供給 LINE Webhook）
- LINE Channel Access Token
- LINE Channel Secret

## 啟動範例

```bash
dotnet run --project examples/LineMessageApi.ExampleApi/LineMessageApi.ExampleApi.csproj
```

開啟瀏覽器：

```
http://localhost:5175/
```

## 操作步驟

1. 在首頁輸入 Channel Access Token 與 Channel Secret。
2. Webhook URL 可留空，系統會自動使用 `https://你的網域/dashboard/hook`。
3. 勾選「自動設定 LINE Webhook Endpoint」後按「儲存並同步」。
4. 右側會顯示 Bot 資訊與 Webhook Endpoint 狀態。
5. 當 LINE 送入 webhook 事件後，前端會即時顯示在事件列表。

## Dashboard API 端點

- `POST /dashboard/api/line/config`：設定 token/secret 並更新 Webhook Endpoint
- `GET /dashboard/api/line/info`：取得 Bot 資訊與 Webhook Endpoint
- `GET /dashboard/api/line/events`：取得最近 webhook 事件
- `POST /dashboard/hook`：Dashboard Webhook 入口
- `GET /`：前端控制台頁面

## 記憶體儲存行為

- 設定資料與事件記錄皆存於記憶體
- 站台重啟後資料會清除

## 常見問題

- **無法更新 Webhook Endpoint？**
  請確認 Access Token 權限與網域可公開存取。
- **收不到事件？**
  請確認 LINE Developers 後台 Webhook 已啟用，且 webhook URL 可被 LINE 連線。

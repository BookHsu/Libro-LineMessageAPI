# 範例

此頁整理可直接執行的範例與建議使用方式。

## 範例專案

- [examples/LineMessageApi.ExampleApi](https://github.com/BookHsu/LibroLineMessageApi/tree/main/examples/LineMessageApi.ExampleApi)

## 執行方式（建議）

1. 設定環境變數
   - `LINE_CHANNEL_ACCESS_TOKEN`
   - `LINE_CHANNEL_SECRET`
2. 啟動範例

```bash
dotnet run --project examples/LineMessageApi.ExampleApi
```

## 範例內容

此範例提供兩條路徑：

1. API 範例：透過設定或環境變數注入 Channel Access Token / Secret
2. Dashboard 範例：由頁面輸入並存於記憶體，提供快速驗證與即時事件流

- Webhook 驗證
- 回覆文字訊息
- 最短消息流程
- 透過 `AddLineSdk(...)` 與 `LineChannelOptions` 綁定設定
 - Dashboard Webhook 控制台（SignalR 即時事件）

# 範例

此頁整理可直接執行的範例與建議使用方式。

## 範例專案

- [examples/LineMessageApi.ExampleApi](https://github.com/BookHsu/LibroLineMessageApi/tree/main/examples/LineMessageApi.ExampleApi)

## 執行方式（建議）

1. 設定環境變數
   - `LINE_CHANNEL_ACCESS_TOKEN`
   - `LINE_CHANNEL_SECRET`
2. 啟動範例 API

```bash
dotnet run --project examples/LineMessageApi.ExampleApi
```

## 範例內容

- Webhook 驗證
- 回覆文字訊息
- 最短消息流程
- 透過 `AddLineSdk(...)` 與 `LineChannelOptions` 綁定設定

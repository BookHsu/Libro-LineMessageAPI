# 參考

這裡提供 SDK 的核心概念與常用入口，讓你知道從哪裡開始。

## 核心概念

- `LineSdkBuilder`：SDK 組態入口
- `LineSdk`：建置後的主要操作物件
- `LineChannel`：Webhook 驗證等通用工具
- `LineChannelOptions`：Access Token / Secret 設定物件
- `AddLineSdk(...)`：DI/Options 快速註冊（需 `LibroLineMessageSDK.Extensions`）

## 常見入口

- `UseBot()`：Bot API
- `UseMessages()`：訊息相關 API

## 常見物件

- `LineReceivedMsg`：Webhook 事件模型
- `TextMessage`：文字訊息

## 相關文件

- [LINE Messaging API 2.0 規格速覽](https://github.com/BookHsu/LibroLineMessageApi/blob/main/docs/line-message-api-2.0.md)
- [參考範例專案](https://github.com/BookHsu/LibroLineMessageApi/tree/main/examples/LineMessageApi.ExampleApi)

## 建議閱讀順序

1. 快速開始
2. LINE Messaging API 2.0 規格速覽
3. 範例專案

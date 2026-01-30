# 專案調整報告

## 目的

- 將專案調整為標準開源結構，方便維護與協作。
- 建立 Git Flow 與 CI/CD 流程。

## 本次調整內容

- 新增 `docs/`、`doc/`、`release/`、`src/`、`tests/` 等資料夾。
- 原始碼移至 `src/LineMessageApiSDK`。
- 新增 Git Flow 與 CI/CD 文件。
- 新增單元測試專案並納入 CI。
- 補充既有專案初始化 Git Flow 與建立 `develop` 分支的說明。
- 升級 SDK 與測試專案至 .NET 10，並改用 SDK-style 專案格式。
- 更新方案檔案版本與測試套件至最新版本。
- 移除 Newtonsoft.Json 依賴，改用 System.Text.Json 並補上序列化測試。
- 抽出 JSON 序列化介面與實作，並以 DI 注入至 API 呼叫流程。

## 後續建議

- 補上 LICENSE 與使用範例。
- 擴充測試案例以提升覆蓋率。

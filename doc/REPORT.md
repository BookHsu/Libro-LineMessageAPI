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
- 依據最新 LINE Messaging API 規格，整理 API 端點並將內容下載改為 api-data 網域。
- 更新專案版本至 2.0.1，並補上 MIT 授權資訊。
- README 建置指令改用 dotnet build 以避免依賴 msbuild。
- 新增 LINE Messaging API 2.0 規格速覽與快速上手文件，並在 README 加入入口連結。
- 補充 LINE Messaging API 2.0 設定方式與 SDK 初始化範例。
- CD 在建立版本標籤時自動打包並發佈 NuGet 套件（含符號檔）。
- 調整 README 連結為 GitHub 絕對路徑，確保 NuGet 套件頁面顯示一致。
- 修正 NuGet 套件的專案與版本庫 URL 以避免不必要的錨點字元。
- 新增 `LineChannelOptions` 至 SDK，並提供 DI/Options 擴充套件。
- 更新範例專案與文件，改用 `AddLineSdk(...)` 設定流程。
- 版本更新至 2.1.2，並補上 Extensions 套件打包流程。
- 補上 MSTest 平行化設定的命名空間引用並啟用 Nullable 註記範圍，修正建置失敗與警告。
- 補上 Postback 動作留言欄位並新增序列化測試（含空值忽略）。
- 調整 `LineChannelOptions` 為非 nullable string 並提供預設值。

## 後續建議

- 補充使用範例。
- 擴充測試案例以提升覆蓋率。

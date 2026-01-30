# CI/CD 流程

## CI（持續整合）

- 觸發條件：
  - Push 到 `main` 或 `develop`
  - 對 `main` 或 `develop` 的 Pull Request
- 工作內容：
  - 還原套件
  - 建置方案
  - 執行單元測試

## CD（持續交付）

- 觸發條件：
  - 建立 `v*` 版本標籤
- 工作內容：
  - Release 模式建置
  - 打包 NuGet 套件（含符號檔）並自動發佈至 NuGet.org
  - 將產物輸出為 Artifact 供下載

## 工作流程檔案

- CI：`.github/workflows/ci.yml`
- CD：`.github/workflows/cd.yml`

# Git Flow

本專案採用 Git Flow 作為分支管理策略，以確保功能開發、修補與發行流程清晰。

## 分支說明

- `main`：穩定可發行版本
- `develop`：日常開發整合分支
- `feature/<name>`：新功能開發
- `release/<version>`：發行前整合與修正
- `hotfix/<name>`：緊急修補

## 作業流程

1. **開發新功能**
   - 從 `develop` 建立 `feature/<name>`
   - 開發完成後發 PR 回 `develop`

2. **發行準備**
   - 從 `develop` 建立 `release/<version>`
   - 完成最後測試與版本號調整後合併回 `main` 與 `develop`

3. **緊急修補**
   - 從 `main` 建立 `hotfix/<name>`
   - 完成後合併回 `main` 與 `develop`

## 版本標記

- 在 `main` 上建立標籤：`v<major>.<minor>.<patch>`
- CI/CD 會根據標籤啟動 Release 流程

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

## 既有專案初始化（補齊 develop 分支）

若專案一開始未使用 Git Flow，可能會因為不存在 `develop` 分支而導致「結束 release/hotfix 無法合併到 develop」的情況。請先建立並推送 `develop` 分支，再初始化 Git Flow：

```bash
git checkout -b develop main
git push -u origin develop
git flow init -d
```

> 提示：本專案已提供 `.gitflow` 設定檔，可直接套用分支命名與前綴規範。

## 版本標記

- 在 `main` 上建立標籤：`v<major>.<minor>.<patch>`
- CI/CD 會根據標籤啟動 Release 流程

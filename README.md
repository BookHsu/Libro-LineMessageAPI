# LineMessageApiSDK

此專案為 LINE Messaging API 的 C# SDK，並以開源專案結構進行整理，方便維護與擴充。

## 專案結構

```
.github/          # CI/CD 工作流程
 docs/            # 對外文件（架構、流程、使用說明）
 doc/             # 專案報告與內部紀錄
 release/         # 發行說明與版本資訊
 src/             # 原始碼
 tests/           # 單元測試
```

## Git Flow

- 主要分支：`main`
- 開發分支：`develop`
- 功能分支：`feature/<name>`
- 修補分支：`hotfix/<name>`
- 發行分支：`release/<version>`

詳細流程請參考 [docs/git-flow.md](docs/git-flow.md)。

## CI/CD

- CI：在 Push/PR 時進行建置與測試。
- CD：在建立 tag (`v*`) 時執行打包並產出 Release Artifact。

詳細流程請參考 [docs/ci-cd.md](docs/ci-cd.md)。

## 開發與測試

```bash
# 建置
dotnet build LineMessageApiSDK.sln -c Release

# 執行測試
dotnet test tests/LineMessageApiSDK.Tests/LineMessageApiSDK.Tests.csproj
```

## 授權

本專案採用 MIT License，詳見 [LICENSE](LICENSE)。

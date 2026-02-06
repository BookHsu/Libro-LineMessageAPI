# API Docs Workflow

## Goal

Generate API reference pages from C# XML comments and keep localized docs in separate folders.

## Locales

- `docs/api/en-US`
- `docs/api/zh-TW`

## Commands

```powershell
pwsh scripts/build-api-docs.ps1
pwsh scripts/build-api-docs.ps1 -Locale en-US
pwsh scripts/build-api-docs.ps1 -Locale zh-TW
```

## Notes

- API signatures are shared from source code.
- Language-specific explanations live in locale folders.
- If `docfx` is not found, install it with: `dotnet tool update -g docfx`

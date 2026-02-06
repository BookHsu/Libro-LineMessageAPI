param(
    [ValidateSet("all", "en-US", "zh-TW")]
    [string]$Locale = "zh-TW",
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
Push-Location $root

try {
    $docfxCommand = Get-Command docfx -ErrorAction SilentlyContinue
    if (-not $docfxCommand) {
        throw "docfx is required. Install it with: dotnet tool update -g docfx"
    }

    Write-Host "Restoring solution..."
    dotnet restore Libro.LineMessageApi.sln

    Write-Host "Building SDK projects with XML documentation..."
    dotnet build src/Libro.LineMessageAPI/Libro.LineMessageAPI.csproj -c $Configuration --no-restore
    dotnet build src/Libro.LineMessageAPI.Extensions/Libro.LineMessageAPI.Extensions.csproj -c $Configuration --no-restore

    if ($Locale -eq "all" -or $Locale -eq "en-US") {
        Write-Host "Generating DocFX site: en-US"
        & $docfxCommand.Source docs/api/docfx.en-US.json
    }

    if ($Locale -eq "all" -or $Locale -eq "zh-TW") {
        Write-Host "Generating DocFX site: zh-TW"
        & $docfxCommand.Source docs/api/docfx.zh-TW.json
    }

    Write-Host "Done. Output path: docs/api/_site"
}
finally {
    Pop-Location
}

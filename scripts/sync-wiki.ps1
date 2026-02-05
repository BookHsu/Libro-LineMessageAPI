param(
    [string]$Source = "$(Resolve-Path "$PSScriptRoot\..\docs\wiki")",
    [string]$Destination = "$(Resolve-Path "$PSScriptRoot\..\Libro-LineMessageAPI.wiki")",
    [switch]$Commit,
    [switch]$Push
)

if (-not (Test-Path $Source)) {
    Write-Error "Source path not found: $Source"
    exit 1
}

if (-not (Test-Path $Destination)) {
    Write-Error "Destination wiki repo not found: $Destination"
    Write-Error "Please clone the wiki repo to $Destination"
    exit 1
}

Write-Host "Syncing wiki from $Source to $Destination"

# Mirror docs/wiki to the wiki repo
robocopy $Source $Destination /MIR /R:2 /W:1 /NFL /NDL /NJH /NJS | Out-Null

Write-Host "Sync complete."

if ($Commit -or $Push) {
    Push-Location $Destination
    try {
        git status --short | Out-Host
        if ($Commit) {
            git add -A
            $msg = "Sync wiki from docs/wiki"
            git commit -m $msg
        }
        if ($Push) {
            git push
        }
    }
    finally {
        Pop-Location
    }
}


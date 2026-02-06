param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
Push-Location $root

function Get-EnglishSummary([string]$memberName) {
    if ([string]::IsNullOrWhiteSpace($memberName)) {
        return "API member."
    }

    $kind = $memberName.Substring(0, 1)
    $full = $memberName.Substring(2)
    $head = $full

    $parenIndex = $head.IndexOf('(')
    if ($parenIndex -ge 0) {
        $head = $head.Substring(0, $parenIndex)
    }

    $hashIndex = $head.IndexOf('#')
    if ($hashIndex -ge 0) {
        $head = $head.Substring(0, $hashIndex)
    }

    $lastDot = $head.LastIndexOf('.')
    $name = if ($lastDot -ge 0) { $head.Substring($lastDot + 1) } else { $head }
    $typeName = if ($lastDot -ge 0) { $head.Substring(0, $lastDot).Split('.')[-1] } else { $head }

    switch ($kind) {
        "T" { return "Represents $name." }
        "M" {
            if ($memberName -like "*#ctor*") {
                return "Initializes a new instance of $typeName."
            }
            return "Executes $name."
        }
        "P" { return "Gets or sets $name." }
        "F" { return "Represents field $name." }
        "E" { return "Occurs when $name is raised." }
        default { return "Represents $name." }
    }
}

function Convert-Xml([string]$sourcePath, [string]$targetPath) {
    if (-not (Test-Path $sourcePath)) {
        throw "Source XML not found: $sourcePath"
    }

    [xml]$xml = Get-Content $sourcePath -Raw
    $membersNode = $xml.doc.members
    if (-not $membersNode) {
        throw "Invalid XML format in: $sourcePath"
    }

    foreach ($member in @($membersNode.member)) {
        $summaryText = Get-EnglishSummary $member.name

        $summaryNode = $member.SelectSingleNode("summary")
        if ($summaryNode) {
            $summaryNode.RemoveAll()
            $summaryNode.InnerText = $summaryText
        }
        else {
            $summary = $xml.CreateElement("summary")
            $summary.InnerText = $summaryText
            [void]$member.AppendChild($summary)
        }

        foreach ($nodeName in @("param", "typeparam", "returns", "remarks", "example", "exception", "value")) {
            foreach ($n in @($member.SelectNodes($nodeName))) {
                [void]$member.RemoveChild($n)
            }
        }
    }

    $targetDir = Split-Path -Parent $targetPath
    if (-not (Test-Path $targetDir)) {
        New-Item -ItemType Directory -Path $targetDir | Out-Null
    }

    $settings = New-Object System.Xml.XmlWriterSettings
    $settings.Indent = $true
    $settings.IndentChars = "  "
    $settings.NewLineChars = "`n"
    $settings.NewLineHandling = "Replace"

    $writer = [System.Xml.XmlWriter]::Create($targetPath, $settings)
    $xml.Save($writer)
    $writer.Dispose()
}

try {
    dotnet build src/Libro.LineMessageAPI/Libro.LineMessageAPI.csproj -c $Configuration
    dotnet build src/Libro.LineMessageAPI.Extensions/Libro.LineMessageAPI.Extensions.csproj -c $Configuration

    Convert-Xml `
      -sourcePath "src/Libro.LineMessageAPI/bin/$Configuration/net8.0/Libro.LineMessageAPI.xml" `
      -targetPath "src/Libro.LineMessageAPI.Language.en-US/contentFiles/any/any/xml/en-US/Libro.LineMessageAPI.xml"

    Convert-Xml `
      -sourcePath "src/Libro.LineMessageAPI.Extensions/bin/$Configuration/net8.0/Libro.LineMessageAPI.Extensions.xml" `
      -targetPath "src/Libro.LineMessageAPI.Language.en-US/contentFiles/any/any/xml/en-US/Libro.LineMessageAPI.Extensions.xml"

    Write-Host "Generated en-US XML files."
}
finally {
    Pop-Location
}

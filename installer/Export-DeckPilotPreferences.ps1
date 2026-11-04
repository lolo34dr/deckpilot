param([Parameter(Mandatory = $true)][string]$OutputPath)

$ErrorActionPreference = 'Stop'
$settings = Join-Path $env:APPDATA 'DeckPilot/settings.json'
$autoUpdates = $true
$reduceMotion = $false

if (Test-Path -LiteralPath $settings) {
    $stored = Get-Content -LiteralPath $settings -Raw | ConvertFrom-Json
    if ($null -ne $stored.AutoCheckUpdates) { $autoUpdates = [bool]$stored.AutoCheckUpdates }
    if ($null -ne $stored.ReduceMotion) { $reduceMotion = [bool]$stored.ReduceMotion }
}

$preferenceKey = Get-ItemProperty -LiteralPath 'HKCU:\Software\DeckPilot' -ErrorAction SilentlyContinue
$startup = $true
if ($null -ne $preferenceKey -and $null -ne $preferenceKey.StartupEnabled) {
    $startup = [int]$preferenceKey.StartupEnabled -ne 0
} elseif ($null -ne $preferenceKey -and $null -ne $preferenceKey.StartupConfigured) {
    $startupRun = Get-ItemProperty -LiteralPath 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run' -ErrorAction SilentlyContinue
    $startup = $null -ne $startupRun.DeckPilot
}

$values = @(
    $(if ($startup) { 'on' } else { 'off' }),
    $(if ($autoUpdates) { 'on' } else { 'off' }),
    $(if ($reduceMotion) { 'on' } else { 'off' })
)
[System.IO.File]::WriteAllLines($OutputPath, $values, [System.Text.Encoding]::ASCII)

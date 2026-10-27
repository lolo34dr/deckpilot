$ErrorActionPreference = 'Stop'
trap { Write-Host ("::error::" + ($_.Exception.Message + ' | ' + $_.ScriptStackTrace).Replace("`r", ' ').Replace("`n", ' ')); throw }
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'Use an isolated Windows runner.' }
New-Item -ItemType Directory -Force artifacts/firmware | Out-Null
Invoke-WebRequest -UseBasicParsing 'https://github.com/lolo34dr/deckpilot/releases/download/v1.7.0/DeckPilot-firmware.zip' -OutFile artifacts/firmware/DeckPilot-firmware.zip
$id = [Guid]::NewGuid().ToString()
$gamingId = [Guid]::NewGuid().ToString()
$buttons = @(
    @{ Kind = 'Spotify'; Label = 'Lecture / pause'; Key = 179; Accent = '#78B7A0' }
    @{ Kind = 'Spotify'; Label = 'Suivante'; Key = 176; Accent = '#78B7A0' }
    @{ Kind = 'Media'; Label = 'Volume +'; Key = 175; Accent = '#E3B46F' }
    @{ Kind = 'Microphone'; Label = 'Micro'; Accent = '#DF8898' }
    @{ Kind = 'Folder'; Label = 'Documents'; Target = [Environment]::GetFolderPath('MyDocuments'); Accent = '#70A7E8' }
    @{ Kind = 'Website'; Label = 'GitHub'; Target = 'https://github.com/lolo34dr/deckpilot'; Accent = '#70A7E8' }
    @{ Kind = 'Shortcut'; Label = 'Capture'; Key = 83; Win = $true; Shift = $true; Accent = '#70A7E8' }
    @{ Kind = 'System'; Label = 'Son Windows'; Target = 'sound-settings'; Accent = '#AD91E8' }
    @{ Kind = 'System'; Label = 'Paramètres'; Target = 'settings'; Accent = '#AD91E8' }
    @{ Kind = 'System'; Label = 'Tâches'; Target = 'task-manager'; Accent = '#AD91E8' }
    @{ Kind = 'Profile'; Label = 'Profil de jeu'; Target = $gamingId; Accent = '#DF8898' }
    @{ Kind = 'Profile'; Label = 'Profil suivant'; Target = 'next'; Accent = '#DF8898' }
)
$gamingButtons = @(1..12 | ForEach-Object { @{ Kind = 'Passthrough'; Label = '' } })
$config = Join-Path $env:APPDATA 'DeckPilot\settings.json'
New-Item -ItemType Directory -Force (Split-Path $config) | Out-Null
@{
    Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; AutoCheckFirmware = $false
    KeyMap = @(124..135); ActiveProfileId = $id
    Profiles = @(
        @{ Id = $id; Name = 'Bureau et musique'; Buttons = $buttons }
        @{ Id = $gamingId; Name = 'Jeux'; Buttons = $gamingButtons }
    )
} | ConvertTo-Json -Depth 10 | Set-Content $config -Encoding UTF8
dotnet run --project src/DeckPilot.Windows -c Release -p:PreviewCapture=true -- --preview-capture
if ($LASTEXITCODE -ne 0) { throw "Native WPF capture failed: $LASTEXITCODE" }
$project = [xml](Get-Content src/DeckPilot.Windows/DeckPilot.Windows.csproj -Raw)
$version = $project.SelectSingleNode('/Project/PropertyGroup/Version').InnerText
Add-Content -Path $env:GITHUB_OUTPUT -Value "image=docs/previews/deckpilot-$version.png"

$ErrorActionPreference = 'Stop'
trap { Write-Host ("::error::" + ($_.Exception.Message + ' | ' + $_.ScriptStackTrace).Replace("`r", ' ').Replace("`n", ' ')); throw }
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'Use an isolated Windows runner.' }
New-Item -ItemType Directory -Force artifacts/firmware | Out-Null
Invoke-WebRequest -UseBasicParsing 'https://github.com/lolo34dr/deckpilot/releases/download/v1.6.1/DeckPilot-firmware.zip' -OutFile artifacts/firmware/DeckPilot-firmware.zip
$id = [Guid]::NewGuid().ToString()
$buttons = @(1..12 | ForEach-Object { @{ Kind = 'Passthrough'; Label = '' } })
$buttons[0] = @{ Kind = 'Spotify'; Label = 'Spotify'; Key = 179; LongPressEnabled = $true; LongPressMilliseconds = 600; LongPressAction = @{ Kind = 'Microphone'; Label = 'Micro' } }
$buttons[1] = @{ Kind = 'Spotify'; Label = 'Suivante'; Key = 176 }
$buttons[2] = @{ Kind = 'Media'; Label = 'Volume +'; Key = 175 }
$buttons[3] = @{ Kind = 'Microphone'; Label = 'Micro' }
$buttons[4] = @{ Kind = 'Folder'; Label = 'Documents'; Target = [Environment]::GetFolderPath('MyDocuments') }
$buttons[5] = @{ Kind = 'Website'; Label = 'GitHub'; Target = 'https://github.com/lolo34dr/deckpilot' }
$config = Join-Path $env:APPDATA 'DeckPilot\settings.json'
New-Item -ItemType Directory -Force (Split-Path $config) | Out-Null
@{ Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; AutoCheckFirmware = $false; KeyMap = @(124..135); ActiveProfileId = $id; Profiles = @(@{ Id = $id; Name = "Profil d'exemple"; Buttons = $buttons }) } | ConvertTo-Json -Depth 10 | Set-Content $config -Encoding UTF8
dotnet run --project src/DeckPilot.Windows -c Release -p:PreviewCapture=true -- --preview-capture
if ($LASTEXITCODE -ne 0) { throw "Native WPF capture failed: $LASTEXITCODE" }

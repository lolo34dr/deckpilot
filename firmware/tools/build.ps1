$ErrorActionPreference = 'Stop'
# Run from the repository root, on Windows, with Arduino CLI 1.2.2 on PATH.
function Invoke-Arduino {
    & arduino-cli @args
    if ($LASTEXITCODE -ne 0) { throw "Arduino CLI failed: $args" }
}
Invoke-Arduino core update-index
Invoke-Arduino core install arduino:avr@1.8.6
Invoke-Arduino lib install Keyboard@1.0.6
Invoke-Arduino compile --fqbn arduino:avr:leonardo --warnings all --output-dir artifacts/firmware-build firmware/DeckPilot

$stage = 'artifacts/firmware-stage'
if (Test-Path $stage) { Remove-Item -Recurse -Force $stage }
New-Item -ItemType Directory -Force "$stage/tools/bin", "$stage/tools/etc", artifacts/firmware, artifacts/firmware-downloads | Out-Null
function Get-Verified([string]$Url, [string]$Destination, [string]$Hash) {
    Invoke-WebRequest $Url -OutFile $Destination
    if ((Get-FileHash $Destination -Algorithm SHA256).Hash.ToLowerInvariant() -ne $Hash) { throw "Checksum mismatch: $Url" }
}
# Upstream AVRDUDE 8.0, pinned release archives, no dependency on an installed Arduino IDE.
Get-Verified 'https://github.com/avrdudes/avrdude/releases/download/v8.0/avrdude-v8.0-windows-x64.zip' 'artifacts/firmware-downloads/avrdude.zip' 'f4aa811042ef95b52c68531f6e5044c5b5a8711bcd4b495d6b9af20f9ac41325'
Get-Verified 'https://github.com/avrdudes/avrdude/releases/download/v8.0/avrdude-8.0.zip' "$stage/avrdude-source.zip" 'dab126d2fd9d0160f732a35501632e4bc63d9278e3391ebd3ced6425752c2817'
Expand-Archive artifacts/firmware-downloads/avrdude.zip artifacts/firmware-downloads/avrdude -Force
Copy-Item artifacts/firmware-downloads/avrdude/avrdude.exe "$stage/tools/bin/avrdude.exe"
Copy-Item artifacts/firmware-downloads/avrdude/avrdude.conf "$stage/tools/etc/avrdude.conf"
Copy-Item artifacts/firmware-build/DeckPilot.ino.hex "$stage/DeckPilot.hex"
& "$stage/tools/bin/avrdude.exe" '-C' "$stage/tools/etc/avrdude.conf" '-?'
if ($LASTEXITCODE -ne 0) { throw 'Bundled AVRDUDE cannot run on Windows' }
Add-Type -AssemblyName System.IO.Compression.FileSystem
$source = [IO.Compression.ZipFile]::OpenRead((Resolve-Path "$stage/avrdude-source.zip"))
try {
    $license = $source.Entries | Where-Object { $_.FullName -match '/avrdude/COPYING$|/COPYING$' } | Select-Object -First 1
    if (!$license) { throw 'AVRDUDE license missing from source archive' }
    [IO.Compression.ZipFileExtensions]::ExtractToFile($license, (Join-Path (Resolve-Path $stage) 'AVRDUDE-LICENSE.txt'), $true)
} finally { $source.Dispose() }
Copy-Item firmware/README.md "$stage/README.md"
Copy-Item firmware/Original/Original.ino "$stage/Original.ino"
$hash = (Get-FileHash "$stage/DeckPilot.hex" -Algorithm SHA256).Hash.ToLowerInvariant()
@{ Board = 'promicro-32u4-16mhz'; Version = '1.0.0'; Sha256 = $hash } | ConvertTo-Json | Set-Content "$stage/manifest.json" -Encoding utf8
Compress-Archive -Path "$stage/*" -DestinationPath artifacts/firmware/DeckPilot-firmware.zip -Force

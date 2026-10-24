$ErrorActionPreference = 'Stop'
# CI bootstrap only. Keep the compiler version and upstream archive hash pinned.
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'This bootstrap is reserved for the disposable Windows CI runner.' }
$directory = Join-Path $env:RUNNER_TEMP 'DeckPilot-ArduinoCLI-1.2.2'
$archive = $directory + '.zip'
Invoke-WebRequest 'https://github.com/arduino/arduino-cli/releases/download/v1.2.2/arduino-cli_1.2.2_Windows_64bit.zip' -OutFile $archive
$expected = 'bdd3ed88a361af8539e51a1cc0bf831b269be155ddfdd90cb96a900ce78723b7'
if ((Get-FileHash $archive -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expected) { throw 'Arduino CLI archive checksum mismatch.' }
Expand-Archive $archive $directory -Force
& (Join-Path $directory 'arduino-cli.exe') version
if ($LASTEXITCODE -ne 0) { throw 'Arduino CLI cannot start.' }
Add-Content -Path $env:GITHUB_PATH -Value $directory

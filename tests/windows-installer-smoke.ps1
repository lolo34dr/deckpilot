param([Parameter(Mandatory=$true)][string]$Installer)
$ErrorActionPreference = 'Stop'
function Read-RegistryValue([string]$Path, [string]$Name) {
    try { return (Get-ItemProperty -LiteralPath $Path -ErrorAction Stop).$Name }
    catch { return $null }
}
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'This installation test must run on the disposable GitHub Actions Windows runner.' }
$phase = 'initialize'
trap {
    $detail = ($phase + ': ' + $_.Exception.Message + ' | ' + $_.ScriptStackTrace).Replace('%','%25').Replace("`r",'%0D').Replace("`n",'%0A')
    Write-Host "::error title=Installer smoke::$detail"
    throw
}
function Wait-Until([scriptblock]$Condition) {
    $deadline = [DateTime]::UtcNow.AddSeconds(40)
    do { if (& $Condition) { return }; Start-Sleep -Milliseconds 200 } while ([DateTime]::UtcNow -lt $deadline)
    throw 'Timed out waiting for installed application.'
}
function Run-Setup([string]$File, [string[]]$Options) {
    $process = Start-Process $File -ArgumentList $Options -PassThru
    if (!$process.WaitForExit(90000)) { throw 'Installer did not complete within 90 seconds.' }
    if ($process.ExitCode -ne 0) { throw "Installer failed with code $($process.ExitCode)" }
}
$setup = (Resolve-Path $Installer).Path
$directory = Join-Path $env:ProgramFiles 'DeckPilot'
$executable = Join-Path $directory 'DeckPilot.exe'
$uninstaller = Join-Path $directory 'unins000.exe'
$uninstallKey = 'HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\{4B12540A-BC41-4A58-B4C6-B71C9D1FC664}_is1'
$desktop = Join-Path ([Environment]::GetFolderPath('CommonDesktopDirectory')) 'DeckPilot.lnk'
$startMenu = Join-Path ([Environment]::GetFolderPath('CommonPrograms')) 'DeckPilot.lnk'
$settingsPath = Join-Path $env:APPDATA 'DeckPilot/settings.json'
$oldSettings = if (Test-Path $settingsPath) { [IO.File]::ReadAllBytes($settingsPath) } else { $null }
$startupKey = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run'
$oldStartup = Read-RegistryValue $startupKey DeckPilot
$oldPreference = Read-RegistryValue 'HKCU:\Software\DeckPilot' StartupConfigured
$app = $null; $updatedApp = $null
try {
    $phase = 'fresh installation into Program Files'
    Run-Setup $setup @('/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART',('/LOG="' + (Join-Path $env:TEMP 'deckpilot-install.log') + '"'))
    foreach ($file in @($executable, (Join-Path $directory 'DeckPilot.install'), $uninstaller, $desktop, $startMenu)) {
        if (!(Test-Path $file)) { throw "Missing installed file: $file" }
    }
    if (!(Test-Path $uninstallKey)) { throw 'Missing Windows uninstall registration' }
    $shell = New-Object -ComObject WScript.Shell
    foreach ($shortcut in @($desktop, $startMenu)) {
        if ($shell.CreateShortcut($shortcut).TargetPath -ne $executable) { throw 'Shortcut points to the wrong executable' }
    }
    if ((Read-RegistryValue $startupKey DeckPilot) -ne ('"' + $executable + '" --background')) { throw 'Startup is not enabled by default for the installing user' }
    Write-Host 'PASS installer: Program Files, desktop and Start menu shortcuts, Windows uninstall registration and default startup'

    $profileId = [Guid]::NewGuid().ToString()
    New-Item -ItemType Directory -Force (Split-Path $settingsPath) | Out-Null
    @{ Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; KeyMap = @(124..135); ActiveProfileId = $profileId; Profiles = @(@{
        Id = $profileId; Name = 'Installer test'; Buttons = @(1..12 | ForEach-Object { @{ Kind = 'Passthrough'; Label = 'Keep me'; Target = ''; Arguments = ''; Key = 65 } })
    }) } | ConvertTo-Json -Depth 10 | Set-Content $settingsPath -Encoding UTF8
    $phase = 'launch installed app and disable startup'
    $app = Start-Process $executable '--background' -PassThru
    if (!$app.WaitForInputIdle(30000)) { throw 'Installed app did not start' }
    Run-Setup $executable @('--configure-startup','off')
    if ($null -ne (Read-RegistryValue $startupKey DeckPilot)) { throw 'Startup preference could not be disabled' }

    $phase = 'upgrade while the installed app is running'
    Run-Setup $setup @('/UPDATE=1','/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART')
    Wait-Until { $app.Refresh(); $app.HasExited }
    Wait-Until {
        $script:updatedApp = Get-Process -Name DeckPilot -ErrorAction SilentlyContinue | Where-Object { $_.Path -eq $executable } | Select-Object -First 1
        $null -ne $script:updatedApp
    }
    if (!$updatedApp.WaitForInputIdle(30000)) { throw 'Updated app did not restart' }
    if ($null -ne (Read-RegistryValue $startupKey DeckPilot)) { throw 'Upgrade overwrote the disabled startup preference' }
    $settings = Get-Content $settingsPath -Raw | ConvertFrom-Json
    if ($settings.Profiles[0].Id -ne $profileId -or $settings.KeyMap.Count -ne 12) { throw 'Upgrade lost profiles or calibration' }
    Write-Host 'PASS installed upgrade: graceful app shutdown, successful replacement and restart, profiles preserved, disabled startup preference respected'

    $phase = 'uninstall while the app is running'
    Run-Setup $executable @('--configure-startup','on')
    Run-Setup $uninstaller @('/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART')
    Wait-Until { !(Test-Path $executable) }
    if ((Test-Path $desktop) -or (Test-Path $startMenu) -or (Test-Path $uninstallKey)) { throw 'Uninstall left shortcuts or Windows registration' }
    if ($null -ne (Read-RegistryValue $startupKey DeckPilot)) { throw 'Uninstall left the installed startup entry' }
    if (!(Test-Path $settingsPath)) { throw 'Uninstall deleted personal profiles' }
    Write-Host 'PASS uninstall: app and shortcuts removed, startup entry removed, personal profiles retained'
}
finally {
    foreach ($process in @($app, $updatedApp)) {
        if ($process -and !$process.HasExited) { Stop-Process -Id $process.Id -Force; $process.WaitForExit() }
    }
    if (Test-Path $uninstaller) { Start-Process $uninstaller -ArgumentList '/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART' -Wait }
    if ($null -ne $oldSettings) { [IO.File]::WriteAllBytes($settingsPath, $oldSettings) }
    elseif (Test-Path $settingsPath) { Remove-Item $settingsPath }
    if ($null -ne $oldStartup) { Set-ItemProperty $startupKey DeckPilot $oldStartup }
    else { try { Remove-ItemProperty $startupKey DeckPilot -ErrorAction Stop } catch { } }
    if ($null -ne $oldPreference) { Set-ItemProperty 'HKCU:\Software\DeckPilot' StartupConfigured $oldPreference }
    else { try { Remove-ItemProperty 'HKCU:\Software\DeckPilot' StartupConfigured -ErrorAction Stop } catch { } }
}

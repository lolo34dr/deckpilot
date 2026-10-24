param([Parameter(Mandatory=$true)][string]$Installer)
$ErrorActionPreference = 'Stop'
. (Join-Path $PSScriptRoot 'WindowsTestIO.ps1')
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
$oldStartupEnabled = Read-RegistryValue 'HKCU:\Software\DeckPilot' StartupEnabled
$approvalPaths = @('HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run', 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run32')
$oldApprovals = @($approvalPaths | ForEach-Object { ,(Read-RegistryValue $_ DeckPilot) })
$app = $null; $updatedApp = $null
try {
    $phase = 'fresh installation into Program Files'
    Run-Setup $setup @('/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART',('/LOG="' + (Join-Path $env:TEMP 'deckpilot-install.log') + '"'))
    foreach ($file in @($executable, (Join-Path $directory 'DeckPilot.install'), (Join-Path $directory 'DeckPilot.ico'), $uninstaller, $desktop, $startMenu)) {
        if (!(Test-Path $file)) { throw "Missing installed file: $file" }
    }
    if (!(Test-Path $uninstallKey)) { throw 'Missing Windows uninstall registration' }
    $shell = New-Object -ComObject WScript.Shell
    foreach ($shortcut in @($desktop, $startMenu)) {
        $link = $shell.CreateShortcut($shortcut)
        if ($link.TargetPath -ne $executable) { throw 'Shortcut points to the wrong executable' }
        if ($link.IconLocation -ne ((Join-Path $directory 'DeckPilot.ico') + ',0')) { throw 'Shortcut does not use the DeckPilot icon' }
    }
    if ((Read-RegistryValue $startupKey DeckPilot) -ne ('"' + $executable + '" --background')) { throw 'Startup is not enabled by default for the installing user' }
    # Compare the actual PE icon resources, not GDI's rendered bitmap: Windows may
    # select a different resolution or alter transparent RGB pixels when rendering.
    Add-Type -TypeDefinition @'
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
public static class DeckPilotIconResource {
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern IntPtr LoadLibraryExW(string path, IntPtr file, uint flags);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    static extern IntPtr FindResourceW(IntPtr module, IntPtr name, IntPtr type);
    [DllImport("kernel32.dll")] static extern uint SizeofResource(IntPtr module, IntPtr resource);
    [DllImport("kernel32.dll")] static extern IntPtr LoadResource(IntPtr module, IntPtr resource);
    [DllImport("kernel32.dll")] static extern IntPtr LockResource(IntPtr resource);
    [DllImport("kernel32.dll")] static extern bool FreeLibrary(IntPtr module);
    public static byte[] Read(string path, int id) {
        var module = LoadLibraryExW(path, IntPtr.Zero, 2);
        if (module == IntPtr.Zero) throw new Win32Exception(Marshal.GetLastWin32Error());
        try {
            var resource = FindResourceW(module, (IntPtr)id, (IntPtr)3);
            if (resource == IntPtr.Zero) throw new Exception("Missing PE icon resource " + id);
            var size = SizeofResource(module, resource);
            var pointer = LockResource(LoadResource(module, resource));
            if (size == 0 || pointer == IntPtr.Zero) throw new Exception("Empty PE icon resource " + id);
            var bytes = new byte[size]; Marshal.Copy(pointer, bytes, 0, bytes.Length); return bytes;
        } finally { FreeLibrary(module); }
    }
}
'@
    $ico = [IO.File]::ReadAllBytes((Join-Path $directory 'DeckPilot.ico'))
    $count = [BitConverter]::ToUInt16($ico, 4)
    if ($count -lt 7) { throw 'Missing high-resolution Windows icon frames' }
    for ($index = 0; $index -lt $count; $index++) {
        $entry = 6 + 16 * $index
        $length = [BitConverter]::ToUInt32($ico, $entry + 8)
        $offset = [BitConverter]::ToUInt32($ico, $entry + 12)
        $expected = New-Object byte[] $length
        [Array]::Copy($ico, $offset, $expected, 0, $length)
        $actual = [DeckPilotIconResource]::Read($executable, $index + 1)
        if ([Convert]::ToBase64String($actual) -ne [Convert]::ToBase64String($expected)) { throw "PE icon frame $index does not match the DeckPilot logo" }
    }
    Write-Host 'PASS installer: Program Files, desktop and Start menu shortcuts, Windows uninstall registration and default startup'

    $profileId = [Guid]::NewGuid().ToString()
    New-Item -ItemType Directory -Force (Split-Path $settingsPath) | Out-Null
    @{ Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; KeyMap = @(124..135); ActiveProfileId = $profileId; Profiles = @(@{
        Id = $profileId; Name = 'Installer test'; Buttons = @(1..12 | ForEach-Object { @{ Kind = 'Passthrough'; Label = 'Keep me'; Target = ''; Arguments = ''; Key = 65 } })
    }) } | ConvertTo-Json -Depth 10 | Set-Content $settingsPath -Encoding UTF8
    $phase = 'respect a Windows startup block until explicit activation'
    New-Item -Path $approvalPaths[0] -Force | Out-Null
    $disabled = New-Object byte[] 12; $disabled[0] = 3
    New-ItemProperty -Path $approvalPaths[0] -Name DeckPilot -PropertyType Binary -Value $disabled -Force | Out-Null
    $app = Start-Process $executable '--background' -PassThru
    if (!$app.WaitForInputIdle(30000)) { throw 'Installed app did not start' }
    if ((Read-RegistryValue $approvalPaths[0] DeckPilot)[0] -ne 3) { throw 'Normal initialization overrode the Windows startup block' }
    Run-Setup $executable @('--configure-startup','on')
    if ($null -ne (Read-RegistryValue $approvalPaths[0] DeckPilot)) { throw 'Explicit activation did not clear the known Windows disable flag' }
    $phase = 'repair a missing startup entry and launch silently'
    Remove-ItemProperty $startupKey DeckPilot
    Run-Setup $executable @('--exit-for-install')
    Wait-Until { $app.Refresh(); $app.HasExited }
    $app = Start-Process $executable '--background' -PassThru
    if (!$app.WaitForInputIdle(30000)) { throw 'Background app did not restart' }
    Wait-Until { (Read-RegistryValue $startupKey DeckPilot) -eq ('"' + $executable + '" --background') }
    # Registry repair runs in the constructor, before WPF's Loaded handler hides
    # the background window. Input-idle is not a Loaded completion signal.
    $script:hiddenSamples = 0
    Wait-Until {
        $app.Refresh()
        if ($app.HasExited) { throw 'Background app exited instead of remaining in the tray' }
        if ($app.MainWindowHandle -eq [IntPtr]::Zero) { $script:hiddenSamples++ } else { $script:hiddenSamples = 0 }
        $script:hiddenSamples -ge 5
    }
    $duplicate = Start-Process $executable '--background' -PassThru
    if (!$duplicate.WaitForExit(10000)) { Stop-Process $duplicate.Id; throw 'Duplicate startup showed a blocking prompt' }
    Write-Host 'PASS startup repair: Windows block respected, explicit activation repairs it, missing entry restored, background launch and duplicate startup are silent'
    $phase = 'disable startup'
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
    $settings = Read-SharedJson $settingsPath
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
    if ($null -ne $oldStartupEnabled) { Set-ItemProperty 'HKCU:\Software\DeckPilot' StartupEnabled $oldStartupEnabled }
    else { try { Remove-ItemProperty 'HKCU:\Software\DeckPilot' StartupEnabled -ErrorAction Stop } catch { } }
    for ($i = 0; $i -lt $approvalPaths.Count; $i++) {
        if ($null -ne $oldApprovals[$i]) { New-Item -Path $approvalPaths[$i] -Force | Out-Null; New-ItemProperty -Path $approvalPaths[$i] -Name DeckPilot -PropertyType Binary -Value $oldApprovals[$i] -Force | Out-Null }
        else { try { Remove-ItemProperty $approvalPaths[$i] DeckPilot -ErrorAction Stop } catch { } }
    }
}

param([Parameter(Mandatory=$true)][string]$Installer)
$ErrorActionPreference = 'Stop'
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'This installation test runs only on the disposable GitHub Actions Windows runner.' }
Add-Type -AssemblyName UIAutomationClient
$phase = 'initialize'
trap {
    $detail = ($phase + ': ' + $_.Exception.Message + ' | ' + $_.ScriptStackTrace).Replace('%','%25').Replace("`r",'%0D').Replace("`n",'%0A')
    $startupLog = Join-Path $env:LOCALAPPDATA 'DeckPilotFactory/startup-errors.log'
    if (Test-Path $startupLog) { $detail += ' | Atelier startup log: ' + (Get-Content $startupLog -Raw) }
    if ($env:GITHUB_STEP_SUMMARY) {
        $summaryDetail = $phase + ': ' + $_.Exception.Message + ' | ' + $_.ScriptStackTrace
        if (Test-Path $startupLog) { $summaryDetail += "`n`nAtelier startup log:`n" + (Get-Content $startupLog -Raw) }
        Add-Content -LiteralPath $env:GITHUB_STEP_SUMMARY -Value ("`n## Atelier installer smoke failure`n`n$summaryDetail") -Encoding UTF8
    }
    Write-Host "::error title=Atelier installer smoke::$detail"
    throw
}
function Wait-Until([scriptblock]$Condition) {
    $deadline = [DateTime]::UtcNow.AddSeconds(45)
    do { if (& $Condition) { return }; Start-Sleep -Milliseconds 200 } while ([DateTime]::UtcNow -lt $deadline)
    throw 'Timed out waiting for DeckPilot Atelier.'
}
function Run-Setup([string]$File, [string[]]$Options) {
    $process = Start-Process $File -ArgumentList $Options -PassThru
    if (!$process.WaitForExit(90000)) { throw 'DeckPilot Atelier setup did not complete within 90 seconds.' }
    if ($process.ExitCode -ne 0) { throw "DeckPilot Atelier setup failed with code $($process.ExitCode)" }
}
function Get-AtelierStartupDiagnostics {
    $path = Join-Path $env:LOCALAPPDATA 'DeckPilotFactory/startup-errors.log'
    if (Test-Path $path) { return Get-Content $path -Raw }
    return 'No startup-errors.log was created.'
}
$setup = (Resolve-Path $Installer).Path
$directory = Join-Path $env:ProgramFiles 'DeckPilot Atelier'
$executable = Join-Path $directory 'DeckPilot.Factory.exe'
$uninstaller = Join-Path $directory 'unins000.exe'
$desktop = Join-Path ([Environment]::GetFolderPath('CommonDesktopDirectory')) 'DeckPilot Atelier.lnk'
$startMenu = Join-Path ([Environment]::GetFolderPath('CommonPrograms')) 'DeckPilot Atelier.lnk'
$app = $null; $updated = $null
try {
    $phase = 'fresh silent install into Program Files'
    Run-Setup $setup @('/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART')
    foreach ($file in @($executable,$uninstaller,$desktop,$startMenu)) { if (!(Test-Path $file)) { throw "Missing installed file: $file" } }
    $shell = New-Object -ComObject WScript.Shell
    foreach ($shortcut in @($desktop,$startMenu)) {
        $link = $shell.CreateShortcut($shortcut)
        if ($link.TargetPath -ne $executable) { throw 'Atelier shortcut targets the wrong executable.' }
    }
    Write-Host 'PASS Atelier installer: Program Files and both shortcuts'

    $phase = 'start installed WPF application'
    $app = Start-Process $executable -PassThru
    try { $idle = $app.WaitForInputIdle(30000) }
    catch { $app.Refresh(); $exit = if ($app.HasExited) { $app.ExitCode } else { 'still running' }; throw "Installed Atelier failed to reach its WPF message loop (exit=$exit). $(Get-AtelierStartupDiagnostics)" }
    if (!$idle) { $app.Refresh(); $exit = if ($app.HasExited) { $app.ExitCode } else { 'still running' }; throw "Installed Atelier did not reach its WPF message loop (exit=$exit). $(Get-AtelierStartupDiagnostics)" }
    Wait-Until {
        $app.Refresh()
        if ($app.HasExited) { throw "Installed Atelier exited before showing its main window (exit=$($app.ExitCode)). $(Get-AtelierStartupDiagnostics)" }
        $app.MainWindowHandle -ne [IntPtr]::Zero
    }
    if ($app.MainWindowTitle -ne 'DeckPilot Atelier') { throw "Unexpected Atelier window title: $($app.MainWindowTitle)" }
    Write-Host 'PASS Atelier startup: the installed application opened its main window'

    $phase = 'verify safe flash and advanced erase controls'
    $window = [System.Windows.Automation.AutomationElement]::FromHandle($app.MainWindowHandle)
    $advancedCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'AdvancedOperationsExpander')
    $advanced = $window.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $advancedCondition)
    if (!$advanced) { throw 'The destructive reset action must be grouped under advanced operations.' }
    $expandCollapse = $advanced.GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern)
    if ($expandCollapse.Current.ExpandCollapseState -ne [System.Windows.Automation.ExpandCollapseState]::Collapsed) { throw 'Advanced operations must be collapsed by default.' }
    $expandCollapse.Expand()
    $eraseCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'EraseBoardButton')
    $eraseButton = $window.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $eraseCondition)
    if (!$eraseButton) { throw 'Atelier advanced erase action is missing from the installed application.' }
    if ($eraseButton.Current.IsEnabled) { throw 'Erase action must remain disabled without a selected physical device.' }
    $flashCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'FlashButton')
    $flashButton = $window.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $flashCondition)
    if (!$flashButton) { throw 'Atelier must expose firmware installation as the normal recovery action.' }
    if ($flashButton.Current.IsEnabled) { throw 'Firmware installation must remain disabled without a selected physical device.' }
    $recoveryCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'RecoveryInstructions')
    $recoveryInstructions = $window.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $recoveryCondition)
    if (!$recoveryInstructions -or $recoveryInstructions.Current.Name -notmatch 'RST' -or $recoveryInstructions.Current.Name -notmatch 'GND') { throw 'Atelier must show exact manual reset recovery steps.' }
    $firmwareCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'FirmwareVersionLabel')
    $firmwareLabel = $window.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $firmwareCondition)
    if (!$firmwareLabel -or $firmwareLabel.Current.Name -notlike 'Firmware inclus : *') { throw 'Atelier rejected the bundled firmware or reset image.' }
    Write-Host 'PASS Atelier reset safety: normal repair action, collapsed advanced erase, no-device guard and RST/GND recovery guidance'

    $phase = 'duplicate launch exits cleanly'
    $duplicate = Start-Process $executable -PassThru
    if (!$duplicate.WaitForExit(15000)) { Stop-Process -Id $duplicate.Id -Force; throw 'A duplicate Atelier instance did not close.' }

    $phase = 'update an open Atelier installation and relaunch'
    Run-Setup $setup @('/UPDATE=1','/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART')
    Wait-Until { $app.Refresh(); $app.HasExited }
    Wait-Until {
        $script:updated = Get-Process -Name DeckPilot.Factory -ErrorAction SilentlyContinue | Where-Object { $_.Path -eq $executable } | Select-Object -First 1
        $null -ne $script:updated
    }
    if (!$updated.WaitForInputIdle(30000)) { throw 'Updated Atelier did not restart.' }
    Write-Host 'PASS Atelier upgrade: graceful shutdown, replacement and relaunch'

    $phase = 'uninstall while Atelier is open'
    Run-Setup $uninstaller @('/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART')
    Wait-Until { !(Test-Path $executable) }
    if ((Test-Path $desktop) -or (Test-Path $startMenu)) { throw 'Atelier uninstall left a shortcut behind.' }
    Write-Host 'PASS Atelier uninstall: application and shortcuts removed'
}
finally {
    foreach ($process in @($app,$updated)) { if ($process -and !$process.HasExited) { Stop-Process -Id $process.Id -Force; $process.WaitForExit() } }
    $startupLog = Join-Path $env:LOCALAPPDATA 'DeckPilotFactory/startup-errors.log'
    if (Test-Path $startupLog) { New-Item -ItemType Directory -Force 'artifacts/factory-smoke' | Out-Null; Copy-Item $startupLog 'artifacts/factory-smoke/startup-errors.log' -Force }
    if (Test-Path $uninstaller) { Start-Process $uninstaller -ArgumentList '/VERYSILENT','/SUPPRESSMSGBOXES','/NORESTART' -Wait }
}

param([Parameter(Mandatory = $true)][string]$Executable)
$ErrorActionPreference = 'Stop'
function Read-RegistryValue([string]$Path, [string]$Name) {
    try { return (Get-ItemProperty -LiteralPath $Path -ErrorAction Stop).$Name }
    catch { return $null }
}
$script:phase = 'initialization'
$script:smokePassed = $false
$testStarted = Get-Date
trap {
    $message = ($script:phase + ': ' + $_.Exception.Message + ' | ' + $_.ScriptStackTrace).Replace('%','%25').Replace("`r",'%0D').Replace("`n",'%0A')
    Write-Host "::error title=Windows smoke::$message"
    throw
}
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'Ce test lance des processus et utilise la configuration du compte de test. Exécutez-le dans GitHub Actions.' }
Add-Type -AssemblyName UIAutomationClient
Add-Type -AssemblyName UIAutomationTypes

function Wait-Until([scriptblock]$Condition, [int]$Seconds = 45) {
    $deadline = [DateTime]::UtcNow.AddSeconds($Seconds)
    do {
        if (& $Condition) { return }
        Start-Sleep -Milliseconds 200
    } while ([DateTime]::UtcNow -lt $deadline)
    throw 'Délai dépassé pendant le test Windows.'
}
function Find-Control($Root, [string]$Id) {
    $condition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, $Id)
    return $Root.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $condition)
}
function Invoke-Control($Element) {
    if ($null -eq $Element) { throw 'Contrôle introuvable.' }
    $Element.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern).Invoke()
}
function Find-ButtonLike($Root, [string]$Name) {
    $type = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Button)
    return @($Root.FindAll([System.Windows.Automation.TreeScope]::Descendants, $type) | Where-Object { $_.Current.Name -like $Name }) | Select-Object -First 1
}
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class DeckPilotDragMouse {
    [DllImport("user32.dll")] public static extern uint GetDpiForWindow(IntPtr window);
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
    [DllImport("user32.dll")] public static extern void mouse_event(uint flags, uint dx, uint dy, uint data, UIntPtr extra);
}
'@
function Drag-Control($Source, $Destination) {
    if (!$Source -or !$Destination) { throw 'Drag source or target is missing' }
    $left = (Find-Control $root 'DeckEditorScroll').GetCurrentPattern([System.Windows.Automation.ScrollPattern]::Pattern)
    if ($left.Current.VerticallyScrollable) { $left.SetScrollPercent([System.Windows.Automation.ScrollPattern]::NoScroll, 0) }
    $sidebar = Find-Control $root 'SidebarScroll'
    if ($Source.Current.BoundingRectangle.Left -ge $sidebar.Current.BoundingRectangle.Left -or $Source.Current.IsOffscreen) { Show-InScroll $root $Source 'SidebarScroll' }
    $a = $Source.Current.BoundingRectangle; $b = $Destination.Current.BoundingRectangle
    $x = [int]($a.Left + $a.Width / 2); $y = [int]($a.Top + $a.Height / 2)
    $toX = [int]($b.Left + $b.Width / 2); $toY = [int]($b.Top + $b.Height / 2)
    [void][DeckPilotDragMouse]::SetCursorPos($x, $y)
    Start-Sleep -Milliseconds 100
    [DeckPilotDragMouse]::mouse_event(2,0,0,0,[UIntPtr]::Zero)
    try {
        for ($step = 1; $step -le 16; $step++) {
            [void][DeckPilotDragMouse]::SetCursorPos([int]($x + ($toX - $x) * $step / 16), [int]($y + ($toY - $y) * $step / 16))
            Start-Sleep -Milliseconds 35
        }
    }
    finally { [DeckPilotDragMouse]::mouse_event(4,0,0,0,[UIntPtr]::Zero) }
}
function Show-InScroll($Root, $Control, [string]$Id) {
    $viewer = Find-Control $Root $Id
    $pattern = $viewer.GetCurrentPattern([System.Windows.Automation.ScrollPattern]::Pattern)
    for ($i = 0; $i -lt 25; $i++) {
        $bounds = $Control.Current.BoundingRectangle; $viewport = $viewer.Current.BoundingRectangle
        $center = $bounds.Top + $bounds.Height / 2
        if (!$Control.Current.IsOffscreen -and $center -gt $viewport.Top + 4 -and $center -lt $viewport.Bottom - 4) { return }
        if (!$pattern.Current.VerticallyScrollable) { return }
        $direction = if ($bounds.Height -gt 0 -and $center -lt $viewport.Top) { [System.Windows.Automation.ScrollAmount]::SmallDecrement } else { [System.Windows.Automation.ScrollAmount]::SmallIncrement }
        $pattern.Scroll([System.Windows.Automation.ScrollAmount]::NoAmount, $direction)
        Start-Sleep -Milliseconds 50
    }
    throw 'Control did not become visible in its scroll viewer'
}
function Select-ComboItem($Root, [string]$Id, [string]$Name, [int]$ProcessId) {
    $combo = Find-Control $Root $Id
    Show-InScroll $Root $combo 'DeckEditorScroll'
    $expand = $combo.GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern)
    $expand.Expand()
    $nameCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::NameProperty, $Name)
    $processCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $ProcessId)
    $typeCondition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::ListItem)
    $comboCondition = [System.Windows.Automation.AndCondition]::new([System.Windows.Automation.Condition[]]@($nameCondition, $processCondition, $typeCondition))
    Wait-Until { $script:gestureItem = [System.Windows.Automation.AutomationElement]::RootElement.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $comboCondition); $null -ne $script:gestureItem }
    $itemBounds = $script:gestureItem.Current.BoundingRectangle
    if ($itemBounds.IsEmpty -or $script:gestureItem.Current.IsOffscreen) { throw 'Gesture option is not visible' }
    [void][DeckPilotDragMouse]::SetCursorPos([int]($itemBounds.Left + $itemBounds.Width / 2), [int]($itemBounds.Top + $itemBounds.Height / 2))
    [DeckPilotDragMouse]::mouse_event(2,0,0,0,[UIntPtr]::Zero)
    [DeckPilotDragMouse]::mouse_event(4,0,0,0,[UIntPtr]::Zero)
    Wait-Until { $expand.Current.ExpandCollapseState -eq [System.Windows.Automation.ExpandCollapseState]::Collapsed }
}

$source = (Resolve-Path $Executable).Path
$stage = Join-Path $env:LOCALAPPDATA ('DeckPilot\Updates\' + [Guid]::NewGuid().ToString('N'))
$installed = Join-Path $env:TEMP ('DeckPilot smoke with spaces ' + [Guid]::NewGuid().ToString('N'))
$settingsPath = Join-Path $env:APPDATA 'DeckPilot\settings.json'
$existingSettings = if (Test-Path $settingsPath) { [IO.File]::ReadAllBytes($settingsPath) } else { $null }
$startupKey = 'HKCU:\Software\Microsoft\Windows\CurrentVersion\Run'
$startupBefore = Read-RegistryValue $startupKey DeckPilot
$preferenceBefore = Read-RegistryValue 'HKCU:\Software\DeckPilot' StartupConfigured
$startupEnabledBefore = Read-RegistryValue 'HKCU:\Software\DeckPilot' StartupEnabled
$parent = $null; $worker = $null; $replacement = $null
try {
    New-Item -ItemType Directory -Force $stage, $installed, (Split-Path $settingsPath) | Out-Null
    $target = Join-Path $installed 'DeckPilot renamed.exe'
    $downloaded = Join-Path $stage 'DeckPilot.exe'
    Copy-Item $source $target
    Copy-Item $source $downloaded
    $profileId = [Guid]::NewGuid().ToString()
    $buttons = @(1..12 | ForEach-Object { @{ Label = ''; Kind = 'Passthrough'; Target = ''; Arguments = ''; Key = 65; Ctrl = $false; Alt = $false; Shift = $false; Win = $false } })
    $buttons[1].Label = 'Terminal test'
    $buttons[1].Kind = 'Application'
    $buttons[1].Target = Join-Path $env:SystemRoot 'System32\cmd.exe'
    $buttons[1].Arguments = '/c exit 0'
    @{ Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; KeyMap = @(124..135); ActiveProfileId = $profileId; Profiles = @(@{ Id = $profileId; Name = 'Profil test de mise à jour'; Buttons = $buttons }) } |
        ConvertTo-Json -Depth 10 | Set-Content -Path $settingsPath -Encoding UTF8
    $script:phase = 'start original app'
    $parent = Start-Process $target -ArgumentList '--background' -PassThru
    if (!$parent.WaitForInputIdle(30000)) { throw 'Le processus original ne répond pas.' }
    if ($parent.HasExited) { throw 'Le processus original a quitté prématurément.' }
    $version = [Diagnostics.FileVersionInfo]::GetVersionInfo($source).FileVersion.Split('.')[0..2] -join '.'
    $hash = (Get-FileHash $source -Algorithm SHA256).Hash.ToLowerInvariant()
    $plan = @{ ParentProcessId = $parent.Id; ParentStartTicks = $parent.StartTime.ToUniversalTime().Ticks; TargetPath = $target; OriginalHash = $hash; DownloadHash = $hash; NewVersion = $version; Background = $false }
    $planPath = Join-Path $stage 'update.json'
    # Write UTF-8 without a BOM, as the application itself does.
    [IO.File]::WriteAllText($planPath, ($plan | ConvertTo-Json), ([Text.UTF8Encoding]::new($false)))
    $script:phase = 'start update helper'
    $worker = Start-Process $downloaded -ArgumentList @('--apply-update', ('"{0}"' -f $planPath)) -PassThru
    $script:phase = 'wait helper readiness'
    Wait-Until { (Test-Path (Join-Path $stage 'ready')) -or $worker.HasExited }
    if ($worker.HasExited) { throw "L'assistant de mise à jour a échoué : $(Get-Content (Join-Path $stage 'error.txt') -ErrorAction SilentlyContinue)" }
    if ((Get-FileHash $target -Algorithm SHA256).Hash.ToLowerInvariant() -ne $hash) { throw 'Le fichier installé a changé avant la fermeture du parent.' }
    Stop-Process -Id $parent.Id
    $parent.WaitForExit()
    $script:phase = 'wait new app startup'
    Wait-Until { (Test-Path (Join-Path $stage 'healthy.json')) -or $worker.HasExited }
    $health = Get-Content (Join-Path $stage 'healthy.json') -Raw | ConvertFrom-Json
    $replacement = Get-Process -Id $health.ProcessId
    Wait-Until { $worker.HasExited }
    $worker.WaitForExit()
    if ($worker.ExitCode -ne 0) { throw (Get-Content (Join-Path $stage 'error.txt') -Raw) }
    if (!(Test-Path (Join-Path $stage 'complete'))) { throw 'Le remplacement n’a pas été confirmé.' }
    if (Test-Path ($target + '.previous-' + (Split-Path $stage -Leaf))) { throw 'Sauvegarde non nettoyée après confirmation.' }
    if ((Get-FileHash $target -Algorithm SHA256).Hash.ToLowerInvariant() -ne $hash) { throw 'Le fichier installé ne correspond pas au téléchargement.' }
    if ($health.Version -ne $version) { throw 'Version redémarrée incorrecte.' }
    $settings = Get-Content $settingsPath -Raw | ConvertFrom-Json
    if ($settings.Profiles[0].Id -ne $profileId -or $settings.KeyMap.Count -ne 12) { throw 'Les réglages n’ont pas été conservés.' }
    Write-Host 'PASS real Windows updater: parent exit, atomic replacement, app startup, health confirmation and settings preservation'

    $script:phase = 'find main window'
    Wait-Until { $replacement.Refresh(); $replacement.MainWindowHandle -ne [IntPtr]::Zero }
    $root = [System.Windows.Automation.AutomationElement]::FromHandle($replacement.MainWindowHandle)
    $buttonType = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Button)
    $deckButtons = @($root.FindAll([System.Windows.Automation.TreeScope]::Descendants, $buttonType) | Where-Object { $_.Current.Name -like 'Ligne *colonne*' })
    if ($deckButtons.Count -ne 12) { throw "Grille inaccessible ou incomplète : $($deckButtons.Count) boutons." }
    $rowCount = @($deckButtons | ForEach-Object { $_.Current.BoundingRectangle.Top } | Sort-Object -Unique).Count
    $columnCount = @($deckButtons | ForEach-Object { $_.Current.BoundingRectangle.Left } | Sort-Object -Unique).Count
    if ($rowCount -ne 3 -or $columnCount -ne 4) { throw 'The rendered keypad must contain three rows of four keys' }
    $positions = @($deckButtons | ForEach-Object { $_.Current.Name })
    if (!($positions -like 'Ligne 1, colonne 4*') -or !($positions -like 'Ligne 3, colonne 4*') -or ($positions -like 'Ligne 4,*')) { throw 'Deck layout must have three rows and four columns' }
    $script:phase = 'check connection dashboard without a physical board'
    $connection = Find-Control $root 'BoardConnectionStatus'
    if (!$connection) { throw 'Connection dashboard is missing' }
    Wait-Until { $connection.Current.Name -like 'Macropad d*connect*' }
    if ((Find-Control $root 'DeckAvailabilityStatus').Current.Name -ne 'Hors ligne') { throw 'The keypad must clearly show its offline state' }
    if ((Find-Control $root 'DeckAvailabilityHint').Current.Name -notlike '*Modifiez et testez*') { throw 'Offline configuration guidance is missing' }
    if (@($deckButtons | Where-Object { !$_.Current.IsEnabled -or $_.Current.BoundingRectangle.Width -lt 56 -or $_.Current.BoundingRectangle.Height -lt 50 }).Count -gt 0) { throw 'Keys must stay editable offline and remain usable when the keypad scales to the desktop' }
    $script:phase = 'test collapsed advanced settings and offline action feedback'
    # Windows PowerShell 5.1 does not assume UTF-8 for scripts without a BOM.
    $sentStatus = "Action envoy$([char]0xE9)e"
    $failedStatus = "$([char]0xC9)chec de l$([char]0x2019)action"
    $readyStatus = "Pr$([char]0xEA)t"
    $secondKey = Find-Control $root 'DeckKey02'
    Invoke-Control $secondKey
    $advanced = Find-Control $root 'AdvancedOptionsExpander'
    $expand = $advanced.GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern)
    if ($expand.Current.ExpandCollapseState -ne [System.Windows.Automation.ExpandCollapseState]::Collapsed) { throw 'Advanced application settings must start collapsed' }
    $testAction = Find-Control $root 'TestActionButton'
    if (!$testAction.Current.IsEnabled) { throw 'Actions should be testable without the physical board' }
    Invoke-Control $testAction
    Wait-Until { $secondKey.Current.ItemStatus -eq $sentStatus }
    if ((Find-Control $root 'ActionExecutionStatus').Current.Name -notlike ($sentStatus + '*')) { throw 'Successful action feedback is missing from the editor' }
    $expand.Expand()
    $targetEditor = Find-Control $root 'TargetEditor'
    $targetEditor.GetCurrentPattern([System.Windows.Automation.ValuePattern]::Pattern).SetValue((Join-Path $installed 'missing-app.exe'))
    $expand.Collapse()
    Invoke-Control $testAction
    Wait-Until { $secondKey.Current.ItemStatus -eq $failedStatus }
    if ((Find-Control $root 'ActionExecutionStatus').Current.Name -notlike '*introuvable*') { throw 'Failed action feedback should explain the problem' }
    Invoke-Control (Find-Control $root 'DeckKey03')
    if ((Find-Control $root 'DeckKey03').Current.ItemStatus -ne $readyStatus) { throw 'Action feedback leaked into another key' }
    $savedSettings = Get-Content $settingsPath -Raw | ConvertFrom-Json
    if ($savedSettings.Profiles[0].Buttons[1].Arguments -ne '/c exit 0') { throw 'Collapsing advanced options lost existing arguments' }
    Invoke-Control (Find-Control $root 'DeckKey01')
    Write-Host 'PASS offline keypad: 3x4 large keys, editable configuration, collapsed advanced options, actual action success and failure feedback'
    $script:phase = 'check firmware startup preference'
    $details = Find-Control $root 'BoardDiagnosticExpander'
    $details.GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern).Expand()
    $firmwareCheck = Find-Control $root 'AutoFirmwareCheck'
    if ($firmwareCheck.GetCurrentPattern([System.Windows.Automation.TogglePattern]::Pattern).Current.ToggleState -ne [System.Windows.Automation.ToggleState]::On) { throw 'Firmware checking should default to enabled' }
    $details.GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern).Collapse()
    $applicationAction = $root.FindFirst([System.Windows.Automation.TreeScope]::Descendants,
        ([System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::NameProperty, 'Ouvrir une application')))
    $script:phase = 'select application action'
    Invoke-Control $applicationAction
    $script:phase = 'open application picker'
    Invoke-Control (Find-Control $root 'ChooseApplication')
    $script:phase = 'find application picker'
    $picker = $null
    Wait-Until {
        $windowId = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'ApplicationPickerWindow')
        $processId = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $replacement.Id)
        $condition = [System.Windows.Automation.AndCondition]::new($windowId, $processId)
        $script:picker = [System.Windows.Automation.AutomationElement]::RootElement.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $condition)
        $null -ne $script:picker
    }
    $list = Find-Control $picker 'InstalledApplications'
    $script:phase = 'list installed applications'
    $items = $null
    Wait-Until {
        $condition = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::ListItem)
        $script:items = $list.FindAll([System.Windows.Automation.TreeScope]::Descendants, $condition)
        $script:items.Count -gt 0
    } 60
    $items[0].GetCurrentPattern([System.Windows.Automation.SelectionItemPattern]::Pattern).Select()
    Invoke-Control (Find-Control $picker 'ChooseInstalledApplication')
    Wait-Until {
        try { $data = Get-Content $settingsPath -Raw | ConvertFrom-Json; return ![string]::IsNullOrWhiteSpace($data.Profiles[0].Buttons[0].Target) } catch { return $false }
    }
    $script:phase = 'open firmware dialog without hardware'
    Invoke-Control (Find-Control $root 'OpenFirmware')
    $firmwareWindow = $null
    Wait-Until {
        $windowId = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::AutomationIdProperty, 'FirmwareWindow')
        $processId = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $replacement.Id)
        $condition = [System.Windows.Automation.AndCondition]::new($windowId, $processId)
        $script:firmwareWindow = [System.Windows.Automation.AutomationElement]::RootElement.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $condition)
        $null -ne $script:firmwareWindow
    }
    $script:phase = 'verify embedded firmware bundle and disabled flash'
    Wait-Until {
        $label = Find-Control $firmwareWindow 'PackageStatus'
        $label -and $label.Current.Name -like 'Firmware inclus : 1.1.1*'
    }
    $installFirmware = Find-Control $firmwareWindow 'InstallFirmware'
    if ($installFirmware.Current.IsEnabled) { throw 'Flash enabled without explicit device selection and confirmation' }
    $firmwareRoot = Join-Path $env:LOCALAPPDATA 'DeckPilot\Firmware'
    $firmwareStages = @(Get-ChildItem $firmwareRoot -Directory | Where-Object { Test-Path (Join-Path $_.FullName 'manifest.json') })
    if ($firmwareStages.Count -ne 1) { throw 'Expected exactly one extracted firmware bundle' }
    $firmwareStage = $firmwareStages[0].FullName
    $manifest = Get-Content (Join-Path $firmwareStage 'manifest.json') -Raw | ConvertFrom-Json
    $imageHash = (Get-FileHash (Join-Path $firmwareStage 'DeckPilot.hex') -Algorithm SHA256).Hash
    if ($manifest.Sha256 -ne $imageHash -or $manifest.Board -ne 'promicro-32u4-16mhz') { throw 'Embedded firmware manifest mismatch' }
    if (!(Test-Path (Join-Path $firmwareStage 'avrdude-source.zip'))) { throw 'AVRDUDE source was not bundled' }
    Wait-Until { (Find-Control $firmwareWindow 'RefreshButton').Current.IsEnabled }
    Invoke-Control (Find-Control $firmwareWindow 'CloseButton')
    Wait-Until { !(Test-Path $firmwareStage) }
    Write-Host 'PASS firmware dialog: embedded tools and image validated, flash disabled without a selected board, temporary files cleaned'
    $script:phase = 'search and drag dedicated Spotify actions'
    $sidebarPattern = (Find-Control $root 'SidebarScroll').GetCurrentPattern([System.Windows.Automation.ScrollPattern]::Pattern)
    if ($sidebarPattern.Current.VerticallyScrollable) { $sidebarPattern.SetScrollPercent([System.Windows.Automation.ScrollPattern]::NoScroll, 0) }
    $search = (Find-Control $root 'ActionSearchBox').GetCurrentPattern([System.Windows.Automation.ValuePattern]::Pattern)
    $search.SetValue('volume')
    Wait-Until { $null -ne (Find-ButtonLike $root 'Multim*Volume +') }
    $search.SetValue('spotify')
    Wait-Until { $null -ne (Find-ButtonLike $root 'Spotify*Piste suivante') }
    Drag-Control (Find-ButtonLike $root 'Spotify*Piste suivante') (Find-Control $root 'DeckKey04')
    Wait-Until {
        $data = Get-Content $settingsPath -Raw | ConvertFrom-Json
        $data.Profiles[0].Buttons[3].Kind -eq 'Spotify' -and $data.Profiles[0].Buttons[3].Key -eq 176
    }
    Drag-Control (Find-Control $root 'DeckKey04') (Find-Control $root 'DeckKey05')
    Wait-Until {
        $data = Get-Content $settingsPath -Raw | ConvertFrom-Json
        $data.Profiles[0].Buttons[4].Kind -eq 'Spotify' -and $data.Profiles[0].Buttons[3].Kind -eq 'Passthrough'
    }
    $data = Get-Content $settingsPath -Raw | ConvertFrom-Json
    if (($data.KeyMap -join ',') -ne ((124..135) -join ',')) { throw 'Dragging actions changed physical calibration' }
    $script:phase = 'undo and redo an actual key swap'
    Invoke-Control (Find-Control $root 'UndoProfileButton')
    Wait-Until { (Get-Content $settingsPath -Raw | ConvertFrom-Json).Profiles[0].Buttons[3].Kind -eq 'Spotify' }
    Invoke-Control (Find-Control $root 'RedoProfileButton')
    Wait-Until { (Get-Content $settingsPath -Raw | ConvertFrom-Json).Profiles[0].Buttons[4].Kind -eq 'Spotify' }
    $script:phase = 'configure short and long actions with a custom threshold'
    $longCheck = Find-Control $root 'LongPressCheck'
    $longCheck.GetCurrentPattern([System.Windows.Automation.TogglePattern]::Pattern).Toggle()
    Invoke-Control (Find-Control $root 'SaveActionButton')
    (Find-Control $root 'LongPressThreshold').GetCurrentPattern([System.Windows.Automation.RangeValuePattern]::Pattern).SetValue(850)
    Invoke-Control (Find-Control $root 'SaveActionButton')
    Select-ComboItem $root 'GesturePicker' 'Appui long' $replacement.Id
    $search.SetValue('microphone')
    Wait-Until { $null -ne (Find-ButtonLike $root 'Microphone Windows') }
    Invoke-Control (Find-ButtonLike $root 'Microphone Windows')
    Wait-Until {
        $configured = (Get-Content $settingsPath -Raw | ConvertFrom-Json).Profiles[0].Buttons[4]
        $configured.Kind -eq 'Spotify' -and $configured.LongPressEnabled -and $configured.LongPressMilliseconds -eq 850 -and $configured.LongPressAction.Kind -eq 'Microphone'
    }
    Select-ComboItem $root 'GesturePicker' 'Appui court' $replacement.Id
    $data = Get-Content $settingsPath -Raw | ConvertFrom-Json
    $script:phase = 'search for an installed application and assign it'
    $knownName = $data.Profiles[0].Buttons[0].ApplicationName
    if ([string]::IsNullOrWhiteSpace($knownName)) { throw 'The earlier application picker did not save its application name' }
    $search.SetValue($knownName)
    Wait-Until { $null -ne (Find-ButtonLike $root ([WildcardPattern]::Escape($knownName))) }
    Invoke-Control (Find-ButtonLike $root ([WildcardPattern]::Escape($knownName)))
    Wait-Until { (Get-Content $settingsPath -Raw | ConvertFrom-Json).Profiles[0].Buttons[4].Kind -eq 'Application' }
    $search.SetValue('')
    if (!(Find-Control $root 'HeaderMicrophoneStatus')) { throw 'Global microphone status is missing from the header' }
    $backups = @(Get-ChildItem (Join-Path $env:APPDATA 'DeckPilot\Backups') -Filter '*.deckpilot.json')
    if ($backups.Count -eq 0 -or $backups.Count -gt 20) { throw 'Automatic profile backup retention is not active' }
    Write-Host 'PASS action tools: volume search, Spotify-specific preset drag, swapping keys without recalibration, installed app search and assignment'
    $script:phase = 'resize with all sidebar sections expanded'
    foreach ($id in @('BoardDiagnosticExpander', 'ProfilesExpander', 'ApplicationPreferences')) {
        (Find-Control $root $id).GetCurrentPattern([System.Windows.Automation.ExpandCollapsePattern]::Pattern).Expand()
    }
    $transform = $root.GetCurrentPattern([System.Windows.Automation.TransformPattern]::Pattern)
    foreach ($size in @(@(960,720), @(1280,1000))) {
        $transform.Resize($size[0], $size[1])
        $deckScroll = (Find-Control $root 'DeckEditorScroll').GetCurrentPattern([System.Windows.Automation.ScrollPattern]::Pattern)
        if ($deckScroll.Current.VerticallyScrollable) { $deckScroll.SetScrollPercent([System.Windows.Automation.ScrollPattern]::NoScroll, 0) }
        $sidebar = (Find-Control $root 'SidebarScroll').GetCurrentPattern([System.Windows.Automation.ScrollPattern]::Pattern)
        Wait-Until { $sidebar.Current.VerticallyScrollable }
        $sidebar.SetScrollPercent([System.Windows.Automation.ScrollPattern]::NoScroll, 100)
        Wait-Until { !(Find-Control $root 'CalibrateButton').Current.IsOffscreen }
        $bounds = (Find-Control $root 'CalibrateButton').Current.BoundingRectangle
        if ($bounds.Bottom -gt $root.Current.BoundingRectangle.Bottom -or $bounds.Left -lt $root.Current.BoundingRectangle.Left) { throw 'Calibration button overflows after expanding sidebar sections' }
        $profileBounds = (Find-Control $root 'ProfilePicker').Current.BoundingRectangle
        $keyBounds = (Find-Control $root 'DeckKey01').Current.BoundingRectangle
        $dpi = [DeckPilotDragMouse]::GetDpiForWindow($replacement.MainWindowHandle)
        if ([Math]::Abs($keyBounds.Width * 96 / $dpi - 104) -gt 1) { throw 'Key text is being fractionally resized with the window rather than rendered at native size' }
        if ($keyBounds.Top -lt $profileBounds.Bottom + 20) { throw 'Profile selector overlaps the keypad after resizing' }
        $sidebar.SetScrollPercent([System.Windows.Automation.ScrollPattern]::NoScroll, 0)
    }
    Write-Host 'PASS responsive layout: expanded sections remain scrollable, calibration stays reachable, profile and keypad remain separated'
    $script:smokePassed = $true
    Write-Host 'PASS Windows UI and installed apps: twelve keys, vector templates, application catalog and persisted selection'
}
finally {
    if (!$script:smokePassed) {
        $errorFile = Join-Path $stage 'error.txt'
        if (Test-Path $errorFile) {
            $detail = (Get-Content $errorFile -Raw).Replace('%','%25').Replace("`r",'%0D').Replace("`n",'%0A')
            Write-Host "::error title=Update helper::$detail"
        }
        Get-WinEvent -FilterHashtable @{ LogName = 'Application'; Id = @(1000,1026); StartTime = $testStarted } -ErrorAction SilentlyContinue |
            Where-Object { $_.Message -match 'DeckPilot' } | Select-Object -First 3 | ForEach-Object {
                $detail = $_.Message.Replace('%','%25').Replace("`r",'%0D').Replace("`n",'%0A')
                Write-Host "::error title=Windows application::$detail"
            }
    }
    if ($replacement -and !$replacement.HasExited) { Stop-Process -Id $replacement.Id -Force; $replacement.WaitForExit() }
    if ($worker -and !$worker.HasExited) { Stop-Process -Id $worker.Id -Force; $worker.WaitForExit() }
    if ($parent -and !$parent.HasExited) { Stop-Process -Id $parent.Id -Force; $parent.WaitForExit() }
    if ($null -ne $existingSettings) { [IO.File]::WriteAllBytes($settingsPath, $existingSettings) }
    elseif (Test-Path $settingsPath) { Remove-Item $settingsPath }
    if ($null -ne $startupBefore) { Set-ItemProperty $startupKey DeckPilot $startupBefore }
    else { try { Remove-ItemProperty $startupKey DeckPilot -ErrorAction Stop } catch { } }
    if ($null -ne $preferenceBefore) { Set-ItemProperty 'HKCU:\Software\DeckPilot' StartupConfigured $preferenceBefore }
    else { try { Remove-ItemProperty 'HKCU:\Software\DeckPilot' StartupConfigured -ErrorAction Stop } catch { } }
    if ($null -ne $startupEnabledBefore) { Set-ItemProperty 'HKCU:\Software\DeckPilot' StartupEnabled $startupEnabledBefore }
    else { try { Remove-ItemProperty 'HKCU:\Software\DeckPilot' StartupEnabled -ErrorAction Stop } catch { } }
    Remove-Item $stage, $installed -Recurse -Force -ErrorAction SilentlyContinue
}

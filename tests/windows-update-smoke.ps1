param([Parameter(Mandatory = $true)][string]$Executable)
$ErrorActionPreference = 'Stop'
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

$source = (Resolve-Path $Executable).Path
$stage = Join-Path $env:LOCALAPPDATA ('DeckPilot\Updates\' + [Guid]::NewGuid().ToString('N'))
$installed = Join-Path $env:TEMP ('DeckPilot smoke with spaces ' + [Guid]::NewGuid().ToString('N'))
$settingsPath = Join-Path $env:APPDATA 'DeckPilot\settings.json'
$existingSettings = if (Test-Path $settingsPath) { [IO.File]::ReadAllBytes($settingsPath) } else { $null }
$parent = $null; $worker = $null; $replacement = $null
try {
    New-Item -ItemType Directory -Force $stage, $installed, (Split-Path $settingsPath) | Out-Null
    $target = Join-Path $installed 'DeckPilot renamed.exe'
    $downloaded = Join-Path $stage 'DeckPilot.exe'
    Copy-Item $source $target
    Copy-Item $source $downloaded
    $profileId = [Guid]::NewGuid().ToString()
    $buttons = @(1..12 | ForEach-Object { @{ Label = ''; Kind = 'Passthrough'; Target = ''; Arguments = ''; Key = 65; Ctrl = $false; Alt = $false; Shift = $false; Win = $false } })
    @{ Version = 1; AutoCheckUpdates = $false; KeyMap = @(124..135); ActiveProfileId = $profileId; Profiles = @(@{ Id = $profileId; Name = 'Profil test de mise à jour'; Buttons = $buttons }) } |
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
        $label -and $label.Current.Name -like 'Firmware inclus : 1.0.0*'
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
    Invoke-Control (Find-Control $firmwareWindow 'CloseButton')
    Wait-Until { !(Test-Path $firmwareStage) }
    Write-Host 'PASS firmware dialog: embedded tools and image validated, flash disabled without a selected board, temporary files cleaned'
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
    Remove-Item $stage, $installed -Recurse -Force -ErrorAction SilentlyContinue
}

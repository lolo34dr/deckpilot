param([Parameter(Mandatory = $true)][string]$Executable)
$ErrorActionPreference = 'Stop'
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
    $target = Join-Path $installed 'DeckPilot.exe'
    $downloaded = Join-Path $stage 'DeckPilot.exe'
    Copy-Item $source $target
    Copy-Item $source $downloaded
    $profileId = [Guid]::NewGuid().ToString()
    $buttons = @(1..12 | ForEach-Object { @{ Label = ''; Kind = 'Passthrough'; Target = ''; Arguments = ''; Key = 65; Ctrl = $false; Alt = $false; Shift = $false; Win = $false } })
    @{ Version = 1; AutoCheckUpdates = $false; KeyMap = @(124..135); ActiveProfileId = $profileId; Profiles = @(@{ Id = $profileId; Name = 'Profil test de mise à jour'; Buttons = $buttons }) } |
        ConvertTo-Json -Depth 10 | Set-Content -Path $settingsPath -Encoding UTF8
    $parent = Start-Process powershell.exe -ArgumentList '-NoLogo -NoProfile -NonInteractive -Command "Start-Sleep -Seconds 120"' -PassThru -WindowStyle Hidden
    $version = [Diagnostics.FileVersionInfo]::GetVersionInfo($source).FileVersion.Split('.')[0..2] -join '.'
    $hash = (Get-FileHash $source -Algorithm SHA256).Hash.ToLowerInvariant()
    $plan = @{ ParentProcessId = $parent.Id; ParentStartTicks = $parent.StartTime.ToUniversalTime().Ticks; TargetPath = $target; OriginalHash = $hash; DownloadHash = $hash; NewVersion = $version; Background = $false }
    $planPath = Join-Path $stage 'update.json'
    # Write UTF-8 without a BOM, as the application itself does.
    [IO.File]::WriteAllText($planPath, ($plan | ConvertTo-Json), ([Text.UTF8Encoding]::new($false)))
    $worker = Start-Process $downloaded -ArgumentList @('--apply-update', ('"{0}"' -f $planPath)) -PassThru
    Wait-Until { (Test-Path (Join-Path $stage 'ready')) -or $worker.HasExited }
    if ($worker.HasExited) { throw "L'assistant de mise à jour a échoué : $(Get-Content (Join-Path $stage 'error.txt') -ErrorAction SilentlyContinue)" }
    if ((Get-FileHash $target -Algorithm SHA256).Hash.ToLowerInvariant() -ne $hash) { throw 'Le fichier installé a changé avant la fermeture du parent.' }
    Stop-Process -Id $parent.Id
    $parent.WaitForExit()
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

    Wait-Until { $replacement.Refresh(); $replacement.MainWindowHandle -ne [IntPtr]::Zero }
    $root = [System.Windows.Automation.AutomationElement]::FromHandle($replacement.MainWindowHandle)
    $buttonType = [System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::ControlTypeProperty, [System.Windows.Automation.ControlType]::Button)
    $deckButtons = @($root.FindAll([System.Windows.Automation.TreeScope]::Descendants, $buttonType) | Where-Object { $_.Current.Name -like 'Ligne *colonne*' })
    if ($deckButtons.Count -ne 12) { throw "Grille inaccessible ou incomplète : $($deckButtons.Count) boutons." }
    $applicationAction = $root.FindFirst([System.Windows.Automation.TreeScope]::Descendants,
        ([System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::NameProperty, 'Ouvrir une application')))
    Invoke-Control $applicationAction
    Invoke-Control (Find-Control $root 'ChooseApplication')
    $picker = $null
    Wait-Until {
        $script:picker = [System.Windows.Automation.AutomationElement]::RootElement.FindFirst([System.Windows.Automation.TreeScope]::Children,
            ([System.Windows.Automation.PropertyCondition]::new([System.Windows.Automation.AutomationElement]::NameProperty, 'Choisir une application')))
        $null -ne $script:picker
    }
    $list = Find-Control $picker 'InstalledApplications'
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
    Write-Host 'PASS Windows UI and installed apps: twelve keys, vector templates, application catalog and persisted selection'
}
finally {
    if ($replacement -and !$replacement.HasExited) { Stop-Process -Id $replacement.Id -Force; $replacement.WaitForExit() }
    if ($worker -and !$worker.HasExited) { Stop-Process -Id $worker.Id -Force; $worker.WaitForExit() }
    if ($parent -and !$parent.HasExited) { Stop-Process -Id $parent.Id -Force; $parent.WaitForExit() }
    if ($null -ne $existingSettings) { [IO.File]::WriteAllBytes($settingsPath, $existingSettings) }
    elseif (Test-Path $settingsPath) { Remove-Item $settingsPath }
    Remove-Item $stage, $installed -Recurse -Force -ErrorAction SilentlyContinue
}

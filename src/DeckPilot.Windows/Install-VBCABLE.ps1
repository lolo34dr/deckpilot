param([switch]$ValidateOnly)

$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
$downloadUrl = 'https://download.vb-audio.com/Download_CABLE/VBCABLE_Driver_Pack45.zip'
$officialPage = 'https://vb-audio.com/Cable/'
$work = Join-Path $env:TEMP ('DeckPilot-VBCABLE-' + [Guid]::NewGuid().ToString('N'))
$downloadComplete = $false

function Show-Notice([string]$message, [string]$title, [string]$icon) {
    $iconValue = [Enum]::Parse('System.Windows.Forms.MessageBoxIcon', $icon)
    [System.Windows.Forms.MessageBox]::Show($message, $title, [System.Windows.Forms.MessageBoxButtons]::OK, $iconValue) | Out-Null
}

try {
    if (!$ValidateOnly -and (Get-Command Get-PnpDevice -ErrorAction SilentlyContinue)) {
        $endpoints = @(Get-PnpDevice -Class AudioEndpoint -PresentOnly -ErrorAction SilentlyContinue | Where-Object { $_.Status -eq 'OK' } | ForEach-Object { $_.FriendlyName })
        if (($endpoints | Where-Object { $_ -like 'CABLE Input*' }) -and ($endpoints | Where-Object { $_ -like 'CABLE Output*' })) {
            Add-Type -AssemblyName System.Windows.Forms
            Show-Notice 'VB-CABLE est déjà installé et détecté. Aucune modification n’a été faite.' 'DeckPilot' 'Information'
            return
        }
    }

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    New-Item -ItemType Directory -Path $work -Force | Out-Null
    $archive = Join-Path $work 'VBCABLE.zip'
    Invoke-WebRequest -Uri $downloadUrl -OutFile $archive -UseBasicParsing
    $downloadComplete = $true
    Expand-Archive -LiteralPath $archive -DestinationPath (Join-Path $work 'package') -Force

    $setups = @(Get-ChildItem -Path (Join-Path $work 'package') -Filter 'VBCABLE_Setup_x64.exe' -File -Recurse)
    if ($setups.Count -ne 1) { throw 'Le paquet officiel ne contient pas un installateur x64 unique.' }
    $setup = $setups[0]
    $signature = Get-AuthenticodeSignature -FilePath $setup.FullName
    if ($signature.Status -ne [System.Management.Automation.SignatureStatus]::Valid) {
        throw 'La signature Authenticode de l’installateur VB-Audio est invalide ou absente. Rien n’a été exécuté.'
    }
    if ($signature.SignerCertificate.Subject -notmatch 'VB-Audio') {
        throw ('L’éditeur signé ne correspond pas à VB-Audio (' + $signature.SignerCertificate.Subject + '). Rien n’a été exécuté.')
    }

    $catalogs = @(Get-ChildItem -Path (Join-Path $work 'package') -Filter '*.cat' -File -Recurse)
    if ($catalogs.Count -gt 0) {
        $validCatalog = $false
        foreach ($catalog in $catalogs) {
            $catalogSignature = Get-AuthenticodeSignature -FilePath $catalog.FullName
            if ($catalogSignature.Status -eq [System.Management.Automation.SignatureStatus]::Valid) {
                $validCatalog = $true
                break
            }
        }
        if (!$validCatalog) { throw 'Aucun catalogue de pilote n’a une signature Windows valide. Rien n’a été exécuté.' }
    }

    if ($ValidateOnly) {
        Write-Host ("PASS: Authenticode signature is valid for " + $signature.SignerCertificate.Subject)
        return
    }

    Add-Type -AssemblyName System.Windows.Forms
    $process = Start-Process -FilePath $setup.FullName -Wait -PassThru
    if ($process.ExitCode -ne 0) { throw ('L’installateur VB-Audio s’est terminé avec le code ' + $process.ExitCode + '.') }
    Show-Notice 'L’installateur VB-Audio est terminé. Si tu as installé le pilote, actualise les périphériques dans DeckPilot. Redémarre Windows si VB-CABLE n’apparaît pas.' 'DeckPilot' 'Information'
}
catch {
    if ($ValidateOnly -and !$downloadComplete) {
        Write-Warning 'The official VB-Audio download is unavailable from this runner; signature verification is deferred to the user installation.'
        exit 0
    }
    if ($ValidateOnly) { Write-Error $_; exit 1 }
    try {
        Add-Type -AssemblyName System.Windows.Forms -ErrorAction SilentlyContinue
        Show-Notice ($_.Exception.Message + "`r`n`r`nDeckPilot n’a exécuté aucun installateur non vérifié. Tu peux consulter la page officielle VB-Audio pour installer le pilote manuellement.") 'Installation de VB-CABLE' 'Warning'
        Start-Process $officialPage
    }
    catch { }
}
finally {
    Remove-Item -LiteralPath $work -Recurse -Force -ErrorAction SilentlyContinue
}

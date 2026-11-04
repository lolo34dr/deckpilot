param([switch]$ValidateOnly)

$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
$downloadUrl = 'https://download.vb-audio.com/Download_CABLE/VBCABLE_Driver_Pack45.zip'
$work = Join-Path $env:TEMP ('DeckPilot-VBCABLE-' + [Guid]::NewGuid().ToString('N'))
$downloadComplete = $false

function Get-PresentAudioEndpoints {
    if (Get-Command Get-PnpDevice -ErrorAction SilentlyContinue) {
        return @(Get-PnpDevice -Class AudioEndpoint -PresentOnly -ErrorAction SilentlyContinue |
            Where-Object { $_.Status -eq 'OK' } | ForEach-Object { $_.FriendlyName })
    }
    if (Get-Command Get-CimInstance -ErrorAction SilentlyContinue) {
        return @(Get-CimInstance -ClassName Win32_PnPEntity -ErrorAction SilentlyContinue |
            Where-Object { $_.PNPClass -eq 'AudioEndpoint' -and $_.Status -eq 'OK' } | ForEach-Object { $_.Name })
    }
    return @()
}

function Show-Notice([string]$message, [string]$title, [string]$icon) {
    $iconValue = [Enum]::Parse('System.Windows.Forms.MessageBoxIcon', $icon)
    [System.Windows.Forms.MessageBox]::Show($message, $title, [System.Windows.Forms.MessageBoxButtons]::OK, $iconValue) | Out-Null
}

try {
    if (!$ValidateOnly) {
        $endpoints = @(Get-PresentAudioEndpoints)
        if (($endpoints | Where-Object { $_ -like 'CABLE Input*' }) -and ($endpoints | Where-Object { $_ -like 'CABLE Output*' })) {
            return
        }
    }

    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    New-Item -ItemType Directory -Path $work -Force | Out-Null
    $archive = Join-Path $work 'VBCABLE.zip'
    Invoke-WebRequest -Uri $downloadUrl -OutFile $archive -UseBasicParsing
    Expand-Archive -LiteralPath $archive -DestinationPath (Join-Path $work 'package') -Force
    $downloadComplete = $true

    $setups = @(Get-ChildItem -Path (Join-Path $work 'package') -Filter 'VBCABLE_Setup_x64.exe' -File -Recurse)
    if ($setups.Count -ne 1) { throw 'Le paquet officiel ne contient pas un installateur x64 unique.' }
    $setup = $setups[0]
    $signature = Get-AuthenticodeSignature -FilePath $setup.FullName
    if ($signature.Status -ne [System.Management.Automation.SignatureStatus]::Valid) {
        throw 'La signature Authenticode de l''installateur VB-Audio est invalide ou absente. Rien n''a été exécuté.'
    }
    $signerSubject = $signature.SignerCertificate.Subject
    # VB-Audio currently signs this installer with its proprietor's legal publisher name.
    $recognizedPublisher = $signerSubject -match '^CN=VB-Audio(?:[^,]*)' -or
        $signerSubject -match '^CN=BUREL VINCENT Entrepreneur individuel(?:,|$)'
    if (!$recognizedPublisher) {
        throw ('L''éditeur signé VB-Audio n''est pas reconnu (' + $signerSubject + '). Rien n''a été exécuté.')
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
        if (!$validCatalog) { throw 'Aucun catalogue de pilote n''a une signature Windows valide. Rien n''a été exécuté.' }
    }

    if ($ValidateOnly) {
        Write-Host ("PASS: Authenticode signature is valid for " + $signature.SignerCertificate.Subject)
        return
    }

    Add-Type -AssemblyName System.Windows.Forms
    $process = Start-Process -FilePath $setup.FullName -ArgumentList @('-i', '-h') -Wait -PassThru
    if ($process.ExitCode -ne 0) { throw ('L''installateur VB-Audio s''est terminé avec le code ' + $process.ExitCode + '.') }
    if ((Get-Command Get-PnpDevice -ErrorAction SilentlyContinue) -or (Get-Command Get-CimInstance -ErrorAction SilentlyContinue)) {
        $verified = $false
        for ($attempt = 0; $attempt -lt 20; $attempt++) {
            $endpoints = @(Get-PresentAudioEndpoints)
            if (($endpoints | Where-Object { $_ -like 'CABLE Input*' }) -and ($endpoints | Where-Object { $_ -like 'CABLE Output*' })) { $verified = $true; break }
            Start-Sleep -Milliseconds 500
        }
        if (!$verified) { throw 'VB-Audio Setup finished, but Windows has not exposed both VB-CABLE endpoints yet. A restart may be required.' }
    }
}
catch {
    if ($ValidateOnly -and !$downloadComplete) {
        Write-Warning 'The official VB-Audio package could not be downloaded or unpacked on this runner; signature verification is deferred to the user installation.'
        exit 0
    }
    if ($ValidateOnly) {
        $detail = ($_.Exception.Message -replace '[\r\n]+', ' ').Trim()
        Write-Host "::error title=VB-CABLE validation failed::$detail"
        Write-Error $_
        exit 1
    }
    try {
        Add-Type -AssemblyName System.Windows.Forms -ErrorAction SilentlyContinue
        $message = $_.Exception.Message + "`r`n`r`nDeckPilot n'a exécuté aucun installateur non vérifié. Vérifie ta connexion puis relance l'installation depuis les préférences Soundboard."
        if ((Get-Culture).TwoLetterISOLanguageName -eq 'en') { $message = $_.Exception.Message + "`r`n`r`nDeckPilot did not run any unverified installer. Check your connection, then retry from Soundboard preferences." }
        Show-Notice $message 'DeckPilot · VB-CABLE' 'Warning'
    }
    catch { }
    exit 0
}
finally {
    Remove-Item -LiteralPath $work -Recurse -Force -ErrorAction SilentlyContinue
}

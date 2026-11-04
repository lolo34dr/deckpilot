$ErrorActionPreference = 'Stop'
[xml]$project = Get-Content src/DeckPilot.Windows/DeckPilot.Windows.csproj
$version = $project.Project.PropertyGroup.Version
$compiler = Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6/ISCC.exe'
if (!(Test-Path $compiler)) { throw 'Inno Setup 6 is required to build the installer.' }
$compilerOutput = & $compiler "/DAppVersion=$version" installer/DeckPilot.iss 2>&1
$compilerExitCode = $LASTEXITCODE
$compilerOutput | ForEach-Object { Write-Host $_ }
if ($compilerExitCode -ne 0) {
    $detail = ($compilerOutput | ForEach-Object { $_.ToString().Trim() } | Where-Object { $_ }) -join ' | '
    if ($detail) {
        $detail = $detail.Replace('%', '%25').Replace("`r", '%0D').Replace("`n", '%0A')
        Write-Host "::error title=DeckPilot installer compilation::$detail"
    }
    throw "Installer compilation failed with exit code $compilerExitCode."
}

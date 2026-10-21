$ErrorActionPreference = 'Stop'
[xml]$project = Get-Content src/DeckPilot.Windows/DeckPilot.Windows.csproj
$version = $project.Project.PropertyGroup.Version
$compiler = Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6/ISCC.exe'
if (!(Test-Path $compiler)) { throw 'Inno Setup 6 is required to build the installer.' }
& $compiler "/DAppVersion=$version" installer/DeckPilot.iss
if ($LASTEXITCODE -ne 0) { throw 'Installer compilation failed.' }

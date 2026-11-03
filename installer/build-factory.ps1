$ErrorActionPreference = 'Stop'
$project = 'src/DeckPilot.Factory/DeckPilot.Factory.csproj'
$output = 'artifacts/factory-standalone'
[xml]$factoryProject = Get-Content $project
$factoryVersion = $factoryProject.Project.PropertyGroup.Version
if (!$factoryVersion) { throw 'Factory application version is missing.' }
$firmwareManifest = Get-Content 'artifacts/firmware/manifest.json' -Raw | ConvertFrom-Json
if (!$firmwareManifest.Version) { throw 'Firmware version is missing from the bundled manifest.' }
if (Test-Path $output) { Remove-Item -Recurse -Force $output }
dotnet publish $project -c Release -r win-x64 --self-contained true -p:RequireFirmwareBundle=true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=None -p:DebugSymbols=false -o $output
if ($LASTEXITCODE -ne 0) { throw 'Factory application publish failed.' }
@{ factoryVersion = $factoryVersion; firmwareVersion = $firmwareManifest.Version } | ConvertTo-Json | Set-Content "$output/DeckPilot-Atelier-version.json" -Encoding utf8
$iscc = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
if (!(Test-Path $iscc)) { throw 'Inno Setup 6 is required to build the factory installer.' }
& $iscc "/DAppVersion=$factoryVersion" 'installer/DeckPilot-Atelier.iss'
if ($LASTEXITCODE -ne 0) { throw 'Factory installer build failed.' }

$ErrorActionPreference = 'Stop'
$project = 'src/DeckPilot.Factory/DeckPilot.Factory.csproj'
$output = 'artifacts/factory-standalone'
$resolvedProject = (Resolve-Path $project).Path
[xml]$factoryProject = Get-Content $resolvedProject -Raw
$factoryVersion = $factoryProject.Project.PropertyGroup.Version
if (!$factoryVersion) { throw "Factory application version is missing from $resolvedProject." }
$firmwareManifestPath = Join-Path (Get-Location) 'artifacts/firmware/manifest.json'
if (!(Test-Path $firmwareManifestPath)) { throw "Firmware manifest is missing: $firmwareManifestPath" }
$firmwareManifest = Get-Content $firmwareManifestPath -Raw | ConvertFrom-Json
if (!$firmwareManifest.Version) { throw "Firmware version is missing from $firmwareManifestPath." }
Write-Host "Publishing DeckPilot Atelier $factoryVersion with firmware $($firmwareManifest.Version)..."
if (Test-Path $output) { Remove-Item -Recurse -Force $output }
dotnet publish $resolvedProject -c Release -r win-x64 --self-contained true -p:RequireFirmwareBundle=true -p:PublishSingleFile=true -p:IncludeAllContentForSelfExtract=true -p:EnableCompressionInSingleFile=true -p:DebugType=None -p:DebugSymbols=false -o $output
if ($LASTEXITCODE -ne 0) { throw "Factory application publish failed with exit code $LASTEXITCODE." }
@{ factoryVersion = $factoryVersion; firmwareVersion = $firmwareManifest.Version } | ConvertTo-Json | Set-Content "$output/DeckPilot-Atelier-version.json" -Encoding utf8
$compiler = Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6/ISCC.exe'
if (!(Test-Path $compiler)) { throw "Inno Setup 6 compiler is missing: $compiler" }
Write-Host "Compiling Atelier setup with $compiler..."
& $compiler "/DAppVersion=$factoryVersion" 'installer/DeckPilot-Atelier.iss'
if ($LASTEXITCODE -ne 0) { throw "Factory installer compilation failed with exit code $LASTEXITCODE." }

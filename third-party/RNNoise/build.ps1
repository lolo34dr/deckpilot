param([string]$OutputDirectory = "")

$ErrorActionPreference = 'Stop'
$sourceDirectory = $PSScriptRoot
$repositoryRoot = (Resolve-Path (Join-Path $sourceDirectory '../..')).Path
$buildDirectory = Join-Path $repositoryRoot 'artifacts/rnnoise-build'
if ([string]::IsNullOrWhiteSpace($OutputDirectory)) {
    $OutputDirectory = Join-Path $repositoryRoot 'artifacts/native/win-x64'
}

cmake -S $sourceDirectory -B $buildDirectory -A x64
if ($LASTEXITCODE -ne 0) { throw "RNNoise CMake configuration failed ($LASTEXITCODE)." }
cmake --build $buildDirectory --config Release --parallel
if ($LASTEXITCODE -ne 0) { throw "RNNoise native build failed ($LASTEXITCODE)." }

$nativeLibrary = Get-ChildItem -LiteralPath $buildDirectory -Filter 'rnnoise.dll' -File -Recurse |
    Select-Object -First 1
if ($null -eq $nativeLibrary) { throw 'RNNoise build did not produce rnnoise.dll.' }

New-Item -ItemType Directory -Path $OutputDirectory -Force | Out-Null
Copy-Item -LiteralPath $nativeLibrary.FullName -Destination (Join-Path $OutputDirectory 'rnnoise.dll') -Force
Write-Host "Built and packaged RNNoise: $((Join-Path $OutputDirectory 'rnnoise.dll'))"

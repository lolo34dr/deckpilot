param([Parameter(Mandatory=$true)][string]$OutputPath)
$ErrorActionPreference = 'Stop'

# ATmega32U4 Leonardo application area is 28 KiB; Caterina starts at 0x7000.
# Fill the application area with erased flash bytes and leave an RJMP-to-self
# vector at address 0 so the empty application cannot execute stale code.
$applicationSize = 28672
$bytes = New-Object byte[] $applicationSize
for ($i = 0; $i -lt $bytes.Length; $i++) { $bytes[$i] = 0xFF }
$bytes[0] = 0xFF
$bytes[1] = 0xCF

$lines = [System.Collections.Generic.List[string]]::new()
for ($offset = 0; $offset -lt $applicationSize; $offset += 16) {
    $record = [System.Collections.Generic.List[byte]]::new()
    foreach ($value in @([byte]16, [byte](($offset -shr 8) -band 0xFF), [byte]($offset -band 0xFF), [byte]0)) { [void]$record.Add($value) }
    for ($i = 0; $i -lt 16; $i++) { [void]$record.Add($bytes[$offset + $i]) }
    $sum = 0
    foreach ($value in $record) { $sum += [int]$value }
    $checksum = [byte]((-$sum) -band 0xFF)
    [byte[]]$encodedBytes = $record.ToArray() + @($checksum)
    $encoded = ($encodedBytes | ForEach-Object { $_.ToString('X2') }) -join ''
    $lines.Add(':' + $encoded)
}
$lines.Add(':00000001FF')
$parent = Split-Path -Parent $OutputPath
New-Item -ItemType Directory -Force $parent | Out-Null
[IO.File]::WriteAllText($OutputPath, ($lines -join "`n") + "`n", [Text.Encoding]::ASCII)

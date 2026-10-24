# Read one immutable settings snapshot without blocking Windows' atomic rename.
# Parse after disposing the reader; UIA polling must not lock the app's writes.
function Open-SharedSettings([string]$Path) {
    for ($attempt = 0; $attempt -lt 20; $attempt++) {
        try {
            return [IO.File]::Open($Path, [IO.FileMode]::Open, [IO.FileAccess]::Read, ([IO.FileShare]::ReadWrite -bor [IO.FileShare]::Delete))
        }
        catch {
            $cause = $_.Exception.GetBaseException()
            $code = $cause.HResult -band 0xFFFF
            # ReplaceFile briefly acquires its own handles. Retry only a read
            # colliding with that operation, never a command or a corrupt file.
            if ($cause -isnot [IO.IOException] -or $code -notin @(32, 33) -or $attempt -eq 19) { throw }
            Start-Sleep -Milliseconds 25
        }
    }
}
function Read-SharedJson([string]$Path) {
    $stream = Open-SharedSettings $Path
    try {
        $reader = [IO.StreamReader]::new($stream, [Text.Encoding]::UTF8, $true)
        try { $json = $reader.ReadToEnd() }
        finally { $reader.Dispose() }
    } finally { $stream.Dispose() }
    return ($json | ConvertFrom-Json)
}

# Read one immutable settings snapshot without blocking Windows' atomic rename.
# Parse after disposing the reader; UIA polling must not lock the app's writes.
function Open-SharedSettings([string]$Path) {
    return [IO.File]::Open($Path, [IO.FileMode]::Open, [IO.FileAccess]::Read, ([IO.FileShare]::ReadWrite -bor [IO.FileShare]::Delete))
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

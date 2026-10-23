$ErrorActionPreference = 'Stop'
if ($env:GITHUB_ACTIONS -ne 'true') { throw 'Use an isolated Windows runner.' }
Add-Type -AssemblyName UIAutomationClient, UIAutomationTypes, System.Drawing
Add-Type -TypeDefinition @'
using System;
using System.Runtime.InteropServices;
public static class PreviewDesktop {
    [StructLayout(LayoutKind.Explicit, Size=220, CharSet=CharSet.Unicode)]
    public struct Mode {
        [FieldOffset(68)] public short Size;
        [FieldOffset(72)] public int Fields;
        [FieldOffset(172)] public int Width;
        [FieldOffset(176)] public int Height;
    }
    [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern bool EnumDisplaySettings(string device, int mode, ref Mode settings);
    [DllImport("user32.dll", CharSet=CharSet.Unicode)] public static extern int ChangeDisplaySettings(ref Mode settings, int flags);
    [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr window);
    [DllImport("user32.dll")] public static extern bool SetWindowPos(IntPtr window, IntPtr after, int x, int y, int width, int height, uint flags);
    [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
    [DllImport("user32.dll")] public static extern bool SetCursorPos(int x, int y);
}
'@
[void][PreviewDesktop]::SetProcessDPIAware()
$mode = New-Object PreviewDesktop+Mode
$mode.Size = 220
if (![PreviewDesktop]::EnumDisplaySettings($null, -1, [ref]$mode)) { throw 'Cannot read display mode.' }
$mode.Width = 1920; $mode.Height = 1080; $mode.Fields = $mode.Fields -bor 0x180000
$displayResult = [PreviewDesktop]::ChangeDisplaySettings([ref]$mode, 0)
if ($displayResult -ne 0) { throw "Cannot resize capture desktop: $displayResult" }

$exe = Join-Path $env:TEMP 'DeckPilot-preview.exe'
Invoke-WebRequest -UseBasicParsing 'https://github.com/lolo34dr/deckpilot/releases/download/v1.6.1/DeckPilot.exe' -OutFile $exe
$checksums = (Invoke-WebRequest -UseBasicParsing 'https://github.com/lolo34dr/deckpilot/releases/download/v1.6.1/SHA256SUMS.txt').Content
$expected = ($checksums -split "`n" | Where-Object { $_.Trim().EndsWith('  DeckPilot.exe') }).Trim().Split(' ')[0]
if ((Get-FileHash $exe -Algorithm SHA256).Hash.ToLowerInvariant() -ne $expected) { throw 'Released executable checksum mismatch.' }
$id = [Guid]::NewGuid().ToString()
$buttons = @(1..12 | ForEach-Object { @{ Kind = 'Passthrough'; Label = '' } })
$buttons[0] = @{ Kind = 'Spotify'; Label = 'Spotify'; Key = 179; LongPressEnabled = $true; LongPressMilliseconds = 600; LongPressAction = @{ Kind = 'Microphone'; Label = 'Micro' } }
$buttons[1] = @{ Kind = 'Spotify'; Label = 'Suivante'; Key = 176 }
$buttons[2] = @{ Kind = 'Media'; Label = 'Volume +'; Key = 175 }
$buttons[3] = @{ Kind = 'Microphone'; Label = 'Micro' }
$buttons[4] = @{ Kind = 'Folder'; Label = 'Documents'; Target = [Environment]::GetFolderPath('MyDocuments') }
$buttons[5] = @{ Kind = 'Website'; Label = 'GitHub'; Target = 'https://github.com/lolo34dr/deckpilot' }
$config = Join-Path $env:APPDATA 'DeckPilot\settings.json'
New-Item -ItemType Directory -Force (Split-Path $config) | Out-Null
@{ Version = 1; LayoutColumns = 4; AutoCheckUpdates = $false; AutoCheckFirmware = $false; KeyMap = @(124..135); ActiveProfileId = $id; Profiles = @(@{ Id = $id; Name = "Profil d'exemple"; Buttons = $buttons }) } | ConvertTo-Json -Depth 10 | Set-Content $config -Encoding UTF8
$app = Start-Process $exe -PassThru
try {
    if (!$app.WaitForInputIdle(30000)) { throw 'App did not become ready.' }
    $deadline = [DateTime]::UtcNow.AddSeconds(45)
    do {
        $app.Refresh()
        if ($app.HasExited) { throw 'App exited before capture.' }
        Start-Sleep -Milliseconds 200
    } while ($app.MainWindowHandle -eq [IntPtr]::Zero -and [DateTime]::UtcNow -lt $deadline)
    if ($app.MainWindowHandle -eq [IntPtr]::Zero) { throw 'No application window.' }
    [void][PreviewDesktop]::SetWindowPos($app.MainWindowHandle, [IntPtr]::Zero, 30, 30, 1080, 980, 0x0040)
    [void][PreviewDesktop]::SetForegroundWindow($app.MainWindowHandle)
    [void][PreviewDesktop]::SetCursorPos(1800, 1000)
    Start-Sleep -Seconds 4
    $root = [System.Windows.Automation.AutomationElement]::FromHandle($app.MainWindowHandle)
    $bounds = $root.Current.BoundingRectangle
    if ($bounds.Width -lt 1000 -or $bounds.Height -lt 900) { throw "Unexpected capture bounds: $bounds" }
    $bitmap = New-Object System.Drawing.Bitmap ([int]$bounds.Width), ([int]$bounds.Height)
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    try {
        $graphics.CopyFromScreen([int]$bounds.Left, [int]$bounds.Top, 0, 0, $bitmap.Size)
        $output = Join-Path $PWD 'docs/previews/deckpilot-1.6.1.png'
        New-Item -ItemType Directory -Force (Split-Path $output) | Out-Null
        $bitmap.Save($output, [System.Drawing.Imaging.ImageFormat]::Png)
    } finally { $graphics.Dispose(); $bitmap.Dispose() }
} finally { if (!$app.HasExited) { Stop-Process -Id $app.Id -Force } }

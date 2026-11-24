# Development

[Back to the project overview](../README.md) · [User guide](USER_GUIDE.md)

## Development

Install the **.NET 8 SDK**, version 8.0.415 or a newer compatible version allowed by `global.json`. Visual Studio 2022 with the .NET desktop development workload is optional.

Run these commands from the repository root:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

The core library and console checks also run on Linux. The WPF interface and Windows input APIs require Windows.

### Validation

The console checks exit with a nonzero status on the first failed assertion. They cover:

- Key mapping, held-key repeats, releases, profile switching, and action copying.
- Settings validation, persistence, and migration from older configurations.
- Application search and target handling.
- Release selection, checksums, truncated or corrupted downloads, redirects, and cancellation.
- Executable replacement, backup preservation, and rollback.

The file-lock check runs only on Windows.

GitHub Actions also runs `tests/windows-update-smoke.ps1` against the standalone executable. It starts the update helper, exits the original process, verifies replacement and startup confirmation, checks settings preservation, then opens the installed application picker through UI Automation and saves a selection. It also exercises renamed executables and paths containing spaces. This script is reserved for the disposable GitHub Actions test account. CI also runs `tests/windows-installer-smoke.ps1`: real Program Files installation, desktop/Start menu shortcuts, default startup, upgrade with the app open, disabled-startup preservation, and uninstallation without deleting profiles.

### Publish a standalone Windows folder

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribute the complete archive. The Windows build workflow also produces this package.

### Publish a GitHub release

Update the application version and `.github/release-notes.md`, then push a matching `v*` tag, such as `v1.0.0`. The release workflow checks that the tag matches the project version and runs validation before publishing:

| Download | Contents |
| --- | --- |
| `DeckPilot.exe` | Self-contained, single-file Windows x64 application. |
| `DeckPilot-win-x64.zip` | Complete self-contained application folder. |
| `SHA256SUMS.txt` | SHA-256 hashes for both downloads. |

For Windows ARM64, publish separately with `-r win-arm64` and validate on the target machine.

## Project structure

| Path | Purpose |
| --- | --- |
| `src/DeckPilot.Core` | Settings, key state, search, GitHub release validation, verified downloads, and replacement transactions. |
| `src/DeckPilot.Windows` | WPF interface, application catalog, icons, Windows actions, notification-area integration, and update helper. |
| `tests/DeckPilot.Checks` | Executable checks without an external test framework. |
| `tests/windows-update-smoke.ps1` | Native Windows update and application-picker smoke test used by CI. |
| `deckpilotlogo.png` | Application logo. |
| `deckpilotbanner.png` | Project banner. |

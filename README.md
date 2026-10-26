<p align="center">
  <img src="deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot banner" width="760">
  <img src="deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot logo" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  A Windows companion for your 12-key DIY macropad.<br>
  Configure your keys, launch applications, control media, and switch profiles.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Download the latest release</a>
  &nbsp;·&nbsp;
  <a href="#getting-started">Getting started</a>
  &nbsp;·&nbsp;
  <a href="#development">Development</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml">
    <img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg" alt="Windows build status">
  </a>
</p>

---

DeckPilot configures Arduino Leonardo-compatible macropads that already send **F13–F24** as USB keyboard inputs, arranged in **3 rows of 4 keys**. The physical key order can be arbitrary: a guided calibration maps each button to its position.

**No additional driver or firmware changes are required.** The current application interface is in French; this documentation is in English.

## Features

- **Guided calibration:** identify all 12 physical buttons without rewiring your macropad.
- **Installed application picker:** search Start menu shortcuts and the Windows application catalog, including Microsoft Store entries. Manual file selection remains available.
- **Custom actions:** launch applications, open folders or websites, send shortcuts, and control media.
- **Free keys:** keep F13–F24 available for shortcuts configured directly in a game or another application.
- **Local profiles:** create, duplicate, rename, delete, and select profiles. Calibration is shared across profiles.
- **Compact dark interface:** vector icons, application icons where available, and subtle animations that respect Windows animation preferences.
- **Background operation:** keep DeckPilot in the notification area, with startup at Windows sign-in enabled by default (can be disabled).
- **Automatic updates:** approve a new release, then let DeckPilot download, verify, replace, and restart itself.

## Getting started

### Requirements

- Windows 10 or Windows 11, **x64**.
- An Arduino Leonardo-compatible macropad sending 12 distinct keys from **F13 to F24**.
- A USB data connection.

The published downloads include the .NET runtime. **You do not need to install .NET to use DeckPilot.**

### Install and launch

1. Download **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** and run the installer. Windows asks for administrator permission to install into **Program Files\DeckPilot**.
2. The installer adds DeckPilot to Windows **Installed apps**, creates a Start menu shortcut, and selects the desktop shortcut and Windows startup options by default. You can deselect these optional tasks.
3. Connect your macropad and open DeckPilot from its shortcut.
4. Follow calibration from left to right across **four columns**, then the next row, for **three rows**. Every button must send a different F13–F24 key.

When upgrading from the former 4-row layout, DeckPilot preserves profiles/actions and requests one new calibration for the corrected positions. Installing over an existing portable setup also preserves the settings in `%APPDATA%\DeckPilot`; quit the old copy if setup asks you to.

A portable [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) and [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) remain available for compatibility. For portable use, keep the executable in a writable personal folder.

Uninstall from Windows **Installed apps**. Uninstallation removes the program and its shortcuts, while retaining personal profiles so a later reinstall can reuse them.

The single-file executable extracts its bundled dependencies into the user's .NET cache on first launch, which can take a little longer. Published executables are currently unsigned, so Windows may display a SmartScreen warning.

### Configure a key

1. Select a key in the grid.
2. Choose an action in the right sidebar.
3. Configure its settings below the grid and save. Switching keys or profiles also saves the current key's settings.

| Action | Behavior |
| --- | --- |
| Free key | Passes F13–F24 through normally. Assign it directly in your game or application. |
| Application | Launches an installed application or a manually selected program or shortcut. |
| Folder | Opens a folder in Windows Explorer. |
| Website | Opens an HTTP or HTTPS address in your default browser. |
| Keyboard shortcut | Sends a key combination to the active window. |
| Media | Controls playback, tracks, volume, or mute. |

For application actions, open the application picker and search by name. The manual file selector remains available, with optional program arguments under advanced options. Enter arguments according to the target application's syntax.

Holding a button triggers a configured action only once until release. In free-key mode, normal keyboard repeat events remain available.

### Manage profiles

Open the profile management section in the right sidebar to create, duplicate, rename, or delete a profile. Use the selector above the key grid to choose the active profile. At least one profile must remain.

Calibration applies to all profiles and can be repeated from the sidebar.

### Background operation

Closing the window hides DeckPilot while keeping its actions active. Double-click its notification-area icon near the clock to reopen it, or right-click the icon and choose the quit command to stop it completely.

Windows startup is enabled by default for the current user. You can disable it with **Lancer avec Windows**; later app updates preserve that choice. Installation and subsequent launches refresh an enabled entry to the current executable path.

## Installed applications

The picker reads shortcuts from the current user's and system-wide Start menus, then supplements them with `Get-StartApps` results from the Windows application catalog.

Search ignores case and accents. Duplicate names are grouped, with Start menu entries taking priority. Portable applications and programs without registered shortcuts can still be selected manually. If the Windows catalog is unavailable, readable Start menu shortcuts remain usable.

Windows launches the selected shortcuts. Catalog entries use `shell:AppsFolder`. Optional arguments apply to file-based programs and shortcuts; they are hidden for AppsFolder entries. Listing applications does not launch them.

## Automatic updates

Available since **0.2.0**, automatic update checks run at startup after calibration, then every six hours. They can be disabled in the sidebar. A manual check remains available.

When a newer stable GitHub release is found, DeckPilot asks whether to update. **Installed copies** download and SHA-256 verify `DeckPilot-Setup.exe`, then launch the installer. Windows asks for administrator permission to update Program Files. The installer closes DeckPilot gracefully when ready, preserves profiles and startup preferences, installs the new version and relaunches the app. Cancelling the Windows permission prompt leaves the app open. Setup refuses to interrupt an open firmware session.

**Portable copies** retain the existing update mechanism. After confirmation, it:

1. Downloads the executable with progress reporting and cancellation support.
2. Verifies its SHA-256 hash against `SHA256SUMS.txt` from the same GitHub release.
3. Saves your settings and prepares the update helper.
4. Exits, replaces the executable, and restarts.
5. Keeps the previous executable until the new process confirms successful initialization; if startup fails, it restores and relaunches the previous version.

Choosing to update later postpones automatic prompts for 24 hours. Manual checks bypass that postponement.

Downloads use HTTPS, with redirects restricted to the expected GitHub hosts. Temporary files are stored in `%LOCALAPPDATA%\DeckPilot\Updates`; downloads older than seven days are cleaned up. The Windows startup entry continues to use the same executable path after an update.

Portable copies require a writable personal folder. Installed copies use the installer for protected directories; the DeckPilot app itself continues to run without administrator rights.

**Upgrading from 0.1.x?** Quit the old version from the notification area and manually replace the executable with the latest release once. Those older versions do not contain the updater. Later updates can be installed from within DeckPilot.

### Repository rename compatibility

Version **1.0.1** prepares the move from `lolo34dr/streamdeck-diy` to `lolo34dr/deckpilot`. The repository is now `lolo34dr/deckpilot`. Update checks use the new name and retain compatibility with the original name. Release downloads are validated against these two exact repository paths.

Use **1.0.1 or later** with the renamed repository. Versions 0.2.0 and 1.0.0 strictly validate the original asset paths and may need a manual update. Download the current executable from the releases page if an older version cannot update itself.

## Settings and backups

Profiles and calibration are stored outside the application folder:

```text
%APPDATA%\DeckPilot\settings.json
```

Quit DeckPilot before copying this file to back up or restore your configuration. If a settings file cannot be read, DeckPilot preserves a copy with a `.backup-<date>` suffix before using a fresh configuration.

No account is required. Update checks contact GitHub and can be disabled in the interface.

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

## Hardware checks on Windows

With the Leonardo connected:

- Calibrate all 12 positions in an arbitrary F13–F24 order. Confirm duplicate keys are rejected and canceling preserves the previous mapping.
- Assign a free key in a game and check that the game receives it normally.
- Launch a configured program, then hold the button: it should launch once until release. Include paths containing spaces and optional arguments.
- Test a folder, a website, a shortcut in an ordinary application, and media controls.
- Send F13–F24 through a shortcut action and confirm there is no loop of DeckPilot actions.
- Create two profiles with different actions, switch between them, quit, and relaunch. Profiles and calibration should persist.
- Close the window, trigger an action, then reopen and quit through the notification-area icon. Once DeckPilot exits, all keys should pass through normally.
- Enable Windows startup and test after signing in again; disable it afterward if unwanted.

## Current limitations

- Key listening is global to the Windows session. Physical F13–F24 keys from another keyboard follow the same configuration. Software-injected keyboard events are ignored to prevent action loops.
- Some games, anti-cheat systems, or programs using direct device input may still receive intercepted keys. Free-key mode remains available. The sign-in screen, secure desktop, and other sessions are outside the application's control.
- Windows may block shortcuts sent to applications running with administrator privileges. DeckPilot runs without administrator rights and does not bypass this restriction.
- Release Ctrl, Alt, Shift, and Windows before triggering a shortcut action. Combinations such as Alt+Tab remain subject to Windows behavior.
- Disconnect detection, automatic per-game profiles, and a signed installer are future improvements. Profiles are currently selected manually.

## Project structure

| Path | Purpose |
| --- | --- |
| `src/DeckPilot.Core` | Settings, key state, search, GitHub release validation, verified downloads, and replacement transactions. |
| `src/DeckPilot.Windows` | WPF interface, application catalog, icons, Windows actions, notification-area integration, and update helper. |
| `tests/DeckPilot.Checks` | Executable checks without an external test framework. |
| `tests/windows-update-smoke.ps1` | Native Windows update and application-picker smoke test used by CI. |
| `deckpilotlogo.png` | Application logo. |
| `deckpilotbanner.png` | Project banner. |

## License and credit

DeckPilot is released under the **[MIT License](LICENSE)**. You may use, copy, modify, and redistribute it, including commercially, as long as you retain the copyright and license notices required by the license.

If you build on DeckPilot, a small visible credit and a link to this repository would be appreciated. This additional credit is optional; keeping the MIT notices is still required.

Suggested credit:

> Based on [DeckPilot](https://github.com/lolo34dr/deckpilot) by [lolo34dr](https://github.com/lolo34dr).

## Macropad firmware updates

DeckPilot 1.1.0 includes a **Firmware du macropad…** dialog and firmware 1.0.0 for the Pro Micro ATmega32U4 **5 V / 16 MHz**, using the Leonardo/Caterina configuration confirmed by the owner's upload log. The bundled Windows flashing tool does not require Arduino IDE. Device selection and confirmation are mandatory; app updates never silently flash the board. Existing F13–F24 mappings, profiles and calibration remain compatible.

**The owner has confirmed successful in-app flashing, firmware 1.0.0 identification and all 12 buttons on the reference board.** Keep the original sketch and access to RST/GND for recovery. Firmware flashing cannot be rolled back automatically. See [firmware documentation](firmware/README.md) for compatibility, validation, recovery, build steps and third-party licenses. Per-button USB modes and native media controls are not part of this firmware release.

### Building the Windows installer

After producing `artifacts/standalone/DeckPilot.exe` with the embedded firmware bundle, install Inno Setup 6 and run `./installer/build.ps1` from the repository root. The output is `artifacts/installer/DeckPilot-Setup.exe`. CI builds and tests this installer before attaching it to a release. The installer uses a stable application ID so future versions upgrade the same installation.

## Connection dashboard and firmware checks (1.3.0)

The **Votre macropad** card shows connection state, the detected firmware and any available bundled update. **Connexion et diagnostic** exposes board selection when several Leonardos are connected, the live USB link status, physical button counts, firmware-notification preferences and a copyable diagnostic summary. Diagnostics do not copy profile actions or USB serial identifiers. The monitor reconnects after unplugging and gives up its serial port automatically while the firmware manager is open. F13–F24 keyboard actions continue to work with firmware 1.0.0 and without the diagnostic link.

Startup firmware checks are enabled by default. After the app update check and calibration are out of the way, an older identified firmware can prompt to open installation; postponing delays reminders for 24 hours. Unknown boards and multiple unselected candidates are not flashed automatically. Firmware releases are bundled with app updates from GitHub, rather than fetched from an independent firmware feed.

Firmware **1.1.1** adds session identification, correlated heartbeat replies, bounded messages and physical key-state events. These events animate the mapped buttons and update diagnostics; they never invoke actions a second time. Its first physical-board validation is still required, although the earlier firmware 1.0.0 and flash procedure have been confirmed by the owner. See the [protocol and compatibility documentation](firmware/README.md).

## Keypad interface (1.4.0)

The larger 3 × 4 keypad follows the physical enclosure and black keycaps. Each key shows its main icon, label, F13–F24 mapping and a small action-type icon; tooltips retain the full label and position. Press feedback, running actions, successful dispatch and errors have distinct appearances, including symbols rather than color alone. Animations follow the Windows animation preference.

Choose a key to edit it. **Options avancées · fichier et arguments** keeps manual application paths and launch arguments collapsed by default, while essential folder and website destinations remain expanded. Collapsing settings does not clear their values. **Tester l’action** saves and dispatches the selected action on the current PC; a success indicator confirms dispatch, not completion inside another app. Keyboard shortcuts target the active window. Free F13–F24 keys have no test action.

When the macropad is disconnected, **Hors ligne** and the accompanying hint explain that profiles can still be edited and actions tested. Configuration is stored on the PC and remains available throughout disconnects and reconnects. These interface improvements work with the existing firmware and require no additional flash.

The notification-area menu now switches profiles directly. **Ctrl+S** saves the current button configuration. Invalid configuration files are backed up before a fresh configuration is used. App installation, desktop/Start menu icons, startup preferences and the existing automatic updater are retained.

For manual Arduino IDE uploads, **quit DeckPilot from its tray menu** to free the serial port. Closing the main window keeps it running in the background.

## Spotify, action search and drag-and-drop (1.5.0)

**Spotify** is separate from **Multimédia**. Its play/pause, next and previous commands address the Spotify desktop app's Windows media session, even while a browser is playing YouTube. Spotify must be open and have an available playback session; start a track once if necessary. Spotify in a browser is not supported. Missing or refusing Spotify sessions produce an error, with no fallback to global media keys or another player. No Spotify account authorization, Premium subscription or stored token is needed by DeckPilot. Windows 10 version 2004 or later, or Windows 11, is required by this build.

Use **Rechercher une action ou une application** for queries such as `volume`, `skip`, `Spotify` or an installed app's name. The installed-app index loads on the first search and is cached for the current app session; manual file selection remains available. Click a result to configure the selected key or drag it onto another key.

Drag an action from the catalog or search results onto a key to assign it. Drag one configured key onto another to **swap their actions**; the physical calibration stays unchanged. You can also drop one `.exe`, `.lnk`, `.appref-ms`, folder or HTTP(S) URL onto a key. Dropping only configures the key and never executes the action. Complete any missing settings in the editor.

Windows startup now keeps an explicit per-user preference, repairs a stale or missing Run entry, and reports known Windows startup disable flags. Normal launches respect Windows disabling; explicitly enabling **Lancer avec Windows** clears the known DeckPilot disable flag for the current user. Background launches remain hidden even before calibration, and duplicate background launches exit quietly. After updating, open **Préférences de l’application**, check the startup status and reactivate the option if Windows blocked it. A real sign-out/sign-in on the user's PC remains the final check for Windows policies or machine-specific restrictions.

## Sharp keypad rendering, profile recovery and audio controls (1.6.1)

The keypad now renders at native WPF sizes instead of scaling text through a Viewbox. Parent/key shadow effects and text scale animations were removed; smaller windows scroll rather than shrinking the keypad. The app declares per-monitor DPI awareness and uses larger Windows shell icons for desktop applications.

**Annuler / Rétablir** restores profile edits, including drag-and-drop swaps, action settings, imports and profile creation/deletion. History keeps 50 edits during the app session. Ctrl+Z / Ctrl+Y operate on profiles outside text fields; text fields retain their own undo/redo. Calibration, startup preferences and executed actions are not undone.

Under **Gérer les profils**, export the current profile or all profiles as `.deckpilot.json`, import profiles as new copies, or restore a backup of all profiles. Import never executes actions and never replaces the PC's calibration. Profiles contain action paths, arguments and audio endpoint IDs, which may need adjusting on another PC. Unknown formats, oversized files and malformed gesture configurations are refused.

Before saving changed profiles, DeckPilot keeps the previous profiles in `%APPDATA%\DeckPilot\Backups` (20 versions, content-deduplicated). Preference-only saves do not create profile backups. A backup failure prevents overwriting the current settings. Restore replaces profiles after confirmation and can itself be undone; calibration remains unchanged.

**Microphone Windows** toggles mute on all currently active Windows capture endpoints. The persistent status bar and Audio Windows card show active/muted/mixed/unavailable status, refreshed every two seconds and after app commands. A failed multi-endpoint update attempts to restore previous mute states. These indicators describe Windows endpoint state, not a hardware switch or an application's own mute button.

**Changer de sortie audio** alternates between two selected, connected output devices, for Windows console, multimedia and communications defaults. A missing target gives an error without choosing another device. Applications with their own fixed output may not follow Windows' default. Imported endpoint IDs are shown as unavailable until adapted to the current PC. Failed routing changes attempt to restore the previous defaults.

To configure **appui court / appui long**, first choose a configured action, enable the two-action option, select the gesture to edit and set a threshold from 200 to 3000 ms. Without this mode, actions retain immediate key-down behavior. With it, short actions run on release before the threshold; long actions run once at the threshold (or on release if the timer has not yet run). A long action never also runs the short one. Free F13–F24 passthrough keys cannot use dual-action mode. Pending gestures are cancelled on profile/configuration changes, calibration, firmware operations, session changes and suspend. No firmware flash is needed; the existing keyboard's key-down/key-up reports supply the gestures.

Core checks cover repeat/release boundaries, simultaneous holds, cancellation, profile history/exchange/backups and audio rollback using fake endpoints. Windows UI checks exercise actual drag undo/redo, configuring a long action and threshold, native keypad dimensions after resizing and the microphone indicator. Actual microphone drivers, headset/speaker switching and physical holds still need validation on the owner's PC.

## Search and CI reliability fixes (1.6.4)

Equivalent search results keep their existing WPF buttons. The installed-app catalog and debounce timer defer result replacement while a search button is captured or a drag is in progress. This prevents a source button from being removed during an interaction.

Windows smoke checks continue to perform native mouse drags and validate saved assignments, swaps, undo/redo and both press gestures. The test client uses physical cursor coordinates to match UI Automation bounds regardless of DPI, focuses before measuring, and scans all scroll percentages. A regression check also verifies that equivalent searches preserve the same accessible button. Background-startup assertions wait for WPF's Loaded handler and require five consecutive hidden-window observations while the app remains alive. Failures report the phase, cursor targets, DPI and last scroll geometry.

Build and release workflows use Node.js 24 actions. The Arduino compiler remains at CLI 1.2.2; CI downloads its Windows archive with a pinned SHA-256 checksum. The firmware and its toolchain versions are unchanged.

Settings reads in Windows tests explicitly allow read/write/delete sharing and parse UTF-8 after closing the reader. This prevents the test itself from denying the app’s atomic file replacement. The history test keeps an old snapshot open across three undo/redo cycles and verifies that it remains intact while the live settings change.

On Windows, existing settings files are committed with `File.Replace` rather than overwrite-mode `File.Move`. This supports readers that allow deletion while retaining a complete old snapshot. The core suite tests three replacements with a reader held open; Windows core checks run before firmware compilation and publishing so persistence failures are detected early.

A test snapshot reader may wait up to 500 ms for Windows sharing/lock violations while `ReplaceFile` commits. The wait applies only to opening the snapshot, never to executing or re-executing an application action. Other errors, malformed JSON and persistent locks remain failures.

## Runtime and editing optimizations (1.7.0)

Saving identical settings checks the current file but skips replacement and backup work. External edits/deletion and failed writes are still detected. `ButtonAction` has value equality (including the long action); editor dirty checks avoid JSON serialization. History keeps detached model snapshots, validates new states, and avoids JSON parsing during undo/redo. The 50-edit limit and 20 profile backups remain.

Search and picker share an immutable installed-app catalog for five minutes (15 seconds for a partial result). Concurrent consumers share one scan; closing one picker does not cancel another consumer. Refresh bypasses the cache, and each picker has its own filter/view. Scanning and catalog parsing run off the UI thread; picker filtering is debounced by 120 ms. Action search rechecks this cache after its debounce and when returning from the picker, so a refreshed catalog is available there too.

Board UI updates now follow connection notifications instead of waking every 100 ms. Ordered disconnect notifications still cancel pending holds. Keyboard hooks queue visual work, while suppression and gesture timing remain synchronous. Hidden/minimized windows stop feedback animations and use a ten-second audio polling interval instead of two seconds; reopening or issuing an audio command requests fresh state immediately. Pending audio refreshes share a loop and include requests arriving during an existing read. Unchanged audio snapshots and frozen brushes avoid repeated UI allocations. Process wrappers returned by launching an app are disposed without closing the launched program.

These changes reduce specified work; they do not establish a machine-independent CPU/RAM percentage. Native hardware audio switching and physical key behavior still require validation on a Windows PC with the macropad.

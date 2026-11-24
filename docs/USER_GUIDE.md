# DeckPilot user guide

This guide covers installation, assigning actions, profiles, updates and known limitations. The application interface is currently in French; its labels are kept as they appear in the app.

For the optional soundboard and microphone filter, see the [soundboard guide](SOUNDBOARD.md). A shorter [French user guide](USER_GUIDE.fr.md) is also available.

[Back to the project overview](../README.md) · [Firmware guide](../firmware/README.md) · [DeckPilot Atelier](FACTORY.md)

## Getting started

### Requirements

- Windows 10 or Windows 11, **x64**.
- An Arduino Leonardo-compatible macropad sending 12 distinct keys from **F13 to F24**.
- A USB data connection.

The published downloads include the .NET runtime. **You do not need to install .NET to use DeckPilot.**

### Install and launch

1. Download **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** and run the installer. Windows asks for administrator permission to install into **Program Files\DeckPilot**.
2. The installer lets you choose whether DeckPilot starts with Windows, checks for app updates automatically, and reduces animations. Startup and update checks are on by default; reduced animations are off. It also checks for the signed VB-CABLE driver required by the Soundboard and installs it automatically if missing. An internet connection is required for that driver check.
3. The installer adds DeckPilot to Windows **Installed apps** and creates a Start menu shortcut. A desktop shortcut is selected by default.
4. Connect your macropad and open DeckPilot from its shortcut.
5. Open **Macropads**, click a physical key in the device card, and choose an action from the library. On firmware 1.2.0 or newer, the first choice enables DeckPilot mode for that device.
6. If the on-screen positions do not match your physical keys, choose **Calibrer les touches** on that device. For older F13–F24 firmware, use the **Actions** tab and calibrate the 3 × 4 grid before assigning shortcuts.

When upgrading from the former 4-row layout, DeckPilot preserves profiles/actions and requests one new calibration for the corrected positions. Installing over an existing portable setup also preserves the settings in `%APPDATA%\DeckPilot`; quit the old copy if setup asks you to.

A portable [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) and [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) remain available for compatibility. For portable use, keep the executable in a writable personal folder.

Uninstall from Windows **Installed apps**. Uninstallation removes the program and its shortcuts, while retaining personal profiles so a later reinstall can reuse them.

The single-file executable extracts its bundled dependencies into the user's .NET cache on first launch, which can take a little longer. Published executables are currently unsigned, so Windows may display a SmartScreen warning.

### Configure a key

1. Open **Macropads** and select the key to personalize. With two devices, first choose the correct device card.
2. Click or search for an action in the library. On narrow windows, the library appears below the key preview.
3. If the action needs a destination or shortcut, complete its settings below the preview. Changes are saved when you switch keys or return to the macropad.

| Action | Behavior |
| --- | --- |
| Free key | Passes F13–F24 through in keyboard mode. In DeckPilot mode, an unassigned key does nothing. |
| Application | Launches an installed application or a manually selected program or shortcut. |
| Folder | Opens a folder in Windows Explorer. |
| Website | Opens an HTTP or HTTPS address in your default browser. |
| Keyboard shortcut | Sends a key combination to the active window. |
| Media | Controls playback, tracks, volume, or mute. |
| Spotify | Controls Spotify playback and adjusts only Spotify's Windows audio-session volume. |
| Deezer | Controls the Deezer Windows media session and adjusts only its Windows audio-session volume. |
| Application — DeckPilot | Shows the existing DeckPilot window, including when it is running in the notification area. |

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

Automatic update checks run at startup after calibration, then every six hours. They can be disabled in the sidebar; a manual check remains available.

When a newer stable GitHub release is found, DeckPilot asks whether to update. **Installed copies** download and SHA-256 verify `DeckPilot-Setup.exe`, then launch the installer with the current startup, automatic-update and reduced-motion preferences preselected. You can change them during the update. Windows asks for administrator permission to update Program Files. The installer closes DeckPilot gracefully when ready, preserves profiles and configuration, checks and installs the signed VB-CABLE driver if missing, and relaunches the app. Cancelling the installer leaves the app open. Setup refuses to interrupt an open firmware session.

**Portable copies** retain the existing update mechanism. After confirmation, it:

1. Downloads the executable with progress reporting and cancellation support.
2. Verifies its SHA-256 hash against `SHA256SUMS.txt` from the same GitHub release.
3. Saves your settings and prepares the update helper.
4. Exits, replaces the executable, and restarts.
5. Keeps the previous executable until the new process confirms successful initialization; if startup fails, it restores and relaunches the previous version.

Choosing to update later postpones automatic prompts for 24 hours. Manual checks bypass that postponement.

Downloads use HTTPS, with redirects restricted to the expected GitHub hosts. Temporary files are stored in `%LOCALAPPDATA%\DeckPilot\Updates`; downloads older than seven days are cleaned up. The Windows startup entry continues to use the same executable path after an update.

Portable copies require a writable personal folder. Installed copies use the installer for protected directories; the DeckPilot app itself continues to run without administrator rights.

## Settings and backups

Profiles and calibration are stored outside the application folder:

```text
%APPDATA%\DeckPilot\settings.json
```

Quit DeckPilot before copying this file to back up or restore your configuration. If a settings file cannot be read, DeckPilot preserves a copy with a `.backup-<date>` suffix before using a fresh configuration.

No account is required. Update checks contact GitHub and can be disabled in the interface.

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
- Automatic per-game profiles and a signed installer are future improvements. Profiles are currently selected manually.

## Firmware and hardware tools

For firmware compatibility, recovery, manual builds and licenses, see the [firmware guide](../firmware/README.md). For preparing macropads and workshop checks, use [DeckPilot Atelier](FACTORY.md), also available from the [latest releases](https://github.com/lolo34dr/deckpilot/releases/latest). App and Atelier updates do not flash a board automatically; flashing requires a deliberate action and confirmation.

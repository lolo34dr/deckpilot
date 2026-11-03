DeckPilot 1.9.7 fixes Atelier uninstalling while the app is open. Atelier 1.0.6 also corrects its Windows DPI manifest, which prevented the installed app from launching.

DeckPilot 1.9.6 fixes the Windows DPI manifest that prevented DeckPilot Atelier from opening after installation.

DeckPilot 1.9.5 includes detailed Atelier startup diagnostics in the Windows release check.

DeckPilot 1.9.4 improves the Atelier Windows startup smoke test to report application crash details.

DeckPilot 1.9.3 fixes the Atelier build metadata to read the firmware manifest from the bundled firmware archive.

DeckPilot 1.9.2 fixes the Atelier release packaging step and improves its build diagnostics.

DeckPilot 1.9.1 improves the Atelier installer startup and adds automatic app and firmware update checks.

- The installer launches Atelier in the signed-in Windows user session and safely stops/restarts it for upgrades.
- Atelier checks GitHub on startup and on demand. It announces newer Atelier builds or bundled firmware, then downloads an installer verified against the published SHA-256 file.
- Firmware is only flashed after the user chooses the flash action; an app update never writes to a connected board on its own.

The Atelier installer also launches in the signed-in user's Windows session after a fresh installation. The release workflow now opens the installed app, then verifies upgrade and uninstall while it is running.

DeckPilot 1.9.0 adds DeckPilot Atelier, a separate Windows workshop app for flashing new macropads and checking all 12 keys.

- Install the included firmware on Leonardo / Pro Micro ATmega32U4 boards with the Caterina bootloader.
- Run guided short-press and long-press checks for every physical key, record its F13–F24 mapping, and save a quality-control report.
- Download **DeckPilot-Atelier-Setup.exe** from this release. The original DeckPilot setup and firmware remain available separately.

DeckPilot 1.8.0 adds a refreshed workspace and faster ways to build and use your profiles.

- Clearer charcoal/teal interface, action library first, crisp 3 × 4 keys, key accent colors and reduced-motion preference.
- Three editable profile templates: Music and audio, Desktop and productivity, and Creation.
- Profile actions: switch to a named profile, the next or the previous profile from a key. Imported profile sets keep their internal links.
- Windows actions: open common settings or Task Manager, or explicitly lock the PC.
- Copy/cut/paste/reset entire keys, including long presses and colors, with undo/redo and an app-local clipboard.
- Capture a keyboard shortcut directly; Escape cancels and macropad keys remain reserved during capture.
- Ctrl+K action search and a clearable, session-only history of the last 30 actions.
- Fix the automatic firmware prompt so accepting it opens the firmware manager. Pending gestures are cancelled across profile changes.

Download **DeckPilot-Setup.exe** or update from the app. Existing profiles, calibration and startup preferences are preserved. Firmware remains **1.1.1**; no flash is required for these features.

Validation includes core compatibility/navigation/import/template checks and native Windows installation, update, picker, drag-and-drop, undo/redo, template creation, profile actions, shortcut capture, clipboard editing, layout and minimize/restore checks. Physical macropad and audio-device behavior still depends on the owner’s Windows hardware.

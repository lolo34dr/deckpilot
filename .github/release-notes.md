DeckPilot 2.1.5 smooths audio-device refreshes and adds a branded startup screen.

- Discover physical microphones and VB-CABLE off the UI thread. Automatic retries share a recent device snapshot; the manual refresh button always performs a fresh scan.
- Reduce repeated Windows audio-state polling and streamline the common mono-microphone conversion path.
- Show the DeckPilot banner while the interactive app starts. Silent startup with Windows stays in the background.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes background scan/cache regression checks, audio conversion checks, Windows builds, installer upgrade/uninstall, native splash/update and application-picker checks. Actual audio CPU use and latency still depend on the PC and audio devices.

DeckPilot 2.1.4 starts the configured virtual microphone automatically with the app, including background startup after Windows sign-in.

- Automatic soundboard microphone startup is enabled by default for existing and new settings. It uses the selected physical microphone and VB-CABLE; no manual activation is needed after restarting the PC when DeckPilot starts with Windows.
- If devices are not ready at sign-in, or audio stops after a disconnect, DeckPilot retries every five seconds while automatic activation is enabled.
- An explicit stop disables and saves the automatic-start preference; enable it again from the Soundboard panel or the activation button. Audio clips are never played automatically.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes settings migration/round-trip checks and a native Windows UI check for the persisted automatic-start preference, plus the existing audio and installer checks. Physical device startup/reconnection still depends on the owner's hardware.

DeckPilot 2.1.3 reduces soundboard microphone latency and removes the one-second conversion blocks from the live audio path.

- Use output-driven, small-block resampling for the microphone and sound clips; matching sample rates bypass resampling entirely.
- Request event-driven 20 ms capture and 40 ms playback buffers instead of the previous 100 ms settings. Actual latency depends on Windows, the audio devices and VB-CABLE.
- Bound microphone buffering to 100 ms and retain recent complete frames after a playback stall instead of queueing old speech. Short underruns add only the silence requested by the current read.
- Support mono, stereo, multichannel and Windows extensible PCM/float inputs without the Media Foundation one-second read-ahead.
- Keep Discord processing and routing unchanged. Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes regression checks for resampling block sizes, channel conversion, extensible formats, underrun recovery, overflow freshness, concurrent capture/playback and clip completion, alongside the existing Windows installer and application tests. No hardware latency figure is claimed; verify the improvement with your microphone and audio driver.

DeckPilot 2.1.2 and DeckPilot Atelier 1.0.11 fix workshop safety, soundboard reliability and portable profile sounds.

- Atelier can prepare Leonardo/Caterina boards that do not yet answer the DeckPilot protocol; serial failures still stop flashing and the USB location, bootloader and MCU signature remain verified.
- Every inspection starts with empty results. Reports retain the tested board and firmware, and stale USB callbacks cannot affect a new session.
- Closing Atelier during firmware writing or verification is blocked.
- Profile exports and automatic profile backups can retain referenced MP3/WAV files (32 MiB audio limit). Imports copy embedded clips into a managed folder and remap short/long action references. Legacy profile imports remain supported; missing backup audio is reported explicitly.
- The soundboard releases finished clips, updates playback indicators, and reports interrupted audio streams. Renamed VB-CABLE endpoints are recognized through their adapter identity.
- Correct translated README links and preserve technical device names during translation. Clarify the VB-CABLE requirement.

Validation includes core regression checks, firmware protocol tests, translation tests, Windows build, DeckPilot and Atelier installation/upgrade/uninstall checks, and native interface/update checks. Physical first-flash and audio disconnect behavior still require the owner's hardware. Firmware remains 1.1.1; no firmware update is required.

DeckPilot 2.1.1 improves installation and updates:

- Automatically check for VB-CABLE during first installation and upgrades, and download and launch the official signed installer when the driver is missing.
- Show Windows startup, automatic update checks and reduced-motion preferences during setup and updates, with existing choices preselected and preserved.
- Keep the selected physical microphone and request microphone setup only when needed.
- Fix installer initialization and detection of existing installations.

Download **DeckPilot-Setup.exe** or accept the update in DeckPilot. Windows administrator approval is required. Existing profiles and calibration are preserved. DeckPilot Atelier remains at 1.0.10 and firmware remains at 1.1.1; both are included in the shared release.

Validation: core checks and Windows build, installer upgrade/uninstall and native application tests run before release publication. Audio hardware behavior still requires testing on the user's PC.

DeckPilot 2.1.0 keeps the deep black interface and customizable colors, and routes the soundboard through VB-Audio VB-CABLE automatically. Users select only their physical microphone; the virtual playback endpoint is fixed so voice and MP3/WAV clips cannot accidentally be sent to speakers. The DeckPilot installer can download the official VB-CABLE package, verify its Authenticode publisher, and launch the vendor's driver setup; the package is not redistributed. In Discord or a game, select CABLE Output as the microphone. Atelier remains at version 1.0.10.

DeckPilot 1.9.11 and DeckPilot Atelier 1.0.10 add pre-flash hardware and firmware diagnostics, a firmware version comparison, a guided recovery action after interrupted writes, sequential multi-board flashing with a batch report, and a physical-position-to-F13–F24 layout check in every key-control report.

DeckPilot 1.9.11 includes the same Atelier update in the shared installer release.

DeckPilot 1.9.10 restores shared releases containing both DeckPilot and DeckPilot Atelier 1.0.9. Both applications once again check the same latest GitHub release.

If you installed Atelier 1.0.8 from the separate release channel, install DeckPilot-Atelier-Setup.exe from this release once to return to shared updates. Earlier Atelier versions and DeckPilot can use their existing updater.

DeckPilot Atelier 1.0.7 adds a confirmed program-area erase that clears all 28 KiB of user flash, leaves a safe reset loop, and preserves Caterina and EEPROM. Reset remains disabled until a physical board is selected.

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

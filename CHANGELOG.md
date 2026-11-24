# Changelog

Version history, with one section per release. Each section contains only the notes for that version. Older release notes are kept here instead of being carried into the project overview or a newer release.

The English source for an upcoming release is maintained in [`.github/release-notes.md`](.github/release-notes.md). Published release pages and downloads are available from [GitHub Releases](https://github.com/lolo34dr/deckpilot/releases).

## [2.4.9](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.9)

DeckPilot 2.4.9 makes macropad setup easier to understand and use.

- One connected macropad opens in a wider workspace with its device name, profile, connection state and twelve keys clearly separated. Two connected macropads appear side by side when the window has room.
- A short guide explains how to choose a key and assign an action. The editor identifies the current device and key, with a direct route back to the macropad. The action library remains usable in narrow windows.
- Firmware, mode and profile controls have clearer labels. Calibration is accessible in the device settings and directly on cards running in DeckPilot mode. The first key selection explains when that mode activates and what happens to unassigned F13–F24 keys.
- Key accent colors now appear in the device overview. Opening a key that is already configured no longer rewrites the settings file unnecessarily. Firmware remains at 1.2.0.

## [2.4.8](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.8)

DeckPilot 2.4.8 adds a dedicated workspace for configuring one or more macropads.

- See each detected macropad and its twelve keys on the same page. Click a key to assign its action, then switch devices directly from the editor.
- Give each macropad its own profile and calibration. Existing shared profiles can be copied without losing actions. Editing a device keeps the active F13–F24 keyboard profile in place.
- Check the status of each device and open its firmware or calibration controls from Macropads.
- Includes firmware 1.2.0 for compatible Leonardo and Pro Micro boards. DeckPilot mode separates USB input from each device. Standalone keyboard mode continues to send F13–F24.

## [2.4.7](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.7)

DeckPilot 2.4.7 reorganizes the configuration workspace and fixes visual issues found during the Windows beta.

- Use separate tabs for actions, the macropad, audio and settings. The header shows connection status and the existing 3 × 4 keypad retains its appearance and behavior.
- Configure short and long presses directly in the editor. Both choices and the hold duration stay visible without opening a menu, and the selected press can be changed with the arrow keys.
- Read checkboxes and tooltips on the dark theme, and see audio values beside their labels.
- Resize the stacked panels in compact windows. The calibration grid fits the available height while its instructions and controls remain visible.

## [2.4.6](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.6)

DeckPilot 2.4.6 keeps new soundboard clips in DeckPilot's own storage and improves profile, media and macropad reliability.

- Adding or replacing an MP3 or WAV now copies the file to `%APPDATA%\DeckPilot\Sounds`. Playback still works if the original is moved or deleted. To copy an existing clip, select its file again through the clip's **…** menu.
- Failed profile imports leave the active configuration intact. Repeated imports reuse matching audio, and automatic backups stay within the package limit by omitting audio that does not fit.
- Rapid Spotify and Deezer volume changes are applied in order. The Music template now includes Deezer play/pause.
- DeckPilot Atelier 1.0.13 keeps the interface responsive during hardware operations, matches recovery to the original USB port and checks that Windows receives each key event.
- Optional firmware 1.1.2 reports all 12 F13–F24 keys independently. Installing the Windows update does not flash a connected macropad.

## [2.4.4](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.4)

DeckPilot 2.4.4 finishes the Deezer integration and places its SVG mark beside Spotify in the action library.

- Show a dedicated, multicolour Deezer SVG icon in the action library and on assigned macropad keys.
- Route play/pause, next/previous through Deezer's Windows media session and volume through its Windows audio session, following the same local APIs as Spotify.
- Match Deezer app identities consistently and keep browser playback, other players and master volume untouched.
- No firmware update or flash is required.

## [2.4.3](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.3)

DeckPilot 2.4.3 adds dedicated Deezer controls to the macropad action library.

- Assign play/pause, next/previous track and per-application volume controls to Deezer.
- Deezer volume changes only its Windows audio session in 5% steps; it never changes the master volume or another app.
- Playback controls require the Deezer Windows app to expose a Windows media session. Browser playback is not supported.
- No firmware update or flash is required.

## [2.4.2](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.2)

DeckPilot 2.4.2 improves the control panel and keeps key editing actions in view.

- Keep **Tester** and **Enregistrer** beside the selected key so they remain visible without scrolling through the editor.
- Retain the existing DeckPilot keypad workspace and its original dark palette while simplifying the right-side action library.
- Reduce the startup window to the app logo and version; the progress indicator follows the reduced-motion setting.
- Remember the application window size and position, and recover safely if a saved monitor is no longer connected.
- Keep OBS/Streamlabs controls retired. Existing profiles remain readable and retired actions cannot execute.

No firmware update or flash is required. DeckPilot Atelier remains at 1.0.12 and firmware remains at 1.1.1.

## [2.4.1](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.1)

DeckPilot 2.4.1 corrects the control panel tabs so their full outlines remain visible and their widths stay aligned when the window is resized.

No firmware update or flash is required.

## [2.4.0](https://github.com/lolo34dr/deckpilot/releases/tag/v2.4.0)

DeckPilot 2.4.0 reorganizes the control panel and adds exact colour selection.

- Use the **Actions**, **Appareil** and **Réglages** tabs to find key actions, device/audio controls and preferences.
- Keep the existing left-hand keypad/editor layout, DeckPilot logo and monochrome vector pictograms.
- Choose exact background, panel and accent colours with **Palette…**, preview their values and swatches, then apply them. The Windows picker supports custom colours; no preset theme is required.
- Remove the OBS/Streamlabs control integration. Existing 2.3 profiles remain readable, and their retired actions are preserved but cannot execute. Reassign those keys from the action library.
- Preserve other profiles, actions and physical calibration. Firmware remains 1.1.1 and Atelier remains 1.0.12; no firmware flash is needed.

## [2.3.0](https://github.com/lolo34dr/deckpilot/releases/tag/v2.3.0)

DeckPilot 2.3.0 adds direct OBS Studio and Streamlabs Desktop controls from macropad buttons.

- Connect to OBS through its WebSocket server and Streamlabs Desktop through Remote Control. DeckPilot sends application API commands directly instead of simulating keyboard shortcuts.
- Configure actions for scenes, live streaming, recording, audio mute, source visibility and replay buffer. OBS also supports Studio Mode and virtual camera controls.
- Streaming credentials are protected for the current Windows user. Existing macropad firmware works as is; no firmware update or flash is required.

## [2.2.0](https://github.com/lolo34dr/deckpilot/releases/tag/v2.2.0)

DeckPilot 2.2.0 adds adjustable, local microphone noise suppression while keeping soundboard clips untouched.

- RNNoise filters the selected physical microphone before DeckPilot mixes in soundboard clips. Clips bypass the filter and remain clear in the virtual microphone.
- Choose any application that uses **CABLE Output** as its microphone—games and voice-chat applications alike receive the filtered voice and soundboard mix. Apps using the physical mic directly bypass DeckPilot's filter.
- Enable or disable filtering and set its 0–100% strength in **Soundboard · micro virtuel**. Audio processing stays on this PC; no firmware flash is required.
- The RNNoise native library is built for Windows x64 and embedded into DeckPilot and its installer. Its BSD-style third-party license is included in the source repository.

The 10 ms RNNoise frame size does not guarantee end-to-end latency. Windows, the audio devices and VB-CABLE also affect delay. Test microphone quality and latency with the intended setup.

## [2.1.9](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.9)

DeckPilot 2.1.9 fixes speaker monitoring on devices whose Windows audio format is extensible, including some USB headsets, and adds a soundboard overlay notification.

- The speaker return keeps the selected output's exact WASAPI mix format, converts clip samples to its PCM or float encoding, and retries without event-driven playback when a driver rejects that mode.
- Starting a soundboard clip shows its name in the configurable on-screen overlay. The existing profile notification preference, placement and reduced-motion setting control both notification types.
- The return still contains only soundboard clips; the physical microphone stays out of the speaker mix and continues to the virtual microphone.

DeckPilot Atelier remains at 1.0.12 and firmware remains 1.1.1. No firmware flash is needed. Speaker compatibility must still be checked with the owner's actual Windows audio device.

## [2.1.8](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.8)

DeckPilot 2.1.8 adds an optional speaker return for Soundboard clips.

- In **Soundboard · micro virtuel**, enable **Entendre aussi les sons du soundboard**, choose a physical speaker or headset output, and set its independent 0–100% volume. The return is off by default and saved across launches.
- Only soundboard clips are sent to this second output. The physical microphone continues to go to the virtual microphone, and is never mixed into the speaker return. Clips still go to VB-CABLE at their original volume.
- Changing the return volume does not restart the virtual microphone. A disconnected speaker output stops only the return; the virtual microphone keeps running. Refresh devices or choose another output to restore it.

DeckPilot Atelier remains at 1.0.12 and firmware remains 1.1.1. No firmware flash is needed. Actual speaker routing should be checked on the owner's Windows PC with its audio devices.

## [2.1.7](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.7)

DeckPilot 2.1.7 adds Spotify-only volume actions, a built-in action to open DeckPilot, and on-screen profile notifications.

- Spotify volume buttons adjust Spotify's Windows audio session in 5% steps, without changing the master volume or other players.
- The application picker and action search always offer DeckPilot, and its action shows the existing window from the notification area.
- Profile changes show a brief, non-activating overlay on the active screen. Choose a corner or custom position in preferences and preview it there. Exclusive full-screen games can hide standard Windows overlays.

DeckPilot Atelier remains at 1.0.12 and firmware remains 1.1.1. No firmware flash is needed for these actions.

## [2.1.6](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.6)

DeckPilot 2.1.6 and DeckPilot Atelier 1.0.12 make Atelier's firmware erase explicit and recoverable.

- Move the destructive firmware erase into collapsed advanced operations and require typing `EFFACER` before writing.
- Explain that the board stops enumerating as a keyboard after erasure, show the exact RST/GND double-reset recovery steps, and keep Atelier's recovery action available after erasure or an interrupted flash.
- Firmware remains 1.1.1; no DeckPilot hardware firmware change is required.

Validation includes firmware and app checks, plus Windows UI automation for the advanced erase control, no-device safety and RST/GND recovery guidance. The reset itself still requires the physical board.

## [2.1.5](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.5)

DeckPilot 2.1.5 smooths audio-device refreshes and adds a branded startup screen.

- Discover physical microphones and VB-CABLE off the UI thread. Automatic retries share a recent device snapshot; the manual refresh button always performs a fresh scan.
- Reduce repeated Windows audio-state polling and streamline the common mono-microphone conversion path.
- Show the DeckPilot banner while the interactive app starts. Silent startup with Windows stays in the background.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes background scan/cache regression checks, audio conversion checks, Windows builds, installer upgrade/uninstall, native splash/update and application-picker checks. Actual audio CPU use and latency still depend on the PC and audio devices.

## [2.1.4](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.4)

DeckPilot 2.1.4 starts the configured virtual microphone automatically with the app, including background startup after Windows sign-in.

- Automatic soundboard microphone startup is enabled by default for existing and new settings. It uses the selected physical microphone and VB-CABLE; no manual activation is needed after restarting the PC when DeckPilot starts with Windows.
- If devices are not ready at sign-in, or audio stops after a disconnect, DeckPilot retries every five seconds while automatic activation is enabled.
- An explicit stop disables and saves the automatic-start preference; enable it again from the Soundboard panel or the activation button. Audio clips are never played automatically.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes settings migration/round-trip checks and a native Windows UI check for the persisted automatic-start preference, plus the existing audio and installer checks. Physical device startup/reconnection still depends on the owner's hardware.

## [2.1.3](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.3)

DeckPilot 2.1.3 reduces soundboard microphone latency and removes the one-second conversion blocks from the live audio path.

- Use output-driven, small-block resampling for the microphone and sound clips; matching sample rates bypass resampling entirely.
- Request event-driven 20 ms capture and 40 ms playback buffers instead of the previous 100 ms settings. Actual latency depends on Windows, the audio devices and VB-CABLE.
- Bound microphone buffering to 100 ms and retain recent complete frames after a playback stall instead of queueing old speech. Short underruns add only the silence requested by the current read.
- Support mono, stereo, multichannel and Windows extensible PCM/float inputs without the Media Foundation one-second read-ahead.
- Keep Discord processing and routing unchanged. Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes regression checks for resampling block sizes, channel conversion, extensible formats, underrun recovery, overflow freshness, concurrent capture/playback and clip completion, alongside the existing Windows installer and application tests. No hardware latency figure is claimed; verify the improvement with your microphone and audio driver.

## [2.1.2](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.2)

DeckPilot 2.1.2 and DeckPilot Atelier 1.0.11 fix workshop safety, soundboard reliability and portable profile sounds.

- Atelier can prepare Leonardo/Caterina boards that do not yet answer the DeckPilot protocol; serial failures still stop flashing and the USB location, bootloader and MCU signature remain verified.
- Every inspection starts with empty results. Reports retain the tested board and firmware, and stale USB callbacks cannot affect a new session.
- Closing Atelier during firmware writing or verification is blocked.
- Profile exports and automatic profile backups can retain referenced MP3/WAV files (32 MiB audio limit). Imports copy embedded clips into a managed folder and remap short/long action references. Legacy profile imports remain supported; missing backup audio is reported explicitly.
- The soundboard releases finished clips, updates playback indicators, and reports interrupted audio streams. Renamed VB-CABLE endpoints are recognized through their adapter identity.
- Correct translated README links and preserve technical device names during translation. Clarify the VB-CABLE requirement.

Validation includes core regression checks, firmware protocol tests, translation tests, Windows build, DeckPilot and Atelier installation/upgrade/uninstall checks, and native interface/update checks. Physical first-flash and audio disconnect behavior still require the owner's hardware. Firmware remains 1.1.1; no firmware update is required.

## [2.1.1](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.1)

DeckPilot 2.1.1 improves installation and updates:

- Automatically check for VB-CABLE during first installation and upgrades, and download and launch the official signed installer when the driver is missing.
- Show Windows startup, automatic update checks and reduced-motion preferences during setup and updates, with existing choices preselected and preserved.
- Keep the selected physical microphone and request microphone setup only when needed.
- Fix installer initialization and detection of existing installations.

Download **DeckPilot-Setup.exe** or accept the update in DeckPilot. Windows administrator approval is required. Existing profiles and calibration are preserved. DeckPilot Atelier remains at 1.0.10 and firmware remains at 1.1.1; both are included in the shared release.

Validation: core checks and Windows build, installer upgrade/uninstall and native application tests run before release publication. Audio hardware behavior still requires testing on the user's PC.

## [2.1.0](https://github.com/lolo34dr/deckpilot/releases/tag/v2.1.0)

DeckPilot 2.1.0 keeps the deep black interface and customizable colors, and routes the soundboard through VB-Audio VB-CABLE automatically. Users select only their physical microphone; the virtual playback endpoint is fixed so voice and MP3/WAV clips cannot accidentally be sent to speakers. The DeckPilot installer can download the official VB-CABLE package, verify its Authenticode publisher, and launch the vendor's driver setup; the package is not redistributed. In Discord or a game, select CABLE Output as the microphone. Atelier remains at version 1.0.10.

## [2.0.0](https://github.com/lolo34dr/deckpilot/releases/tag/v2.0.0)

DeckPilot 2.0.0 introduces a deep black interface with saved background, panel and accent colors. The new soundboard accepts MP3/WAV clips, lets users choose a physical microphone and a virtual cable output, and mixes voice with clips for use as a microphone in games or calls. Clips can be started from the app or assigned to a macropad key. A virtual audio cable such as VB-CABLE must be installed separately on Windows; Atelier remains at version 1.0.10.

## [1.9.11](https://github.com/lolo34dr/deckpilot/releases/tag/v1.9.11)

DeckPilot 1.9.11 and DeckPilot Atelier 1.0.10 add pre-flash hardware and firmware diagnostics, a firmware version comparison, a guided recovery action after interrupted writes, sequential multi-board flashing with a batch report, and a physical-position-to-F13–F24 layout check in every key-control report.

DeckPilot 1.9.11 includes the same Atelier update in the shared installer release.

## [1.9.10](https://github.com/lolo34dr/deckpilot/releases/tag/v1.9.10)

DeckPilot 1.9.10 restores shared releases containing both DeckPilot and DeckPilot Atelier 1.0.9. Both applications once again check the same latest GitHub release.

If you installed Atelier 1.0.8 from the separate release channel, install DeckPilot-Atelier-Setup.exe from this release once to return to shared updates. Earlier Atelier versions and DeckPilot can use their existing updater.

DeckPilot Atelier 1.0.7 adds a confirmed program-area erase that clears all 28 KiB of user flash, leaves a safe reset loop, and preserves Caterina and EEPROM. Reset remains disabled until a physical board is selected.

## [1.9.8](https://github.com/lolo34dr/deckpilot/releases/tag/v1.9.8)

DeckPilot Atelier 1.0.7 adds a confirmed program-area erase that clears all 28 KiB of user flash, leaves a safe reset loop, and preserves Caterina and EEPROM. Reset remains disabled until a physical board is selected.

## [1.9.7](https://github.com/lolo34dr/deckpilot/releases/tag/v1.9.7)

DeckPilot 1.9.7 fixes Atelier uninstalling while the app is open. Atelier 1.0.6 also corrects its Windows DPI manifest, which prevented the installed app from launching.

## [1.9.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.9.0)

DeckPilot 1.9.0 adds DeckPilot Atelier, a separate Windows workshop app for flashing new macropads and checking all 12 keys.

- Install the included firmware on Leonardo / Pro Micro ATmega32U4 boards with the Caterina bootloader.
- Run guided short-press and long-press checks for every physical key, record its F13–F24 mapping, and save a quality-control report.
- Download **DeckPilot-Atelier-Setup.exe** from this release. The original DeckPilot setup and firmware remain available separately.

## [1.8.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.8.0)

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

## [1.7.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.7.0)

DeckPilot 1.7.0 reduces repeated work while editing profiles, finding applications and running in the background.

- Saving unchanged settings no longer replaces the file or creates backups. Actual disk contents are still checked, so external edits, deletions and failed saves are not hidden.
- Profile history keeps detached model snapshots. Unchanged edits and undo/redo avoid JSON round-trips; action editing uses value comparisons instead of repeated serialization.
- Application search and picker share a five-minute catalog cache and one discovery task. Discovery runs outside WPF; Refresh still forces a new scan. Each picker owns its filter and keeps current results usable during refresh.
- Replace the permanent 100 ms board UI poll with ordered connection notifications. Keyboard-hook callbacks queue visual work instead of rendering twelve keys synchronously.
- Audio status polls every two seconds while visible and every ten seconds when hidden/minimized, refreshing immediately on reopen and after audio commands. Overlapping refresh requests are coalesced without dropping the post-command update.
- Hidden windows stop visual feedback timers and skip cosmetic rendering waits. Frozen UI brushes are reused and launched application process wrappers are disposed.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are preserved. No firmware flash is required.

Validation includes settings/history/cache regressions and Windows installation, update, picker, drag-and-drop, short/long action editing and minimize/restore checks. These are structural optimizations, not a measured claim of a particular CPU or RAM saving on every PC.

## [1.6.4](https://github.com/lolo34dr/deckpilot/releases/tag/v1.6.4)

DeckPilot 1.6.4 fixes search-result replacement during drag-and-drop and stabilizes Windows verification.

- Keep existing search buttons when the debounce timer or installed-app catalog produces the same results, so a drag source is not detached unexpectedly.
- Defer search-result replacement while a mouse interaction or OLE drag is in progress.
- Windows UI checks use physical cursor coordinates, focus controls before measuring them, and scan the full scroll range instead of assuming a fixed number of line increments. Native mouse drags remain required; failed assignments are not retried to obtain a pass.
- Windows settings saves use the dedicated atomic replacement API so an existing reader can retain its old snapshot without blocking the next save. A core regression verifies three successive replacements with a shared reader held open.
- Settings readers in Windows tests use UTF-8 and allow atomic file replacement. Snapshot opening waits up to 500 ms only for Windows sharing/lock violations during replacement; other read errors and persistent locks fail the test. Three undo/redo cycles also keep an old snapshot open to verify that saves succeed without modifying existing readers.
- Background-startup checks wait for WPF to finish hiding its window, then require the process to remain running with no visible main window.
- Upgrade GitHub Actions to Node.js 24 versions and install the same Arduino CLI 1.2.2 from its checksum-verified upstream archive.

Download **DeckPilot-Setup.exe** or update from the app. Profiles and calibration are preserved. No firmware flash is needed.

## [1.6.1](https://github.com/lolo34dr/deckpilot/releases/tag/v1.6.1)

DeckPilot 1.6.1 fixes soft keypad rendering and adds profile recovery, Windows audio controls and short/long presses.

- Native-size keypad text: removed whole-deck scaling and content shadow effects, added per-monitor DPI awareness and larger desktop app icons. Small windows scroll instead of shrinking keys.
- Undo/redo for profile edits, including drag-and-drop (50 changes per app session); text fields keep native Ctrl+Z/Ctrl+Y.
- Profile import/export and 20 automatic previous-version backups. Imports add new copies; restore and undo preserve calibration. Imported actions are never executed automatically.
- Global Windows microphone toggle with a persistent status-bar indicator and live endpoint status. All active capture endpoints are muted/unmuted together.
- Toggle between two chosen connected output devices, for all Windows default audio roles. Missing devices fail without silently choosing another output.
- Two configured actions per key with a 200–3000 ms threshold. Short runs on release, long replaces short and runs once. Existing immediate actions and free F13–F24 keys keep their behavior. No firmware flash required.

Download **DeckPilot-Setup.exe** or update from the app. Profiles and calibration are preserved. Audio endpoint choices may need adjusting after importing profiles on another PC. Windows endpoint state differs from an application's own mute or a hardware mute switch; fixed-output applications may not follow default output changes.

Validation: core profile/gesture/audio tests (audio endpoints simulated), Windows installer/update/UI tests, native mouse drag undo/redo and long-action editing. Physical key holds, actual microphone drivers and headset/speaker switching still need confirmation on the owner's hardware.

## [1.5.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.5.0)

DeckPilot 1.5.0 adds Spotify-specific controls, action search and drag-and-drop, and repairs Windows startup registration.

- **Spotify** play/pause, next and previous address only the Spotify desktop media session, keeping browser/YouTube playback separate. Open Spotify and start playback once. Spotify in a browser is not supported; there is no global-media fallback if Spotify is missing or refuses a command.
- Search actions and installed apps by name or keywords such as volume, skip, Spotify or Discord. Click results or drag them onto a key.
- Drag catalog/search actions onto keys, swap two keys' actions without changing calibration, or drop an executable, shortcut, folder or HTTP(S) link. Drops configure actions without running them.
- Startup keeps a saved per-user preference, repairs missing/stale paths, detects known Windows disable flags and supports explicit reactivation. Background and duplicate startup launches remain silent, including before calibration.

The build requires Windows 10 version 2004 or later, or Windows 11. Download **DeckPilot-Setup.exe** or update from DeckPilot. Profiles and calibration are preserved; no firmware flash is needed. To repair a previously blocked startup, check its status under **Préférences de l’application** and enable **Lancer avec Windows** again.

Validation includes simulated Spotify/browser isolation, search and settings checks, Windows installer/startup repair tests, real mouse drag-and-drop and native update/UI checks. Spotify playback alongside YouTube and a real Windows sign-in still need confirmation on the user's PC.

## [1.4.2](https://github.com/lolo34dr/deckpilot/releases/tag/v1.4.2)

DeckPilot 1.4.2 fixes interface consistency and layout issues.

- Distinct hover, selection and keyboard focus states. Buttons no longer grow into surrounding panels or show misleading selection borders.
- Dark expandable sections with vector chevrons replace the white system arrows. Headers and checkbox text wrap within their panels.
- The entire sidebar scrolls, including preferences and calibration, so expanded sections cannot cover other controls.
- Larger, clearer key captions and secondary text, a better balanced keypad/editor layout, pixel-aligned main-window rendering and clipped long profile names.
- Editor headings wrap without overlapping. Free-key help now explains the difference between normal F13–F24 keyboard use and calibrating positions for DeckPilot actions.

Windows UI checks cover action dispatch/error feedback, expanded panels at 960 × 720 and 1280 × 1000, scrolling to calibration and profile/keypad separation.

Download **DeckPilot-Setup.exe** or update from the app. Profiles and calibration are preserved. No firmware flash is needed.

## [1.4.1](https://github.com/lolo34dr/deckpilot/releases/tag/v1.4.1)

DeckPilot 1.4.1 adds a clear 20 px gap between the profile selector and the keypad enclosure, including when the window adapts to a smaller screen.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. No firmware update is required for this interface fix.

## [1.4.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.4.0)

DeckPilot 1.4.0 refreshes the keypad interface around the physical macropad's black keycaps.

- Larger 3 × 4 keys in a dark enclosure, with a 36 px icon, a short label, an F13–F24 caption and a small action-type icon. Full labels remain available in tooltips.
- Independent feedback for physical presses, actions in progress, successful dispatch and errors. Subtle animations respect Windows' animation setting; errors include an explanation in the editor.
- A **Tester l’action** button saves and runs the selected action on the PC, including without a connected board. A success indicator means the action was dispatched, not that the launched application finished its work.
- Manual application paths and arguments are grouped under collapsed advanced options. Folder and website destinations stay expanded. App preferences are also collapsed by default.
- The keypad clearly shows its connection state and explains that profiles remain editable and actions testable offline. Reconnecting restores live button feedback without changing configuration.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. This release keeps firmware **1.1.1**; no new firmware flash is needed for the interface changes. Firmware 1.1.1 still needs physical-board validation; the earlier 1.0.0 firmware and flashing procedure were confirmed by the owner.

## [1.3.2](https://github.com/lolo34dr/deckpilot/releases/tag/v1.3.2)

DeckPilot 1.3.2 improves the USB connection and checks macropad firmware at startup.

- Connection dashboard with automatic reconnect, detected firmware, live button state/USB diagnostics and explicit board selection when several devices are present.
- Startup firmware checks and update proposals, with a 24-hour reminder delay and an option to disable firmware notifications. Installation still needs confirmation.
- New firmware **1.1.1**: bounded protocol messages, fresh session tokens, correlated heartbeat replies and key-state events. The F13–F24 mapping remains unchanged, including standalone use.
- Firmware manager automatically releases the monitor's serial port before identification/flashing and reconnects afterward. Firmware 1.0.0 and legacy identity queries remain supported.
- Profiles can be selected from the tray menu; Ctrl+S saves changes; connection diagnostics can be copied for support.

Firmware updates are delivered in app releases; startup checks compare the detected version against the included bundle. No hardware is flashed silently. **Firmware 1.1.1 still needs a first physical-board test:** firmware 1.0.0 and the earlier flash procedure were validated by the owner, while this new protocol is covered by host tests and AVR compilation.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. Quit DeckPilot before using Arduino IDE or another serial monitor; opening the in-app firmware manager releases the connection automatically.

## [1.3.1](https://github.com/lolo34dr/deckpilot/releases/tag/v1.3.1)

DeckPilot 1.3.1 improves the USB connection and checks macropad firmware at startup.

- Connection dashboard with automatic reconnect, detected firmware, live button state/USB diagnostics and explicit board selection when several devices are present.
- Startup firmware checks and update proposals, with a 24-hour reminder delay and an option to disable firmware notifications. Installation still needs confirmation.
- New firmware **1.1.0**: bounded protocol messages, fresh session tokens, correlated heartbeat replies and key-state events. The F13–F24 mapping remains unchanged, including standalone use.
- Firmware manager automatically releases the monitor's serial port before identification/flashing and reconnects afterward. Firmware 1.0.0 and legacy identity queries remain supported.
- Profiles can be selected from the tray menu; Ctrl+S saves changes; connection diagnostics can be copied for support.

Firmware updates are delivered in app releases; startup checks compare the detected version against the included bundle. No hardware is flashed silently. **Firmware 1.1.0 still needs a first physical-board test:** firmware 1.0.0 and the earlier flash procedure were validated by the owner, while this new protocol is covered by host tests and AVR compilation.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. Quit DeckPilot before using Arduino IDE or another serial monitor; opening the in-app firmware manager releases the connection automatically.

## [1.3.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.3.0)

DeckPilot 1.3.0 improves the USB connection and checks macropad firmware at startup.

- Connection dashboard with automatic reconnect, detected firmware, live button state/USB diagnostics and explicit board selection when several devices are present.
- Startup firmware checks and update proposals, with a 24-hour reminder delay and an option to disable firmware notifications. Installation still needs confirmation.
- New firmware **1.1.0**: bounded protocol messages, fresh session tokens, correlated heartbeat replies and key-state events. The F13–F24 mapping remains unchanged, including standalone use.
- Firmware manager automatically releases the monitor's serial port before identification/flashing and reconnects afterward. Firmware 1.0.0 and legacy identity queries remain supported.
- Profiles can be selected from the tray menu; Ctrl+S saves changes; connection diagnostics can be copied for support.

Firmware updates are delivered in app releases; startup checks compare the detected version against the included bundle. No hardware is flashed silently. **Firmware 1.1.0 still needs a first physical-board test:** firmware 1.0.0 and the earlier flash procedure were validated by the owner, while this new protocol is covered by host tests and AVR compilation.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. Quit DeckPilot before using Arduino IDE or another serial monitor; opening the in-app firmware manager releases the connection automatically.

## [1.2.2](https://github.com/lolo34dr/deckpilot/releases/tag/v1.2.2)

DeckPilot now displays its own logo in Windows instead of the generic application icon.

- The application executable embeds a multi-resolution Windows icon (16–256 px).
- The installer and Windows uninstall entry use the project logo.
- Desktop and Start menu shortcuts explicitly reference the installed DeckPilot icon, including on upgrades.

Download **DeckPilot-Setup.exe** and install over the existing version. Profiles, calibration and startup preferences are kept. Firmware 1.0.0 is unchanged; no hardware flash is needed.

## [1.2.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.2.0)

DeckPilot now installs like a regular Windows application, with the correct **3 rows × 4 columns** macropad layout.

- Download **DeckPilot-Setup.exe** to install into **Program Files\DeckPilot**, with Start menu and desktop shortcuts and a Windows uninstall entry.
- Windows startup is enabled by default and can be disabled during installation or from the app. Updates preserve that choice.
- Installed app updates download and verify the setup package, request normal Windows administrator permission, then install and restart the app.
- The main grid, position labels and guided calibration now use three rows of four keys. Existing profiles/actions are kept; the previous layout requires one new calibration.
- Firmware 1.0.0 is unchanged. The owner has confirmed successful flashing, version identification and all twelve keys on the reference macropad. This app update does not flash hardware.

Windows 10/11 x64. The standalone **DeckPilot.exe** and portable ZIP remain available for older updater compatibility. Personal profiles remain in `%APPDATA%\DeckPilot` and are retained on uninstall.

## [1.1.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.1.0)

DeckPilot can now install firmware on the DIY Pro Micro macropad directly from the app.

- New **Firmware du macropad…** dialog with device selection and confirmation before flashing.
- Bundled firmware **1.0.0** preserves F13–F24 and adds a DeckPilot identity/version response over USB serial.
- Bundled AVRDUDE: no Arduino IDE installation needed for normal updates.
- Validated firmware checksum and memory boundaries, physical USB connection matching, write verification and post-flash version check.
- Existing PC profiles and calibration are preserved. Updating the app never flashes hardware automatically.

**First hardware test still required:** the original firmware's Caterina upload procedure is confirmed; the new firmware/updater must still be tried on the actual macropad. For Pro Micro **ATmega32U4, 5 V / 16 MHz**, wired on **2–10, 14, 15, 16**, with Leonardo USB IDs and Caterina bootloader only. Keep the original sketch and RST/GND access for recovery. No firmware backup or automatic rollback.

Windows 10/11 x64. Download **DeckPilot.exe** or **DeckPilot-win-x64.zip**. The firmware/tools are embedded in the app and also available in **DeckPilot-firmware.zip**; AVRDUDE has its own GPL license and bundled source.

## [1.0.1](https://github.com/lolo34dr/deckpilot/releases/tag/v1.0.1)

Repository rename preparation: `streamdeck-diy` → `deckpilot`.

- Update checks support both repository names, before and after the rename.
- Download validation accepts only the exact release asset paths belonging to these two repositories.
- Profiles, calibration, and application actions are unchanged.

**Install this version before renaming the GitHub repository.** Versions 0.2.0 and 1.0.0 validate the original repository name strictly and may require a manual update if the repository has already been renamed.

Windows 10/11 x64. Download **DeckPilot.exe** or **DeckPilot-win-x64.zip**. Updates remain available from within the app while the original repository name is active.

## [1.0.0](https://github.com/lolo34dr/deckpilot/releases/tag/v1.0.0)

Version 1.0.0 publiée uniquement pour tester la mise à jour automatique depuis 0.2.0.

Aucun changement fonctionnel ni visuel. Seul le numéro de version de l’application change ; les profils et le calibrage sont conservés.

Dans DeckPilot 0.2.0, utilisez **Vérifier maintenant**, puis confirmez **Mettre à jour**, ou attendez la recherche automatique. Aucun téléchargement manuel n’est nécessaire pour ce test.

Téléchargements également disponibles : **DeckPilot.exe**, **DeckPilot-win-x64.zip** et **SHA256SUMS.txt**. Windows 10/11 x64.

## [0.2.0](https://github.com/lolo34dr/deckpilot/releases/tag/v0.2.0)

Applications installées, interface affinée et mises à jour automatiques.

- Sélecteur d’applications avec recherche par nom : menu Démarrer et catalogue Windows/Microsoft Store. Le choix manuel d’un `.exe` reste disponible.
- Icônes des applications lorsqu’elles sont disponibles, pictogrammes vectoriels et animations discrètes respectant les préférences Windows.
- Recherche automatique des versions stables sur GitHub. Une confirmation suffit pour télécharger, vérifier, installer et redémarrer.
- Vérification SHA-256 et conservation de l’ancien exécutable jusqu’à confirmation du démarrage, avec restauration en cas d’échec.
- Profils et calibrage existants conservés.

**Depuis 0.1.x : quittez l’ancienne version près de l’horloge et remplacez l’exécutable une dernière fois manuellement.** Les mises à jour suivantes seront proposées automatiquement. Utilisez un dossier personnel modifiable pour permettre le remplacement.

Téléchargez **DeckPilot.exe** pour la version autonome, ou **DeckPilot-win-x64.zip** pour le dossier complet. Windows 10/11 x64 ; aucun runtime .NET à installer.

Avant publication : compilation, tests de logique et de téléchargement, contrôle du verrouillage Windows et test natif de remplacement/redémarrage avec sélection d’une application. Le Leonardo et les jeux restent à valider sur votre PC. Les exécutables ne sont pas signés.

## [0.1.1](https://github.com/lolo34dr/deckpilot/releases/tag/v0.1.1)

Interface plus sobre pour DeckPilot, inspirée de la disposition Stream Deck.

- Palette anthracite et gris, typographie plus compacte et suppression du grand texte d’accueil.
- Grille de 4 × 3 touches avec pictogrammes et bordure de sélection discrète.
- Choix des actions à droite et réglages de la touche sous la grille.
- Gestion des profils dans un panneau repliable ; calibrage et barre de titre assortis au thème sombre.

Les profils et le calibrage existants sont conservés. Quittez l’ancienne version depuis son icône près de l’horloge avant de remplacer l’exécutable.

Téléchargez **DeckPilot.exe** pour la version autonome, ou **DeckPilot-win-x64.zip** pour le dossier complet à extraire. Windows 10/11 x64, aucun runtime .NET à installer.

La compilation Windows et les 16 vérifications automatiques sont exécutées avant publication. Le rendu à l’écran et le matériel restent à valider sur votre PC Windows. Les exécutables ne sont pas signés.

## [0.1.0](https://github.com/lolo34dr/deckpilot/releases/tag/v0.1.0)

Première version de DeckPilot pour Windows 10/11 x64.

- Interface sombre avec grille de 4 lignes et 3 colonnes.
- Calibrage guidé des 12 boutons F13 à F24.
- Profils manuels et actions : touche libre, application, dossier, site, raccourci et multimédia.
- Zone de notification et démarrage Windows facultatif.
- Aucun pilote ni changement du programme du Leonardo.

Téléchargez **DeckPilot.exe** pour la version autonome, ou **DeckPilot-win-x64.zip** pour le dossier complet à extraire. Aucun runtime .NET à installer.

Le code est compilé et les vérifications automatiques sont exécutées par ce workflow. Le fonctionnement matériel et l’interface restent à valider sur un PC Windows avec le Leonardo. Les exécutables ne sont pas signés.

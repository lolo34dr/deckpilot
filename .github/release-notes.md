DeckPilot 1.6.0 fixes soft keypad rendering and adds profile recovery, Windows audio controls and short/long presses.

- Native-size keypad text: removed whole-deck scaling and content shadow effects, added per-monitor DPI awareness and larger desktop app icons. Small windows scroll instead of shrinking keys.
- Undo/redo for profile edits, including drag-and-drop (50 changes per app session); text fields keep native Ctrl+Z/Ctrl+Y.
- Profile import/export and 20 automatic previous-version backups. Imports add new copies; restore and undo preserve calibration. Imported actions are never executed automatically.
- Global Windows microphone toggle with a header indicator and live endpoint status. All active capture endpoints are muted/unmuted together.
- Toggle between two chosen connected output devices, for all Windows default audio roles. Missing devices fail without silently choosing another output.
- Two configured actions per key with a 200–3000 ms threshold. Short runs on release, long replaces short and runs once. Existing immediate actions and free F13–F24 keys keep their behavior. No firmware flash required.

Download **DeckPilot-Setup.exe** or update from the app. Profiles and calibration are preserved. Audio endpoint choices may need adjusting after importing profiles on another PC. Windows endpoint state differs from an application's own mute or a hardware mute switch; fixed-output applications may not follow default output changes.

Validation: core profile/gesture/audio tests (audio endpoints simulated), Windows installer/update/UI tests, native mouse drag undo/redo and long-action editing. Physical key holds, actual microphone drivers and headset/speaker switching still need confirmation on the owner's hardware.

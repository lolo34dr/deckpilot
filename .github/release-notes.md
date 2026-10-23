DeckPilot 1.5.0 adds Spotify-specific controls, action search and drag-and-drop, and repairs Windows startup registration.

- **Spotify** play/pause, next and previous address only the Spotify desktop media session, keeping browser/YouTube playback separate. Open Spotify and start playback once. Spotify in a browser is not supported; there is no global-media fallback if Spotify is missing or refuses a command.
- Search actions and installed apps by name or keywords such as volume, skip, Spotify or Discord. Click results or drag them onto a key.
- Drag catalog/search actions onto keys, swap two keys' actions without changing calibration, or drop an executable, shortcut, folder or HTTP(S) link. Drops configure actions without running them.
- Startup keeps a saved per-user preference, repairs missing/stale paths, detects known Windows disable flags and supports explicit reactivation. Background and duplicate startup launches remain silent, including before calibration.

The build requires Windows 10 version 2004 or later, or Windows 11. Download **DeckPilot-Setup.exe** or update from DeckPilot. Profiles and calibration are preserved; no firmware flash is needed. To repair a previously blocked startup, check its status under **Préférences de l’application** and enable **Lancer avec Windows** again.

Validation includes simulated Spotify/browser isolation, search and settings checks, Windows installer/startup repair tests, real mouse drag-and-drop and native update/UI checks. Spotify playback alongside YouTube and a real Windows sign-in still need confirmation on the user's PC.

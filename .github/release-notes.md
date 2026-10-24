DeckPilot 1.6.3 fixes search-result replacement during drag-and-drop and stabilizes Windows verification.

- Keep existing search buttons when the debounce timer or installed-app catalog produces the same results, so a drag source is not detached unexpectedly.
- Defer search-result replacement while a mouse interaction or OLE drag is in progress.
- Windows UI checks use physical cursor coordinates, focus controls before measuring them, and scan the full scroll range instead of assuming a fixed number of line increments. Native mouse drags remain required; failed assignments are not retried to obtain a pass.
- Settings readers in Windows tests use UTF-8 and allow atomic file replacement. Three undo/redo cycles also keep an old snapshot open to verify that saves succeed without modifying existing readers.
- Background-startup checks wait for WPF to finish hiding its window, then require the process to remain running with no visible main window.
- Upgrade GitHub Actions to Node.js 24 versions and install the same Arduino CLI 1.2.2 from its checksum-verified upstream archive.

Download **DeckPilot-Setup.exe** or update from the app. Profiles and calibration are preserved. No firmware flash is needed.

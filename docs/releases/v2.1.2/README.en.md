DeckPilot 2.1.2 and DeckPilot Atelier 1.0.11 fix workshop safety, soundboard reliability and portable profile sounds.

- Atelier can prepare Leonardo/Caterina boards that do not yet answer the DeckPilot protocol; serial failures still stop flashing and the USB location, bootloader and MCU signature remain verified.
- Every inspection starts with empty results. Reports retain the tested board and firmware, and stale USB callbacks cannot affect a new session.
- Closing Atelier during firmware writing or verification is blocked.
- Profile exports and automatic profile backups can retain referenced MP3/WAV files (32 MiB audio limit). Imports copy embedded clips into a managed folder and remap short/long action references. Legacy profile imports remain supported; missing backup audio is reported explicitly.
- The soundboard releases finished clips, updates playback indicators, and reports interrupted audio streams. Renamed VB-CABLE endpoints are recognized through their adapter identity.
- Correct translated README links and preserve technical device names during translation. Clarify the VB-CABLE requirement.

Validation includes core regression checks, firmware protocol tests, translation tests, Windows build, DeckPilot and Atelier installation/upgrade/uninstall checks, and native interface/update checks. Physical first-flash and audio disconnect behavior still require the owner's hardware. Firmware remains 1.1.1; no firmware update is required.

DeckPilot 2.1.5 smooths audio-device refreshes and adds a branded startup screen.

- Discover physical microphones and VB-CABLE off the UI thread. Automatic retries share a recent device snapshot; the manual refresh button always performs a fresh scan.
- Reduce repeated Windows audio-state polling and streamline the common mono-microphone conversion path.
- Show the DeckPilot banner while the interactive app starts. Silent startup with Windows stays in the background.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes background scan/cache regression checks, audio conversion checks, Windows builds, installer upgrade/uninstall, native splash/update and application-picker checks. Actual audio CPU use and latency still depend on the PC and audio devices.

DeckPilot 1.3.1 improves the USB connection and checks macropad firmware at startup.

- Connection dashboard with automatic reconnect, detected firmware, live button state/USB diagnostics and explicit board selection when several devices are present.
- Startup firmware checks and update proposals, with a 24-hour reminder delay and an option to disable firmware notifications. Installation still needs confirmation.
- New firmware **1.1.0**: bounded protocol messages, fresh session tokens, correlated heartbeat replies and key-state events. The F13–F24 mapping remains unchanged, including standalone use.
- Firmware manager automatically releases the monitor's serial port before identification/flashing and reconnects afterward. Firmware 1.0.0 and legacy identity queries remain supported.
- Profiles can be selected from the tray menu; Ctrl+S saves changes; connection diagnostics can be copied for support.

Firmware updates are delivered in app releases; startup checks compare the detected version against the included bundle. No hardware is flashed silently. **Firmware 1.1.0 still needs a first physical-board test:** firmware 1.0.0 and the earlier flash procedure were validated by the owner, while this new protocol is covered by host tests and AVR compilation.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are retained. Quit DeckPilot before using Arduino IDE or another serial monitor; opening the in-app firmware manager releases the connection automatically.

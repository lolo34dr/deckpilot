DeckPilot 2.1.6 and DeckPilot Atelier 1.0.12 make Atelier's firmware erase explicit and recoverable.

- Move the destructive firmware erase into collapsed advanced operations and require typing `EFFACER` before writing.
- Explain that the board stops enumerating as a keyboard after erasure, show the exact RST/GND double-reset recovery steps, and keep Atelier's recovery action available after erasure or an interrupted flash.
- Firmware remains 1.1.1; no DeckPilot hardware firmware change is required.

Validation includes firmware and app checks, plus Windows UI automation for the advanced erase control, no-device safety and RST/GND recovery guidance. The reset itself still requires the physical board.

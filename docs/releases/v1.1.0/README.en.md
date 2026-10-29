DeckPilot can now install firmware on the DIY Pro Micro macropad directly from the app.

- New **Firmware du macropad…** dialog with device selection and confirmation before flashing.
- Bundled firmware **1.0.0** preserves F13–F24 and adds a DeckPilot identity/version response over USB serial.
- Bundled AVRDUDE: no Arduino IDE installation needed for normal updates.
- Validated firmware checksum and memory boundaries, physical USB connection matching, write verification and post-flash version check.
- Existing PC profiles and calibration are preserved. Updating the app never flashes hardware automatically.

**First hardware test still required:** the original firmware's Caterina upload procedure is confirmed; the new firmware/updater must still be tried on the actual macropad. For Pro Micro **ATmega32U4, 5 V / 16 MHz**, wired on **2–10, 14, 15, 16**, with Leonardo USB IDs and Caterina bootloader only. Keep the original sketch and RST/GND access for recovery. No firmware backup or automatic rollback.

Windows 10/11 x64. Download **DeckPilot.exe** or **DeckPilot-win-x64.zip**. The firmware/tools are embedded in the app and also available in **DeckPilot-firmware.zip**; AVRDUDE has its own GPL license and bundled source.

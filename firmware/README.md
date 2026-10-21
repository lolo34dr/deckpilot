# DeckPilot firmware 1.0.0

For the tested hardware configuration only: **Pro Micro ATmega32U4, 5 V / 16 MHz**, using the **Arduino Leonardo** board profile and a **Caterina / AVR109** bootloader. USB-C describes the connector, not board compatibility. The owner's original sketch was successfully uploaded with the 1200-baud reset and AVR109 at 57600 baud. This new firmware and the application's flash sequence still require a first test on that physical board.

## Wiring and behavior

Each switch connects one pin to GND. Internal pull-ups are enabled. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** send **F13 through F24** in that order. A stable 30 ms debounce handles press and release independently. Keyboard behavior, Windows-side profiles and calibration remain compatible; USB keyboard rollover remains limited by Arduino's Keyboard library. The firmware does not introduce per-button USB modes or native media controls yet.

Pin **1** held to GND at startup disables keyboard operation, preserving the original failsafe. The USB core can still support bootloader entry. Do not connect VCC to GND.

The USB CDC serial interface answers `DECKPILOT?\n` at 115200 baud with:

```
DECKPILOT|1|promicro-32u4-16mhz|1.0.0
```

No serial connection is required for normal keyboard operation. The identity is a compatibility check, not cryptographic hardware authentication. Windows still reports the Arduino Leonardo USB identifiers; many other boards share those identifiers.

## Install from DeckPilot

1. Keep the supplied recovery sketch (`firmware/Original/Original.ino`) and a working Arduino upload setup as a recovery option. Keep RST and GND physically accessible.
2. Close Arduino IDE, Serial Monitor and other serial tools. Disconnect other Leonardo boards.
3. Open **Firmware du macropad…**, choose the correct device and confirm the board and wiring. An unidentified original sketch can be installed over only after explicit confirmation.
4. Confirm installation and leave the USB cable connected until completion. Keyboard actions are suspended in DeckPilot while the firmware dialog is open; ordinary F13–F24 events still reach Windows.

The firmware and Windows AVRDUDE 8.0 are bundled inside the published app. No Arduino IDE or separate driver installation is performed by DeckPilot. Windows must already expose the board's USB serial port, as it did for the owner's successful upload. New firmware versions are delivered with app updates from GitHub; updating the app does **not** automatically flash hardware. Each flash requires confirmation.

DeckPilot checks SHA-256, Intel HEX record checksums, reset-vector presence, overlaps and the **28672-byte** application boundary before the reset. It matches the physical USB location across the runtime and bootloader ports, uses AVR109 at 57600 baud, requires AVRDUDE's write verification, then checks the running firmware's identity and version. It does not write fuses, EEPROM or the bootloader. Only Leonardo IDs `2341:8036` / `2A03:8036` and Caterina `2341:0036` / `2A03:0036` are accepted. If Windows cannot provide a matching physical connection, flashing is blocked rather than guessing a port.

Flashing is not atomic: a cable disconnect or power loss can leave the application firmware incomplete. There is no automatic firmware backup or rollback. If necessary, re-upload the original sketch with Arduino IDE set to **Arduino Leonardo**. Depending on the board's bootloader, briefly connecting RST to GND twice may expose a temporary bootloader port; upload while that port is available. A damaged bootloader requires ISP hardware and cannot be repaired by this USB updater. Do not write a different board or clock variant.

The last completed AVRDUDE process log is stored at `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

## Build

On Windows, from the repository root, install Arduino CLI **1.2.2** and .NET SDK 8, then run:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

The script pins Arduino AVR core **1.8.6**, Keyboard **1.0.6**, and upstream AVRDUDE **8.0**. CLI-managed packages use Arduino's package checksum validation; AVRDUDE binary/source archives have explicit SHA-256 pins in the script. `artifacts/firmware/DeckPilot-firmware.zip` contains the HEX, manifest, Windows tool, license and corresponding upstream AVRDUDE source archive. Publishing CI requires the bundle; an ordinary developer build without it can run the app but cannot flash.

The original 12-button sketch was adapted from David Madison's **Stream Cheap** project, copyright 2018, MIT. Its notice remains in `DeckPilot.ino`. DeckPilot additions use the repository MIT license. **AVRDUDE is separate software under GPL-2.0-or-later**, with its notice/license and source bundled; the repository MIT license does not replace that license. See https://github.com/avrdudes/avrdude/tree/v8.0 for upstream build instructions and dependency notices.

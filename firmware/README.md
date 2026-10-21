# DeckPilot firmware

For the tested hardware configuration only: **Pro Micro ATmega32U4, 5 V / 16 MHz**, using the **Arduino Leonardo** board profile and a **Caterina / AVR109** bootloader. USB-C describes the connector, not board compatibility. The owner's original sketch was successfully uploaded with the 1200-baud reset and AVR109 at 57600 baud. The owner has also confirmed successful in-app flashing, firmware 1.0.0 identification and operation of all 12 buttons on that reference board.

## Wiring and behavior

Each switch connects one pin to GND. Internal pull-ups are enabled. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** send **F13 through F24** in that order. A stable 30 ms debounce handles press and release independently. Keyboard behavior, Windows-side profiles and calibration remain compatible; USB keyboard rollover remains limited by Arduino's Keyboard library. The firmware does not introduce per-button USB modes or native media controls yet.

Pin **1** held to GND at startup disables keyboard operation, preserving the original failsafe. The USB core can still support bootloader entry. Do not connect VCC to GND.

The current USB CDC serial interface answers `DECKPILOT?\n` at 115200 baud with:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
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

## Firmware 1.1.1 and USB protocol 2

Firmware 1.1.1 adds a read-only diagnostic link while retaining the same F13–F24 keyboard mapping and pin-1 startup failsafe. **The owner's physical test covered firmware 1.0.0. Firmware 1.1.1 has automated protocol tests and an AVR build, but still needs its first test on the actual board.** Profiles and calibration remain on the PC and do not need to be recreated for this firmware update.

The new app identifies the connected card, keeps its serial port open, reconnects after unplugging, and checks its firmware version at startup and after reconnection. Firmware updates are delivered with app releases: startup checks compare the connected firmware with the validated bundle included in the installed app. App update checks still obtain newer releases from GitHub. There is no separate firmware-only download channel.

An older detected firmware prompts once per app session, with a 24-hour reminder delay when postponed. Notifications can be disabled under **Connexion et diagnostic**. Unknown Arduino sketches, disconnected devices and ambiguous multiple-card selections do not trigger automatic flashing or update prompts. Firmware installation always requires confirmation. Opening the firmware manager suspends the monitor and waits for the serial port to be released; monitoring resumes after the dialog closes.

**For a manual upload from Arduino IDE, quit DeckPilot from its tray icon first.** Closing the main window only hides it, so it continues to own the serial connection. DeckPilot and another serial monitor cannot use the same port simultaneously. If another tool already owns the port, the app reports the problem and retries without affecting the ordinary keyboard keys.

The new `DeckPilotProtocol.h` is the same implementation exercised by `tests/firmware-protocol.cpp`. It uses fixed-size buffers, bounded input processing and available USB transmit space; button scanning is never deliberately delayed while waiting for a host. The latest pending button mask is coalesced under backpressure. There are no commands to execute macros, write EEPROM, disable the keyboard, modify profiles or flash firmware over this diagnostic protocol.

At 115200 baud, lines end with LF (CRLF is accepted). Session tokens contain exactly eight uppercase hexadecimal characters. All numbers are unsigned decimal. Examples:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` advertises 12 keys and capability bits: 1 = heartbeat/state snapshot, 2 = key-state events. `PONG` echoes the request counter, then reports uptime in milliseconds, state sequence and the 12-bit button mask. `KEYS` reports sequence and mask; bit 0 is F13 and bit 11 is F24, independent of physical calibration. Sessions and request counters reject stale replies. Sequence comparisons handle 32-bit wraparound. Keys remain diagnostic data only: app actions still run once through the existing keyboard path, so a serial event cannot execute the same action twice.

Heartbeat requests are normally sent every two seconds. The app reconnects if a matching reply is missing for 1.5 seconds; the firmware stops unsolicited diagnostics after six seconds without a heartbeat or when the serial connection closes. Firmware 1.0.0 remains supported through the legacy identity query, without live key diagnostics. Older DeckPilot apps can still read the legacy identity of firmware 1.1.1.

The source version in `DeckPilotProtocol.h` supplies the release manifest version. Published firmware bundles now also contain the current sketch/header under `source/DeckPilot`, the recovery sketch and the DeckPilot MIT license.

The diagnostic link deliberately uses `Serial.dtr()` rather than `bool(Serial)`: Arduino AVR core 1.8.6 inserts a 10 ms delay in `Serial_::operator bool()`. Reading DTR directly keeps the diagnostic path from adding that pause to every key-scan cycle. The host protocol test deletes the implicit bool conversion so this blocking pattern cannot be reintroduced into the shared protocol implementation unnoticed.

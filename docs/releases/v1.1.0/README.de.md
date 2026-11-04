> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot Firmware kann jetzt direkt aus der App auf dem DIY Pro Micro Macropad installiert werden.

- Neu **Firmware du macropad…** Dialog mit Geräteauswahl und -bestätigung vor dem Blinken.
- Gebündelte Firmware **1.0.0** bewahrt F13-F24 und fügt eine DeckPilot Identitäts-/Versionsantwort über USB seriell.
- Gebündelte AVRDUDE: keine Arduino IDE-Installation für normale Updates erforderlich.
- Validierte Firmware-Prüfsumme und Speichergrenzen, Übereinstimmung der physischen USB-Verbindung, Schreibverifizierung und Überprüfung der Post-Flash-Version.
- Bestehende PC-Profile und die Kalibrierung bleiben erhalten. Die Aktualisierung der App blinkt niemals automatisch auf die Hardware.

**Erster Hardware-Test noch erforderlich:** die ursprüngliche Firmware-Upload-Prozedur wird bestätigt; die neue Firmware/Updater muss noch auf dem eigentlichen Makropad ausprobiert werden. **ATmega32U4 5 V / 16 MHz**, verdrahtet auf **2–10, 14, 15, 16**, nur mit Leonardo USB IDs und Caterina Bootloader. Behalten Sie die Originalskizze und den RST/GND-Zugang für die Wiederherstellung. Keine Firmware-Backup oder automatisches Rollback.

Windows 10/11 x64. Download **DeckPilot.exe** oder **DeckPilot-win-x64.zip**Die Firmware/Tools sind in die App eingebettet und auch in **DeckPilot-firmware.zip**AVRDUDE verfügt über eine eigene GPL-Lizenz und eine gebündelte Quelle.

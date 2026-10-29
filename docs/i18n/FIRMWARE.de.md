> Hierbei handelt es sich um eine automatische Übersetzung der kompletten Firmware-Dokumentation. Das englische Original ist die Referenz. Befehle und Identifikatoren bleiben exakt erhalten.

<a id="deckpilot-firmware"></a>
# DeckPilot Firmware

Nur für die getestete Hardware-Konfiguration: **Pro Micro ATmega32U4 5 V / 16 MHz**, unter Verwendung der **Arduino Leonardo** Boardprofil und ein **Caterina / AVR109** USB-C beschreibt den Stecker, nicht die Board-Kompatibilität. Die ursprüngliche Skizze des Besitzers wurde erfolgreich mit dem 1200-baud reset und AVR109 bei 57600 baud. Der Besitzer hat auch erfolgreiches In-App-Flashing, Firmware bestätigt 1.0.0 Identifizierung und Funktionsweise aller 12 Knöpfe auf dieser Referenztafel.

<a id="wiring-and-behavior"></a>
## Verdrahtung und Verhalten

Jeder Schalter verbindet einen Pin mit GND. Interne Klimmzüge sind aktiviert. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** Senden **F13 bis F24** in dieser Reihenfolge. Eine stabile 30 ms debounce behandelt das Drücken und Loslassen unabhängig. Tastaturverhalten, Windows-seitige Profile und Kalibrierung bleiben kompatibel; USB-Tastaturrollover bleibt durch Arduinos Tastaturbibliothek eingeschränkt. Die Firmware führt noch keine USB-Modi per Taste oder native Mediensteuerung ein.

Stift **1** Der USB-Kern kann weiterhin den Bootloader-Eintrag unterstützen. Schließen Sie VCC nicht an GND an.

Die aktuelle USB CDC Serial Interface Antworten `DECKPILOT?\n` bei 115200 Baud mit:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

Für den normalen Tastaturbetrieb ist keine serielle Verbindung erforderlich. Die Identität ist eine Kompatibilitätsprüfung, keine kryptographische Hardware-Authentifizierung. Windows meldet weiterhin die Arduino Leonardo USB-Kennungen; viele andere Boards teilen diese Kennungen.

<a id="install-from-deckpilot"></a>
## Installieren Sie von DeckPilot

1. Halten Sie die gelieferte Recovery-Skizze ()`firmware/Original/Original.ino`) und ein funktionierendes Arduino-Upload-Setup als Wiederherstellungsoption. RST und GND physisch zugänglich halten.
2. Arduino IDE, Serial Monitor und andere serielle Tools schließen, andere Leonardo-Boards trennen.
3. Offen **Firmware du macropad…**, wählen Sie das richtige Gerät und bestätigen Sie die Platine und die Verdrahtung. eine nicht identifizierte Originalskizze kann nur nach ausdrücklicher Bestätigung installiert werden.
4. Bestätigen Sie die Installation und lassen Sie das USB-Kabel bis zum Abschluss angeschlossen. DeckPilot während der Firmware-Dialog geöffnet ist; gewöhnliche F13-F24-Ereignisse erreichen immer noch Windows.

Die Firmware und Windows AVRDUDE 8.0 werden in der veröffentlichten App gebündelt. Keine Arduino IDE oder separate Treiberinstallation wird durchgeführt von DeckPilot. Windows muss bereits den seriellen USB-Anschluss des Boards freilegen, wie es beim erfolgreichen Upload des Besitzers der Fall war. Neue Firmware-Versionen werden mit App-Updates von GitHub geliefert; die Aktualisierung der App funktioniert **nicht** automatisches Flashen von Hardware. Jeder Blitz muss bestätigt werden.

DeckPilot prüft SHA-256Intel HEX zeichnet Prüfsummen, Reset-Vektor-Präsenz, Überlappungen und die **28672-byte** Anwendungsgrenze vor dem Reset. Es entspricht dem physischen USB-Standort über Laufzeit- und Bootloader-Ports, verwendet AVR109 bei 57600 baud, erfordert die Schreibverifizierung von AVRDUDE, überprüft dann die Identität und Version der laufenden Firmware. Es werden keine Sicherungen, EEPROM oder der Bootloader geschrieben. `2341:8036` / `2A03:8036` und Caterina `2341:0036` / `2A03:0036` Wenn Windows keine passende physische Verbindung bereitstellen kann, wird das Blinken blockiert, anstatt einen Port zu erraten.

Blinken ist nicht atomar: Eine Kabeltrennung oder ein Stromausfall kann die Firmware der Anwendung unvollständig machen. Es gibt keine automatische Firmware-Backup oder Rollback. **Arduino Leonardo**. Je nach Bootloader des Boards kann ein kurzzeitiges zweimaliges Anschließen von RST an GND einen temporären Bootloader-Port freilegen; Uploaden während dieser Port verfügbar ist. Ein beschädigter Bootloader benötigt ISP-Hardware und kann nicht mit diesem USB-Updater repariert werden. Schreiben Sie kein anderes Board oder eine andere Clock-Variante.

Das letzte abgeschlossene AVRDUDE-Prozessprotokoll wird gespeichert unter `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## Bauwerk

Unter Windows installieren Sie von der Repository-Root Arduino CLI **1.2.2** und .NET SDK 8, dann laufen:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

Das Skript Pins Arduino AVR Kern **1.8.6**, Tastatur **1.0.6**, und vorgelagerte AVRDUDE **8.0**CLI-verwaltete Pakete verwenden die Paketprüfsummenvalidierung von Arduino; AVRDUDE-Binär- / Quellarchive haben explizit SHA-256 Pins im Drehbuch. `artifacts/firmware/DeckPilot-firmware.zip` enthält das HEX, Manifest, Windows-Tool, Lizenz und das entsprechende vorgelagerte AVRDUDE-Quellarchiv. Publishing CI benötigt das Bundle; ein gewöhnlicher Entwickler Build ohne es kann die App ausführen, kann aber nicht blinken.

Das Original 12-Button Skizze wurde von David Madison angepasst **Stream Billig** Projekt, Copyright 2018, MIT. Seine Mitteilung bleibt in `DeckPilot.ino`. DeckPilot Additionen verwenden die Repository MIT-Lizenz. **AVRDUDE ist eine separate Software unter GPL-2.0-or-later**, mit seiner Mitteilung / Lizenz und Quelle gebündelt; das Repository MIT-Lizenz ersetzt diese Lizenz nicht. https://github.com/avrdudes/avrdude/tree/v8.0 für vorgelagerte Bauanweisungen und Abhängigkeitsmeldungen.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 und USB-Protokoll 2

Firmware 1.1.1 fügt einen schreibgeschützten Diagnoselink hinzu, während das gleiche F13-F24-Tastatur-Mapping und -Pin beibehalten wird1 Startup Failsafe. **Der physische Test des Besitzers deckte die Firmware ab 1.0.0Firmware 1.1.1 hat automatisierte Protokolltests und einen AVR-Build, benötigt aber immer noch seinen ersten Test auf der eigentlichen Platine.** Profile und Kalibrierung bleiben auf dem PC und müssen für dieses Firmware-Update nicht neu erstellt werden.

Die neue App identifiziert die angeschlossene Karte, hält ihren seriellen Port offen, verbindet sich nach dem Ausstecken wieder und überprüft ihre Firmware-Version beim Start und nach der Wiederverbindung. Firmware-Updates werden mit App-Releases geliefert: Start-Checks vergleichen die angeschlossene Firmware mit dem validierten Bundle in der installierten App. App-Update-Checks erhalten immer noch neuere Releases von GitHub. Es gibt keinen separaten Firmware-Download-Kanal.

Eine ältere erkannte Firmware fordert einmal pro App-Sitzung mit einem 24Benachrichtigungen können deaktiviert werden unter: **Connexion et diagnostic**Unbekannte Arduino-Skizzen, getrennte Geräte und mehrdeutige Mehrkartenauswahlen lösen keine automatischen Blink- oder Aktualisierungsaufforderungen aus. Die Firmware-Installation erfordert immer eine Bestätigung. Das Öffnen des Firmware-Managers setzt den Monitor aus und wartet auf die Freigabe des seriellen Ports; die Überwachung wird nach dem Schließen des Dialogs fortgesetzt.

**Für einen manuellen Upload von Arduino IDE, beenden DeckPilot von seinem Tray-Icon zuerst.** Das Schließen des Hauptfensters verbirgt es nur, so dass es weiterhin die serielle Verbindung besitzt. DeckPilot Wenn ein anderes Tool den Port bereits besitzt, meldet die App das Problem und versucht es erneut, ohne die normalen Tastaturtasten zu beeinträchtigen.

Die neue `DeckPilotProtocol.h` ist die gleiche Umsetzung, die von `tests/firmware-protocol.cpp`. Es verwendet Puffer in fester Größe, begrenzte Eingabeverarbeitung und verfügbaren USB-Übertragungsraum; das Scannen von Tasten wird nie absichtlich verzögert, während auf einen Host gewartet wird. Die letzte ausstehende Tastenmaske wird unter Gegendruck zusammengeführt. Es gibt keine Befehle zum Ausführen von Makros, zum Schreiben von EEPROM, zum Deaktivieren der Tastatur, zum Ändern von Profilen oder zur Flash-Firmware über dieses Diagnoseprotokoll.

im 115200 Baud, Zeilen enden mit LF (CRLF wird akzeptiert). Session-Token enthalten genau acht große Hexadezimalzeichen. Alle Zahlen sind unsigniert dezimal. Beispiele:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` Werbung 12 Schlüssel und Fähigkeitsbits: 1 = Herzschlag/Zustands-Schnappschuss, 2 = Key-State Events. `PONG` Echos des Anforderungszählers, berichtet dann die Verfügbarkeit in Millisekunden, die Zustandssequenz und die 12-Bit Button Maske. `KEYS` Meldesequenz und Maske; Bit 0 ist F13 und bit 11 F24 ist, unabhängig von der physikalischen Kalibrierung, Sitzungen und Anforderungszähler lehnen veraltete Antworten ab. 32Schlüssel bleiben nur Diagnosedaten: App-Aktionen laufen immer noch einmal durch den vorhandenen Tastaturpfad, so dass ein serielles Ereignis dieselbe Aktion nicht zweimal ausführen kann.

Heartbeat-Anfragen werden normalerweise alle zwei Sekunden gesendet. Die App verbindet sich wieder, wenn eine passende Antwort fehlt. 1.5 Sekunden; die Firmware stoppt die unerwünschte Diagnose nach sechs Sekunden ohne Herzschlag oder wenn die serielle Verbindung geschlossen wird. Firmware 1.0.0 bleibt durch die Legacy-Identitätsabfrage unterstützt, ohne Live-Key-Diagnose. DeckPilot Apps können immer noch die Legacy-Identität der Firmware lesen 1.1.1.

Die Quellversion in `DeckPilotProtocol.h` liefert die Release-Manifest-Version. Veröffentlichte Firmware-Bundles enthalten nun auch die aktuelle Skizze/Header unter `source/DeckPilot`, die Recovery Skizze und die DeckPilot MIT-Lizenz.

Der Diagnoselink nutzt bewusst `Serial.dtr()` eher als `bool(Serial)`: Arduino AVR Kern 1.8.6 Einsätze a 10 ms Verzögerung in `Serial_::operator bool()`Das Lesen von DTR verhindert direkt, dass der Diagnosepfad diese Pause zu jedem Key-Scan-Zyklus hinzufügt. Der Host-Protokolltest löscht die implizite Bool-Konvertierung, so dass dieses Blockierungsmuster nicht unbemerkt in die Implementierung des gemeinsamen Protokolls eingeführt werden kann.

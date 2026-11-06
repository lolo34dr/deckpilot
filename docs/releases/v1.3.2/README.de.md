> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 1.3.2 verbessert die USB-Verbindung und prüft die Macropad-Firmware beim Start.

- Verbindungs-Dashboard mit automatischer Wiederverbindung, erkannter Firmware, Live-Button-Zustand/USB-Diagnose und expliziter Board-Auswahl, wenn mehrere Geräte vorhanden sind.
- Startup-Firmware prüft und aktualisiert Vorschläge, mit einem 24- Stunde Erinnerungsverzögerung und eine Option zum Deaktivieren von Firmware-Benachrichtigungen.
- Neue Firmware **1.1.1**: Bounded Protocol Messages, Fresh Session Tokens, Correled Heartbeat Responses und Key-State Events Die F13-F24-Mapping bleibt unverändert, einschließlich Standalone-Nutzung.
- Der Firmware-Manager gibt den seriellen Port des Monitors automatisch frei, bevor er identifiziert/blinkt, und verbindet sich danach wieder. 1.0.0 Legacy Identity Queries werden weiterhin unterstützt.
- Profile können aus dem Tray-Menü ausgewählt werden; Ctrl+S speichert Änderungen; Verbindungsdiagnosen können zur Unterstützung kopiert werden.

Firmware-Updates werden in App-Releases geliefert; Startup-Checks vergleichen die erkannte Version mit dem mitgelieferten Bundle. Keine Hardware wird lautlos geblinkt. **Firmware 1.1.1 Es muss noch ein erster Physical-Board-Test durchgeführt werden:** Firmware 1.0.0 und das frühere Flash-Verfahren wurden vom Eigentümer validiert, während dieses neue Protokoll durch Host-Tests und AVR-Compilation abgedeckt wird.

Download **DeckPilotSetup.exe** oder Update aus der App. Profile, Kalibrierung und Startpräferenzen bleiben erhalten. Beenden DeckPilot vor der Verwendung von Arduino IDE oder eines anderen seriellen Monitors; das Öffnen des In-App-Firmware-Managers löst die Verbindung automatisch aus.

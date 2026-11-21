> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.1.6 und DeckPilot Atelier 1.0.12 Machen Sie die Löschung der Firmware von Atelier explizit und wiederherstellbar.

- Verschieben Sie die destruktive Firmware-Löschung in zusammengebrochene erweiterte Operationen und erfordern eine Eingabe `EFFACER` vor dem Schreiben.
- Erklären Sie, dass das Board nach dem Löschen nicht mehr als Tastatur aufgezählt wird, zeigen Sie die genauen RST / GND-Double-Reset-Wiederherstellungsschritte an und halten Sie die Wiederherstellungsaktion von Atelier nach dem Löschen oder einem unterbrochenen Blitz verfügbar.
- Firmware bleibt bestehen 1.1.1; nein DeckPilot Hardware-Firmware-Änderungen sind erforderlich.

Die Validierung umfasst Firmware- und App-Checks sowie die Windows-UI-Automatisierung für die erweiterte Löschsteuerung, keine Gerätesicherheit und RST / GND-Wiederherstellungshinweise.

> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 1.6.1 Behebt das weiche Tastatur-Rendering und fügt Profilwiederherstellung, Windows-Audio-Steuerelemente und kurze / lange Drücke hinzu.

- Native-Size-Tastatur-Text: Ganzdeck-Skalierung und Content-Shadow-Effekte entfernt, DPI-Awareness pro Monitor und größere Desktop-App-Symbole hinzugefügt. Kleine Fenster scrollen statt schrumpfender Tasten.
- Undo / Redo für Profilbearbeitungen, einschließlich Drag-and-Drop ()50 Änderungen pro App-Sitzung); Textfelder behalten native Ctrl+Z/Ctrl+Y.
- Profil Import/Export und 20 automatische Backups der vorherigen Version; Importe fügen neue Kopien hinzu; Wiederherstellung und Rückgängigmachung der Kalibrierung; Importierte Aktionen werden niemals automatisch ausgeführt.
- Globales Windows-Mikrofon schaltet mit einer permanenten Statusleistenanzeige und einem Live-Endpunktstatus um.
- Wechseln Sie zwischen zwei ausgewählten verbundenen Ausgabegeräten für alle Windows-Standard-Audiorollen. Fehlende Geräte versagen, ohne stillschweigend einen anderen Ausgang auszuwählen.
- Zwei konfigurierte Aktionen pro Schlüssel mit einer 200–3000 ms-Schwellenwert. Kurze Läufe bei Veröffentlichung, lange ersetzt kurze und läuft einmal. Bestehende sofortige Aktionen und freie F13-F24-Tasten behalten ihr Verhalten bei. Kein Firmware-Flash erforderlich.

Download **DeckPilotSetup.exe** oder Update aus der App. Profile und Kalibrierung bleiben erhalten. Audio-Endpunktauswahlen müssen möglicherweise nach dem Importieren von Profilen auf einem anderen PC angepasst werden. Windows-Endpunktstatus unterscheidet sich vom anwendungseigenen Mute- oder Hardware-Stummschalter; Fixed-Output-Anwendungen folgen möglicherweise nicht den Standard-Ausgabeänderungen.

Validierung: Kernprofil-/Geste-/Audiotests (Audioendpunkte simuliert), Windows-Installationsprogramm-/Update-/UI-Tests, native Maus-Drag-undo-/Redo- und Long-Action-Bearbeitung; physische Schlüsselspeicher, tatsächliche Mikrofontreiber und Headset-/Lautsprecher-Schaltungen müssen noch auf der Hardware des Besitzers bestätigt werden.

> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 1.7.0 Reduziert wiederholte Arbeit beim Bearbeiten von Profilen, beim Finden von Anwendungen und beim Ausführen im Hintergrund.

- Durch das Speichern unveränderter Einstellungen wird die Datei nicht mehr ersetzt oder Backups erstellt. Tatsächliche Festplatteninhalte werden weiterhin überprüft, sodass externe Bearbeitungen, Löschungen und fehlgeschlagene Speicher nicht ausgeblendet werden.
- Profilverlauf hält losgelöste Modell-Snapshots. Unveränderte Bearbeitungen und Rückgängig-/Wiederholung vermeiden JSON-Roundtrips; Action-Editing verwendet Wertevergleiche anstelle von wiederholter Serialisierung.
- Anwendungssuche und Picker teilen sich einen fünfminütigen Katalog-Cache und eine Discovery-Task. Discovery läuft außerhalb von WPF; Refresh erzwingt immer noch einen neuen Scan. Jeder Picker besitzt seinen Filter und hält aktuelle Ergebnisse während der Aktualisierung nutzbar.
- Ersetzen Sie die permanente 100 ms Board UI Poll mit bestellten Verbindungsbenachrichtigungen. Keyboard-Hook Callbacks Warteschlangen für visuelle Arbeit, anstatt zwölf Tasten synchron zu rendern.
- Audiostatus-Umfragen alle zwei Sekunden im sichtbaren Zustand und alle zehn Sekunden im verborgenen/minimierten Zustand, sofort beim erneuten Öffnen und nach Audiobefehlen aktualisierend. Überlappende Aktualisierungsanforderungen werden zusammengeführt, ohne das Update nach dem Befehl fallen zu lassen.
- Versteckte Fenster stoppen visuelle Feedback-Timer und überspringen kosmetische Rendering-Wartezeiten. Gefrorene UI-Bürsten werden wiederverwendet und gestartete Anwendungsprozess-Wrapper werden entsorgt.

Download **DeckPilotSetup.exe** oder Update aus der App. Profile, Kalibrierungs- und Starteinstellungen bleiben erhalten. Es ist kein Firmware-Flash erforderlich.

Die Validierung umfasst Einstellungen/Historie/Cache-Regressionen und Windows-Installation, Update, Picker, Drag-and-Drop, Kurz-/Lang-Action-Bearbeitung und Minimierung/Wiederherstellungsprüfungen.

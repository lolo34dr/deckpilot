> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 1.6.4 Behebt den Ersatz von Suchergebnissen während des Drag-and-Drop und stabilisiert die Windows-Verifizierung.

- Behalten Sie vorhandene Suchschaltflächen bei, wenn der Debounce-Timer oder der Installed-App-Katalog die gleichen Ergebnisse liefert, so dass eine Drag-Quelle nicht unerwartet getrennt wird.
- Verschieben Sie den Ersatz des Suchergebnisses, während eine Mausinteraktion oder ein OLE-Ziehvorgang im Gange ist.
- Windows UI überprüft die Verwendung von physischen Cursorkoordinaten, Fokussteuerungen, bevor sie gemessen werden, und scannt den gesamten Scrollbereich, anstatt eine feste Anzahl von Zeileninkrementen anzunehmen. Native Maus-Ziehungen bleiben erforderlich; fehlgeschlagene Zuweisungen werden nicht wiederholt, um einen Durchlauf zu erhalten.
- Windows-Einstellungen speichern die Verwendung der dedizierten Atomersatz-API, so dass ein vorhandener Lesegerät seinen alten Snapshot beibehalten kann, ohne den nächsten Speicher zu blockieren. Eine Kernregression überprüft drei aufeinanderfolgende Ersetzungen mit einem freigegebenen Lesegerät.
- Einstellungen Leser in Windows-Tests verwenden UTF-8 und Atomdatei-Ersatz erlauben. Snapshot-Öffnung wartet bis 500 ms nur für Windows-Sharing-/Lock-Verstöße während des Austauschs; andere Lesefehler und persistente Sperren bestehen nicht im Test. Drei Rückgängig-/Redo-Zyklen halten auch einen alten Snapshot offen, um zu überprüfen, ob das Speichern erfolgreich ist, ohne bestehende Leser zu ändern.
- Hintergrund-Startup-Checks warten, bis WPF sein Fenster vollständig versteckt hat, und erfordern dann, dass der Prozess ohne sichtbares Hauptfenster weiterläuft.
- Aktualisieren Sie GitHub-Aktionen auf Node.js 24 Versionen und installieren Sie das gleiche Arduino CLI 1.2.2 aus seinem checksummenverifizierten Upstream-Archiv.

Download **DeckPilotSetup.exe** oder Update aus der App. Profile und Kalibrierung bleiben erhalten. Es wird kein Firmware-Flash benötigt.

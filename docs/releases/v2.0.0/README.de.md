> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.0.0 führt eine tiefschwarze Oberfläche mit gespeichertem Hintergrund, Panel und Akzentfarben ein. Die neue Resonanzwand akzeptiert MP3/WAV-Clips, lässt Benutzer ein physisches Mikrofon und einen virtuellen Kabelausgang auswählen und mischt Stimme mit Clips für die Verwendung als Mikrofon in Spielen oder Anrufen. Clips können von der App aus gestartet oder einer Makropad-Taste zugewiesen werden. Ein virtuelles Audiokabel wie VB-CABLE muss separat unter Windows installiert werden; Atelier bleibt in der Version 1.0.10.

DeckPilot 1.9.11 und DeckPilot Atelier 1.0.10 Hinzufügen von Pre-Flash-Hardware- und Firmware-Diagnose, eines Firmware-Versionsvergleichs, einer geführten Wiederherstellungsaktion nach unterbrochenem Schreiben, sequentiellen Multi-Board-Flashing mit einem Batch-Bericht und einer physikalischen Position-zu-F13-F24-Layout-Check in jedem Key-Control-Bericht.

DeckPilot 1.9.11 enthält das gleiche Atelier-Update in der freigegebenen Installationsversion.

DeckPilot 1.9.10 stellt freigegebene Releases wieder her, die beides enthalten DeckPilot und DeckPilot Atelier 1.0.9Beide Anwendungen überprüfen erneut die gleiche neueste GitHub-Version.

Wenn Sie Atelier installiert haben 1.0.8 aus dem separaten Release-Kanal, installieren DeckPilot-Atelier-Setup.exe von diesem Release einmal zu freigegebenen Updates zurückzukehren. DeckPilot Sie können den vorhandenen Updater verwenden.

DeckPilot Atelier 1.0.7 fügt eine bestätigte Programmbereichs-Löschung hinzu, die alle löscht 28 KiB des Benutzers blinkt, hinterlässt eine sichere Reset-Schleife und bewahrt Caterina und EEPROM. Reset bleibt deaktiviert, bis eine physische Platine ausgewählt ist.

DeckPilot 1.9.7 Behebt die Deinstallation von Atelier, während die App geöffnet ist. 1.0.6 korrigiert auch sein Windows DPI-Manifest, das das Starten der installierten App verhinderte.

DeckPilot 1.9.6 Behebt das Windows DPI-Manifest, das verhindert wurde DeckPilot Atelier vom Öffnen nach der Installation.

DeckPilot 1.9.5 enthält eine detaillierte Atelier-Startdiagnose im Windows-Release-Check.

DeckPilot 1.9.4 verbessert den Atelier Windows-Startrauchtest, um Details zum Anwendungsabsturz zu melden.

DeckPilot 1.9.3 behebt die Atelier-Build-Metadaten, um das Firmware-Manifest aus dem gebündelten Firmware-Archiv zu lesen.

DeckPilot 1.9.2 Behebt den Schritt der Atelier-Release-Verpackung und verbessert die Build-Diagnose.

DeckPilot 1.9.1 verbessert das Atelier Installer-Startup und fügt automatische App- und Firmware-Update-Checks hinzu.

- Der Installer startet Atelier in der angemeldeten Windows-Benutzersitzung und stoppt/startet es sicher für Upgrades.
- Atelier prüft GitHub beim Start und auf Abruf. Es kündigt neuere Atelier-Builds oder gebündelte Firmware an und lädt dann ein Installationsprogramm herunter, das mit dem veröffentlichten SHA-Test verifiziert wurde.256 Datei.
- Firmware wird erst geblinkt, nachdem der Benutzer die Flash-Aktion ausgewählt hat; ein App-Update schreibt niemals selbst auf ein angeschlossenes Board.

Das Atelier-Installationsprogramm wird auch in der angemeldeten Windows-Sitzung des Benutzers nach einer Neuinstallation gestartet. Der Release-Workflow öffnet nun die installierte App, überprüft dann das Upgrade und die Deinstallation, während sie ausgeführt wird.

DeckPilot 1.9.0 Zusätze DeckPilot Atelier, eine separate Windows-Workshop-App zum Flashen neuer Makropads und zum Überprüfen aller 12 Schlüssel.

- Installieren Sie die mitgelieferte Firmware auf Leonardo / Pro Micro ATmega32U4 Boards mit dem Caterina Bootloader.
- Führen Sie geführte Kurz- und Langdruckprüfungen für jeden physischen Schlüssel aus, notieren Sie die F13-F24-Zuordnung und speichern Sie einen Qualitätskontrollbericht.
- Download **DeckPilot-Atelier-Setup.exe** aus diesem Release. Das Original DeckPilot Setup und Firmware bleiben separat verfügbar.

DeckPilot 1.8.0 fügt einen aktualisierten Arbeitsbereich und schnellere Möglichkeiten zum Erstellen und Verwenden Ihrer Profile hinzu.

- Klarere Holzkohle/Tee-Schnittstelle, Aktionsbibliothek zuerst, knackig 3 × 4 Tasten, Akzentfarben und Reduzierte Bewegungspräferenz.
- Drei editierbare Profilvorlagen: Musik und Audio, Desktop und Produktivität und Erstellung.
- Profilaktionen: Wechseln Sie von einem Schlüssel zu einem benannten Profil, dem nächsten oder dem vorherigen Profil. Importierte Profilsätze behalten ihre internen Links bei.
- Windows-Aktionen: Öffnen Sie allgemeine Einstellungen oder Task-Manager oder sperren Sie den PC explizit.
- Kopieren / Schneiden / Einfügen / Zurücksetzen ganzer Tasten, einschließlich langer Drücke und Farben, mit Undo / Redo und einer App-lokalen Zwischenablage.
- Erfassen Sie eine Tastenkombination direkt; Escape bricht ab und Makropad-Tasten bleiben während der Aufnahme reserviert.
- Ctrl+K-Action-Suche und eine übersichtliche, Session-only-Historie der letzten 30 Aktionen.
- Beheben Sie die automatische Firmware-Eingabeaufforderung, so dass sie den Firmware-Manager öffnet. Ausstehende Gesten werden bei Profiländerungen abgebrochen.

Download **DeckPilotSetup.exe** oder Update aus der App. Bestehende Profile, Kalibrierungs- und Startpräferenzen bleiben erhalten. Firmware bleibt erhalten **1.1.1**Für diese Features ist kein Flash erforderlich.

Die Validierung umfasst Kernkompatibilität / Navigation / Import / Vorlagenprüfungen und native Windows-Installation, Update, Picker, Drag-and-Drop, Undo / Redo, Vorlagenerstellung, Profilaktionen, Verknüpfungserfassung, Clipboard-Bearbeitung, Layout und Minimierung / Wiederherstellung von Überprüfungen. Das Verhalten von Macropads und Audiogeräten hängt immer noch von der Windows-Hardware des Besitzers ab.

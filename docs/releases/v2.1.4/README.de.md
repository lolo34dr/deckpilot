> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.1.4 startet das konfigurierte virtuelle Mikrofon automatisch mit der App, einschließlich des Hintergrundstarts nach der Windows-Anmeldung.

- Der automatische Start des Resonanzbodenmikrofons ist standardmäßig für bestehende und neue Einstellungen aktiviert. VB-CABLE; keine manuelle Aktivierung nach dem Neustart des PC erforderlich ist, wenn DeckPilot Beginnt mit Windows.
- Wenn die Geräte beim Anmelden nicht bereit sind oder das Audio nach einer Trennung stoppt, DeckPilot wiederholt alle fünf Sekunden, während die automatische Aktivierung aktiviert ist.
- Ein expliziter Stop deaktiviert und speichert die Automatik-Start-Präferenz; aktivieren Sie sie erneut aus dem Soundboard Schaltfläche oder Aktivierungsschaltfläche. Audioclips werden nie automatisch abgespielt.
- Atelier bleibt bestehen 1.0.11 Firmware bleibt bestehen 1.1.1.

Die Validierung umfasst Einstellungen Migration / Round-Trip-Checks und eine native Windows-Benutzeroberflächenüberprüfung für die anhaltende automatische Starteinstellung sowie die vorhandenen Audio- und Installationsüberprüfungen. Der Start / die Wiederverbindung des physischen Geräts hängt immer noch von der Hardware des Besitzers ab.

DeckPilot 2.1.3 Reduziert die Resonanzbodenmikrofonlatenz und entfernt die 1-Sekunden-Konvertierungsblöcke aus dem Live-Audiopfad.

- Verwenden Sie Output-driven, Small-Block-Resampling für das Mikrofon und Sound-Clips; Matching Sample Rates Bypass Resampling vollständig.
- Request ereignisgesteuert 20 ms-Erfassung und 40 ms-Wiedergabepuffer anstelle der vorherigen 100 ms-Einstellungen. Die tatsächliche Latenz hängt von Windows, den Audiogeräten und VB-CABLE.
- Gebundene Mikrofonpufferung 100 Kurze Unterläufe fügen nur die Stille hinzu, die von der aktuellen Lektüre gefordert wird.
- Unterstützen Sie Mono-, Stereo-, Multichannel- und Windows-erweiterbare PCM/Float-Eingänge ohne die Media Foundation-Einsekunden-Lesevorgabe.
- Discord-Verarbeitung und Routing unverändert lassen. Atelier bleibt 1.0.11 Firmware bleibt bestehen 1.1.1.

Die Validierung umfasst Regressionsüberprüfungen für das Resampling von Blockgrößen, Kanalkonvertierung, erweiterbare Formate, Underrun-Wiederherstellung, Überlauffrische, gleichzeitige Aufnahme/Wiedergabe und Clip-Vervollständigung neben den vorhandenen Windows-Installationsprogramm- und Anwendungstests. Es wird keine Hardware-Latenzzahl beansprucht; Überprüfen Sie die Verbesserung mit Ihrem Mikrofon und Audiotreiber.

DeckPilot 2.1.2 und DeckPilot Atelier 1.0.11 fixe Werkstattsicherheit, Resonanzbodenzuverlässigkeit und tragbare Profilgeräusche

- Atelier kann Leonardo / Caterina Boards vorbereiten, die noch nicht antworten DeckPilot Protokoll; Serienfehler hören immer noch auf zu blinken und der USB-Speicherort, der Bootloader und die MCU-Signatur bleiben verifiziert.
- Jede Inspektion beginnt mit leeren Ergebnissen. Berichte behalten die getestete Platine und Firmware bei, und veraltete USB-Rückrufe können eine neue Sitzung nicht beeinflussen.
- Das Schließen des Ateliers während des Schreibens oder der Überprüfung der Firmware wird blockiert.
- Profilexporte und automatische Profil-Backups können referenzierte MP3/WAV-Dateien beibehalten.32 MiB-Audiolimit: Importiert das Kopieren eingebetteter Clips in einen verwalteten Ordner und Remap-Referenzen für kurze/lange Aktionen. Legacy-Profilimporte werden weiterhin unterstützt; fehlendes Backup-Audio wird explizit gemeldet.
- Die Resonanzplatte veröffentlicht fertige Clips, aktualisiert Wiedergabeanzeigen und meldet unterbrochene Audiostreams. Umbenannt VB-CABLE Endpunkte werden durch ihre Adapteridentität erkannt.
- Korrekt übersetzte README-Links und Bewahren von technischen Gerätenamen während der Übersetzung. VB-CABLE Anforderung.

Validierung umfasst Kernregressionsüberprüfungen, Firmware-Protokolltests, Übersetzungstests, Windows-Build, DeckPilot und Atelier-Installations-/Upgrade-/Deinstallations-Prüfungen und native Schnittstellen-/Update-Prüfungen. Physisches First-Flash- und Audio-Disconnect-Verhalten erfordert weiterhin die Hardware des Besitzers. Firmware bleibt bestehen 1.1.1Es ist kein Firmware-Update erforderlich.

DeckPilot 2.1.1 Verbessert Installation und Updates:

- Automatische Überprüfung für VB-CABLE bei der ersten Installation und Upgrades, und laden Sie den offiziell signierten Installateur herunter und starten Sie ihn, wenn der Fahrer fehlt.
- Windows-Start, automatische Update-Checks und reduzierte Bewegungseinstellungen während der Einrichtung und Updates anzeigen, wobei vorhandene Optionen vorgewählt und erhalten bleiben.
- Behalten Sie das ausgewählte physische Mikrofon und fordern Sie die Mikrofoneinrichtung nur bei Bedarf an.
- Fix Installer Initialisierung und Erkennung von vorhandenen Installationen.

Download **DeckPilotSetup.exe** oder akzeptieren Sie das Update in DeckPilotWindows-Administratorgenehmigung erforderlich. Bestehende Profile und Kalibrierung bleiben erhalten. DeckPilot Atelier bleibt bei 1.0.10 Firmware bleibt bei 1.1.1Beide sind im Shared Release enthalten.

Validierung: Kernüberprüfungen und Windows Build, Installer-Upgrade/Deinstallierung und native Anwendungstests laufen vor der Veröffentlichung. Das Verhalten der Audio-Hardware erfordert weiterhin Tests auf dem PC des Benutzers.

DeckPilot 2.1.0 behält das tiefschwarze Interface und die anpassbaren Farben bei und leitet die Resonanzwand durch VB-Audio VB-CABLE Benutzer wählen nur ihr physisches mikrofon aus; der virtuelle wiedergabe-endpunkt ist behoben, so dass sprach- und mp3/wav-clips nicht versehentlich an lautsprecher gesendet werden können. DeckPilot Installateur kann das offizielle herunterladen VB-CABLE Paket, überprüfen Sie den Authenticode-Publisher und starten Sie das Treiber-Setup des Anbieters; das Paket wird nicht neu verteilt. CABLE Output Atelier bleibt in der Version 1.0.10.

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

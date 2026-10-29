> Hierbei handelt es sich um eine automatische Übersetzung des kompletten README. Das englische Original ist die Referenz.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot Banner" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="DeckPilot-Logo" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Ein Windows-Begleiter für Ihre 12-Key DIY Makropad.<br>
  Konfigurieren Sie Ihre Schlüssel, starten Sie Anwendungen, Steuermedien und wechseln Sie Profile.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Laden Sie die neueste Version herunter</a>
   · 
  <a href="#getting-started">Beginnen Sie</a>
   · 
  <a href="#development">Entwicklung</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt="Letzte Veröffentlichung"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt="Download für Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="MIT-Lizenz"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Windows Build Status"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt="GitHub veröffentlicht Downloads"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

---

DeckPilot konfiguriert Arduino Leonardo-kompatible Makropads, die bereits senden **F13-F24** als USB-Tastatureingänge, angeordnet in **3 Reihen von 4 Schlüssel**Die physische Schlüsselreihenfolge kann beliebig sein: Eine geführte Kalibrierung bildet jede Taste auf ihre Position ab.

**Es sind keine zusätzlichen Treiber- oder Firmware-Änderungen erforderlich.** Die Anwendungsoberfläche ist derzeit in Französisch. Wählen Sie oben eine Sprache für eine vollständige übersetzte README, einschließlich der technischen Referenz und Versionshistorie. Übersetzungen werden automatisch generiert; dieses englische Original ist die Referenz.

<a id="features"></a>
## Merkmale

- **Geführte Kalibrierung:** Kennzeichnen Sie alle 12 physische Tasten, ohne Ihr Makropad neu zu verkabeln.
- **Installierter Application Picker:** Suchen Sie nach Startmenükürzeln und dem Windows-Anwendungskatalog, einschließlich Microsoft Store-Einträgen. Die manuelle Dateiauswahl bleibt verfügbar.
- **Zollverfahren:** Anwendungen starten, Ordner oder Websites öffnen, Verknüpfungen senden und Medien steuern.
- **Freie Schlüssel:** Halten Sie F13-F24 für Verknüpfungen verfügbar, die direkt in einem Spiel oder einer anderen Anwendung konfiguriert sind.
- **Lokale Profile:** Profile erstellen, duplizieren, umbenennen, löschen und auswählen.
- **Fertige Profile:** Beginnen Sie mit Musik-, Produktivitäts- oder Erstellungsvorlagen und passen Sie sie dann an. Wechseln Sie von einem physischen Schlüssel zu einem bestimmten, nächsten oder vorherigen Profil.
- **Schnellere Bearbeitung:** Kopieren / Schneiden / Einfügen / Zurücksetzen vollständiger Tasten, Tastenkombinationen aufzeichnen, Akzentfarben auswählen und Änderungen rückgängig machen.
- **Windows Tools:** Öffnen Sie Sound, Display, Bluetooth- und Netzwerkeinstellungen, Task-Manager oder sperren Sie die Sitzung.
- **Kompaktes Dark Interface:** Vektorsymbole, Anwendungssymbole, sofern verfügbar, und subtile Animationen, die die Windows-Animationspräferenzen respektieren.
- **Hintergrundbetrieb:** Halten DeckPilot im Benachrichtigungsbereich, wobei der Start bei Windows-Anmeldung standardmäßig aktiviert ist (kann deaktiviert werden).
- **Automatische Updates:** Ein neues Release genehmigen, dann lassen Sie DeckPilot Herunterladen, Verifizieren, Ersetzen und Neustarten.

<a id="getting-started"></a>
## Beginnen Sie

<a id="requirements"></a>
### Anforderungen

- Windows 10 oder Windows 11, **x64**.
- Ein Arduino Leonardo kompatibles Makropad senden 12 Unterscheidbare Schlüssel von **F13 bis F24**.
- USB-Datenverbindung.

Die veröffentlichten Downloads beinhalten die .NET Runtime. **Sie müssen .NET nicht installieren, um es zu verwenden DeckPilot.**

<a id="install-and-launch"></a>
### Installation und Start

1. Download **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** und führen Sie das Installationsprogramm aus. Windows bittet um die Administratorberechtigung für die Installation in **Program Files\DeckPilot**.
2. Der Installateur fügt hinzu DeckPilot für Windows **Installierte Apps**, erstellt eine Startmenü-Verknüpfung und wählt standardmäßig die Desktop-Verknüpfung und die Windows-Startoptionen aus.
3. Verbinden Sie Ihr Makropad und öffnen DeckPilot Aus seiner Abkürzung.
4. Folgen Sie der Kalibrierung von links nach rechts quer **vier Spalten**, dann die nächste Reihe, für **drei Reihen**Jede Taste muss eine andere F13-F24-Taste senden.

Beim Upgrade von der ehemaligen 4-Zeilenlayout, DeckPilot bewahrt Profile/Aktionen und fordert eine neue Kalibrierung für die korrigierten Positionen an. `%APPDATA%\DeckPilot`Beenden Sie die alte Kopie, wenn das Setup Sie auffordert.

Ein tragbares [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) und [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) für den tragbaren Gebrauch die ausführbare Datei in einem beschreibbaren persönlichen Ordner aufbewahren.

Deinstallieren von Windows **Installierte Apps**Deinstallation entfernt das Programm und seine Verknüpfungen, während persönliche Profile beibehalten werden, damit eine spätere Neuinstallation sie wiederverwenden kann.

Die ausführbare Einzeldatei extrahiert ihre gebündelten Abhängigkeiten beim ersten Start in den .NET-Cache des Benutzers, was etwas länger dauern kann. Veröffentlichte ausführbare Dateien sind derzeit nicht signiert, sodass Windows möglicherweise eine SmartScreen-Warnung anzeigt.

<a id="configure-a-key"></a>
### Konfigurieren eines Schlüssels

1. Wählen Sie einen Schlüssel im Raster.
2. Wählen Sie eine Aktion in der rechten Seitenleiste.
3. Konfigurieren Sie die Einstellungen unterhalb des Rasters und speichern Sie. Durch das Wechseln von Tasten oder Profilen werden auch die Einstellungen des aktuellen Schlüssels gespeichert.

| Aktion | Verhalten |
| --- | --- |
| Freier Schlüssel | Führt F13-F24 normal durch und weist es direkt in Ihrem Spiel oder Ihrer Anwendung zu. |
| Antragstellung | Startet eine installierte Anwendung oder ein manuell ausgewähltes Programm oder eine Verknüpfung. |
| Ordner | Öffnet einen Ordner im Windows Explorer. |
| Website | Öffnet eine HTTP- oder HTTPS-Adresse in Ihrem Standardbrowser. |
| Tastenkürzel | Sendet eine Schlüsselkombination an das aktive Fenster. |
| Medien | Steuerelemente Wiedergabe, Tracks, Lautstärke oder Stummschaltung. |

Bei Anwendungsaktionen öffnen Sie den Anwendungswähler und suchen nach dem Namen. Die manuelle Dateiauswahl bleibt verfügbar, mit optionalen Programmargumenten unter erweiterten Optionen. Geben Sie Argumente entsprechend der Syntax der Zielanwendung ein.

Durch das Halten einer Taste wird eine konfigurierte Aktion nur einmal bis zur Veröffentlichung ausgelöst.Im Free-Key-Modus bleiben normale Wiederholungsereignisse auf der Tastatur verfügbar.

<a id="manage-profiles"></a>
### Profile verwalten

Öffnen Sie den Abschnitt Profilverwaltung in der rechten Seitenleiste, um ein Profil zu erstellen, zu duplizieren, umzubenennen oder zu löschen. Verwenden Sie den Selektor über dem Schlüsselraster, um das aktive Profil auszuwählen. Mindestens ein Profil muss verbleiben.

Die Kalibrierung gilt für alle Profile und kann von der Seitenleiste aus wiederholt werden.

<a id="background-operation"></a>
### Hintergrundbetrieb

Schließen des Fensters versteckt DeckPilot Doppelklicken Sie auf das Symbol für den Benachrichtigungsbereich in der Nähe der Uhr, um es wieder zu öffnen, oder klicken Sie mit der rechten Maustaste auf das Symbol und wählen Sie den Befehl beenden, um es vollständig zu stoppen.

Windows-Startup ist standardmäßig für den aktuellen Benutzer aktiviert. **Lancer avec Windows**; spätere App-Updates behalten diese Option bei. Installation und nachfolgende Starts aktualisieren einen aktivierten Eintrag zum aktuellen ausführbaren Pfad.

<a id="installed-applications"></a>
## Installierte Anwendungen

Der Picker liest Verknüpfungen aus den aktuellen Benutzer- und systemweiten Startmenüs und ergänzt sie dann mit `Get-StartApps` Ergebnisse aus dem Windows-Anwendungskatalog.

Die Suche ignoriert Fall und Akzente. Duplizierte Namen werden gruppiert, wobei Startmenüeinträge Vorrang haben. Portable Anwendungen und Programme ohne registrierte Verknüpfungen können immer noch manuell ausgewählt werden. Wenn der Windows-Katalog nicht verfügbar ist, bleiben lesbare Startmenüverknüpfungen verwendbar.

Windows startet die ausgewählten Verknüpfungen. Katalogeinträge verwenden `shell:AppsFolder`Optionale Argumente gelten für dateibasierte Programme und Verknüpfungen; sie werden für AppsFolder-Einträge ausgeblendet.

<a id="automatic-updates"></a>
## Automatische Updates

Verfügbar seit **0.2.0**Automatische Update-Checks laufen beim Start nach der Kalibrierung, dann alle sechs Stunden. Sie können in der Seitenleiste deaktiviert werden. Eine manuelle Überprüfung bleibt verfügbar.

Wenn eine neuere stabile GitHub-Version gefunden wird, DeckPilot fragt, ob aktualisiert werden soll. **Installierte Kopien** Download und SHA-256 Überprüfung `DeckPilot-Setup.exe`Windows bittet um Administratorberechtigung zum Aktualisieren von Programmdateien. Das Installationsprogramm wird geschlossen DeckPilot Wenn es fertig ist, behält es Profile und Starteinstellungen bei, installiert die neue Version und startet die App neu. Stornieren der Windows-Eingabeaufforderung lässt die App offen. Setup weigert sich, eine offene Firmware-Sitzung zu unterbrechen.

**Tragbare Kopien** den bestehenden Aktualisierungsmechanismus beibehalten; nach Bestätigung

1. Laden Sie die ausführbare Datei mit Fortschrittsberichterstattung und Stornierungsunterstützung herunter.
2. Überprüft seine SHA-256 Hash gegen `SHA256SUMS.txt` Aus dem gleichen GitHub-Release.
3. Speichert Ihre Einstellungen und bereitet den Update-Helfer vor.
4. Beendet, ersetzt die ausführbare Datei und startet neu.
5. Behält die vorherige ausführbare Datei, bis der neue Prozess die erfolgreiche Initialisierung bestätigt; wenn der Start fehlschlägt, wird die vorherige Version wiederhergestellt und neu gestartet.

Die Entscheidung, später zu aktualisieren, verschiebt automatische Aufforderungen für 24 Stunden. Manuelle Kontrollen umgehen diese Verschiebung.

Downloads verwenden HTTPS, wobei die Weiterleitungen auf die erwarteten GitHub-Hosts beschränkt sind. Temporäre Dateien werden in `%LOCALAPPDATA%\DeckPilot\Updates`; Downloads, die älter als sieben Tage sind, werden bereinigt. Der Windows-Starteintrag verwendet nach einem Update weiterhin den gleichen ausführbaren Pfad.

Portable Kopien erfordern einen beschreibbaren persönlichen Ordner. Installierte Kopien verwenden den Installer für geschützte Verzeichnisse; die DeckPilot Die App selbst läuft weiterhin ohne Administratorrechte.

**Upgrade von 0.1.x?** Beenden Sie die alte Version aus dem Benachrichtigungsbereich und ersetzen Sie die ausführbare Datei einmal manuell durch die neueste Version. Diese älteren Versionen enthalten den Updater nicht. DeckPilot.

<a id="repository-rename-compatibility"></a>
### Repository Umbenennungskompatibilität

Fassung **1.0.1** bereitet den Umzug von `lolo34dr/streamdeck-diy` bis `lolo34dr/deckpilot`. Das Repository ist jetzt `lolo34dr/deckpilot`. Aktualisierungsprüfungen verwenden den neuen Namen und behalten die Kompatibilität mit dem ursprünglichen Namen bei.

Verwendung **1.0.1 oder später** mit dem umbenannten Repository. 0.2.0 und 1.0.0 Validieren Sie die ursprünglichen Asset-Pfade streng und benötigen Sie möglicherweise ein manuelles Update. laden Sie die aktuelle ausführbare Datei von der Releases-Seite herunter, wenn eine ältere Version sich nicht selbst aktualisieren kann.

<a id="settings-and-backups"></a>
## Einstellungen und Backups

Profile und Kalibrierung werden außerhalb des Anwendungsordners gespeichert:

```text
%APPDATA%\DeckPilot\settings.json
```

Quittung DeckPilot vor dem Kopieren dieser Datei zum Sichern oder Wiederherstellen der Konfiguration, wenn eine Einstellungsdatei nicht gelesen werden kann, DeckPilot bewahrt eine Kopie mit einem `.backup-<date>` Suffix vor Verwendung einer neuen Konfiguration.

Es ist kein Konto erforderlich. Update prüft den Kontakt mit GitHub und kann in der Benutzeroberfläche deaktiviert werden.

<a id="development"></a>
## Entwicklung

Installieren Sie **.NET 8 SDK**, Version 8.0.415 oder eine neuere kompatible Version, die von `global.json`Visual Studio 2022 .NET Desktop-Entwicklung Workload ist optional.

Führen Sie diese Befehle aus der Repository-Root aus:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

Die Kernbibliothek und die Konsolenüberprüfungen laufen auch unter Linux. Die WPF-Schnittstelle und die Windows-Eingabe-APIs erfordern Windows.

<a id="validation"></a>
### Validierung

Die Konsole prüft den Ausgang mit einem Nicht-Null-Status bei der ersten fehlgeschlagenen Behauptung.

- Key Mapping, Hold-Key-Wiederholungen, Releases, Profilwechsel und Aktionskopieren.
- Einstellungen Validierung, Persistenz und Migration aus älteren Konfigurationen.
- Anwendungssuche und Zielhandling.
- Freigabeauswahl, Prüfsummen, gekürzte oder beschädigte Downloads, Umleitungen und Stornierungen.
- Ausführbarer Ersatz, Backup-Konservierung und Rollback.

Der File-Lock-Check läuft nur unter Windows.

GitHub-Aktionen laufen auch `tests/windows-update-smoke.ps1` Es startet den Update-Helfer, beendet den ursprünglichen Prozess, überprüft die Ersatz- und Startbestätigung, prüft die Einstellungskonservierung, öffnet dann den installierten Anwendungs-Picker über UI Automation und speichert eine Auswahl. Es führt auch umbenannte ausführbare Dateien und Pfade aus, die Leerzeichen enthalten. Dieses Skript ist dem Einweg-GitHub-Aktions-Testkonto vorbehalten. CI läuft auch `tests/windows-installer-smoke.ps1`: Installation von echten Programmdateien, Desktop-/Startmenükürzel, Standardstart, Upgrade mit geöffneter App, Erhaltung des deaktivierten Starts und Deinstallation ohne Löschen von Profilen.

<a id="publish-a-standalone-windows-folder"></a>
### Veröffentlichen Sie einen eigenständigen Windows-Ordner

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Verteilen Sie das komplette Archiv. Der Windows Build Workflow erzeugt auch dieses Paket.

<a id="publish-a-github-release"></a>
### Veröffentlichen Sie ein GitHub Release

Aktualisieren der Anwendungsversion und `.github/release-notes.md`, schieben Sie dann ein Matching `v*` Tag, wie z.B. `v1.0.0`. Der Release-Workflow überprüft, ob das Tag mit der Projektversion übereinstimmt, und führt die Validierung durch, bevor es veröffentlicht wird:

| Download | Inhalt |
| --- | --- |
| `DeckPilot.exe` | In sich geschlossene, einzelne Windows x64-Anwendung. |
| `DeckPilot-win-x64.zip` | Vollständiger, in sich geschlossener Anwendungsordner. |
| `SHA256SUMS.txt` | SHA-256 Hashes für beide Downloads. |

Für Windows ARM64, separat veröffentlichen mit `-r win-arm64` und validiert auf der Zielmaschine.

<a id="hardware-checks-on-windows"></a>
## Hardware-Checks bei Windows

Mit dem Leonardo verbunden:

- Alle kalibrieren 12 Positionen in einer beliebigen F13-F24-Ordnung: Bestätigen Sie, dass doppelte Schlüssel abgelehnt werden, und das Abbrechen behält die vorherige Zuordnung bei.
- Weisen Sie einen freien Schlüssel in einem Spiel zu und überprüfen Sie, ob das Spiel ihn normalerweise erhält.
- Starten Sie ein konfiguriertes Programm und halten Sie dann die Schaltfläche gedrückt: Es sollte einmal bis zum Release gestartet werden.
- Testen Sie einen Ordner, eine Website, eine Verknüpfung in einer gewöhnlichen Anwendung und Mediensteuerungen.
- Senden Sie F13-F24 durch eine Verknüpfungsaktion und bestätigen Sie, dass es keine Schleife von DeckPilot Aktionen.
- Erstellen Sie zwei Profile mit unterschiedlichen Aktionen, wechseln Sie zwischen ihnen, beenden Sie und starten Sie neu. Profile und Kalibrierung sollten bestehen bleiben.
- Schließen Sie das Fenster, lösen Sie eine Aktion aus, öffnen Sie sie erneut und beenden Sie sie durch das Symbol für den Benachrichtigungsbereich. Einmal DeckPilot Ausgänge, sollten alle Schlüssel normal durchlaufen.
- Aktivieren Sie Windows-Start und Test nach erneuter Anmeldung; Deaktivieren Sie es anschließend, wenn dies nicht gewünscht ist.

<a id="current-limitations"></a>
## Aktuelle Beschränkungen

- Key Listening ist global für die Windows-Sitzung. Physische F13-F24-Tasten von einer anderen Tastatur folgen der gleichen Konfiguration. Software-injizierte Tastaturereignisse werden ignoriert, um Aktionsschleifen zu verhindern.
- Einige Spiele, Anti-Cheat-Systeme oder Programme, die direkte Geräteeingaben verwenden, erhalten möglicherweise immer noch abgefangene Schlüssel. Der Freischlüsselmodus bleibt verfügbar. Der Anmeldebildschirm, der sichere Desktop und andere Sitzungen liegen außerhalb der Kontrolle der Anwendung.
- Windows blockiert möglicherweise Verknüpfungen, die an Anwendungen mit Administratorprivilegien gesendet werden. DeckPilot läuft ohne Administratorrechte und umgeht diese Einschränkung nicht.
- Veröffentlichen Sie Strg, Alt, Shift und Windows, bevor Sie eine Verknüpfungsaktion auslösen. Kombinationen wie Alt + Tab bleiben dem Windows-Verhalten unterworfen.
- Die Trennungserkennung, automatische Profile pro Spiel und ein signierter Installer sind zukünftige Verbesserungen. Profile werden derzeit manuell ausgewählt.

<a id="project-structure"></a>
## Projektstruktur

| Wegstrecke | Zweck |
| --- | --- |
| `src/DeckPilot.Core` | Einstellungen, Schlüsselstatus, Suche, GitHub-Versionsvalidierung, verifizierte Downloads und Ersatztransaktionen. |
| `src/DeckPilot.Windows` | WPF-Schnittstelle, Anwendungskatalog, Icons, Windows-Aktionen, Benachrichtigungsbereichsintegration und Update-Helfer. |
| `tests/DeckPilot.Checks` | Ausführbare Prüfungen ohne externes Test-Framework. |
| `tests/windows-update-smoke.ps1` | Native Windows Update und Application-Picker Rauchtest von CI verwendet. |
| `deckpilotlogo.png` | Anwendungslogo. |
| `deckpilotbanner.png` | Projektbanner. |

<a id="license-and-credit"></a>
## Lizenz und Kredit

DeckPilot wird freigegeben unter dem **[MIT-Lizenz](../../LICENSE)**Sie dürfen es verwenden, kopieren, ändern und weiterverbreiten, auch kommerziell, solange Sie die Urheberrechts- und Lizenzvermerke behalten, die von der Lizenz verlangt werden.

Wenn Sie bauen auf DeckPilot, wäre ein kleiner sichtbarer Kredit und ein Link zu diesem Repository zu schätzen, dieser zusätzliche Kredit ist optional; die Aufbewahrung der MIT-Mitteilungen ist weiterhin erforderlich.

Empfohlener Kredit:

> Basierend auf [DeckPilot](https://github.com/lolo34dr/deckpilot) durch [lolo34dr.](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Macropad Firmware Updates

DeckPilot 1.1.0 enthält eine **Firmware du macropad…** Dialog und Firmware 1.0.0 für den Pro Micro ATmega32U4 **5 V / 16 MHz**, mit der Leonardo/Caterina-Konfiguration, die durch das Upload-Log des Besitzers bestätigt wird. Das gebündelte Windows-Flashing-Tool erfordert keine Arduino-IDE. Geräteauswahl und -bestätigung sind obligatorisch; App-Updates blinken niemals stillschweigend das Board. Bestehende F13-F24-Mappings, Profile und Kalibrierungen bleiben kompatibel.

**Der Besitzer hat erfolgreiches In-App-Flashing, Firmware bestätigt 1.0.0 Identifikation und alle 12 Knöpfe auf der Referenztafel.** Behalten Sie die Originalskizze und den Zugriff auf RST/GND zur Wiederherstellung bei. Firmware-Blinken kann nicht automatisch zurückgerollt werden. [Firmware-Dokumentation](FIRMWARE.de.md) Kompatibilität, Validierung, Wiederherstellung, Build-Schritte und Lizenzen von Drittanbietern. USB-Modi pro Taste und native Mediensteuerung sind nicht Teil dieser Firmware-Version.

<a id="building-the-windows-installer"></a>
### Erstellen des Windows Installers

Nach der Herstellung `artifacts/standalone/DeckPilot.exe` Mit dem eingebetteten Firmware-Bundle installieren Sie Inno Setup 6 und laufen `./installer/build.ps1` Aus dem Repository Root. `artifacts/installer/DeckPilot-Setup.exe`CI erstellt und testet dieses Installationsprogramm, bevor es an ein Release angehängt wird. Das Installationsprogramm verwendet eine stabile Anwendungs-ID, so dass zukünftige Versionen die gleiche Installation aktualisieren.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Verbindungs-Dashboard und Firmware-Prüfungen ()1.3.0)

Die **Votre macropad** Eine Karte zeigt den Verbindungszustand, die erkannte Firmware und alle verfügbaren gebündelten Updates an. **Connexion et diagnostic** Die Auswahl des Boards wird angezeigt, wenn mehrere Leonardos angeschlossen sind, der Status der Live-USB-Verbindung, die Anzahl der physischen Tasten, die Präferenzen der Firmware-Benachrichtigung und eine kopierbare Diagnosezusammenfassung. Die Diagnose kopiert keine Profilaktionen oder USB-Serienkennungen. Der Monitor verbindet sich nach dem Ausstecken wieder und gibt seinen seriellen Port automatisch auf, während der Firmware-Manager geöffnet ist. F13-F24-Tastaturaktionen funktionieren weiterhin mit Firmware 1.0.0 Ohne den diagnostischen Link.

Start-Firmware-Prüfungen sind standardmäßig aktiviert. Nachdem die App-Update-Prüfung und -Kalibrierung aus dem Weg geräumt sind, kann eine ältere identifizierte Firmware die Installation öffnen; Verzögerungserinnerungen für 24 Unbekannte Boards und mehrere nicht ausgewählte Kandidaten werden nicht automatisch geflasht. Firmware-Releases werden mit App-Updates von GitHub gebündelt und nicht aus einem unabhängigen Firmware-Feed abgerufen.

Firmware **1.1.1** Sitzungsidentifikation, korrelierte Heartbeat-Antworten, begrenzte Nachrichten und physische Key-State-Ereignisse. Diese Ereignisse animieren die abgebildeten Schaltflächen und aktualisieren die Diagnose; sie rufen niemals Aktionen ein zweites Mal auf. Die erste physische Board-Validierung ist immer noch erforderlich, obwohl die frühere Firmware 1.0.0 und Flash-Verfahren wurden vom Eigentümer bestätigt. [Protokoll und Kompatibilitätsdokumentation](FIRMWARE.de.md).

<a id="keypad-interface-140"></a>
## Tastaturschnittstelle (Keypad)1.4.0)

Der größere 3 × 4 Die Tastatur folgt dem physischen Gehäuse und den schwarzen Tastenkappen. Jede Taste zeigt ihr Hauptsymbol, ihr Label, ein F13-F24-Mapping und ein kleines Aktionssymbol; Tooltips behalten das vollständige Label und die Position bei. Drücken Sie Feedback, führen Sie Aktionen aus, erfolgreicher Versand und Fehler haben unterschiedliche Erscheinungsbilder, einschließlich Symbole und nicht nur Farbe. Animationen folgen der Windows-Animation Präferenz.

Wählen Sie einen Schlüssel, um es zu bearbeiten. **Options avancées · fichier et arguments** hält manuelle Anwendungspfade und Startargumente standardmäßig zusammen, während wichtige Ordner und Website-Ziele erweitert bleiben. **Tester l’action** Speichert und sendet die ausgewählte Aktion auf dem aktuellen PC; ein Erfolgsindikator bestätigt den Versand, nicht den Abschluss in einer anderen App. Tastenkombinationen zielen auf das aktive Fenster. Kostenlose F13-F24-Tasten haben keine Testaktion.

Wenn das Makropad getrennt ist, **Hors ligne** Mit dem beigefügten Hinweis wird erläutert, dass Profile noch bearbeitet und Aktionen getestet werden können. Die Konfiguration wird auf dem PC gespeichert und bleibt während der gesamten Unterbrechungen und Wiederverbindungen verfügbar. Diese Schnittstellenverbesserungen funktionieren mit der vorhandenen Firmware und erfordern keinen zusätzlichen Flash.

Das Benachrichtigungsgebietsmenü wechselt nun direkt Profile. **STRG+S** Speichert die aktuelle Button-Konfiguration. Ungültige Konfigurationsdateien werden gesichert, bevor eine neue Konfiguration verwendet wird. App-Installation, Desktop/Start-Menüsymbole, Starteinstellungen und der vorhandene automatische Updater bleiben erhalten.

Für manuelle Arduino IDE Uploads, **Ausstieg DeckPilot aus dem Tablett-Menü** Das Schließen des Hauptfensters lässt es im Hintergrund laufen.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, Action Search und Drag-and-Drop1.5.0)

**Spotify** ist getrennt von **Multimédia**. Seine Wiedergabe-/Pausen-, nächsten und vorherigen Befehle richten sich an die Windows-Mediensitzung der Spotify-Desktop-App, auch wenn ein Browser YouTube spielt. Spotify muss geöffnet sein und eine verfügbare Wiedergabesitzung haben; starten Sie bei Bedarf einmal einen Track. Spotify in einem Browser wird nicht unterstützt. Fehlende oder ablehnende Spotify-Sitzungen führen zu einem Fehler, ohne Rückgriff auf globale Medienschlüssel oder einen anderen Player. Es werden keine Spotify-Kontoautorisierung, Premium-Abonnement oder gespeichertes Token benötigt DeckPilot. Windows 10 Version 2004 oder höher oder Windows 11, wird von diesem Build benötigt.

Verwendung **Rechercher une action ou une application** für Abfragen wie z.B. `volume`, `skip`, `Spotify` Der Installed-App-Index wird bei der ersten Suche geladen und für fünf Minuten zwischengespeichert; manuelle Dateiauswahl bleibt verfügbar. Klicken Sie auf ein Ergebnis, um den ausgewählten Schlüssel zu konfigurieren oder ziehen Sie ihn auf einen anderen Schlüssel.

Ziehen Sie eine Aktion aus dem Katalog oder den Suchergebnissen auf einen Schlüssel, um sie zuzuweisen. Ziehen Sie einen konfigurierten Schlüssel auf einen anderen, um **tauschen ihre Aktionen aus**; die physikalische Kalibrierung bleibt unverändert. `.exe`, `.lnk`, `.appref-ms`, Ordner oder HTTP(S) URL auf einen Schlüssel. Dropping konfiguriert nur den Schlüssel und führt die Aktion niemals aus. Vervollständigen Sie alle fehlenden Einstellungen im Editor.

Windows-Startup behält nun eine explizite Präferenz pro Benutzer, repariert einen veralteten oder fehlenden Run-Eintrag und meldet bekannte Windows-Start-Deaktivierungs-Flags. Normale Starts respektieren Windows-Deaktivierung; ausdrücklich aktivieren **Lancer avec Windows** Räumt das Bekannte DeckPilot Deaktivieren Sie das Flag für den aktuellen Benutzer. Hintergrundstarts bleiben noch vor der Kalibrierung verborgen, und doppelte Hintergrundstarts verlassen leise. Nach der Aktualisierung öffnen **Préférences de l’application**, überprüfen Sie den Startstatus und reaktivieren Sie die Option, wenn Windows sie blockiert hat. Eine echte Anmeldung / Anmeldung auf dem PC des Benutzers bleibt die letzte Prüfung für Windows-Richtlinien oder maschinenspezifische Einschränkungen.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Sharp Keypad Rendering, Profilwiederherstellung und Audiosteuerung ()1.6.1)

Die Tastatur wird jetzt in nativen WPF-Größen dargestellt, anstatt Text durch eine Viewbox zu skalieren. Eltern-/Tastenschatteneffekte und Textskalierungsanimationen wurden entfernt; kleinere Fenster scrollen, anstatt die Tastatur zu verkleinern. Die App deklariert die DPI-Awareness pro Monitor und verwendet größere Windows-Shell-Symbole für Desktop-Anwendungen.

**Annuler / Rétablir** Wiederherstellung von Profilbearbeitungen, einschließlich Drag-and-Drop-Swaps, Aktionseinstellungen, Importen und Profilerstellung/-löschung. 50 Bearbeitungen während der App-Sitzung. Ctrl+Z/Ctrl+Y arbeiten auf Profilen außerhalb von Textfeldern; Textfelder behalten ihre eigene Rückgängigmachung/Wiederholung bei. Kalibrierung, Starteinstellungen und ausgeführte Aktionen werden nicht rückgängig gemacht.

Unter **Gérer les profils**Exportieren Sie das aktuelle Profil oder alle Profile als `.deckpilot.json`, Profile als neue Kopien importieren oder ein Backup aller Profile wiederherstellen. Import führt niemals Aktionen aus und ersetzt niemals die Kalibrierung des PCs. Profile enthalten Aktionspfade, Argumente und Audio-Endpunkt-IDs, die möglicherweise auf einem anderen PC angepasst werden müssen. Unbekannte Formate, übergroße Dateien und fehlerhafte Gestenkonfigurationen werden abgelehnt.

Vor dem Speichern geänderter Profile, DeckPilot hält die vorherigen Profile in `%APPDATA%\DeckPilot\Backups` (20 Versionen, Content-dedupliziert). Preference-only speichert keine Profil-Backups. Ein Backup-Fehler verhindert das Überschreiben der aktuellen Einstellungen. Restore ersetzt Profile nach Bestätigung und kann selbst rückgängig gemacht werden; die Kalibrierung bleibt unverändert.

**Microphone Windows** Die permanente Statusleiste und die Audio-Windows-Karte zeigen den Status active/muted/mixed/unavailable an, der alle zwei Sekunden und nach App-Befehlen aktualisiert wird. Ein fehlgeschlagenes Multi-Endpoint-Update versucht, frühere Mute-Zustände wiederherzustellen. Diese Indikatoren beschreiben den Windows-Endpunktzustand, nicht einen Hardware-Switch oder eine anwendungseigene Mute-Taste.

**Changer de sortie audio** Wechsel zwischen zwei ausgewählten, angeschlossenen Ausgabegeräten für Windows-Konsole, Multimedia und Kommunikationsstandards. Ein fehlendes Ziel führt zu einem Fehler, ohne ein anderes Gerät auszuwählen. Anwendungen mit eigenem festen Ausgang folgen möglicherweise nicht dem Windows-Standard. Importierte Endpunkt-IDs werden als nicht verfügbar angezeigt, bis sie an den aktuellen PC angepasst sind. Durch fehlgeschlagene Routing-Änderungen wird versucht, die vorherigen Standardwerte wiederherzustellen.

Konfigurieren **appui court / appui long**Wählen Sie zunächst eine konfigurierte Aktion aus, aktivieren Sie die Option Zwei-Aktionen, wählen Sie die Geste zum Bearbeiten und legen Sie einen Schwellenwert von 200 bis 3000 ms. Ohne diesen Modus behalten Aktionen das sofortige Keydown-Verhalten bei. Mit ihm laufen kurze Aktionen beim Release vor dem Schwellenwert; lange Aktionen laufen einmal am Schwellenwert (oder beim Release, wenn der Timer noch nicht gelaufen ist). Eine lange Aktion läuft niemals auch die kurze aus. Kostenlose F13-F24-Durchgangstasten können den Dual-Action-Modus nicht verwenden. Ausstehende Gesten werden bei Profil-/Konfigurationsänderungen, Kalibrierung, Firmware-Operationen, Sitzungsänderungen und Suspendierung abgebrochen. Es wird kein Firmware-Flash benötigt; die Keyboard-Tastaturmeldungen liefern die Gesten.

Core-Prüfungen umfassen Wiederholungs-/Freigabegrenzen, gleichzeitige Halteeinstellungen, Stornierung, Profilverlauf/Austausch/Backups und Audio-Rollback mit gefälschten Endpunkten. Windows-Benutzeroberfläche überprüft die tatsächliche Ausübung von Drag Undo/Redo, Konfiguration einer langen Aktion und eines Schwellenwerts, native Tastaturabmessungen nach Größenänderung und Mikrofonanzeige. Tatsächliche Mikrofontreiber, Headset-/Lautsprecherwechsel und physische Halteeinstellungen müssen noch auf dem PC des Besitzers validiert werden.

<a id="search-and-ci-reliability-fixes-164"></a>
## Such- und CI-Zuverlässigkeitskorrekturen ()1.6.4)

Gleichwertige Suchergebnisse behalten ihre vorhandenen WPF-Buttons bei. Der installierte App-Katalog und der Zeitgeber verschieben den Ergebnisersatz, während ein Suchbutton erfasst wird oder ein Drag im Gange ist. Dies verhindert, dass ein Quellbutton während einer Interaktion entfernt wird.

Der Testclient verwendet physische Cursorkoordinaten, um UI-Automatisierungsgrenzen unabhängig von DPI zu entsprechen, fokussiert vor dem Messen und scannt alle Scroll-Prozentsätze. Eine Regressionsüberprüfung überprüft auch, dass äquivalente Suchanfragen die gleiche zugängliche Schaltfläche beibehalten. Hintergrundstart-Behauptungen warten auf den geladenen Handler von WPF und erfordern fünf aufeinanderfolgende Beobachtungen mit versteckten Fenstern, während die App am Leben bleibt. Fehler melden die Phase, Cursorziele, DPI und letzte Scroll-Geometrie.

Workflows erstellen und veröffentlichen mit Node.js 24 Der Arduino-Compiler bleibt bei CLI 1.2.2; CI lädt sein Windows-Archiv mit einem angehefteten SHA-256 Prüfsumme: Die Firmware und ihre Toolchain-Versionen sind unverändert.

Einstellungen, die in Windows-Tests gelesen werden, erlauben ausdrücklich das Lesen/Schreiben/Löschen und Parsen von UTF-8 Nach dem Schließen des Lesegeräts. Dies verhindert, dass der Test selbst den atomaren Dateiersatz der App leugnet. Der Verlaufstest hält einen alten Snapshot über drei Rückgängig-/Wiederholzyklen offen und überprüft, ob er intakt bleibt, während sich die Live-Einstellungen ändern.

Unter Windows werden vorhandene Einstellungen mit `File.Replace` statt Overwrite-Modus `File.Move`. Dies unterstützt Leser, die das Löschen erlauben, während ein kompletter alter Snapshot beibehalten wird. Die Core-Suite testet drei Ersetzungen mit einem geöffneten Leser; Windows-Core-Checks laufen vor der Firmware-Kompilierung und -Veröffentlichung, so dass Persistenzfehler frühzeitig erkannt werden.

Ein Test-Snapshot-Reader kann bis zu 500 ms für Windows-Sharing/Lock-Verstöße, während `ReplaceFile` Das Warten gilt nur für das Öffnen des Snapshots, niemals für das Ausführen oder Wiederausführen einer Anwendungsaktion. Andere Fehler, fehlgeformte JSON und persistente Schlösser bleiben Fehler.

<a id="runtime-and-editing-optimizations-170"></a>
## Laufzeit- und Bearbeitungsoptimierungen ()1.7.0)

Das Speichern identischer Einstellungen überprüft die aktuelle Datei, überspringt jedoch Ersatz- und Sicherungsarbeiten. Externe Bearbeitungen/Löschungen und fehlgeschlagene Schreibvorgänge werden immer noch erkannt. `ButtonAction` Die Geschichte hält losgelöste Modell-Schnappschüsse, validiert neue Zustände und vermeidet JSON-Parsing während Rückgängigmachen/Wiederholen. 50-Editing Limit und 20 Profil-Backups bleiben bestehen.

Search und Picker teilen sich einen unveränderlichen Installed-App-Katalog für fünf Minuten15 Sekunden für ein Teilergebnis). Gleichzeitige Verbraucher teilen sich einen Scan; das Schließen eines Pickers löscht keinen anderen Verbraucher. Refresh umgeht den Cache und jeder Picker hat seinen eigenen Filter/Ansicht. Scannen und Katalog-Parsing läuft vom UI-Thread ab; Picker-Filterung wird entlarvt durch 120 ms. Die Aktionssuche überprüft diesen Cache nach dem Debounce und bei der Rückkehr vom Picker erneut, sodass auch dort ein aktualisierter Katalog verfügbar ist.

Board-UI-Updates folgen jetzt Verbindungsbenachrichtigungen, anstatt alle zu wecken 100 Ausgeblendete/minimierte Fenster stoppen Feedback-Animationen und verwenden ein zehnsekündiges Audio-Abfrageintervall anstelle von zwei Sekunden; Wiedereröffnen oder Ausgeben eines Audio-Befehls sofort frischer Zustand. Ausstehende Audio-Aktualisierungen teilen sich eine Schleife und schließen Anfragen ein, die während eines vorhandenen Lesens ankommen. Unveränderte Audio-Schnappschüsse und eingefrorene Pinsel vermeiden wiederholte UI-Zuordnungen. Prozess-Wrapper, die durch das Starten einer App zurückgegeben werden, werden ohne Schließen des gestarteten Programms angeordnet.

Diese Änderungen reduzieren die angegebene Arbeit; sie legen keinen maschinenunabhängigen CPU/RAM-Prozentsatz fest. Native Hardware-Audioschaltung und physisches Schlüsselverhalten erfordern weiterhin eine Validierung auf einem Windows-PC mit dem Makropad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Workspace Tools und aktualisiertes Interface ()1.8.0)

Die Holzkohle-Schnittstelle stellt die Aktionsbibliothek an die erste Stelle, trennt Profilkontrollen von der knackigen nativen Größe 3 × 4 Tasten-Akzente unterscheiden Ihre eigenen Kategorien; Auswahl, Tastaturfokus und Aktions-Feedback bleiben getrennt. **Réduire les animations** in preferences ergänzt die Windows-Animationseinstellung. Kleinere Fenster behalten den Scroll-Zugriff bei, anstatt den Schlüsseltext zu verkleinern.

- **Profilvorlagen:** unter **Gérer les profils**, Musik und Audio, Desktop und Produktivität oder Erstellung hinzufügen. Jede Vorlage erstellt ein separates editierbares Profil und führt niemals seine Aktionen aus. Bestehende Profile und physische Kalibrierung werden beibehalten. Tastenkombinationen verwenden die aktive Anwendung, deren Bindungen unterschiedlich sein können.
- **Profilschlüssel:** Zuweisung **Changer de profil**, wählen Sie dann ein benanntes Profil oder einen Zyklus vorwärts/rückwärts. Verweise zwischen Profilen werden beim Importieren eines Satzes oder beim Duplizieren eines selbstreferenzierenden Profils neu zugeordnet. Ein fehlendes Ziel meldet einen Fehler und kann im Editor ersetzt werden.
- **Windows Befehle:** Verwendung **Commandes Windows** Zum Öffnen gemeinsamer Einstellungen oder des Task-Managers oder zum expliziten Sperren des PCs. Befehle stammen aus einer festen Liste, ohne dass ein Skript eingegeben werden muss.
- **Zwischenablage mit vollständigem Schlüssel:** Klicken Sie mit der rechten Maustaste auf eine Taste, um sie zu kopieren, zu schneiden, einzufügen oder zurückzusetzen, einschließlich der langen Aktion und des Akzents. Strg + C / X / V und Löschen funktionieren, während eine Decktaste den Tastaturfokus hat; gewöhnliche Textfelder behalten ihre Bearbeitungskürzel. Die Tastenzwischenablage bleibt im Inneren DeckPilot Diese Bearbeitungen unterstützen das Undo/Redo.
- **Kurzschreiber:** Wählen Sie eine Tastaturaktion und klicken Sie **Enregistrer un raccourci**. Drücken Sie eine unterstützte Kombination und speichern Sie sie dann. Escape bricht ab; F13-F24 ist während der Aufzeichnung für das Makropad reserviert. Windows-reservierte Kombinationen können immer noch die manuelle Steuerung erfordern.
- **Schnellzugriff:** Ctrl+K oder die Library-Schaltfläche fokussiert die Aktionssuche. **Activité récente** hält den letzten 30 Dispatched/failed actions im Speicher für diese Session, ohne Programmargumente oder Zielpfade zu speichern. Sie können jederzeit gelöscht werden. Ein erfolgreicher Versand garantiert nicht den Abschluss in der Ziel-App.

Die Firmware bleibt **1.1.1**. Die Annahme eines automatischen Firmware-Angebots öffnet nun den Firmware-Manager korrekt; der vorherige Modal Guard verhinderte die Übergabe. Bestehende Firmware-Identität, Heartbeat, Reconnect und Flash-Checks bleiben bestehen. Neue Workspace-Funktionen benötigen keinen Firmware-Flash.

Die Validierung umfasst Legacy-Einstellungen, Akzentvalidierung, Profilnavigation und Referenz-Remapping, tragbare Vorlagen, native Windows-Vorlage / Undo / Redo und Profilwechsel, Shortcut-Aufnahme, Kopieren / Einfügen / Zurücksetzen, Einstellungen Persistenz, Installation / Aktualisierung, Layout-Änderung und eine tatsächliche Benutzeroberflächenerfassung. Hardwarespezifisches Verhalten erfordert weiterhin Makropad und Audiogeräte auf einem Windows-PC.

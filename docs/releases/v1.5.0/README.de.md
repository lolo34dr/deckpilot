> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 1.5.0 fügt Spotify-spezifische Steuerelemente, Aktionssuche und Drag-and-Drop hinzu und repariert die Windows-Startregistrierung.

- **Spotify** Wiedergabe/Pause, nächste und vorherige Adresse nur die Spotify-Desktop-Mediensitzung, wobei die Browser-/YouTube-Wiedergabe getrennt bleibt. Öffnen Sie Spotify und starten Sie die Wiedergabe einmal. Spotify in einem Browser wird nicht unterstützt; es gibt kein globales Medien-Fallback, wenn Spotify fehlt oder einen Befehl ablehnt.
- Suchen Sie Aktionen und installierte Apps nach Namen oder Schlüsselwörtern wie Lautstärke, Überspringen, Spotify oder Discord. Klicken Sie auf Ergebnisse oder ziehen Sie sie auf einen Schlüssel.
- Ziehen Sie Katalog-/Suchaktionen auf Schlüssel, tauschen Sie die Aktionen zweier Schlüssel aus, ohne die Kalibrierung zu ändern, oder lassen Sie einen ausführbaren Link, eine Verknüpfung, einen Ordner oder einen HTTP(S)-Link fallen.
- Der Startup behält eine gespeicherte Präferenz pro Benutzer, repariert fehlende/stale Pfade, erkennt bekannte Windows-Deaktivierungs-Flags und unterstützt explizite Reaktivierung. Hintergrund- und doppelte Start-Starts bleiben still, auch vor der Kalibrierung.

Der Build erfordert Windows 10 Version 2004 oder höher oder Windows 11. Download **DeckPilotSetup.exe** oder Update von DeckPilotProfile und Kalibrierung bleiben erhalten, es ist kein Firmware-Flash erforderlich, um einen zuvor blockierten Start zu reparieren, überprüfen Sie dessen Status unter **Préférences de l’application** und ermöglichen **Lancer avec Windows** noch einmal.

Die Validierung umfasst simulierte Spotify/Browser-Isolation, Such- und Einstellungsprüfungen, Windows-Installations-/Start-Reparaturtests, echte Maus-Drag-and-Drop- und native Update/UI-Prüfungen. Spotify-Wiedergabe neben YouTube und eine echte Windows-Anmeldung benötigen noch eine Bestätigung auf dem PC des Benutzers.

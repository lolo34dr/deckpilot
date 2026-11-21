> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.1.2 und DeckPilot Atelier 1.0.11 fixe Werkstattsicherheit, Resonanzbodenzuverlässigkeit und tragbare Profilgeräusche.

- Atelier kann Leonardo / Caterina Boards vorbereiten, die noch nicht antworten DeckPilot Protokoll; Serienfehler hören immer noch auf zu blinken und der USB-Speicherort, der Bootloader und die MCU-Signatur bleiben verifiziert.
- Jede Inspektion beginnt mit leeren Ergebnissen. Berichte behalten die getestete Platine und Firmware bei, und veraltete USB-Rückrufe können eine neue Sitzung nicht beeinflussen.
- Das Schließen des Ateliers während des Schreibens oder der Überprüfung der Firmware wird blockiert.
- Profilexporte und automatische Profil-Backups können referenzierte MP3/WAV-Dateien beibehalten.32 MiB-Audiolimit: Importiert das Kopieren eingebetteter Clips in einen verwalteten Ordner und Remap-Referenzen für kurze/lange Aktionen. Legacy-Profilimporte werden weiterhin unterstützt; fehlendes Backup-Audio wird explizit gemeldet.
- Die Resonanzplatte veröffentlicht fertige Clips, aktualisiert Wiedergabeanzeigen und meldet unterbrochene Audiostreams. Umbenannt VB-CABLE Endpunkte werden durch ihre Adapteridentität erkannt.
- Korrekt übersetzte README-Links und Bewahren von technischen Gerätenamen während der Übersetzung. VB-CABLE Anforderung.

Validierung umfasst Kernregressionsüberprüfungen, Firmware-Protokolltests, Übersetzungstests, Windows-Build, DeckPilot und Atelier-Installations-/Upgrade-/Deinstallations-Prüfungen und native Schnittstellen-/Update-Prüfungen. Physisches First-Flash- und Audio-Disconnect-Verhalten erfordert weiterhin die Hardware des Besitzers. Firmware bleibt bestehen 1.1.1Es ist kein Firmware-Update erforderlich.

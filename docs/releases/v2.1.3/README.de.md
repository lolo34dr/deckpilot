> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.1.3 Reduziert die Resonanzbodenmikrofonlatenz und entfernt die 1-Sekunden-Konvertierungsblöcke aus dem Live-Audiopfad.

- Verwenden Sie Output-driven, Small-Block-Resampling für das Mikrofon und Sound-Clips; Matching Sample Rates Bypass Resampling vollständig.
- Request ereignisgesteuert 20 ms-Erfassung und 40 ms-Wiedergabepuffer anstelle der vorherigen 100 ms-Einstellungen. Die tatsächliche Latenz hängt von Windows, den Audiogeräten und VB-CABLE.
- Gebundene Mikrofonpufferung 100 Kurze Unterläufe fügen nur die von der aktuellen Lektüre geforderte Stille hinzu.
- Unterstützen Sie Mono-, Stereo-, Multichannel- und Windows-erweiterbare PCM/Float-Eingänge ohne die Media Foundation-Einsekunden-Lesevorgabe.
- Discord-Verarbeitung und Routing unverändert lassen. Atelier bleibt 1.0.11 Firmware bleibt bestehen 1.1.1.

Die Validierung umfasst Regressionsüberprüfungen für das Resampling von Blockgrößen, Kanalkonvertierung, erweiterbare Formate, Underrun-Wiederherstellung, Überlauffrische, gleichzeitige Aufnahme/Wiedergabe und Clip-Vervollständigung neben den vorhandenen Windows-Installationsprogramm- und Anwendungstests. Es wird keine Hardware-Latenzzahl beansprucht; Überprüfen Sie die Verbesserung mit Ihrem Mikrofon und Audiotreiber.

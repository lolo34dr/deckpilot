> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.1.9 Behebt die Lautsprecherüberwachung auf Geräten, deren Windows-Audioformat erweiterbar ist, einschließlich einiger USB-Headsets, und fügt eine Soundboard-Overlay-Benachrichtigung hinzu.

- Die Lautsprecherrückgabe behält das genaue WASAPI-Mixformat des ausgewählten Outputs bei, konvertiert Clip-Samples in seine PCM- oder Float-Codierung und wiederholt ohne ereignisgesteuerte Wiedergabe, wenn ein Treiber diesen Modus ablehnt.
- Das Starten eines Resonanzbodenclips zeigt seinen Namen im konfigurierbaren On-Screen-Overlay: Die vorhandenen Profilbenachrichtigungspräferenzen, die Platzierung und die Einstellung der reduzierten Bewegung steuern beide Benachrichtigungstypen.
- Die Rückgabe enthält immer noch nur Resonanzbodenclips; das physische Mikrofon bleibt aus dem Lautsprechermix heraus und fährt zum virtuellen Mikrofon fort.

DeckPilot Atelier bleibt bei 1.0.12 Firmware bleibt bestehen 1.1.1Es ist kein Firmware-Flash erforderlich. Die Lautsprecherkompatibilität muss weiterhin mit dem tatsächlichen Windows-Audiogerät des Besitzers überprüft werden.

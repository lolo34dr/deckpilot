> Dies ist eine automatische Übersetzung der vollständigen Release Notes. Das englische Original ist die Referenz.

DeckPilot 2.2.0 fügt einstellbare, lokale Mikrofongeräuschunterdrückung hinzu, während die Resonanzbodenclips unberührt bleiben.

- RNNoise filtert das ausgewählte physische Mikrofon vor DeckPilot Clips, die den Filter umgehen und im virtuellen Mikrofon frei bleiben.
- Wählen Sie eine beliebige Anwendung, die verwendet wird **CABLE Output** da seine Mikrofon-Spiele und Voice-Chat-Anwendungen gleichermaßen den gefilterten Voice- und Soundboard-Mix erhalten. Apps mit dem physischen Mikrofon umgehen direkt DeckPilot's-Filter.
- Aktivieren oder deaktivieren Sie die Filterung und setzen Sie 0–100% Festigkeit in **Soundboard · micro virtuel**Audioverarbeitung bleibt auf diesem PC; es ist kein Firmware-Flash erforderlich.
- Die native RNNoise-Bibliothek wurde für Windows x64 entwickelt und in DeckPilot und sein Installateur. Seine BSD-ähnliche Drittlizenz ist im Quell-Repository enthalten.

Die 10 ms RNNoise Rahmengröße garantiert keine End-to-End Latenz. Windows, die Audio-Geräte und VB-CABLE Prüfen Sie die Mikrofonqualität und -latenz mit der vorgesehenen Einstellung.

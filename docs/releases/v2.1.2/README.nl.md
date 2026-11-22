> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.2 en DeckPilot Atelier 1.0.11 fix workshop veiligheid, soundboard betrouwbaarheid en draagbare profiel geluiden.

- Atelier kan Leonardo / Caterina boards klaar te maken die nog niet beantwoorden de DeckPilot protocol; seriële storingen stoppen nog steeds knipperen en de USB-locatie, bootloader en MCU handtekening blijven geverifieerd.
- Elke inspectie begint met lege resultaten. Rapporten behouden de geteste board en firmware, en oude USB callbacks kunnen geen invloed hebben op een nieuwe sessie.
- Het sluiten van Atelier tijdens firmware schrijven of verificatie wordt geblokkeerd.
- Profiel export en automatische profiel back-ups kunnen behouden waarnaar MP3/WAV bestanden (32 MiB-audiolimiet). Importeert ingesloten clips in een beheerde map en remap korte/lange actiereferenties. Legacy-profielimporten blijven ondersteund; ontbrekende back-upaudio wordt expliciet gerapporteerd.
- De soundboard brengt afgewerkte clips, updates afspelen indicatoren, en rapporten onderbroken audio streams. VB-CABLE eindpunten worden herkend door hun adapter identiteit.
- Correct vertaald README links en behoud van technische apparaatnamen tijdens de vertaling. VB-CABLE vereiste.

Validatie omvat core regressie controles, firmware protocol tests, vertaling tests, Windows build, DeckPilot en Atelier installatie / upgrade / uninstall controles, en native interface / update controles. Fysische eerste-flash en audio ontkoppelen gedrag nog steeds de hardware van de eigenaar nodig. Firmware blijft 1.1.1; geen firmware-update is vereist.

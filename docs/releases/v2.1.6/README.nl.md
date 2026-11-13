> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.1.6 en DeckPilot Atelier 1.0.12 maken de firmware van Atelier expliciet wissen en realiseerbaar.

- Verplaats de destructieve firmware wissen in ingestorte geavanceerde bewerkingen en vereisen typen `EFFACER` vóór het schrijven.
- Leg uit dat het bord stopt met opnoemen als een toetsenbord na het wissen, laat de exacte RST/GND dubbel-reset herstel stappen, en houd Atelier's herstel actie beschikbaar na het wissen of een onderbroken flits.
- Firmware blijft 1.1.1; nee DeckPilot hardware firmware verandering is vereist.

Validatie omvat firmware en app controles, plus Windows UI automatisering voor de geavanceerde erase control, no-device veiligheid en RST/GND herstel begeleiding. De reset zelf vereist nog steeds de fysieke board.

DeckPilot 2.1.5 glad maakt audio-apparaat verfrist en voegt een branded opstartscherm.

- Ontdek fysieke microfoons en VB-CABLE uit de UI-draad. Automatische herhalingen delen een recente apparaat snapshot; de handmatige verversen knop voert altijd een nieuwe scan uit.
- Verminder herhaalde Windows audio-state polling en stroomlijn de gemeenschappelijke mono-microfoon conversie pad.
- Toon het DeckPilot banner terwijl de interactieve app start. Stille opstart met Windows blijft op de achtergrond.
- Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat achtergrondscan / cache regressie controles, audio conversie controles, Windows builds, installer upgrade / uninstall, native splash / update en applicatie-picker controles. Werkelijke audio CPU gebruik en latency nog steeds afhankelijk van de PC en audio-apparaten.

DeckPilot 2.1.4 start de geconfigureerde virtuele microfoon automatisch met de app, inclusief achtergrond opstarten na Windows-aanmelden.

- Automatisch opstarten van een soundboard microfoon is standaard ingeschakeld voor bestaande en nieuwe instellingen. Het maakt gebruik van de geselecteerde fysieke microfoon en VB-CABLE; geen handmatige activering is nodig na het herstarten van de PC wanneer DeckPilot begint met Windows.
- Als de apparaten niet klaar zijn bij het aanmelden, of als de audio stopt na het verbreken van de verbinding, DeckPilot herstart elke vijf seconden terwijl automatische activering is ingeschakeld.
- Een expliciete stop schakelt uit en slaat de automatische start-voorkeur op; activeer het opnieuw van de Soundboard paneel of de activeringsknop. Audioclips worden nooit automatisch afgespeeld.
- Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat instellingen migratie / ronde-trip controles en een native Windows UI check voor de aanhoudende automatische start voorkeur, plus de bestaande audio en installateur controles. Fysiek apparaat opstarten / opnieuw aansluiten is nog steeds afhankelijk van de hardware van de eigenaar.

DeckPilot 2.1.3 vermindert soundboard microfoon latency en verwijdert de een-seconde conversie blokken van de live audio pad.

- Gebruik output-gedreven, klein-blok resampling voor de microfoon en geluidsclips; matching sample rates bypass resampling volledig.
- Verzoek gebeurtenisgestuurd 20 ms vangst en 40 ms afspeelbuffers in plaats van vorige 100 ms instellingen. De werkelijke latentie is afhankelijk van Windows, de audio-apparaten en VB-CABLE.
- Bound microfoonbuffering naar 100 ms en de recente volledige frames behouden na een afspeelkraam in plaats van een oude speech in de wachtrij te zetten. Korte underruns voegen alleen de stilte toe die door de huidige lezing wordt gevraagd.
- Ondersteuning mono, stereo, multichannel en Windows uitbreidbare PCM/float ingangen zonder de Media Foundation een-seconde read-ahead.
- Houd Discord processing en routing ongewijzigd. Atelier blijft 1.0.11 en firmware blijft 1.1.1.

Validatie omvat regressie controles voor resampling blok maten, kanaal conversie, uitbreidbare formaten, underrun recovery, overflow fressness, gelijktijdige opname/weergave en clip voltooiing, naast de bestaande Windows-installer en toepassing testen. Geen hardware latency cijfer wordt beweerd; controleer de verbetering met uw microfoon en audio driver.

DeckPilot 2.1.2 en DeckPilot Atelier 1.0.11 fix workshop veiligheid, soundboard betrouwbaarheid en draagbare profiel geluiden.

- Het atelier kan Leonardo/Caterina boards voorbereiden die nog niet beantwoorden DeckPilot protocol; seriële storingen stoppen nog steeds knipperen en de USB-locatie, bootloader en MCU handtekening blijven geverifieerd.
- Elke inspectie begint met lege resultaten. Rapporten behouden de geteste board en firmware, en oude USB callbacks kunnen geen invloed hebben op een nieuwe sessie.
- Het sluiten van Atelier tijdens firmware schrijven of verificatie wordt geblokkeerd.
- Profiel export en automatische profiel back-ups kunnen behouden referentie MP3/WAV bestanden (32 MiB-audiolimiet). Importeert ingesloten clips in een beheerde map en remap korte/lange actiereferenties. Legacy-profielimporten blijven ondersteund; ontbrekende back-upaudio wordt expliciet gerapporteerd.
- De soundboard brengt afgewerkte clips, updates afspelen indicatoren, en rapporten onderbroken audio streams. VB-CABLE eindpunten worden herkend door hun adapter identiteit.
- Correct vertaald README links en behoud van technische apparaatnamen tijdens de vertaling. VB-CABLE vereiste.

Validatie omvat core regressie controles, firmware protocol tests, vertaaltests, Windows build, DeckPilot en Atelier installatie / upgrade / uninstall controles, en native interface / update controles. Fysische eerste-flash en audio ontkoppelen gedrag nog steeds de hardware van de eigenaar nodig. Firmware blijft 1.1.1; geen firmware-update is vereist.

DeckPilot 2.1.1 verbetert de installatie en updates:

- Automatisch controleren op VB-CABLE tijdens de eerste installatie en upgrades, en download en start de officiële ondertekende installateur wanneer de bestuurder ontbreekt.
- Toon Windows opstarten, automatische update controles en gereduceerde-motion voorkeuren tijdens de setup en updates, met bestaande keuzes vooraf geselecteerd en bewaard.
- Houd de geselecteerde fysieke microfoon en verzoek microfoon setup alleen wanneer nodig.
- Fix installateur initialisatie en detectie van bestaande installaties.

Downloaden **DeckPilot-Setup.exe** of de update accepteren in DeckPilot. Windows administrator goedkeuring is vereist. Bestaande profielen en kalibratie worden bewaard. DeckPilot Atelier blijft op 1.0.10 en firmware blijft op 1.1.1; beide zijn opgenomen in de gedeelde release.

Validatie: core checks en Windows build, installer upgrade/uninstall en native applicatie testen worden uitgevoerd voor release publicatie. Audio hardware gedrag vereist nog steeds testen op de PC van de gebruiker.

DeckPilot 2.1.0 houdt de diep zwarte interface en aanpasbare kleuren, en routeert de soundboard door VB-Audio VB-CABLE automatisch. Gebruikers selecteren alleen hun fysieke microfoon; het virtuele afspeeleindpunt is vastgesteld zodat spraak en MP3/WAV-clips niet per ongeluk naar luidsprekers kunnen worden verzonden. DeckPilot installateur kan downloaden van de officiële VB-CABLE pakket, controleer zijn Authenticode uitgever, en start de driver setup van de leverancier; het pakket wordt niet herverdeeld. In Discord of een spel, selecteer CABLE Output als de microfoon. Atelier blijft op versie 1.0.10.

DeckPilot 1.9.11 en DeckPilot Atelier 1.0.10 voeg pre-flash hardware en firmware diagnostics, een firmware versie vergelijking, een geleide herstel actie na onderbroken brieven, sequentiële multi-board knipperen met een batch rapport, en een fysieke-position-to-F13

DeckPilot 1.9.11 bevat dezelfde Atelier update in de gedeelde installer release.

DeckPilot 1.9.10 herstelt gedeelde releases die beide bevatten DeckPilot en DeckPilot Atelier 1.0.9. Beide toepassingen nogmaals controleren dezelfde laatste GitHub release.

Als u Atelier installeerde 1.0.8 vanaf het afzonderlijke releasekanaal, installeren DeckPilot-Atelier-Setup.exe van deze release eenmaal om terug te keren naar gedeelde updates. Eerdere Atelier versies en DeckPilot kan gebruik maken van hun bestaande updater.

DeckPilot Atelier 1.0.7 voegt een bevestigde programma-gebied wissen dat alles wist 28 KiB van gebruiker flash, laat een veilige reset loop, en behoudt Caterina en EEPROM. Reset blijft uitgeschakeld totdat een fysieke board is geselecteerd.

DeckPilot 1.9.7 lost Atelier verwijderen terwijl de app is geopend. Atelier 1.0.6 corrigeert ook de Windows DPI manifest, die verhinderde dat de geïnstalleerde app te starten.

DeckPilot 1.9.6 herstelt de Windows DPI manifest die voorkomen DeckPilot Atelier van opening na installatie.

DeckPilot 1.9.5 bevat gedetailleerde Atelier opstarten diagnostiek in de Windows release check.

DeckPilot 1.9.4 verbetert de Atelier Windows opstart rooktest om toepassing crash details te melden.

DeckPilot 1.9.3 fixeert de Atelier bouwen metadata om de firmware manifest te lezen uit de gebundelde firmware archief.

DeckPilot 1.9.2 fixeert de Atelier release verpakking stap en verbetert de bouw diagnostiek.

DeckPilot 1.9.1 verbetert de Atelier installatie startup en voegt automatische app en firmware update controles.

- Het installatieprogramma lanceert Atelier in de ingelogde Windows-gebruikerssessie en stopt/herstart het veilig voor upgrades.
- Atelier controleert GitHub bij het opstarten en op verzoek. Het kondigt nieuwere Atelier bouwt of gebundeld firmware, vervolgens downloadt een installateur geverifieerd tegen de gepubliceerde SHA-256 bestand.
- Firmware wordt alleen geflitst nadat de gebruiker de flash actie kiest; een update van de app schrijft nooit op zichzelf naar een verbonden bord.

Het Atelier installatieprogramma start ook in de ingelogde Windows-sessie van de gebruiker na een nieuwe installatie. De release workflow opent nu de geïnstalleerde app, controleert vervolgens upgrade en verwijdert terwijl het draait.

DeckPilot 1.9.0 voegt toe DeckPilot Atelier, een aparte Windows workshop app voor het knipperen van nieuwe macropads en het controleren van alle 12 Sleutels.

- Installeer de meegeleverde firmware op Leonardo / Pro Micro ATmega32U4 boards met de Caterina bootloader.
- Voer begeleide short-press en long-press controles voor elke fysieke sleutel, neem zijn F13.F24 mapping, en sla een kwaliteitscontrolerapport.
- Downloaden **DeckPilot-Atelier-Setup.exe** van deze release. Het origineel DeckPilot setup en firmware blijven apart beschikbaar.

DeckPilot 1.8.0 voegt een vernieuwde werkruimte toe en snellere manieren om uw profielen te bouwen en te gebruiken.

- Helderere houtskool/teale interface, actiebibliotheek eerst, helder 3 × 4 sleutels, accentkleuren en voorkeur voor verminderde beweging.
- Drie bewerkbare profielsjablonen: Muziek en audio, Bureaublad en productiviteit, en Creatie.
- Profielacties: schakel over naar een genoemd profiel, het volgende of het vorige profiel van een sleutel. Geïmporteerde profielsets behouden hun interne links.
- Windows-acties: open gemeenschappelijke instellingen of Taakbeheer, of sluit expliciet de PC.
- Kopieer / knippen / plakken / reset hele toetsen, inclusief lange persen en kleuren, met undo / redo en een app-local klembord.
- Neem een sneltoets direct op; Escape annuleert en macropad toetsen blijven gereserveerd tijdens het vastleggen.
- Ctrl+K-actie zoeken en een duidelijke, sessie-alleen geschiedenis van de laatste 30 acties.
- Repareer de automatische firmware prompt zodat het accepteert opent de firmware manager. In afwachting gebaren worden geannuleerd over profielwijzigingen.

Downloaden **DeckPilot-Setup.exe** of update van de app. Bestaande profielen, kalibratie en opstart voorkeuren worden bewaard. Firmware blijft **1.1.1**; geen flits is vereist voor deze functies.

Validatie omvat kerncompatibiliteit/navigatie/import/template controles en native Windows installatie, update, picker, drag-and-drop, undo/redo, sjabloon creatie, profiel acties, snelkoppeling, klembord bewerken, lay-out en minimaliseren / herstellen controles. Fysische macropad en audio-apparaat gedrag is nog steeds afhankelijk van de eigenaar van Windows hardware.

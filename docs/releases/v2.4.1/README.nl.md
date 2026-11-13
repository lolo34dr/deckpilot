> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.4.1 corrigeert de tabbladen van het bedieningspaneel zodat hun volledige contouren zichtbaar blijven en hun breedtes blijven uitgelijnd wanneer het venster wordt verkleind.

Er is geen firmware-update of flits nodig.

DeckPilot 2.4.0 reorganiseert het bedieningspaneel en voegt een exacte kleurselectie toe.

- Gebruik de **Acties**, **Appareil** en **Réglages** tabbladen om belangrijke acties, apparaat/audio controles en voorkeuren te vinden.
- het bestaande linker toetsenbord/-editor-layout behouden; DeckPilot logo en monochrome vector pictogrammen.
- Kies exacte achtergrond, paneel en accentkleuren met **Palet...**De Windows-picker ondersteunt aangepaste kleuren; er is geen vooraf ingesteld thema vereist.
- Verwijder de OBS/Streamlabs controle-integratie. 2.3 profielen blijven leesbaar, en hun gepensioneerde acties worden bewaard maar kunnen niet worden uitgevoerd. Deze sleutels opnieuw toewijzen uit de actiebibliotheek.
- Bewaar andere profielen, acties en fysieke kalibratie. Firmware blijft 1.1.1 en Atelier blijft 1.0.12; geen firmware flash is nodig.

DeckPilot 2.3.0 voegt directe OBS Studio en Streamlabs Bureaublad controles van macropad knoppen.

- Verbind met OBS via zijn WebSocket server en Streamlabs Desktop via Remote Control. DeckPilot stuurt applicatie API commando's direct in plaats van het simuleren van sneltoetsen.
- Configureer acties voor scènes, livestreaming, opname, audio dempen, bronzicht en replay buffer. OBS ondersteunt ook Studiomodus en virtuele camerabesturingen.
- Streaming referenties zijn beschermd voor de huidige Windows-gebruiker. Bestaande macropad firmware werkt zoals is; geen firmware update of flits is vereist.

DeckPilot 2.2.0 voegt verstelbare, lokale microfoon lawaai onderdrukking terwijl het houden van soundboard clips onaangeroerd.

- RNNoise filtert de geselecteerde fysieke microfoon voor DeckPilot mixt in soundboard clips. Clips omzeilen het filter en blijven duidelijk in de virtuele microfoon.
- Kies een toepassing die gebruikt **CABLE Output** als de microfoon-games en spraak-chat toepassingen ontvangen zowel de gefilterde stem en soundboard mix. Apps met behulp van de fysieke microfoon direct bypass DeckPilotHet filter.
- Filteren inschakelen of uitschakelen en de instelling ervan instellen 0–100% sterkte in **Soundboard · micro virtuel**. Audioverwerking blijft op deze PC; geen firmware flits is vereist.
- De RNNoise native bibliotheek is gebouwd voor Windows x64 en ingebed in DeckPilot De BSD-stijl licentie van derden is opgenomen in de bron repository.

De 10 ms RNNoise frame grootte garandeert geen end-to-end latency. Windows, de audio-apparaten en VB-CABLE Ook de vertraging beïnvloeden. Test microfoon kwaliteit en latency met de beoogde setup.

DeckPilot 2.1.7 voegt Spotify-only volume acties toe, een ingebouwde actie om te openen DeckPilot, en on-screen profielmeldingen.

- Spotify volume knoppen aanpassen Spotify's Windows audio sessie in 5% stappen, zonder het hoofdvolume of andere spelers te veranderen.
- De toepassing picker en actie zoeken bieden altijd DeckPilot, en de actie toont het bestaande venster van het kennisgevingsgebied.
- Profielwijzigingen tonen een korte, niet-activerende overlay op het actieve scherm. Kies een hoek of aangepaste positie in voorkeuren en bekijk het daar. Exclusieve full-screen games kunnen standaard Windows-overlays verbergen.

DeckPilot Atelier blijft op 1.0.12 en firmware blijft 1.1.1. Er is geen firmware flash nodig voor deze acties.

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
DeckPilot 2.1.8 voegt een optionele speaker terugkeer voor Soundboard Clips.

- In **Soundboard · micro virtuel**, inschakelen **Entendre aussi les sons du soundboard**, kies een fysieke speaker of headset uitgang, en stel zijn onafhankelijke 0–100% volume. Het retourneren is standaard uitgeschakeld en opgeslagen bij lanceringen.
- Alleen soundboard clips worden naar deze tweede uitgang gestuurd. De fysieke microfoon blijft naar de virtuele microfoon gaan, en wordt nooit in de speaker terug gemengd. Clips gaan nog steeds naar VB-CABLE op hun oorspronkelijke volume.
- De virtuele microfoon wordt niet herstart door het volume van het retourneren. Een niet-gekoppelde speakeruitgang stopt alleen de terugkeer; de virtuele microfoon blijft draaien. Vernieuw apparaten of kies een andere uitvoer om het te herstellen.

DeckPilot Atelier blijft op 1.0.12 en firmware blijft 1.1.1. Geen firmware flash is nodig. Werkelijke speaker routing moet worden gecontroleerd op de eigenaar Windows-pc met zijn audio-apparaten.

DeckPilot 2.1.9 fixes speaker monitoring op apparaten waarvan het Windows-audioformaat is uitbreidbaar, met inbegrip van een aantal USB-headsets, en voegt een soundboard overlay kennisgeving.

- De speaker return houdt de geselecteerde output's exacte WASAPI mix formaat, converteert clip samples naar zijn PCM of float codering, en retries zonder event-driven afspelen wanneer een driver weigert die modus.
- Het starten van een soundboard clip toont zijn naam in de configureerbare on-screen-overlay. De bestaande profielnotificatie voorkeur, plaatsing en reduced-motion instelling controle beide notificatietypen.
- De terugkeer bevat nog steeds alleen soundboard clips; de fysieke microfoon blijft uit de speakermix en gaat verder naar de virtuele microfoon.

DeckPilot Atelier blijft op 1.0.12 en firmware blijft 1.1.1. Geen firmware flash nodig is. Speaker compatibiliteit moet nog steeds worden gecontroleerd met de eigenaar van de werkelijke Windows-audio-apparaat.

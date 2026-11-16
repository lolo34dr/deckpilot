> Dit is een automatische vertaling van de volledige release notes. Het Engelse origineel is de referentie.

DeckPilot 2.0.0 introduceert een diep zwart interface met opgeslagen achtergrond, paneel en accent kleuren. De nieuwe soundboard accepteert MP3/WAV clips, laat gebruikers kiezen voor een fysieke microfoon en een virtuele kabel uitgang, en mengt stem met clips voor gebruik als microfoon in games of oproepen. Clips kunnen worden gestart vanuit de app of toegewezen aan een macropad sleutel. Een virtuele audio-kabel zoals VB-CABLE moet afzonderlijk worden geïnstalleerd op Windows; Atelier blijft op versie 1.0.10.

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

- Helderere houtskool/teale interface, actiebibliotheek eerst, knapperig 3 × 4 sleutels, accentkleuren en voorkeur voor verminderde beweging.
- Drie bewerkbare profielsjablonen: Muziek en audio, Bureaublad en productiviteit, en Creatie.
- Profielacties: schakel over naar een genoemd profiel, het volgende of het vorige profiel van een sleutel. Geïmporteerde profielsets behouden hun interne links.
- Windows-acties: open gemeenschappelijke instellingen of Taakbeheer, of sluit expliciet de PC.
- Kopieer / knippen / plakken / reset hele toetsen, inclusief lange persen en kleuren, met undo / redo en een app-local klembord.
- Neem een sneltoets direct op; Escape annuleert en macropad toetsen blijven gereserveerd tijdens het vastleggen.
- Ctrl+K-actie zoeken en een duidelijke, sessie-alleen geschiedenis van de laatste 30 acties.
- Repareer de automatische firmware prompt zodat het accepteert opent de firmware manager. In afwachting gebaren worden geannuleerd over profielwijzigingen.

Downloaden **DeckPilot-Setup.exe** of update van de app. Bestaande profielen, kalibratie en opstart voorkeuren worden bewaard. Firmware blijft **1.1.1**; geen flits is vereist voor deze functies.

Validatie omvat kerncompatibiliteit/navigatie/import/template controles en native Windows installatie, update, picker, drag-and-drop, undo/redo, sjabloon creatie, profiel acties, snelkoppeling, klembord bewerken, lay-out en minimaliseert / herstellen controles. Fysische macropad en audio-apparaat gedrag is nog steeds afhankelijk van de eigenaar Windows hardware.

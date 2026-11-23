> Dit is een automatische vertaling van de volledige firmware documentatie. Het Engelse origineel is de referentie. Opdrachten en identificaties worden precies bewaard.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Alleen voor de geteste hardwareconfiguratie: **Pro Micro ATmega32U4, 5 V / 16 MHz**, met gebruikmaking van de **Arduino Leonardo** board profiel en a **Caterina / AVR109** bootloader. USB-C beschrijft de connector, niet de boardcompatibiliteit. De oorspronkelijke schets van de eigenaar is succesvol geüpload met de 1200-baud reset en AVR109 op 57600 baud. De eigenaar heeft ook bevestigd succesvolle in-app knipperen, firmware 1.0.0 identificatie en werking van alle 12 Knoopjes op dat naslagbord.

<a id="wiring-and-behavior"></a>
## Bedrading en gedrag

Elke schakelaar verbindt één pin met GND. Interne pull-ups zijn ingeschakeld. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** verzenden **F13 tot en met F24** Een stal in die volgorde. 30 ms debounce handvatten drukken en loslaten onafhankelijk. Firmware 1.1.2 maakt gebruik van een bit per toets in zijn USB-toetsenbord rapport, zodat het alle twaalf gehouden toetsen kan vertegenwoordigen. Windows-profielen en kalibratie gebruik maken van dezelfde sleutelcodes.

Pin **1** wordt vastgehouden aan GND bij opstarten schakelt de toetsenbordbewerking uit, waarbij de oorspronkelijke failsafe behouden blijft. De USB-kern kan nog steeds bootloader-invoer ondersteunen. Verbind VNC niet met GND.

De huidige USB CDC seriële interface antwoorden `DECKPILOT?\n` op 115200 Brood met:

```
DECKPILOT|1|promicro-32u4-16mhz|1.2.0
```

Er is geen seriële verbinding nodig voor een normale toetsenbordbewerking. De identiteit is een compatibiliteitscontrole, geen cryptografische hardware-authenticatie. Windows meldt nog steeds de Arduino Leonardo USB-identificaties; veel andere boards delen deze identificaties.

<a id="hybrid-input-mode-120"></a>
## Hybride invoermodus (1.2.0)

Beide boards gebruiken dezelfde firmware. De app identificeert ze door hun Windows USB-apparaat instance en opent een seriële verbinding per board. In **Macropads**, klik op een sleutel op elk bord DeckPilot modus en configureer het profiel. **Kalibrator** Op die kaart als de fysieke knop orde verschilt. Een bord zonder een uniek USB serienummer kan een ander apparaat instantie krijgen wanneer verplaatst naar een andere USB socket. Houd elk van deze board op de toegewezen socket.

Na een DP2 handshake adverteert vermogen 4, de app stuurt `DP2|INPUT|<session>`. Het bestuur antwoordt met `DP2|INPUT|<session>` wanneer alle toetsen en het toetsenbordrapport worden uitgebracht, of `DP2|BUSY|<session>` als een sleutel nog steeds wordt vastgehouden. Zodra geactiveerd, stuurt het bord besteld `KEYS` frames beginnend bij opeenvolging 1 en onderdrukt F13. Diagnostische clients die gewoon verzenden `HELLO` normaal toetsenbordgedrag behouden. DTR laten vallen of het hartslagcontract zes seconden missen, herstelt het toetsenbord nadat alle knoppen zijn losgelaten. Een volledige agenda eindigt de seriële invoersessie met `DP2|LOST|<session>` in plaats van stil samen te voegen pers en release gebeurtenissen.

Fysische test op Windows:

1. Flits en verifieer elk bord afzonderlijk met **Firmware du macropad…**, sluit dan beide boards aan.
2. Open **Macropads** Klik op de eerste knop op de kaart van elk apparaat. Geef een andere actie toe aan elke knop. Controleer of de editor de geselecteerde COM-poort noemt en dat elke kaart zijn eigen profiel weergeeft.
3. Druk dezelfde positie op beide dekken samen en herhaal deze dan met korte en lange persen. Elke actie moet eenmaal op het toegewezen profiel draaien.
4. Trek het ene dek los terwijl u een knop vasthoudt, controleer of de andere nog reageert, en sluit het eerste aan op de originele USB-socket. DeckPilot van het systeemvak en bevestig een verse pers stuurt de bijbehorende F13.F24 sleutel.

<a id="install-from-deckpilot"></a>
## Installeren vanaf DeckPilot

1. Houd de meegeleverde herstel schets (`firmware/Original/Original.ino`) en een werkende Arduino upload setup als een herstel optie. Houd RST en GND fysiek toegankelijk.
2. Sluit Arduino IDE, seriële monitor en andere seriële tools. Koppel andere Leonardo boards.
3. Open **Firmware du macropad…**, kies het juiste apparaat en bevestig het bord en de bedrading. Een ongeïdentificeerde originele schets kan alleen worden geïnstalleerd na uitdrukkelijke bevestiging.
4. Bevestig installatie en laat de USB-kabel aangesloten tot voltooiing. Keyboard acties worden opgeschort in DeckPilot terwijl het firmware-dialoogvenster geopend is; gewone F13

De firmware en Windows AVRDUDE 8.0 worden gebundeld in de gepubliceerde app. Geen Arduino IDE of afzonderlijke driver installatie wordt uitgevoerd door DeckPilot. Windows moet de USB seriële poort van het bord al blootleggen, zoals het deed voor de succesvolle upload van de eigenaar. Nieuwe firmwareversies worden geleverd met updates van de app van GitHub; het bijwerken van de app doet **niet** automatisch flash hardware. Elke flits vereist bevestiging.

DeckPilot Atelier houdt **Effacer le firmware de la carte** onder ingestorte geavanceerde bewerkingen. `EFFACER`, ontruimt alles 28 KiB van toepassing flash met een geverifieerde reset loop, en stopt F13 **RST** tot **GND tweemaal snel**, selecteer dan de tijdelijke Arduino Leonardo / bootloader COM-poort en gebruik Atelier herstel actie om opnieuw te installeren DeckPilot. Atelier toont deze instructies en houdt herstel ingeschakeld na het wissen of een onderbroken flitser. De bootloader en EEPROM worden bewaard; dit is geen full-chip wissen. Profielgegevens blijven op de PC.

DeckPilot controleert SHA-256, Intel HEX registreren controlesums, reset-vector aanwezigheid, overlappingen en de **28672-byte** toepassingsgrens voor de reset. Het komt overeen met de fysieke USB-locatie over de runtime- en bootloaderpoorten, gebruikt AVR109 op 57600 baud, vereist AVRDUDE's schrijfverificatie, controleert dan de identiteit en versie van de draaiende firmware. Het schrijft geen zekeringen, EEPROM of de bootloader. Alleen Leonardo ID's `2341:8036` / `2A03:8036` en Caterina `2341:0036` / `2A03:0036` worden geaccepteerd. Als Windows geen passende fysieke verbinding kan bieden, wordt knipperen eerder geblokkeerd dan raden van een poort.

Flitsen is niet atomair: een kabel loskoppelen of stroomverlies kan de toepassing firmware incompleet laten. Er is geen automatische firmware back-up of rollback. Indien nodig, herupload de originele schets met Arduino IDE ingesteld op **Arduino Leonardo**. Afhankelijk van de bootloader van het bord, kan kort aansluiten van RST op GND tweemaal een tijdelijke bootloader poort ontmaskeren; uploaden terwijl die poort beschikbaar is. Een beschadigde bootloader vereist ISP hardware en kan niet worden gerepareerd door deze USB updater. Schrijf geen andere board of klokvariant.

Het laatst voltooide AVRDUDE-logboek van de belangrijkste app wordt opgeslagen op `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`. Atelier houdt logs installeren en resetten in `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log` en `last-reset.log`.

<a id="build"></a>
## Bouwen

Op Windows, vanaf de repository root, installeer Arduino CLI **1.2.2** en .NET SDK 8, dan rennen:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

Het script pinnen Arduino AVR core **1.8.6**, Keyboard **1.0.6**, en stroomopwaarts AVRDUDE **8.0**. CLI-beheerde pakketten gebruiken Arduino's package checksum validatie; AVRDUDE binaire/bronarchieven hebben expliciete SHA-256 Pinnen in het script. `artifacts/firmware/DeckPilot-firmware.zip` bevat de DeckPilot HEX, gegenereerde applicatie-gebied reset HEX en zijn generator, checksums, Windows-tool, licentie en bijbehorende upstream AVRDUDE bron archief. Publishing CI vereist de bundel; een gewone ontwikkelaar bouwen zonder het kan de app draaien, maar kan niet knipperen of een board resetten.

Het origineel 12- De schets van de knoop is aangepast aan die van David Madison. **Stroom Goedkope** project, auteursrecht 2018MIT, zijn bericht blijft in `DeckPilot.ino`. DeckPilot toevoegingen maken gebruik van de repository MIT licentie. **AVRDUDE is aparte software onder GPL-2.0-of-later**, met de aankondiging/licentie en bron gebundeld; de repository MIT licentie vervangt die licentie niet. https://github.com/avrdudes/avrdude/tree/v8.0 voor instructies voor upstream-bouw en notificaties voor afhankelijkheid.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 en USB-protocol 2

Firmware 1.1.1 voegt een read-only kenmerkende link met behoud van dezelfde F13.F24 toetsenbord mapping en pin-1 Opstart faalt veilig. **De fysieke test van de eigenaar bedekt firmware 1.0.0- Firmware 1.1.1 heeft geautomatiseerde protocol tests en een AVR build, maar moet nog steeds zijn eerste test op de werkelijke board.** Profielen en kalibratie blijven op de PC en hoeven niet opnieuw te worden gemaakt voor deze firmware-update.

De nieuwe app identificeert de aangesloten kaart, houdt de seriële poort open, maakt na het uitpluggen weer verbinding en controleert de firmware-versie bij het opstarten en na het opnieuw aansluiten. Firmware-updates worden geleverd met app-releases: opstartcontroles vergelijken de aangesloten firmware met de gevalideerde bundel in de geïnstalleerde app. App-updatecontroles verkrijgen nog nieuwere releases van GitHub. Er is geen aparte firmware-alleen downloadkanaal.

Een oudere gedetecteerde firmware vraagt één keer per app sessie, met een 24-uurherinnering vertraging bij uitstel. Meldingen kunnen worden uitgeschakeld onder **Connexion et diagnostic**. Onbekende Arduino schetsen, losgekoppelde apparaten en dubbelzinnige multiple-card selecties leiden niet automatisch knipperen of updateprompts. Firmware installatie vereist altijd bevestiging. Het openen van de firmware manager schorst de monitor en wacht tot de seriële poort wordt vrijgegeven; monitoring hervat nadat het dialoogvenster sluit.

**Voor een handmatige upload van Arduino IDE, stop DeckPilot van zijn dienblad pictogram eerst.** Het sluiten van het hoofdvenster verbergt het alleen, dus het blijft de eigenaar van de seriële verbinding. DeckPilot en een andere seriële monitor kan dezelfde poort niet gelijktijdig gebruiken. Als een ander hulpmiddel al eigenaar is van de poort, rapporteert de app het probleem en herhaalt het zonder de gewone toetsenbordtoetsen te beïnvloeden.

De kenmerkende modus in `DeckPilotProtocol.h` maakt gebruik van buffers van vaste grootte, begrensde invoerverwerking en beschikbare USB-transmissieruimte. Het nieuwste diagnostische masker wordt onder tegendruk gecoald. Firmware 1.2.0 voegt de opt-in seriële invoermodus hierboven toe.

Op 115200 baud, regels eindigen met LF (CRLF wordt geaccepteerd). Sessietekens bevatten precies acht hexadecimale tekens in hoofdletters. Alle getallen zijn niet ondertekend decimale tekens. Voorbeelden:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` adverteerders 12 sleutels en vermogensbits: 1 = hartslag/state snapshot, 2 = gebeurtenissen in de sleuteltoestand, 4 = opt-in seriële invoer. `PONG` echo's van de verzoek teller en rapporten uptime, sequentie en knop masker voor diagnostiek. `KEYS` rapporteert reeks en masker; bit 0 correspondeert met F13 en bit 11 naar F24, onafhankelijk van fysieke kalibratie. Sessies en verzoektellers weigeren oude antwoorden. In seriële invoermodus volgen de acties de bestelde `KEYS` Frames.

Hartslag verzoeken worden gewoonlijk elke twee seconden verzonden. De app opnieuw verbinding als een passend antwoord ontbreekt voor 1.5 seconden; de firmware stopt ongevraagde diagnostiek na zes seconden zonder hartslag of wanneer de seriële verbinding sluit. Firmware 1.0.0 blijft ondersteund door de legacy identiteit query, zonder levende sleutel diagnostiek. DeckPilot apps kunnen nog steeds de oude identiteit van firmware lezen 1.1.1.

De bronversie in `DeckPilotProtocol.h` levert de release manifest versie. Gepubliceerde firmware bundels nu bevatten ook de huidige schets / header onder `source/DeckPilot`, de herstelschets en de DeckPilot MIT licentie.

De kenmerkende koppeling maakt bewust gebruik van `Serial.dtr()` in plaats van `bool(Serial)`: Arduino AVR kern 1.8.6 plaatst a 10 ms vertraging `Serial_::operator bool()`. Het lezen van DTR houdt direct het kenmerkende pad van het toevoegen van die pauze aan elke key-scan cyclus. De host protocol test verwijdert de impliciete bool conversie, zodat dit blokkeren patroon kan niet opnieuw in de gedeelde protocol implementatie onopgemerkt.

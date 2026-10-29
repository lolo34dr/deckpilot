> ▁Dit is▁een▁automatische▁vertaling van de▁volledige firmware▁documentatie.▁Het▁Engelse▁origineel is de▁referentie. Opdrachten en▁identificaties▁worden▁precies▁bewaard.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

▁Alleen voor de▁geteste hardwareconfiguratie: **Pro Micro ATmega32U4, 5 V / 16 MHz**, met▁gebruikmaking van de **Arduino▁Leonardo** board▁profiel en a **Caterina / AVR109** bootloader. USB-C▁beschrijft de connector,▁niet de boardcompatibiliteit. De▁oorspronkelijke▁schets van de▁eigenaar is▁succesvol geüpload met de 1200-baud reset en AVR109 op 57600 baud. De▁eigenaar▁heeft▁ook▁bevestigd▁succesvol in-app▁knipperen, firmware 1.0.0 ▁identificatie en▁werking van▁alle 12 Knoopjes op▁dat naslagbord.

<a id="wiring-and-behavior"></a>
## Bedrading en▁gedrag

▁Elke schakelaar▁verbindt▁één pin met▁GND.▁Interne pull-ups▁zijn▁ingeschakeld. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** ▁verzenden **F13▁tot en met F24** ▁Een▁stal in die▁volgorde. 30 ms debounce handvatten▁drukken en▁vrijgeven▁onafhankelijk van▁elkaar.▁Toetsenbordgedrag, Windows-side▁profielen en▁kalibratie▁blijven▁compatibel; USB-toetsenbordroller▁blijft▁beperkt door de▁toetsenbordbibliotheek van Arduino. De firmware introduceert▁nog▁geen USB-modi per knop of native media controls.

Pin **1** ▁wordt▁vastgehouden▁aan▁GND▁bij▁het▁opstarten▁schakelt de▁toetsenbordbewerking▁uit,▁waarbij de▁originele failsafe▁behouden▁blijft. De USB-kern▁kan▁nog▁steeds bootloader-invoer▁ondersteunen. Verbind▁VNC▁niet met▁GND.

De▁huidige USB CDC seriële interface▁antwoorden `DECKPILOT?\n` op 115200 brood met:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

Er is▁geen seriële▁verbinding▁vereist voor▁normale▁toetsenbordbewerking. De▁identiteit is▁een▁compatibiliteitscontrole,▁niet cryptografische hardware-authenticatie. Windows▁rapporteert▁nog▁steeds de Arduino▁Leonardo USB-identificaties;▁veel▁andere boards▁delen die▁identificaties.

<a id="install-from-deckpilot"></a>
## Installeren▁vanaf DeckPilot

1. ▁Houd de▁meegeleverde▁herstel▁schets (`firmware/Original/Original.ino`) en▁een▁werkende Arduino upload setup▁als▁een▁herstel▁optie.▁Houd RST en▁GND▁fysiek▁toegankelijk.
2. ▁Sluit Arduino IDE, seriële monitor en▁andere seriële tools. Koppel▁andere▁Leonardo boards.
3. Open **Firmware du macropad…**,▁kies▁het▁juiste▁apparaat en▁bevestig▁het bord en de bedrading.▁Een▁ongeïdentificeerde▁originele▁schets▁kan▁alleen▁worden▁geïnstalleerd na▁uitdrukkelijke▁bevestiging.
4. ▁Bevestig▁installatie en▁laat de USB-kabel▁aangesloten▁tot▁voltooiing. Keyboard▁acties▁worden▁opgeschort in DeckPilot ▁terwijl▁het firmware▁dialoogvenster▁geopend is;▁gewone F13_F24▁gebeurtenissen▁bereiken Windows.

De firmware en Windows AVRDUDE 8.0 ▁worden▁gebundeld in de▁gepubliceerde app.▁Geen Arduino IDE of▁aparte▁bestuurder▁installatie▁wordt▁uitgevoerd door DeckPilot. Windows▁moet de USB seriële▁poort van▁het bord al▁blootleggen,▁zoals▁het▁deed voor de▁succesvolle upload van de▁eigenaar.▁Nieuwe firmwareversies▁worden▁geleverd met updates van de app van GitHub;▁het▁bijwerken van de app▁doet **▁niet** ▁automatisch▁flits hardware.▁Elke▁flits▁vereist▁bevestiging.

DeckPilot ▁controleert SHA-256, Intel HEX▁registreren▁controlesums, reset-vector▁aanwezigheid, overlappingen en de **28672-byte** ▁toepassingsgrens voor de reset.▁Het▁komt▁overeen met de▁fysieke USB-locatie over de runtime en bootloader▁poorten,▁gebruikt AVR109 op 57600 baud,▁vereist AVRDUDE's▁schrijfverificatie,▁controleert▁dan de▁identiteit en▁versie van de▁draaiende firmware.▁Het▁schrijft▁geen▁zekeringen, EEPROM of de bootloader.▁Alleen▁Leonardo ID's `2341:8036` / `2A03:8036` en Caterina `2341:0036` / `2A03:0036` ▁worden▁geaccepteerd.▁Als Windows▁geen▁passende▁fysieke▁verbinding▁kan▁bieden,▁wordt▁knipperen▁eerder▁geblokkeerd▁dan▁raden van▁een▁poort.

Flitsen is▁niet atomair:▁een▁kabel▁loskoppelen of▁stroomverlies▁kan de▁toepassing firmware incompleet▁laten. Er is▁geen▁automatische firmware back-up of▁rollback.▁Indien▁nodig,▁opnieuw uploaden van de▁originele▁schets met Arduino IDE▁ingesteld op **Arduino▁Leonardo**.▁Afhankelijk van de bootloader van▁het bord,▁kan▁kort▁aansluiten van RST op▁GND▁tweemaal▁een▁tijdelijke bootloader▁poort▁ontmaskeren; uploaden▁terwijl die▁poort▁beschikbaar is.▁Een▁beschadigde bootloader▁vereist▁ISP hardware en▁kan▁niet▁worden▁gerepareerd door▁deze USB update. Schrijf▁geen▁andere board of▁klokvariant.

▁Het▁laatst▁voltooide AVRDUDE▁proceslogboek▁wordt▁opgeslagen op `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## ▁Bouwen

Op Windows,▁vanaf de repository root, installeer Arduino CLI **1.2.2** en .NET SDK 8,▁dan▁rennen:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

▁Het script pinnen Arduino AVR▁kern **1.8.6**, Keyboard **1.0.6**, en▁stroomopwaarts AVRDUDE **8.0**. CLI-beheerde▁pakketten▁gebruiken Arduino's package checksum▁validatie; AVRDUDE▁binaire/bronarchieven▁hebben▁expliciete SHA-256 Pinnen in▁het script. `artifacts/firmware/DeckPilot-firmware.zip` ▁bevat de HEX, manifest, Windows tool,▁licentie en▁bijbehorende upstream AVRDUDE▁bron▁archief.▁Publishing CI▁vereist de▁bundel;▁een▁gewone ontwikkelaar▁bouwen▁zonder▁het▁kan de app▁uitvoeren,▁maar▁kan▁niet▁flitsen.

▁Het▁origineel 12- De▁schets van de▁knoop is▁aangepast▁aan David Madison's **▁Stroom▁Goedkope** project,▁auteursrecht 2018MIT,▁zijn▁bericht▁blijft▁binnen. `DeckPilot.ino`. DeckPilot ▁toevoegingen▁maken▁gebruik van de repository MIT▁licentie. **AVRDUDE is▁aparte software▁onder GPL-2.0-of-later**, met de▁aankondiging/licentie en▁bron▁gebundeld; de repository MIT▁licentie▁vervangt die▁licentie▁niet. https://github.com/avrdudes/avrdude/tree/v8.0 voor▁instructies voor upstreambouw en▁kennisgeving van▁afhankelijkheid.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 en USB-protocol 2

Firmware 1.1.1 ▁voegt▁een read-only▁kenmerkende link met▁behoud van▁dezelfde F13.F24▁toetsenbord mapping en pin-1 Opstart faalt▁veilig. **De▁fysieke test van de▁eigenaar▁bedekt firmware 1.0.0- Firmware 1.1.1 ▁heeft geautomatiseerde protocol tests en▁een AVR▁build,▁maar▁moet▁nog▁steeds▁zijn▁eerste test op de▁werkelijke board.** ▁Profielen en▁kalibratie▁blijven op de PC en▁hoeven▁niet▁opnieuw te▁worden▁gemaakt voor▁deze firmware-update.

De▁nieuwe app▁identificeert de▁aangesloten▁kaart,▁houdt de seriële▁poort open, herkoppelt na▁het▁uitpluggen, en▁controleert de firmware-versie▁bij▁het▁opstarten en na▁het▁opnieuw▁aansluiten. Firmware-updates▁worden▁geleverd met app-releases: opstartcontroles▁vergelijken de▁aangesloten firmware met de▁gevalideerde▁bundel in de▁geïnstalleerde app. App-updatecontroles▁verkrijgen▁nog▁steeds▁nieuwere releases van GitHub. Er is▁geen▁aparte firmware-alleen downloadkanaal.

▁Een▁oudere▁gedetecteerde firmware▁vraagt▁één▁keer per app sessie, met▁een 24-uurherinnering▁vertraging▁bij▁uitstel.▁Meldingen▁kunnen▁worden▁uitgeschakeld▁onder **Connexion et diagnostic**.▁Onbekende Arduino▁schetsen,▁losgekoppelde▁apparaten en▁dubbelzinnige multi-card▁selecties▁leiden▁niet▁automatisch▁knipperen of updateprompts. Firmware▁installatie▁vereist▁altijd▁bevestiging.▁Het▁openen van de firmware manager▁schorst de monitor en▁wacht▁tot de seriële▁poort▁wordt▁vrijgegeven; monitoring▁hervat▁nadat▁het▁dialoogvenster▁sluit.

**▁Voor▁een▁handmatige upload van Arduino IDE, stop DeckPilot van▁zijn▁dienblad pictogram▁eerst.** ▁Het▁sluiten van▁het▁hoofdvenster verbergt▁het▁alleen,▁dus▁het▁blijft de▁eigenaar van de seriële▁verbinding. DeckPilot en▁een▁andere seriële monitor▁kan▁dezelfde▁poort▁niet▁tegelijkertijd▁gebruiken.▁Als▁een▁ander▁hulpmiddel al▁eigenaar is van de▁poort,▁rapporteert de app▁het▁probleem en▁herhaalt▁het▁zonder de▁gewone▁toetsenbordtoetsen te▁beïnvloeden.

▁Het▁nieuwe `DeckPilotProtocol.h` is▁dezelfde▁uitvoering▁uitgeoefend door `tests/firmware-protocol.cpp`.▁Het▁maakt▁gebruik van fixed-size buffers,▁begrensde▁invoerverwerking en▁beschikbare USB-ruimte▁verzenden; knop scannen▁wordt▁nooit▁opzettelijk▁vertraagd▁tijdens▁het▁wachten op▁een host. De▁nieuwste▁hangende knop masker▁wordt gecoalesced▁onder▁tegendruk. Er▁zijn▁geen▁commando's om macro's▁uit te▁voeren, EEPROM▁schrijven,▁uitschakelen van▁het▁toetsenbord,▁wijzigen▁profielen of flash firmware over▁dit▁kenmerkende protocol.

▁Bij 115200 baud,▁regels▁eindigen met LF (CRLF▁wordt▁geaccepteerd). Sessietekens▁bevatten▁precies▁acht hexadecimale▁tekens in▁hoofdletters.▁Alle▁getallen▁zijn▁niet▁ondertekend decimale▁tekens.▁Voorbeelden:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` adverteerders 12 ▁sleutels en▁vermogensbits: 1 =▁hartslag/state snapshot, 2 =▁key-state▁gebeurtenissen. `PONG` echo's van▁het▁verzoek teller,▁dan▁rapporteert uptime in milliseconden,▁staat sequentie en de 12-Bitknop masker. `KEYS` ▁rapporteert sequentie en masker; bit 0 is F13 en bit 11 is F24,▁onafhankelijk van▁fysieke▁kalibratie. Sessions en▁verzoektellers▁weigeren▁oude▁antwoorden. Sequentievergelijkingen handvat 32-bit wraparound.▁Sleutels▁blijven▁alleen▁kenmerkende▁gegevens: app-acties▁draaien▁nog▁één▁keer door▁het▁bestaande▁toetsenbordpad,▁dus▁een seriële▁gebeurtenis▁kan▁niet▁dezelfde▁actie▁twee▁keer▁uitvoeren.

▁Hartslag▁verzoeken▁worden▁meestal▁elke▁twee▁seconden▁verzonden. De app▁opnieuw▁verbinding▁als▁een▁passend▁antwoord▁ontbreekt voor 1.5 ▁seconden; de firmware▁stopt ongevraagde diagnostiek na▁zes▁seconden▁zonder▁hartslag of▁wanneer de seriële▁verbinding▁sluit. Firmware 1.0.0 ▁blijft▁ondersteund door de legacy▁identiteit query,▁zonder▁levende▁sleutel diagnostiek. DeckPilot apps▁kunnen▁nog▁steeds de▁oude▁identiteit van firmware▁lezen 1.1.1.

De▁bronversie in `DeckPilotProtocol.h` ▁levert de release manifest▁versie.▁Gepubliceerde firmware▁bundels nu▁bevatten▁ook de▁huidige▁schets / header▁onder `source/DeckPilot`, de▁herstelschets en de DeckPilot MIT▁licentie.

▁Het▁kenmerkende▁verband▁maakt▁bewust▁gebruik van `Serial.dtr()` in▁plaats van `bool(Serial)`: Arduino AVR▁kern 1.8.6 ▁plaatst a 10 ms▁vertraging in `Serial_::operator bool()`.▁Het▁lezen van DTR▁houdt direct▁het▁kenmerkende pad van▁het▁toevoegen van die pauze▁aan▁elke▁key-scan▁cyclus. De host protocol test▁verwijdert de▁impliciete bool▁conversie,▁zodat▁dit▁blokkeren▁patroon▁kan▁niet▁worden▁opnieuw in de▁gedeelde protocol▁implementatie ongemerkt.

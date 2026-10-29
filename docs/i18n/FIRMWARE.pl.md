> Jest to automatyczne tłumaczenie kompletnej dokumentacji firmware. Angielski oryginał jest referencją. Polecenia i identyfikatory są dokładnie zachowane.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Tylko dla testowanej konfiguracji sprzętowej: **Pro Micro ATmega32U4, 5 V / 16 MHz**, za pomocą **Arduino Leonardo** profil planszowy i **Caterina / AVR109** bootloader. USB- C opisuje złącze, a nie kompatybilność planszy. Oryginalny szkic właściciela został pomyślnie przesłany z 1200-baud reset i AVR109 at 57600 baud. Właściciel potwierdził również sukces in- app miganie, firmware 1.0.0 identyfikacja i działanie wszystkich 12 przyciski na tablicy odniesienia.

<a id="wiring-and-behavior"></a>
## Uzyskanie i zachowanie

Każdy przełącznik łączy jeden pin z GND. Wewnętrzne pull- ups są włączone. **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** wysyłaj **F13 do F24** Stajnia. 30 ms debounce hands naciśnij i wydaj niezależnie. Zachowanie klawiatury, profile boczne Windows- i kalibracja pozostają kompatybilne; przewracanie klawiatury USB pozostaje ograniczone przez bibliotekę klawiatury Arduino. Oprogramowanie firmowe nie wprowadza jeszcze trybów nadguzika USB lub native media controls.

Pin **1** Trzyma się GND przy startup wyłącza obsługę klawiatury, zachowując oryginalne zabezpieczenie. Rdzeń USB może nadal obsługiwać wejście bootloader. Nie podłącz VCC do GND.

Aktualny interfejs szeregowy USB CDC odpowiada `DECKPILOT?\n` at 115200 baud z:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

Dla normalnej pracy klawiatury nie jest wymagane połączenie szeregowe. Tożsamość to kontrola zgodności, a nie uwierzytelnianie sprzętu kryptograficznego. Windows nadal zgłasza identyfikatory USB Arduino Leonardo; wiele innych tablic dzieli się tymi identyfikatorami.

<a id="install-from-deckpilot"></a>
## Instalacja z DeckPilot

1. Należy zachować dostarczony szkic odzyskiwania (`firmware/Original/Original.ino`) i działająca konfiguracja wysyłania Arduino jako opcja odzyskiwania. Utrzymuj RST i GND fizycznie dostępne.
2. Zamknij Arduino IDE, Serial Monitor i inne narzędzia seryjne. Rozłącz inne deski Leonardo.
3. Otwórz **Firmware du macropad…**, wybrać odpowiednie urządzenie i potwierdzić deskę i okablowanie. Niezidentyfikowany oryginalny szkic może być zainstalowany tylko po wyraźnym potwierdzeniu.
4. Potwierdź instalację i pozostaw podłączony kabel USB do zakończenia. DeckPilot podczas gdy okno dialogowe firmware jest otwarte; zwykłe zdarzenia F13- F24 nadal docierają do systemu Windows.

Oprogramowanie firmowe i Windows AVRDUDE 8.0 są połączone wewnątrz opublikowanej aplikacji. Nie Arduino IDE lub oddzielnej instalacji sterownika jest wykonywana przez DeckPilot. Windows musi już zdemaskować port szeregowy płyty USB, tak jak to zrobił dla pomyślnego wysłania przez właściciela. Nowe wersje firmware są dostarczane z aktualizacjami aplikacji z GitHub; aktualizacja aplikacji robi **nie** automatycznie błyskać sprzętem. Każdy błysk wymaga potwierdzenia.

DeckPilot kontroluje Sha-256, Intel HEX rekordowe sumy kontrolne, obecność wektora przesiedlenia, nakładanie się i **28672-byte** Granica aplikacji przed resetem. Pasuje do fizycznej lokalizacji USB w portach runtime i bootloader, używa AVR109 w 57600 baud, wymaga weryfikacji na piśmie AVRDUDE, a następnie sprawdza tożsamość i wersję uruchomionego oprogramowania firmowego. Nie pisze bezpieczników, EEPROM lub bootloader. Tylko Leonardo ID `2341:8036` / `2A03:8036` i Caterina `2341:0036` / `2A03:0036` są akceptowane. Jeśli system Windows nie może zapewnić dopasowanego połączenia fizycznego, miganie jest blokowane zamiast zgadywania portu.

Flashing nie jest atomowe: odłączenie kabla lub utrata mocy może pozostawić aplikację firmware niekompletny. Nie ma automatycznego tworzenia kopii zapasowych oprogramowania firmowego lub rollback. W razie potrzeby, ponownie przesłać oryginalny szkic z Arduino IDE zestaw do **Arduino Leonardo**. W zależności od bootloadera planszy, krótkie podłączenie RST do GND dwa razy może ujawnić tymczasowy port bootloadera; przesłać podczas gdy ten port jest dostępny. Uszkodzony bootloader wymaga sprzętu ISP i nie może być naprawiony przez ten updater USB. Nie pisz innej płyty lub opcji zegara.

Ostatni wypełniony dziennik procesów AVRDUDE jest przechowywany w `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## Budowanie

W systemie Windows, z głównego repozytorium, zainstalować Arduino CLI **1.2.2** oraz .NET SDK 8, następnie uruchomić:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

Scenariusz przyciska rdzeń Arduino AVR **1.8.6**, Klawiatura **1.0.6**oraz AVRDUDE **8.0**. CLI- zarządzane pakiety używają walidacji sumy kontrolnej pakietu Arduino; AVRDUDE binarne / źródłowe archiwa mają wyraźny SHA-256 Pinezki w scenariuszu. `artifacts/firmware/DeckPilot-firmware.zip` zawiera HEX, manifest, narzędzie Windows, licencja i odpowiednie archiwum źródłowe AVRDUDE. Publikowanie CI wymaga pakietu; zwykły deweloper build bez niego może uruchomić aplikację, ale nie może błysnąć.

Oryginał 12-szkic guzika został dostosowany z David Madison 's **Strumień tani** project, copyright 2018, MIT. Jego zawiadomienie pozostaje w `DeckPilot.ino`. DeckPilot dodatki korzystają z licencji MIT repozytorium. **AVRDUDE to osobne oprogramowanie w ramach GPL-2.0- Albo później.**, z jego notice / licence i source wiązane; repozytorium MIT licencja nie zastępuje tej licencji. Zobacz https://github.com/avrdudes/avrdude/tree/v8.0 w odniesieniu do instrukcji dotyczących rozwoju wyższego szczebla i powiadomień o zależności.

<a id="firmware-111-and-usb-protocol-2"></a>
## Oprogramowanie firmowe 1.1.1 oraz protokół USB 2

Oprogramowanie firmowe 1.1.1 dodaje tylko read- link diagnostyczny przy zachowaniu tego samego mapowania klawiatury F13- F24 i pin-1 Uruchom zabezpieczenie. **Test fizyczny właściciela obejmował oprogramowanie firmowe 1.0.0. Firmware 1.1.1 posiada zautomatyzowane testy protokołu i budowę AVR, ale nadal potrzebuje swojego pierwszego testu na rzeczywistej tablicy.** Profile i kalibracja pozostają na komputerze PC i nie muszą być odtwarzane dla tej aktualizacji oprogramowania firmowego.

Nowa aplikacja identyfikuje podłączoną kartę, utrzymuje port szeregowy otwarty, ponownie łączy się po odłączeniu i sprawdza swoją wersję oprogramowania firmowego przy starcie i ponownym podłączeniu. Aktualizacje oprogramowania firmowego są dostarczane z wydaniami aplikacji: kontrole uruchamiania porównują podłączone oprogramowanie firmowe z zatwierdzonym pakietem dołączonym do zainstalowanej aplikacji. Sprawdzenia aktualizacji aplikacji nadal otrzymują nowsze wydania z GitHub. Nie ma oddzielnego kanału pobierania tylko firmware-.

Starszy wykryty firmware podpowiada raz na sesję aplikacji, z 24-godziny przypomnienia opóźnienia po odroczeniu. Powiadomienia mogą być wyłączone pod **Connexion et diagnostic**. Nieznane szkice Arduino, odłączone urządzenia i niejednoznaczne wielokrotne wybieranie kart nie wywołują automatycznego migania lub aktualizacji. Instalacja firmware zawsze wymaga potwierdzenia. Otwarcie menedżera firmware zawiesza monitor i czeka na wydanie portu szeregowego; monitoring ponownie rozpoczyna się po zamknięciu okna dialogowego.

**Dla ręcznego wysyłania z Arduino IDE, zrezygnować DeckPilot z ikony na tacce.** Zamknięcie głównego okna tylko go ukrywa, więc nadal posiada połączenie szeregowe. DeckPilot i inny monitor szeregowy nie może używać tego samego portu jednocześnie. Jeśli inne narzędzie jest już właścicielem portu, aplikacja zgłasza problem i ponownie próbuje bez wpływu na zwykłe klawisze klawiatury.

Nowy `DeckPilotProtocol.h` jest to samo wykonanie wykonywane przez `tests/firmware-protocol.cpp`Używa buforów o mocowanych rozmiarach, ograniczonego przetwarzania danych wejściowych i dostępnej przestrzeni transmisji USB; skanowanie przycisków nigdy nie jest celowo opóźnione w oczekiwaniu na hosta. Ostatnia oczekująca maska przycisków jest połączona pod ciśnieniem wstecznym. Nie ma żadnych poleceń do wykonywania makr, zapisu EEPROM, wyłączania klawiatury, modyfikowania profili lub oprogramowania flash przez ten protokół diagnostyczny.

At 115200 baud, linie kończą się na LF (akceptowane jest CRLF). Tokeny sesji zawierają dokładnie osiem znaków szesnastkowych. Wszystkie numery są niepodpisane dziesiętne. Przykłady:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` reklamy 12 klucze i bity funkcji: 1 = migawka bicia serca / stanu, 2 = eventy-state. `PONG` echoes licznik żądań, a następnie raportuje uptime w milisekundach, sekwencja stanu i 12- Maska na guziki. `KEYS` sekwencja raportów i maska; bit 0 jest F13 i bit 11 jest F24, niezależny od fizycznej kalibracji. Sesje i liczniki zapytań odrzucają nieświeże odpowiedzi. 32-bit wraparound. Klucze pozostają tylko danymi diagnostycznymi: akcje aplikacji nadal działają raz przez istniejącą ścieżkę klawiatury, więc zdarzenie seryjne nie może wykonać tego samego działania dwa razy.

Żądania Heartbeat są zwykle wysyłane co dwie sekundy. Aplikacja ponownie łączy się, jeśli brakuje dopasowanej odpowiedzi dla 1.5 seconds; firmware zatrzymuje niezamówioną diagnostykę po sześciu sekundach bez bicia serca lub po zamknięciu połączenia szeregowego. 1.0.0 pozostaje obsługiwane poprzez dotychczasowe zapytanie tożsamości, bez żywej kluczowej diagnostyki. DeckPilot aplikacje mogą nadal odczytywać dziedziczną tożsamość firmware 1.1.1.

Wersja źródłowa w `DeckPilotProtocol.h` dostarcza wersję manifest wydania. Opublikowane pakiety firmware teraz zawierają również aktualny szkic / nagłówek pod `source/DeckPilot`, szkic rekonwalescencji i DeckPilot Licencja MIT.

Połączenie diagnostyczne celowo wykorzystuje `Serial.dtr()` raczej niż `bool(Serial)`: Arduino AVR core 1.8.6 Wstawia 10 ms opóźnienie w `Serial_::operator bool()`. Czytanie DTR bezpośrednio utrzymuje ścieżkę diagnostyczną z dodawania tej pauzy do każdego cyklu key- scan. Test protokołu hosta usuwa domyślną konwersję bool więc ten wzór blokowania nie może być przywrócony do wspólnej implementacji protokołu niezauważony.

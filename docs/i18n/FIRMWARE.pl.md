> Jest to automatyczne tłumaczenie kompletnej dokumentacji firmware. Angielski oryginał jest punktem odniesienia. Polecenia i identyfikatory są dokładnie zachowane.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Tylko dla testowanej konfiguracji sprzętowej: **Pro Micro ATmega32U4, 5 V / 16 MHz**, za pomocą **Arduino Leonardo** profil planszy i **Caterina / AVR109** bootloader. USB- C opisuje złącze, nie kompatybilność planszy. Oryginalny szkic właściciela został pomyślnie przesłany z 1200-baud reset i AVR109 at 57600 baud. Właściciel potwierdził również udany in- app miganie, firmware 1.0.0 identyfikacja i działanie wszystkich 12 przyciski na tablicy odniesienia.

<a id="wiring-and-behavior"></a>
## Obróbka i zachowanie

Każdy przełącznik łączy jeden pin z GND. Włączone są wewnętrzne pull- upy. **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** wysyłaj **F13 do F24** Stajnia. 30 ms debounce uchwyty prasy i wydania niezależnie. Firmware 1.1.2 używa jednego bitu na klucz w raporcie klawiatury USB, aby móc reprezentować wszystkie dwanaście kluczy. Profile Windows i kalibracja używają tych samych kodów klawiszy.

Pin **1** Trzyma się GND przy starcie wyłącza obsługę klawiatury, zachowując oryginalne zabezpieczenie. Rdzeń USB może nadal obsługiwać wejście bootloader. Nie podłącz VCC do GND.

Aktualny interfejs szeregowy USB CDC odpowiada `DECKPILOT?\n` at 115200 baud z:

```
DECKPILOT|1|promicro-32u4-16mhz|1.2.0
```

Dla normalnej pracy klawiatury nie jest wymagane połączenie szeregowe. Tożsamość to kontrola zgodności, a nie uwierzytelnianie sprzętu kryptograficznego. Windows nadal zgłasza identyfikatory USB Arduino Leonardo; wiele innych tablic dzieli się tymi identyfikatorami.

<a id="hybrid-input-mode-120"></a>
## Tryb wejścia hybrydowego (1.2.0)

Obie płyty używają tego samego oprogramowania firmowego. Aplikacja identyfikuje je za pomocą urządzenia Windows USB i otwiera połączenie szeregowe na płytę. **Makroplony**, kliknij klawisz na karcie każdej tablicy, aby włączyć DeckPilot tryb i skonfigurować jego profil. Użyj **Kalibrator** na tej karcie, jeśli jego fizyczne zamówienie guzika różni się. Płyta bez unikalnego numeru USB może uzyskać inną instancję urządzenia, gdy przeniesiony do innego gniazda USB. Zachować każdą taką tablicę na przydzielonym gniazdku.

Po uścisku dłoni DP2 reklamuje możliwości 4, aplikacja wysyła `DP2|INPUT|<session>`. Odpowiedź zarządu z `DP2|INPUT|<session>` gdy wszystkie klucze i raport z klawiatury zostaną wydane, lub `DP2|BUSY|<session>` jeśli klucz jest nadal trzymany. Po uruchomieniu, zarząd wysyła zamówione `KEYS` ramki zaczynające się w kolejności 1 i tłumi F13- F24. Klienci diagnostyczni, które po prostu wysłać `HELLO` zachować normalne zachowanie klawiatury. Upuszczanie DTR lub brakująca dzierżawa bicia serca przez sześć sekund przywraca klawiaturę po zwolnieniu wszelkich przycisków. Pełna kolejka zdarzeń kończy sesję wejścia szeregowego z `DP2|LOST|<session>` zamiast po cichu łączyć wydarzenia prasowe i release.

Test fizyczny na systemie Windows:

1. Błysk i sprawdzić każdą deskę indywidualnie z **Firmware du macropad…**, a następnie podłączyć do obu desek.
2. Otwórz **Makroplony** i kliknij pierwszy przycisk na karcie każdego urządzenia. Przypisz inną akcję do każdego przycisku. Sprawdź, czy edytor określa wybrany port COM i czy każda karta wyświetla swój własny profil.
3. Naciśnij tę samą pozycję na obu pokładach razem, a następnie powtórzyć za pomocą prasy krótkiej i długiej. Każda akcja powinna działać raz na swoim przypisanym profilu.
4. Odłącz jeden pokład, trzymając przycisk, weryfikuj drugi i podłącz go ponownie do oryginalnego gniazda USB. Zakończ DeckPilot z tacki systemowej i potwierdzić świeżą prasę wysyła odpowiedni klawisz F13- F24.

<a id="install-from-deckpilot"></a>
## Instalacja z DeckPilot

1. Należy zachować dostarczony szkic odzysku (`firmware/Original/Original.ino`) i działająca konfiguracja wysyłania Arduino jako opcja odzyskiwania. Zachowaj RST i GND fizycznie dostępne.
2. Zamknij Arduino IDE, Serial Monitor i inne narzędzia seryjne. Rozłącz inne deski Leonardo.
3. Otwórz **Firmware du macropad…**, wybrać odpowiednie urządzenie i potwierdzić deskę i okablowanie. Niezidentyfikowany oryginalny szkic może być zainstalowany tylko po wyraźnym potwierdzeniu.
4. Potwierdź instalację i pozostaw podłączony kabel USB do zakończenia. DeckPilot podczas gdy okno dialogowe firmware jest otwarte; zwykłe zdarzenia F13- F24 nadal docierają do systemu Windows.

Oprogramowanie firmowe i Windows AVRDUDE 8.0 są połączone wewnątrz opublikowanej aplikacji. Nie Arduino IDE lub oddzielnej instalacji sterownika jest wykonywana przez DeckPilot. Windows musi już zdemaskować port szeregowy USB płyty, tak jak to zrobił dla pomyślnego wysłania właściciela. Nowe wersje oprogramowania firmowego są dostarczane z aktualizacjami aplikacji z GitHub; aktualizacja aplikacji robi **nie** automatycznie błyskać sprzętem. Każdy błysk wymaga potwierdzenia.

DeckPilot Atelier trzyma **Effacer le firmware de la carte** pod zawalonymi zaawansowanymi operacjami. Wymaga maszynopisania `EFFACER`, clears all 28 KiB aplikacji flash z zweryfikowaną pętlą resetową i zatrzymuje F13- F24 i normalne wyliczenie USB. Caterina pozostaje dostępna, ale karta nie pojawi się ponownie w Atelier, dopóki nie zostanie ręcznie umieszczony w trybie bootloadera. **RST** do **PNB dwa razy szybko**, następnie wybrać swój tymczasowy Arduino Leonardo / bootloader port COM i użyć Atelier działania odzyskiwania ponownie zainstalować DeckPilot. Atelier wyświetla te instrukcje i utrzymuje odzyskiwanie włączone po wykasowaniu lub przerywanym błysku. Bootloader i EEPROM są zachowane; nie jest to pełnowymiarowe wymazanie chipa. Dane profilowe pozostają na komputerze.

DeckPilot sprawdza SHA-256, Intel HEX rekordowe sumy kontrolne, obecność wektora przesiedlenia, nakładanie się i **28672-byte** Granica aplikacji przed resetem. Pasuje do fizycznej lokalizacji USB w portach runtime i bootloader, używa AVR109 w 57600 baud, wymaga weryfikacji na piśmie AVRDUDE, a następnie sprawdza tożsamość i wersję uruchomionego oprogramowania firmowego. Nie pisze bezpieczników, EEPROM lub bootloader. Tylko Leonardo ID `2341:8036` / `2A03:8036` i Caterina `2341:0036` / `2A03:0036` są akceptowane. Jeśli system Windows nie może zapewnić pasującego połączenia fizycznego, miganie jest blokowane zamiast zgadywania portu.

Flashing nie jest atomowe: odłączenie kabla lub utrata mocy może pozostawić aplikację firmware niekompletny. Nie ma automatycznego tworzenia kopii zapasowej oprogramowania firmowego lub rollback. W razie potrzeby, ponownie przesłać oryginalny szkic z Arduino IDE zestaw do **Arduino Leonardo**. W zależności od bootloadera planszy, krótkie podłączenie RST do GND dwa razy może ujawnić tymczasowy port bootloadera; przesłać podczas gdy ten port jest dostępny. Uszkodzony bootloader wymaga sprzętu ISP i nie może być naprawiony przez ten updater USB. Nie pisz innej płyty lub opcji zegara.

Ostatni wypełniony dziennik AVRDUDE aplikacji głównej jest przechowywany w `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`. Atelier utrzymuje zainstalować i zresetować logi w `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log` oraz `last-reset.log`.

<a id="build"></a>
## Buduj

W systemie Windows, z repozytorium root, zainstalować Arduino CLI **1.2.2** oraz .NET SDK 8, następnie uruchomić:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

Scenariusz przyciska rdzeń Arduino AVR **1.8.6**, Klawiatura **1.0.6**oraz AVRDUDE **8.0**. CLIzarządzane przez CLI- pakiety używają walidacji sumy kontrolnej pakietu Arduino; archiwa binarne / źródłowe AVRDUDE mają wyraźny SHA-256 Pinezki w scenariuszu. `artifacts/firmware/DeckPilot-firmware.zip` zawiera DeckPilot HEX, wygenerowany obszar aplikacji zresetował HEX i jego generator, sumy kontrolne, narzędzie Windows, licencja i odpowiednie archiwum źródłowe AVRDUDE. Publikowanie CI wymaga pakietu; zwykły deweloper build bez niego może uruchomić aplikację, ale nie może błysnąć lub zresetować płyty.

Oryginał 12-szkic guzika został dostosowany z David Madison 's **Strumień tani** project, copyright 2018, MIT. Jego zawiadomienie pozostaje w `DeckPilot.ino`. DeckPilot dodatki korzystają z licencji MIT repozytorium. **AVRDUDE jest oddzielnym oprogramowaniem w ramach GPL-2.0- Albo później.**, ze swoim powiadomieniem / licencją i źródłem dołączonym; licencja MIT repozytorium nie zastępuje tej licencji. Zobacz https://github.com/avrdudes/avrdude/tree/v8.0 w odniesieniu do instrukcji dotyczących budowania wyższego szczebla i powiadomień o zależności.

<a id="firmware-111-and-usb-protocol-2"></a>
## Oprogramowanie firmowe 1.1.1 i protokół USB 2

Oprogramowanie firmowe 1.1.1 dodaje tylko read- link diagnostyczny przy zachowaniu tego samego mapowania klawiatury F13- F24 i pin-1 Uruchom zabezpieczenie. **Fizyczny test właściciela obejmował oprogramowanie firmowe 1.0.0. Firmware 1.1.1 posiada zautomatyzowane testy protokołu i budowę AVR, ale nadal potrzebuje swojego pierwszego testu na rzeczywistej tablicy.** Profile i kalibracja pozostają na komputerze PC i nie muszą być odtwarzane dla tej aktualizacji oprogramowania firmowego.

Nowa aplikacja identyfikuje podłączoną kartę, utrzymuje port szeregowy otwarty, ponownie łączy się po odłączeniu i sprawdza jej wersję oprogramowania firmowego przy starcie i ponownym połączeniu. Aktualizacje oprogramowania są dostarczane z wydaniami aplikacji: kontrole uruchamiania porównują podłączone oprogramowanie firmowe z zatwierdzonym pakietem dołączonym do zainstalowanej aplikacji. Sprawdzenia aktualizacji aplikacji nadal otrzymują nowsze wydania z GitHub. Nie ma oddzielnego kanału pobierania tylko firmware-.

Starszy wykryty firmware podpowiada raz na sesję aplikacji, z 24-godzina przypomnienia opóźnienie po odroczeniu. Powiadomienia mogą być wyłączone pod **Connexion et diagnostic**. Nieznane szkice Arduino, odłączone urządzenia i niejednoznaczne wielokrotne wybieranie kart nie wywołują automatycznego migania lub aktualizacji. Instalacja firmware zawsze wymaga potwierdzenia. Otwarcie menedżera firmware zawiesza monitor i czeka na wydanie portu szeregowego; monitoring ponownie uruchamia się po zamknięciu okna dialogowego.

**Dla ręcznego wysyłania z Arduino IDE, zrezygnować DeckPilot najpierw z ikony tacki.** Zamknięcie głównego okna tylko go ukrywa, więc nadal posiada połączenie szeregowe. DeckPilot i inny monitor szeregowy nie może używać tego samego portu jednocześnie. Jeśli inne narzędzie jest już właścicielem portu, aplikacja zgłasza problem i ponownie próbuje bez wpływu na zwykłe klawisze klawiatury.

Tryb diagnostyczny w `DeckPilotProtocol.h` korzysta z buforów o wielkości mocowanej, ograniczonego przetwarzania danych wejściowych i dostępnej przestrzeni transmisji USB. Najnowsza oczekująca maska diagnostyczna jest sprzęgana pod ciśnieniem wstecznym. 1.2.0 dodaje opt- w trybie wprowadzania szeregowego powyżej.

At 115200 baud, linie kończą się na LF (akceptowane jest CRLF). Tokeny sesji zawierają dokładnie osiem znaków szesnastkowych w górnej skrzyni. Wszystkie numery są niepodpisane po przecinku. Przykłady:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` reklamy 12 klawisze i bity funkcji: 1 = migawka bicia serca / stanu, 2 = zdarzenia stanu key-, 4 = opt- in serial input. `PONG` echoes licznik żądań i raportuje uptime, sekwencji i maski przycisk do diagnostyki. `KEYS` sekwencja raportów i maska; bit 0 odpowiada F13 i bit 11 do F24, niezależnie od fizycznej kalibracji. Sesje i liczniki zapytań odrzucają nieświeże odpowiedzi. `KEYS` ramki.

Żądania Heartbeat są zwykle wysyłane co dwie sekundy. Aplikacja ponownie łączy się, jeśli brakuje dopasowanej odpowiedzi dla 1.5 seconds; firmware zatrzymuje niezamówioną diagnostykę po sześciu sekundach bez bicia serca lub po zamknięciu połączenia szeregowego. Firmware 1.0.0 pozostaje obsługiwane poprzez dotychczasowe zapytanie tożsamości, bez diagnostyki klucza na żywo. DeckPilot aplikacje mogą nadal odczytywać dotychczasową tożsamość firmware 1.1.1.

Wersja źródłowa w `DeckPilotProtocol.h` dostarcza wersję manifest wydania. Opublikowane pakiety firmware teraz zawierają również aktualny szkic / nagłówek pod `source/DeckPilot`, szkic rekonwalescencji i DeckPilot Licencja MIT.

Połączenie diagnostyczne celowo wykorzystuje `Serial.dtr()` raczej niż `bool(Serial)`: Arduino AVR core 1.8.6 Wstawia 10 ms opóźnienie w `Serial_::operator bool()`. Czytanie DTR bezpośrednio utrzymuje ścieżkę diagnostyczną z dodawania tej pauzy do każdego cyklu key- scan. Test protokołu hosta usuwa domyślną konwersję bool tak, że ten wzór blokowania nie może być ponownie wprowadzone do wspólnej implementacji protokołu niezauważony.

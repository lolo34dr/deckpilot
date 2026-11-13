> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 2.4.1 koryguje zakładki panelu sterowania, tak aby ich pełne zarysy pozostały widoczne, a ich szerokość ustawiona jest przy zmianie rozmiaru okna.

Nie jest wymagana aktualizacja oprogramowania firmowego ani flash.

DeckPilot 2.4.0 reorganizuje panel sterujący i dodaje dokładny wybór kolorów.

- Użyj **Działania**, **Appareil** oraz **Réglages** zakładki, aby znaleźć kluczowe działania, sterowniki urządzeń / audio i preferencje.
- Zachowaj istniejący układ klawiatury / edytora lewej ręki, DeckPilot logo i monochromatyczne piktogramy wektorowe.
- Wybierz kolor tła, panelu i akcentu z **Paleta...**, podgląd ich wartości i pobrań, a następnie stosować je. Windows picker obsługuje niestandardowe kolory; nie jest wymagany motyw preset.
- Usuń OSS / Integracja kontroli strumieni. Istniejące 2.3 profile pozostają czytelne, a ich wycofane działania są zachowane, ale nie mogą wykonać. Przypisz te klucze z biblioteki akcji.
- Zachowaj inne profile, działania i fizyczną kalibrację. 1.1.1 i Atelier pozostaje 1.0.12; nie jest potrzebny błysk firmware.

DeckPilot 2.3.0 dodaje bezpośrednie sterowniki OBS Studio i Streampalters Desktop z przycisków macropad.

- Podłącz się do OBS poprzez serwer WebSocket i Streampalters Desktop poprzez zdalne sterowanie. DeckPilot wysyła polecenia API bezpośrednio zamiast symulowania skrótów klawiszowych.
- Konfiguracja akcji dla scen, streaming na żywo, nagrywanie, wyciszanie dźwięku, wizualizacja źródła i bufor powtórzeń.
- Funkcje Streaming są chronione przez obecnego użytkownika systemu Windows. Istniejące oprogramowanie firmowe makropad działa tak, jak jest; nie jest wymagana aktualizacja oprogramowania firmowego ani flash.

DeckPilot 2.2.0 dodaje regulowane, lokalne tłumienie szumów mikrofonu przy jednoczesnym zachowaniu klipów dźwiękowych nietkniętych.

- RNNoise filtruje wybrany fizyczny mikrofon przed DeckPilot Miesza się w klipach płyt dźwiękowych. Klapy omijają filtr i pozostają jasne w wirtualnym mikrofonie.
- Wybierz dowolną aplikację, która używa **CABLE Output** jak jego mikrofon - gry i programy głosowe-chat otrzymują filtrowany głos i mix soundboard. Aplikacje za pomocą mikrofonu fizycznego bezpośrednio bajpas DeckPilotFiltr.
- Włącz lub wyłączyć filtrowanie i ustawić jego 0–100% mocy w **Soundboard · micro virtuel**. Przetwarzanie dźwięku pozostaje na tym komputerze PC; nie jest wymagany błysk firmware.
- Native biblioteki RNNoise jest zbudowany dla Windows x64 i wbudowany w DeckPilot i jego instalatorem. Jego licencja w stylu BSD- strona trzecia jest zawarta w repozytorium źródłowym.

W przypadku 10 ms RNNoise rozmiar ramki nie gwarantuje latencji end-to@-@ end. Windows, urządzenia audio i VB-CABLE również mają wpływ na opóźnienie. Sprawdzić jakość mikrofonu i opóźnienie z planowanym ustawieniem.

DeckPilot 2.1.7 dodaje Spotify-only volume actions, built- in action to open DeckPilotoraz powiadomień o profilu na ekranie.

- Spotify przyciski głośności dostosować Spotify sesji audio systemu Windows w 5% kroków, bez zmiany głównego wolumenu lub innych graczy.
- Wyszukiwarka aplikacji i wyszukiwanie działań zawsze oferują DeckPilot, a jego działanie pokazuje istniejące okno z obszaru notyfikacji.
- Zmiany profilu pokazują krótki, nieaktywujący się narzut na aktywnym ekranie. Wybierz pozycję narożną lub niestandardową w preferencjach i podgląd go tam. Wyłączne gry pełnoekranowe mogą ukryć standardowe narzuty systemu Windows.

DeckPilot Atelier pozostaje w 1.0.12 i firmware pozostaje 1.1.1. Do tych działań nie jest potrzebny żaden błysk firmware.

DeckPilot 2.1.6 oraz DeckPilot Atelier 1.0.12 sprawiają, że oprogramowanie Atelier usuwa wyraźne i odzyskiwalne.

- Przenieś destrukcyjne oprogramowanie firmware do zawalonych zaawansowanych operacji i wymagają maszynopisania `EFFACER` przed napisaniem.
- Wyjaśnij, że plansza przestaje wymawiać się jako klawiatura po wykasowaniu, pokazać dokładne RST / GND dwukrotnie zresetowane kroki odzyskiwania, i zachować Atelier działania odzyskiwania dostępne po usuwaniu lub przerywany błysk.
- Pozostaje oprogramowanie firmowe 1.1.1; nie DeckPilot wymagana jest zmiana oprogramowania sprzętowego.

Walidacja obejmuje sprawdzanie oprogramowania firmowego i aplikacji, oraz automatykę UI systemu Windows dla zaawansowanej kontroli skasowania, bezpieczeństwo nieurządzeń i wskazówki RST / GND odzyskiwania. Sam reset wymaga jeszcze fizycznej płyty.

DeckPilot 2.1.5 wygładza urządzenie audio- odświeża i dodaje markowy ekran startowy.

- Odkryj fizyczne mikrofony i VB-CABLE z gwintu UI. Automatyczne powtórki dzielą się najnowszym migawką urządzenia; przycisk ręcznego odświeżania zawsze wykonuje świeży skan.
- Zredukuj powtarzające się sondaże stanu Windows i usprawnij wspólną ścieżkę konwersji monomikrofonu.
- Pokaż DeckPilot baner podczas uruchamiania interaktywnej aplikacji. Cichy startup z systemem Windows pozostaje w tle.
- Pozostałości atelier 1.0.11 i firmware pozostaje 1.1.1.

Walidacja obejmuje sprawdzanie tła / regresji pamięci podręcznej, sprawdzanie konwersji audio, tworzenie systemu Windows, uaktualnianie / usuwanie instalacji, sprawdzanie natywnego plusku / aktualizacji oraz wybieranie aplikacji. Rzeczywiste wykorzystanie procesora audio i opóźnienie nadal zależy od komputera PC i urządzeń audio.

DeckPilot 2.1.4 automatycznie uruchamia skonfigurowany wirtualny mikrofon za pomocą aplikacji, włączając w to uruchomienie tła po zalogowaniu się systemu Windows.

- Automatyczne uruchamianie mikrofonu soundboard jest domyślnie włączone dla istniejących i nowych ustawień. VB-CABLE; po ponownym uruchomieniu komputera nie jest konieczna ręczna aktywacja DeckPilot zaczyna się od Windows.
- Jeżeli urządzenia nie są gotowe do logowania się lub zatrzymania dźwięku po odłączeniu, DeckPilot cofa się co pięć sekund, podczas gdy aktywacja automatyczna jest włączona.
- Wyraźne zatrzymanie wyłącza i zapisuje preferencje automatic- start; włączyć go ponownie z Soundboard panel lub przycisk aktywacji. Klipy audio nigdy nie są odtwarzane automatycznie.
- Pozostałości atelier 1.0.11 i firmware pozostaje 1.1.1.

Walidacja zawiera ustawienia migracji / kontroli przejazdu oraz natywną kontrolę UI systemu Windows dla utrzymujących się preferencji automatycznego uruchamiania, plus istniejące kontrole audio i instalatora. Fizyczne urządzenie uruchamianie / ponowne połączenie nadal zależy od sprzętu właściciela.

DeckPilot 2.1.3 redukuje opóźnienie mikrofonu na płycie dźwiękowej i usuwa jednosekundowe bloki konwersji z ścieżki audio na żywo.

- Do mikrofonu i klipów dźwiękowych użyj wyrzutni, z małymi blokami; dopasowanie liczby próbek całkowicie się przełączyło.
- Prośba o wywołanie zdarzenia 20 s wychwytywanie i 40 ms zamiast poprzednich buforów odtwarzania 100 Ustawienia ms. Rzeczywiste opóźnienie zależy od systemu Windows, urządzeń audio i VB-CABLE.
- Związany mikrofon buforujący do 100 ms i zachować ostatnie kompletne ramki po playback stall zamiast kolejki starej mowy. Krótkie podbiegi dodać tylko ciszę wymaganą przez bieżący odczyt.
- Obsługa mono, stereo, multikanałowych i rozszerzalnych wejść PCM / float bez Media Foundation jeden-drugi read- forward.
- Zachować przetwarzanie i routing Discord bez zmian. 1.0.11 i firmware pozostaje 1.1.1.

Walidacja obejmuje kontrole regresji dla resemplingu wielkości bloku, konwersji kanału, rozszerzone formaty, nieprzerwany odzysk, nadmiar świeżości, jednoczesne wychwytywanie / odtwarzanie i zakończenie klipu, wraz z istniejącym instalatorem systemu Windows i testów aplikacji. Nie ma danych na temat opóźnienia sprzętowego; sprawdź poprawę z mikrofonem i sterownikiem audio.

DeckPilot 2.1.2 oraz DeckPilot Atelier 1.0.11 naprawić bezpieczeństwo warsztatu, niezawodność płyt dźwiękowych i przenośne dźwięki profilowe.

- Atelier może przygotować deski Leonardo / Caterina, które nie odpowiadają jeszcze na DeckPilot protokół; błędy seryjne nadal przerywają miganie, a lokalizacja USB, bootloader i podpis MCU pozostają zweryfikowane.
- Każda inspekcja zaczyna się od pustych wyników. Raporty zatrzymują sprawdzoną tablicę i oprogramowanie firmowe, a nieświeże wywołania USB nie mogą wpływać na nową sesję.
- Atelier zamykający podczas pisania lub weryfikacji oprogramowania firmowego jest zablokowany.
- Eksport profili i automatyczne kopie zapasowe profili mogą przechowywać wymienione pliki MP3 / WAV (32 Limit dźwiękowy MiB). Importuje kopie wbudowanych klipów do zarządzanego folderu i remap krótkie / długie odniesienia działania. Import profilu legacy pozostają wspierane; brak kopii zapasowej audio jest wyraźnie zgłaszane.
- Płyta dźwiękowa wydaje ukończone klipy, aktualizuje wskaźniki odtwarzania i raporty przerywane strumienie audio. VB-CABLE punkty końcowe są rozpoznawane poprzez ich tożsamość adaptera.
- Prawidłowe przetłumaczone linki README i zachować nazwy urządzeń technicznych podczas tłumaczenia. VB-CABLE wymóg.

Walidacja obejmuje podstawowe kontrole regresji, testy protokołów firmware, testy tłumaczeniowe, budowa systemu Windows, DeckPilot i Atelier instalacji / upgrade / delayed checks, a natywny interfejs / upgrade checks. Fizyczne zachowanie pierwszego-flash i audio odłączenia nadal wymagają sprzętu właściciela. 1.1.1; nie jest wymagana aktualizacja oprogramowania firmowego.

DeckPilot 2.1.1 ulepsza instalację i aktualizacje:

- Automatycznie sprawdzić VB-CABLE podczas pierwszej instalacji i aktualizacji, a następnie pobrać i uruchomić oficjalny podpisany instalator, gdy kierowca jest brakujący.
- Pokaż startup systemu Windows, automatyczne sprawdzanie aktualizacji i redukowane preferencje ruchu podczas konfiguracji i aktualizacji, z istniejącymi wyborami wstępnie wybranymi i zachowanymi.
- Należy zachować wybrany mikrofon fizyczny i ustawić mikrofon na żądanie tylko w razie potrzeby.
- Napraw inicjalizację instalatora i wykrywanie istniejących instalacji.

Pobieranie **DeckPilot-Setup.exe** lub zaakceptować aktualizację w DeckPilot. Wymagane jest zatwierdzenie administratora systemu Windows. Istniejące profile i kalibracja są zachowane. DeckPilot Atelier pozostaje w 1.0.10 i firmware pozostaje w 1.1.1; oba są włączone do wspólnego wydania.

Walidacja: sprawdzanie rdzenia i budowa systemu Windows, modernizacja / dezaktywowanie instalatora i testy aplikacji rodzimej uruchomić przed publikacją. Zachowanie sprzętu audio nadal wymaga testowania na komputerze użytkownika.

DeckPilot 2.1.0 utrzymuje głęboki czarny interfejs i dostosowane kolory, i trasy płyty dźwiękowej przez VB- Audio VB-CABLE automatycznie. Użytkownicy wybierają tylko swój fizyczny mikrofon; wirtualny punkt końcowy odtwarzania jest ustawiony tak, że głos i klipy MP3 / WAV nie mogą być przypadkowo wysyłane do głośników. DeckPilot instalator może pobrać oficjalny VB-CABLE pakiet, zweryfikuj jego wydawcę Authenticode i uruchom konfigurację sterownika sprzedawcy; pakiet nie jest redystrybuowany. CABLE Output jak mikrofon. Atelier pozostaje w wersji 1.0.10.

DeckPilot 1.9.11 oraz DeckPilot Atelier 1.0.10 Dodać sprzęt przedbłyskowy i diagnostykę oprogramowania firmowego, porównanie wersji firmware, akcję odzyskiwania po przerwaniu pisania, sekwencyjne wielopłytowe migotanie z raportem wsadowym oraz kontrolę układu fizycznego-pozycjonowania-do-F13-F24 w każdym raporcie sterowania kluczami.

DeckPilot 1.9.11 zawiera tę samą aktualizację Atelier w wydaniu współdzielonego instalatora.

DeckPilot 1.9.10 przywraca współdzielone wydania zawierające zarówno DeckPilot oraz DeckPilot Atelier 1.0.9. Obie aplikacje po raz kolejny sprawdzić tę samą najnowszą wersję GitHub.

Jeśli zainstalowałeś Atelier 1.0.8 z oddzielnego kanału wydania, zainstalować DeckPilot-Atelier- Setup.exe z tej wersji raz powrócić do wspólnych aktualizacji. Wcześniejsze wersje Atelier i DeckPilot mogą korzystać z istniejących updater.

DeckPilot Atelier 1.0.7 dodaje potwierdzoną wymazanie obszaru programu, które oczyszcza wszystkie 28 KiB flash użytkownika, pozostawia bezpieczną pętlę resetową i zachowuje Caterina i EEPROM. Reset pozostaje wyłączony do momentu wyboru fizycznej płyty.

DeckPilot 1.9.7 naprawia deinstalowanie Atelier, gdy aplikacja jest otwarta. 1.0.6 koryguje również manifest DPI systemu Windows, który uniemożliwiał uruchomienie zainstalowanej aplikacji.

DeckPilot 1.9.6 naprawia manifest DPI systemu Windows, który zapobiegł DeckPilot Atelier z otwarcia po instalacji.

DeckPilot 1.9.5 zawiera szczegółową diagnostykę uruchamiania Atelier w kontroli wydania systemu Windows.

DeckPilot 1.9.4 Poprawia test dymny Atelier Windows, aby zgłosić szczegóły awarii aplikacji.

DeckPilot 1.9.3 naprawia metadane budowania Atelier, aby odczytać manifest firmware z dołączonego archiwum firmware.

DeckPilot 1.9.2 naprawia etap pakowania uwalniającego Atelier i poprawia jego diagnostykę budowy.

DeckPilot 1.9.1 Poprawia uruchomienie instalatora Atelier i dodaje automatyczne sprawdzanie aktualizacji aplikacji i oprogramowania firmowego.

- Instalator uruchamia Atelier w sesji użytkownika systemu Windows i bezpiecznie zatrzymuje / ponownie uruchamia go do aktualizacji.
- Atelier sprawdza GitHub na starcie i na żądanie. ogłasza nowsze Atelier buduje lub splecione firmware, a następnie pobiera instalatora zweryfikowane w stosunku do opublikowanych SHA-256 Plik.
- Oprogramowanie firmowe jest wyświetlane dopiero po wybraniu przez użytkownika akcji flash; aktualizacja aplikacji nigdy nie pisze do podłączonej płyty samodzielnie.

Instalator Atelier uruchamia również w sesji Windows Signed- in użytkownika po świeżej instalacji. Przebieg pracy wydania otwiera zainstalowaną aplikację, a następnie weryfikuje aktualizację i odblokowuje podczas jej uruchamiania.

DeckPilot 1.9.0 dodaje DeckPilot Atelier, osobna aplikacja warsztatowa Windows do migania nowych makropadów i sprawdzania wszystkich 12 Klucze.

- Zainstaluj dołączone oprogramowanie firmowe na płytach Leonardo / Pro Micro ATmega32U4 za pomocą bootloadera Caterina.
- Sprawdź wszystkie klucze fizyczne, napisz mapowanie F13- F24 i zapisz raport z kontroli jakości.
- Pobieranie **DeckPilot-Atelier- Setup.exe** z tego wydania. Oryginał DeckPilot konfiguracja i oprogramowanie firmowe pozostają dostępne oddzielnie.

DeckPilot 1.8.0 dodaje odświeżone miejsce pracy i szybsze sposoby budowania i korzystania z profili.

- Interfejs z węglem / herbatą, najpierw biblioteka akcji, chrupiące 3 × 4 klawisze, kolory akcent i skrócone preferencje ruchu.
- Trzy edytowalne szablony profilu: Muzyka i audio, Desktop i produktywność, i Tworzenie.
- Działania profilowe: przełącz na nazwany profil, następny lub poprzedni profil z klucza. Importowane zestawy profili zachowują swoje wewnętrzne linki.
- Działania Windows: otwórz wspólne ustawienia lub Task Manager, lub wyraźnie zablokuj komputer.
- Kopiowanie / wycinanie / wklejanie / resetowanie całych kluczy, w tym długich pras i kolorów, z cofnięciem / wznowieniem i schowkiem app- local.
- Uchwycić skrót klawiatury bezpośrednio; Ucieczka anuluje i klawisze makro-pad pozostają zastrzeżone podczas przechwytywania.
- Ctrl + K wyszukiwanie akcji i rozdzielenie, sesja - tylko historia ostatniego 30 działania.
- Napraw automatyczny sygnał firmware, więc akceptacja otwiera menedżera firmware. Oczekiwane gesty są anulowane przez zmiany profilu.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacji z aplikacji. Istniejące profile, kalibracje i ustawienia startowe są zachowane. **1.1.1**; dla tych funkcji nie jest wymagany błysk.

Walidacja obejmuje zgodność rdzenia / nawigację / import / szablon i natywną instalację systemu Windows, aktualizację, picker, drag- and- drop, cofnięcie / redo, tworzenie szablonów, działania profili, przechwytywanie skrótów, edycję schowka, układ i minimalizację / przywrócenie kontroli. Fizyczne zachowanie makropad i audio- urządzenia nadal zależy od sprzętu Windows właściciela.
DeckPilot 2.1.8 dodaje opcjonalny powrót głośnika dla Soundboard Zaciski.

- W **Soundboard · micro virtuel**, enable **Entendre aussi les sons du soundboard**, wybrać głośnik fizyczny lub zestaw słuchawkowy, i ustawić jego niezależny 0–100% głośności. Zwrot jest domyślnie wyłączony i zapisany na startach.
- Tylko klipy soundboard są wysyłane do tego drugiego wyjścia. Fizyczny mikrofon nadal idzie do wirtualnego mikrofonu, i nigdy nie jest mieszane z powrotem głośnika. VB-CABLE w oryginalnej objętości.
- Zmiana głośności zwrotnej nie restartuje wirtualnego mikrofonu. Odłączony sygnał wyjściowy głośnika zatrzymuje tylko powrót; wirtualny mikrofon ciągle działa. Odśwież urządzenia lub wybierz inne wyjście, aby go przywrócić.

DeckPilot Atelier pozostaje w 1.0.12 i firmware pozostaje 1.1.1. Nie jest potrzebne oprogramowanie firmware flash. Rzeczywiste routing głośników należy sprawdzić na komputerze Windows właściciela za pomocą urządzeń audio.

DeckPilot 2.1.9 Naprawia monitoring głośników na urządzeniach, których format audio z systemem Windows jest rozszerzony, w tym niektórych zestawów słuchawkowych USB, i dodaje powiadomienie o nakładaniu płyty dźwiękowej.

- Głośnik zwraca zachowuje dokładny format mix WASAPI wybranego wyjścia, konwertuje próbki klipów do jego PCM lub kodowania float, a następnie ponownie próbuje odtwarzać bez zdarzenia, gdy kierowca odrzuca ten tryb.
- Uruchomienie klipu z tablicą dźwiękową pokazuje swoją nazwę w konfigurowalnym nakładce na ekranie. Istniejące preferencje powiadamiania o profilu, umieszczanie i redukcja ustawienia ruchu obu typów powiadomień.
- Powrót nadal zawiera tylko klipy soundboard; fizyczny mikrofon pozostaje poza głośnikiem mix i kontynuuje do wirtualnego mikrofonu.

DeckPilot Atelier pozostaje w 1.0.12 i firmware pozostaje 1.1.1. Nie jest potrzebne oprogramowanie firmware flash. Kompatybilność głośnika musi być nadal sprawdzona z rzeczywistym urządzeniem audio właściciela systemu Windows.

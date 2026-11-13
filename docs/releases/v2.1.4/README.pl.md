> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest referencją.

DeckPilot 2.1.4 automatycznie uruchamia skonfigurowany wirtualny mikrofon za pomocą aplikacji, włączając w to uruchomienie tła po zalogowaniu się systemu Windows.

- Automatyczne uruchamianie mikrofonu soundboard jest domyślnie włączone dla istniejących i nowych ustawień. VB-CABLE; po ponownym uruchomieniu komputera nie jest konieczna ręczna aktywacja DeckPilot zaczyna się od Windows.
- Jeżeli urządzenia nie są gotowe do logowania się lub zatrzymania dźwięku po odłączeniu, DeckPilot powtórki co pięć sekund, podczas gdy aktywacja automatyczna jest włączona.
- Wyraźne zatrzymanie wyłącza i zapisuje preferencje automatic- start; włączyć go ponownie z Soundboard panel lub przycisk aktywacji. Klipy audio nigdy nie są odtwarzane automatycznie.
- Pozostałości atelier 1.0.11 i firmware pozostaje 1.1.1.

Walidacja zawiera ustawienia migracji / kontroli przejazdu oraz natywną kontrolę UI systemu Windows dla utrzymujących się preferencji automatic- start, plus istniejące kontrole audio i instalatora. Fizyczne urządzenie uruchamianie / ponowne połączenie nadal zależy od sprzętu właściciela.

DeckPilot 2.1.3 redukuje opóźnienie mikrofonu na płycie dźwiękowej i usuwa jednosekundowe bloki konwersji z żywej ścieżki audio.

- Do mikrofonu i klipów dźwiękowych użyj sterowanych wyrzutniami małych bloków; dopasowanie liczby próbek całkowicie się przełącza.
- Prośba kierowana eventem 20 s wychwytywanie i 40 ms odtwarza bufory zamiast poprzednich 100 Ustawienia ms. Rzeczywiste opóźnienie zależy od systemu Windows, urządzeń audio i VB-CABLE.
- Związany mikrofon buforujący do 100 ms i zachować ostatnie kompletne ramki po playback stall zamiast kolejki starej mowy. Krótkie minusy dodać tylko ciszę wymaganą przez bieżący odczyt.
- Obsługa mono, stereo, wielokanałowych i rozszerzalnych wejść PCM / float bez Media Foundation one- second read- forward.
- Utrzymuj przetwarzanie i routing Discord bez zmian. 1.0.11 i firmware pozostaje 1.1.1.

Walidacja obejmuje kontrole regresji dla resemplingu wielkości bloku, konwersji kanału, rozszerzalne formaty, nieprzerwany odzysk, nadmiar świeżości, jednoczesne wychwytywanie / odtwarzanie i zakończenie klipu, obok istniejącego instalatora systemu Windows i testów aplikacji. Nie ma danych dotyczących opóźnienia sprzętowego; sprawdź poprawność za pomocą mikrofonu i sterownika audio.

DeckPilot 2.1.2 oraz DeckPilot Atelier 1.0.11 naprawić bezpieczeństwo warsztatu, niezawodność płyt dźwiękowych i przenośne dźwięki profilowe.

- Atelier może przygotować deski Leonardo / Caterina, które nie odpowiadają jeszcze na DeckPilot protokół; błędy seryjne nadal przerywają miganie, a lokalizacja USB, bootloader i podpis MCU pozostają zweryfikowane.
- Każda inspekcja zaczyna się od pustych wyników. Raporty zatrzymują sprawdzoną tablicę i oprogramowanie firmowe, a nieświeże wywołania USB nie mogą mieć wpływu na nową sesję.
- Atelier zamykający podczas pisania lub weryfikacji oprogramowania firmowego jest zablokowany.
- Eksport profili i automatyczne kopie zapasowe profili mogą przechowywać wymienione pliki MP3 / WAV (32 Import kopii wbudowanych klipów do zarządzanego folderu i remap krótkie / długie referencje działania. Import profilu legacy pozostają wspierane; brak kopii zapasowej audio jest wyraźnie zgłaszane.
- Płyta dźwiękowa wydaje ukończone klipy, aktualizuje wskaźniki odtwarzania i raporty przerywane strumienie audio. VB-CABLE punkty końcowe są rozpoznawane poprzez ich tożsamość adaptera.
- Prawidłowe przetłumaczone linki README i zachować nazwy urządzeń technicznych podczas tłumaczenia. VB-CABLE wymóg.

Walidacja obejmuje testy regresji rdzenia, testy protokołów firmware, testy tłumaczeniowe, budowę systemu Windows, DeckPilot i Atelier instalacji / upgrade / delayed checks, i natywny interfejs / upgrade checks. Fizyczne zachowanie pierwszego-flash i audio odłączenia nadal wymagają sprzętu właściciela. 1.1.1; nie jest wymagana aktualizacja oprogramowania firmowego.

DeckPilot 2.1.1 ulepsza instalację i aktualizacje:

- Automatycznie sprawdzić VB-CABLE podczas pierwszej instalacji i aktualizacji, a także pobrać i uruchomić oficjalny podpisany instalator, gdy kierowca jest brakujący.
- Pokaż startup systemu Windows, automatyczne sprawdzanie aktualizacji i reduced- motion preferencje podczas konfiguracji i aktualizacji, z istniejącymi wyborami wstępnie wybranymi i zachowanymi.
- Należy zachować wybrany mikrofon fizyczny i ustawić mikrofon na żądanie tylko w razie potrzeby.
- Napraw inicjalizację instalatora i wykrywanie istniejących instalacji.

Pobieranie **DeckPilot-Setup.exe** lub zaakceptować aktualizację w DeckPilot. Wymagane jest zatwierdzenie administratora systemu Windows. Istniejące profile i kalibracja są zachowane. DeckPilot Atelier pozostaje w 1.0.10 i firmware pozostaje w 1.1.1; oba są włączone do wspólnego wydania.

Walidacja: sprawdzanie rdzenia i budowa systemu Windows, modernizacja / odblokowanie instalatora i testy aplikacji rodzimej uruchomić przed publikacją. Zachowanie sprzętu audio nadal wymaga testowania na komputerze użytkownika.

DeckPilot 2.1.0 utrzymuje głęboki czarny interfejs i dostosowane kolory, i trasy płyty dźwiękowej przez VB- Audio VB-CABLE automatycznie. Użytkownicy wybierają tylko swój fizyczny mikrofon; wirtualny punkt końcowy odtwarzania jest ustawiony tak, że nie można przypadkowo wysłać do głośników klipów MP3 / WAV. DeckPilot instalator może pobrać oficjalny VB-CABLE pakiet, zweryfikuj jego wydawcę Authenticode i uruchom konfigurację sterownika sprzedawcy; pakiet nie jest redystrybuowany. CABLE Output jako mikrofon. Atelier pozostaje w wersji 1.0.10.

DeckPilot 1.9.11 oraz DeckPilot Atelier 1.0.10 Dodać sprzęt przedbłyskowy i diagnostykę oprogramowania firmowego, porównanie wersji firmware, po przerwaniu pisania, sekwencyjne wielopokładowe migotanie z raportem wsadowym oraz kontrolę układu fizycznego-pozycjonowania-do-F13-F24 w każdym raporcie sterowania kluczami.

DeckPilot 1.9.11 zawiera tę samą aktualizację Atelier w wydaniu współdzielonego instalatora.

DeckPilot 1.9.10 Przywraca współdzielone wydania zawierające zarówno DeckPilot oraz DeckPilot Atelier 1.0.9. Obie aplikacje po raz kolejny sprawdzić tę samą najnowszą wersję GitHub.

Jeśli zainstalowałeś Atelier 1.0.8 z oddzielnego kanału wydania, zainstalować DeckPilot-Atelier- Setup.exe z tej wersji raz powrócić do wspólnych aktualizacji. Wcześniejsze wersje Atelier i DeckPilot mogą korzystać z ich istniejącego updater.

DeckPilot Atelier 1.0.7 dodaje potwierdzoną skasowanie obszaru programu, które oczyszcza wszystkie 28 KiB flash użytkownika, pozostawia bezpieczną pętlę resetową i zachowuje Caterina i EEPROM. Reset pozostaje wyłączony do momentu wyboru fizycznej płyty.

DeckPilot 1.9.7 naprawia deinstalowanie Atelier, gdy aplikacja jest otwarta. 1.0.6 koryguje również manifest DPI systemu Windows, który uniemożliwiał uruchomienie zainstalowanej aplikacji.

DeckPilot 1.9.6 naprawia manifest DPI systemu Windows, który zapobiegł DeckPilot Atelier z otwarcia po instalacji.

DeckPilot 1.9.5 zawiera szczegółową diagnostykę uruchamiania Atelier w kontroli wydania systemu Windows.

DeckPilot 1.9.4 Poprawia test dymny Atelier Windows, aby zgłosić szczegóły awarii aplikacji.

DeckPilot 1.9.3 naprawia metadane budowania Atelier, aby odczytać manifest firmware z dołączonego archiwum firmware.

DeckPilot 1.9.2 naprawia etap pakowania uwalniającego Atelier i poprawia jego diagnostykę budowy.

DeckPilot 1.9.1 Poprawia uruchomienie instalatora Atelier i dodaje automatyczne sprawdzanie aktualizacji aplikacji i oprogramowania firmowego.

- Instalator uruchamia Atelier w sesji użytkownika systemu Windows i bezpiecznie zatrzymuje / ponownie uruchamia go do aktualizacji.
- Atelier sprawdza GitHub na starcie i na żądanie. Ogłasza nowsze Atelier buduje lub splecione firmware, a następnie pobiera instalatora zweryfikowanego w stosunku do opublikowanego SHA-256 plik.
- Oprogramowanie firmowe jest wyświetlane dopiero po wybraniu przez użytkownika akcji flash; aktualizacja aplikacji nigdy nie pisze do podłączonej płyty samodzielnie.

Instalator Atelier uruchamia również w sesji Windows Signed- in użytkownika po świeżej instalacji. Przebieg pracy wydania otwiera zainstalowaną aplikację, a następnie weryfikuje aktualizację i odblokowuje podczas jej uruchamiania.

DeckPilot 1.9.0 dodaje DeckPilot Atelier, osobna aplikacja warsztatowa Windows do migania nowych makropadów i sprawdzania wszystkich 12 Klucze.

- Zainstaluj dołączone oprogramowanie firmowe na płytach Leonardo / Pro Micro ATmega32U4 za pomocą bootloadera Caterina.
- Sprawdź wszystkie klucze fizyczne, napisz mapowanie F13- F24 i zapisz raport kontroli jakości.
- Pobieranie **DeckPilot-Atelier- Setup.exe** z tego wydania. Oryginał DeckPilot konfiguracja i oprogramowanie firmowe pozostają dostępne oddzielnie.

DeckPilot 1.8.0 dodaje odświeżone miejsce pracy i szybsze sposoby budowania i korzystania z profili.

- Interfejs z węglem / herbatą, najpierw biblioteka akcji, chrupiące 3 × 4 klawisze, kolory akcentów i skrócone preferencje ruchu.
- Trzy edytowalne szablony profili: Muzyka i audio, Desktop i produktywność, i Tworzenie.
- Działania profilowe: przełącz na nazwany profil, następny lub poprzedni profil z klucza. Importowane zestawy profili zachowują swoje wewnętrzne linki.
- Działania Windows: otwórz wspólne ustawienia lub Task Manager, lub wyraźnie zablokuj komputer.
- Kopiowanie / wycinanie / wklejanie / resetowanie całych kluczy, w tym długich pras i kolorów, z cofnięciem / wznowieniem i schowkiem app- local.
- Uchwycić skrót klawiatury bezpośrednio; Ucieczka anuluje i klawisze makro-pad pozostają zastrzeżone podczas przechwytywania.
- Ctrl + K wyszukiwanie akcji i rozdzielenie, sesja - tylko historia ostatniego 30 działania.
- Napraw automatyczny sygnał firmware, więc akceptacja otwiera menedżera firmware. Oczekiwane gesty są anulowane przez zmiany profilu.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacja z aplikacji. Istniejące profile, kalibracje i ustawienia startowe są zachowane. **1.1.1**; dla tych funkcji nie jest wymagany błysk.

Walidacja obejmuje zgodność rdzenia / nawigację / import / szablon i natywną instalację systemu Windows, aktualizację, picker, drag- and- drop, cofnięcie / redo, tworzenie szablonów, działania profili, przechwytywanie skrótów, edycję schowka, układ i minimalizację / przywrócenie kontroli. Fizyczne zachowanie makropad i urządzenia audio- nadal zależy od sprzętu Windows właściciela.

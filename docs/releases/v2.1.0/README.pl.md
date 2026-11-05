> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 2.1.0 zachowuje głęboki czarny interfejs i dostosowane kolory, i trasa płyty dźwiękowej przez VB- Audio VB- Cable automatycznie. Użytkownicy wybierają tylko ich mikrofon fizyczny; wirtualny punkt końcowy odtwarzania jest ustalony, więc głos i MP3 / WAV klipy nie mogą być przypadkowo wysłane do głośników. DeckPilot Instalator może pobrać oficjalny pakiet VB- CABLE, zweryfikować wydawcę Authenticode i uruchomić konfigurację sterownika sprzedawcy; pakiet nie jest redystrybuowany. W Discord lub gra, wybierz wyjście CABLE jako mikrofon. Atelier pozostaje w wersji 1.0.10.

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

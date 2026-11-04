> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 1.6.4 naprawia wymianę wyników wyszukiwania podczas drag- and- drop i stabilizuje weryfikację systemu Windows.

- Zachować istniejące przyciski wyszukiwania, gdy czasomierz debuuncji lub katalog instalalled- app generuje te same wyniki, więc źródło przeciągnij nie jest nieoczekiwanie odłączony.
- Defer search- result wymiany podczas interakcji z myszką lub OLE Drag jest w trakcie.
- Sprawdzanie interfejsu użytkownika systemu Windows używa fizycznych współrzędnych kursora, kontroli ostrości przed ich mierzeniem i skanuje pełen zakres przewijania zamiast zakładania stałej liczby przyrostów linii. Wymagane są nadal przeciągnięcia myszki; nieudane przydziały nie są ponownie próbowane, aby uzyskać przepustkę.
- Ustawienia systemu Windows oszczędzają korzystanie z dedykowanego API zamiany atomowej, tak aby istniejący czytnik mógł zachować swoje stare migawki bez blokowania następnego zapisu. Regresja rdzenia weryfikuje trzy kolejne zamiany ze wspólnym czytnikiem utrzymywanym w otwartym.
- Ustawienia czytniki w testach Windows używają UTF-8 i pozwolić na wymianę plików atomowych. 500 ms tylko dla Windows udostępniania / Lock naruszenia podczas wymiany; inne błędy odczytu i trwałe blokady nie udały się test. Trzy cykle cofania / redo również zachować stary migawki otwarte, aby sprawdzić, że oszczędza sukces bez modyfikacji istniejących czytelników.
- Kontrole początkowe czekają, aż WPF zakończy ukrywanie swojego okna, a następnie wymagają, aby proces pozostał uruchomiony bez widocznego głównego okna.
- Upgrade GitHub Actions to Node.js 24 wersje i zainstalować ten sam Arduino CLI 1.2.2 ze swojego sprawdzonego archiwum wyższego szczebla.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacji z aplikacji. Profile i kalibracja są zachowane. Nie jest potrzebny błysk firmware.

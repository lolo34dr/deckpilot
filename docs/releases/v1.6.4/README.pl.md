> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest referencją.

DeckPilot 1.6.4 Poprawki search- result wymiany podczas drag- and- drop i stabilizuje weryfikację systemu Windows.

- Zachować istniejące przyciski wyszukiwania, gdy czasomierz debuuncji lub katalog instalalled- app generuje te same wyniki, więc źródło przeciągnij nie jest nieoczekiwanie odłączony.
- Odradza wymianę wyników wyszukiwania podczas interakcji z myszką lub OLE Drag jest w trakcie.
- Windows UI sprawdza używa fizycznych współrzędnych kursora, kontroli ostrości przed mierzeniem ich, i skanuje pełny zakres przewijania zamiast zakładając stałą liczbę przyrostów linii. Native drags myszki pozostają wymagane; nieudany przydziały nie są ponownie próbowane, aby uzyskać przepustkę.
- Ustawienia systemu Windows zapisują użycie dedykowanego API zamiennika atomowego, tak aby istniejący czytnik mógł zachować swoje stare migawki bez blokowania następnego zapisu. Regresja rdzenia weryfikuje trzy kolejne zamienniki ze wspólnym czytnikiem utrzymywanym w otwartym.
- Ustawienia czytników w testach Windows używają UTF-8 i pozwolić na wymianę plików atomowych. 500 ms tylko dla Windows udostępniania / Lock naruszenia podczas wymiany; inne błędy odczytu i trwałe blokady nie udały się test. Trzy cykle cofania / redo również zachować stary migawki otwarte, aby sprawdzić, że oszczędza sukces bez modyfikacji istniejących czytelników.
- Background-startup checks czeka, aż WPF zakończy ukrywanie swojego okna, a następnie wymaga, aby proces pozostał uruchomiony bez widocznego głównego okna.
- Upgrade GitHub Actions to Node.js 24 wersje i zainstalować ten sam Arduino CLI 1.2.2 z sprawdzonego archiwum wyższego szczebla.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacji z aplikacji. Profile i kalibracja są zachowane. Nie jest potrzebny błysk firmware.

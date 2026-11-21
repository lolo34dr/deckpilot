> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 2.1.2 oraz DeckPilot Atelier 1.0.11 naprawić bezpieczeństwo warsztatu, niezawodność płyt dźwiękowych i przenośne dźwięki profilowe.

- Atelier może przygotować deski Leonardo / Caterina, które nie odpowiadają jeszcze na DeckPilot protokół; błędy seryjne nadal przerywają miganie, a lokalizacja USB, bootloader i podpis MCU pozostają zweryfikowane.
- Każda inspekcja zaczyna się od pustych wyników. Raporty zatrzymują sprawdzoną tablicę i oprogramowanie firmowe, a nieświeże wywołania USB nie mogą wpływać na nową sesję.
- Atelier zamykający podczas pisania lub weryfikacji oprogramowania firmowego jest zablokowany.
- Eksport profili i automatyczne kopie zapasowe profili mogą przechowywać wymienione pliki MP3 / WAV (32 Limit dźwiękowy MiB). Importuje kopie wbudowanych klipów do zarządzanego folderu i remap krótkie / długie odniesienia działania. Import profilu legacy pozostają wspierane; brak kopii zapasowej audio jest wyraźnie zgłaszane.
- Płyta dźwiękowa wydaje ukończone klipy, aktualizuje wskaźniki odtwarzania i raporty przerywane strumienie audio. VB-CABLE punkty końcowe są rozpoznawane poprzez ich tożsamość adaptera.
- Prawidłowe przetłumaczone linki README i zachować nazwy urządzeń technicznych podczas tłumaczenia. VB-CABLE wymóg.

Walidacja obejmuje podstawowe kontrole regresji, testy protokołów firmware, testy tłumaczeniowe, budowa systemu Windows, DeckPilot i Atelier instalacji / upgrade / delayed checks, a natywny interfejs / upgrade checks. Fizyczne zachowanie pierwszego-flash i audio odłączenia nadal wymagają sprzętu właściciela. 1.1.1; nie jest wymagana aktualizacja oprogramowania firmowego.

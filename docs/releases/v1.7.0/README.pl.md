> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 1.7.0 zmniejsza powtarzającą się pracę podczas edycji profili, odnajdywania aplikacji i uruchamiania w tle.

- Zapisywanie niezmienionych ustawień nie zastępuje już pliku ani nie tworzy kopii zapasowych. Rzeczywista zawartość dysku jest nadal sprawdzana, tak więc zewnętrzne edycje, usuwanie i nieskuteczne zapisywanie nie są ukryte.
- Historia profilu utrzymuje oddzielone migawki modeli. Niezmienione edycje i cofnij / redo uniknąć JSON rund- trips; edycja akcji wykorzystuje porównania wartości zamiast powtarzanej serializacji.
- Wyszukiwanie aplikacji i picker dzielą się pięciominutowym katalogiem cache i jednym zadaniem odkrywania. Odkrywanie działa poza WPF; Refresh nadal zmusza do nowego skanowania. Każdy picker posiada swój filtr i utrzymuje aktualne wyniki przydatne podczas odświeżania.
- Zastąp stały 100 ms board UI ankieta z zamówionymi powiadomieniami o połączeniach. Keyboard- hak połączeń kolejka wizualne pracy zamiast renderowania dwunastu klawiszy synchronicznie.
- Sondaże stanu dźwięku co dwie sekundy, podczas gdy widoczne i co dziesięć sekund, gdy ukryte / zminimalizowane, orzeźwiające natychmiast po ponownym otwarciu i po poleceniach audio. Overlapping odświeżanie żądania są coalesced bez upuszczania aktualizacji post-command.
- Ukryte okna zatrzymują wizualne zegary sprzężenia zwrotnego i pomijają kosmetyczne renderowanie czeka. Mrożone szczotki UI są ponownie używane i uruchomione opakowania procesu aplikacji są usuwane.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacja z aplikacji. Preferencje profili, kalibracji i startup są zachowane. Nie jest wymagany błysk firmware.

Walidacja obejmuje ustawienia / historię / regresje pamięci podręcznej i instalację systemu Windows, aktualizację, picker, drag- and- drop, krótką / długą edycję akcji i minimalizację / przywrócenie kontroli. Są to optymalizacje strukturalne, a nie zmierzone roszczenie o poszczególnych CPU lub RAM oszczędności na każdym komputerze PC.

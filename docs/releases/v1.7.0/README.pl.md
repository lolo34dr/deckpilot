> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest referencją.

DeckPilot 1.7.0 zmniejsza powtarzającą się pracę podczas edycji profili, odnajdywania aplikacji i działania w tle.

- Zapisywanie niezmienionych ustawień nie zastępuje już pliku ani nie tworzy kopii zapasowych. Rzeczywista zawartość dysku jest nadal sprawdzana, więc zewnętrzne edycje, usuwanie i nieskuteczne zapisywanie nie są ukryte.
- Historia profilu utrzymuje oddzielone migawki modelu. Niezmienione edycje i cofnij / redo unikać JSON-wycieczki; edycja akcji wykorzystuje porównania wartości zamiast powtarzanej serializacji.
- Wyszukiwanie aplikacji i picker dzielą się pięciominutowym cache katalogu i jednym zadaniem odkrywania. Odkrywanie działa poza WPF; Refresh nadal zmusza do nowego skanowania. Każdy picker posiada swój filtr i utrzymuje aktualne wyniki do wykorzystania podczas odświeżania.
- Zastąp stały 100 ms board UI ankieta z zamówionymi powiadomieniami o połączeniach. Keyboard- hak callbacks kolejka wizualnej pracy zamiast renderowania dwunastu klawiszy synchronicznie.
- Sondaże stanu dźwięku co dwie sekundy, podczas gdy widoczne i co dziesięć sekund, gdy ukryte / zminimalizowane, orzeźwiające natychmiast po ponownym otwarciu i po komendach audio. Overlapping odświeżanie żądania są coalesced bez upuszczania aktualizacji post-command.
- Ukryte okna zatrzymują wizualne zegary zwrotne i pomijają kosmetyczne renderowanie czeka. Mrożone szczotki UI są ponownie używane i uruchomione opakowania procesu aplikacji są usuwane.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacja z aplikacji. Preferencje profili, kalibracji i startup są zachowane. Nie jest wymagane oprogramowanie firmware flash.

Walidacja obejmuje ustawienia / historię / regresje pamięci podręcznej i instalację systemu Windows, aktualizację, picker, drag- and- drop, krótką / długą edycję akcji i minimalizację / przywrócenie kontroli. Są to optymalizacje strukturalne, a nie zmierzone roszczenie o poszczególnych CPU lub RAM oszczędności na każdym komputerze.

> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest referencją.

DeckPilot 1.3.2 poprawia połączenie USB i sprawdza oprogramowanie firmowe makro pad przy starcie.

- Podłącz deskę rozdzielczą z automatycznym ponownym podłączeniem, wykrytym oprogramowaniem firmware, diagnostyką na żywo / USB oraz wyraźnym wyborem planszy, gdy jest kilka urządzeń.
- Kontrola oprogramowania firmowego i aktualizacja wniosków, z 24-hour przypomnienia opóźnienia i możliwość wyłączenia powiadomień firmware. Instalacja nadal wymaga potwierdzenia.
- Nowe oprogramowanie firmowe **1.1.1**: ograniczone komunikaty protokołu, świeże żetony sesyjne, skorelowane odpowiedzi na bicie serca i zdarzenia stanu kluczowego. Mapowanie F13- F24 pozostaje niezmienione, w tym samodzielne użycie.
- Menedżer oprogramowania automatycznie uwalnia port szeregowy monitora przed identyfikacją / miganiem i ponownie łączy się później. 1.0.0 i nadal wspierane są dotychczasowe pytania o tożsamość.
- Profile można wybrać z menu tacki; Ctrl + S zapisuje zmiany; diagnostyka połączenia może być skopiowana dla wsparcia.

Aktualizacje firmware są dostarczane w wersjach aplikacji; kontrole uruchamiania porównują wykrytą wersję z dołączonym pakietem. **Oprogramowanie firmowe 1.1.1 nadal potrzebuje pierwszego testu na płycie fizycznej:** firmware 1.0.0 a wcześniejsza procedura błysku została zatwierdzona przez właściciela, podczas gdy ten nowy protokół jest objęty testami gospodarza i kompilacją AVR.

Pobieranie **DeckPilot-Setup.exe** lub aktualizacja z aplikacji. Preferencje profili, kalibracji i startup są zachowane. Zakończ DeckPilot przed użyciem Arduino IDE lub innego monitora szeregowego; otwarcie menedżera oprogramowania in- app automatycznie uwalnia połączenie.

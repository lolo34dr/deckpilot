> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 2.1.3 redukuje opóźnienie mikrofonu na płycie dźwiękowej i usuwa jednosekundowe bloki konwersji z ścieżki audio na żywo.

- Do mikrofonu i klipów dźwiękowych użyj wyrzutni, z małymi blokami; dopasowanie liczby prób całkowicie się przełączyło.
- Prośba o wywołanie zdarzenia 20 s wychwytywanie i 40 ms zamiast poprzednich buforów odtwarzania 100 Ustawienia ms. Rzeczywiste opóźnienie zależy od systemu Windows, urządzeń audio i VB-CABLE.
- Związany mikrofon buforujący do 100 ms i zachować ostatnie kompletne ramki po playback stall zamiast kolejki starej mowy. Krótkie podbiegi dodać tylko ciszę wymaganą przez bieżący odczyt.
- Obsługa mono, stereo, multikanałowych i rozszerzalnych wejść PCM / float bez Media Foundation jeden-drugi read- forward.
- Zachować przetwarzanie i routing Discord bez zmian. 1.0.11 i firmware pozostaje 1.1.1.

Walidacja obejmuje kontrole regresji dla resemplingu wielkości bloku, konwersji kanału, rozszerzone formaty, nieprzerwany odzysk, nadmiar świeżości, jednoczesne wychwytywanie / odtwarzanie i zakończenie klipu, obok istniejącego instalatora systemu Windows i testów aplikacji. Nie ma danych dotyczących opóźnienia sprzętowego; sprawdź poprawność za pomocą mikrofonu i sterownika audio.

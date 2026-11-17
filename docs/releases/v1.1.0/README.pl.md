> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot może teraz zainstalować firmware na DIY Pro Micro macropad bezpośrednio z aplikacji.

- Nowy **Firmware du macropad…** okno dialogowe z wyborem urządzenia i potwierdzeniem przed migotaniem.
- Pętla firmware **1.0.0** zachowuje F13- F24 i dodaje DeckPilot odpowiedź tożsamości / wersji nad serią USB.
- Zmontowany AVRDUDE: nie jest potrzebna instalacja Arduino IDE do zwykłych aktualizacji.
- Zatwierdzona suma kontrolna oprogramowania firmowego i granice pamięci, dopasowanie fizycznego połączenia USB, weryfikacja zapisu i sprawdzanie wersji postflash.
- Istniejące profile PC i kalibracja są zachowane. Aktualizacja aplikacji nigdy nie błyszczy sprzętem automatycznie.

**Wymagany jest pierwszy test sprzętowy:** potwierdzona jest oryginalna procedura wysyłania oprogramowania Caterina; nowe oprogramowanie / updater musi być nadal testowane na faktycznym makro-pad. **ATmega32U4, 5 V / 16 MHz**, podłączony **2–10, 14, 15, 16**, z Leonardo USB ID i Caterina bootloader tylko. Zachować oryginalny szkic i RST / GND dostęp do odzyskiwania. Brak kopii zapasowych oprogramowania firmowego lub automatycznego rollback.

Okna 10/11 x64. Pobierz **DeckPilot.exe** lub **DeckPilot- win- x64.zip**. Oprogramowanie firmowe / narzędzia są wbudowane w aplikacji, a także dostępne w **DeckPilot-firmware.zip**AVRDUDE posiada własną licencję GPL i powiązane źródło.

> Jest to automatyczne tłumaczenie kompletnych notatek wydania. Angielski oryginał jest punktem odniesienia.

DeckPilot 2.2.0 dodaje regulowane, lokalne tłumienie szumów mikrofonu przy jednoczesnym zachowaniu klipów dźwiękowych nietkniętych.

- RNNoise filtruje wybrany fizyczny mikrofon przed DeckPilot Miesza się w klipach płyt dźwiękowych. Klapy omijają filtr i pozostają jasne w wirtualnym mikrofonie.
- Wybierz dowolną aplikację, która używa **CABLE Output** jak jego mikrofon - gry i programy głosowe-chat otrzymują filtrowany głos i mix soundboard. Aplikacje za pomocą mikrofonu fizycznego bezpośrednio bajpas DeckPilotFiltr.
- Włącz lub wyłączyć filtrowanie i ustawić jego 0–100% mocy w **Soundboard · micro virtuel**. Przetwarzanie dźwięku pozostaje na tym komputerze PC; nie jest wymagany błysk firmware.
- Native biblioteki RNNoise jest zbudowany dla Windows x64 i wbudowany w DeckPilot i jego instalatorem. Jego licencja w stylu BSD- strona trzecia jest zawarta w repozytorium źródłowym.

W przypadku 10 ms RNNoise rozmiar ramki nie gwarantuje latencji end-to@-@ end. Windows, urządzenia audio i VB-CABLE również mają wpływ na opóźnienie. Sprawdzić jakość mikrofonu i opóźnienie z planowanym ustawieniem.

> ▁Dit is▁een▁automatische▁vertaling van de▁volledige release notes.▁Het▁Engelse▁origineel is de▁referentie.

DeckPilot 1.6.1 fixes soft▁keypad▁rendering en▁voegt▁profielherstel, Windows audio▁controles en▁korte /▁lange▁persen.

- Native-size▁keypad▁tekst:▁verwijderde▁hele-deck schalen en▁inhoud▁schaduw▁effecten,▁toegevoegd per-monitor DPI▁bewustzijn en▁grotere desktop app▁pictogrammen.▁Kleine▁vensters scroll in▁plaats van krimpende▁toetsen.
- ▁Ongedaan▁maken/opnieuw▁doen voor▁profielbewerkingen,▁inclusief slepen en▁neerzetten (50 ▁wijzigingen per app-sessie);▁tekstvelden▁behouden Ctrl+Z/Ctrl+Y.
- ▁Profiel import/export en 20 ▁automatische back-ups van▁vorige▁versie. Importeren▁voegt▁nieuwe▁kopieën▁toe;▁herstel en▁herstel de conserverkalibratie. Geïmporteerde▁acties▁worden▁nooit▁automatisch▁uitgevoerd.
- Global Windows-microfoon▁schakelen met▁een persistente status-balk indicator en live endpoint status.▁Alle▁actieve▁opname▁eindpunten▁zijn▁gedempt/onmuteerd.
- Schakelen▁tussen▁twee▁gekozen▁aangesloten▁uitvoerapparaten, voor▁alle▁standaard audiorollen van Windows.▁Ontbrekende▁apparaten▁falen▁zonder▁stil▁een▁andere▁uitvoer te▁kiezen.
- ▁Twee▁geconfigureerde▁acties per▁sleutel met▁een 200–3000 ms▁drempel.▁Korte runs op release,▁lange▁vervangt▁kort en▁loopt▁eenmaal.▁Bestaande▁onmiddellijke▁acties en▁gratis F13

▁Downloaden **DeckPilot-Setup.exe** of update van de app.▁Profielen en▁kalibratie▁worden▁bewaard. Audio-eindpunt▁keuzes▁moeten▁mogelijk▁worden▁aangepast na▁het▁importeren van▁profielen op▁een▁andere PC. Windows-eindpunt▁staat▁verschilt van de▁eigen mute van▁een applicatie of▁een hardware mute switch; fixed-output▁toepassingen▁kunnen▁niet▁volgen▁standaard▁uitvoer▁wijzigingen.

Validatie: core▁profile/gesture/audio tests (audio endpoints▁gesimuleerd), Windows installer/update/UI tests, native mouse drag undo/redo en long-action editing.▁Fysische▁sleutel holds,▁werkelijke▁microfoon drivers en headset/speaker switching▁moeten▁nog▁steeds▁bevestiging op de hardware van de▁eigenaar.

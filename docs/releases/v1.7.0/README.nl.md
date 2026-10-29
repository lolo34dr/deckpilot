> ▁Dit is▁een▁automatische▁vertaling van de▁volledige release notes.▁Het▁Engelse▁origineel is de▁referentie.

DeckPilot 1.7.0 ▁vermindert▁herhaalde▁werkzaamheden▁tijdens▁het▁bewerken van▁profielen,▁het▁vinden van▁toepassingen en▁draaien op de▁achtergrond.

- ▁Het▁opslaan van▁ongewijzigde▁instellingen▁vervangt▁het▁bestand▁niet▁langer of▁maakt back-ups. De▁werkelijke▁schijfinhoud▁wordt▁nog▁steeds▁aangevinkt,▁dus▁externe▁bewerkingen,▁verwijderingen en▁mislukte▁opslaan▁zijn▁niet▁verborgen.
- ▁Profielgeschiedenis▁houdt▁losse model snapshots. Ongewijzigde▁bewerkingen en▁ongedaan▁maken/opnieuw▁voorkomen JSON▁ronde-trips;▁actiebewerking▁maakt▁gebruik van▁waardevergelijkingen in▁plaats van▁herhaalde serialisatie.
- ▁Toepassing▁zoeken en picker▁delen▁een▁vijf▁minuten durende▁catalogus cache en▁een▁ontdekking▁taak. Discovery▁loopt▁buiten WPF; Refresh▁nog▁steeds forceert▁een▁nieuwe scan.▁Elke picker▁bezit▁zijn filter en▁houdt de▁huidige▁resultaten▁bruikbaar▁tijdens▁het▁vernieuwen.
- Vervang de▁permanente 100 ms board UI poll met▁bestelde▁verbinding▁meldingen. Keyboard-haak callbacks▁wachtrij▁visueel▁werk in▁plaats van▁twaalf▁toetsen synchroon▁renderen.
- Audiostatus▁peilt om de▁twee▁seconden▁terwijl▁zichtbaar en▁elke▁tien▁seconden▁wanneer▁verborgen / geminimaliseerd,▁verfrissend▁onmiddellijk▁bij▁heropening en na audio-opdrachten. Overlappende▁verzoeken▁tot▁vernieuwing▁worden gecoëëerd▁zonder de post-commando-update te▁laten▁vallen.
- Verborgen▁vensters▁stoppen▁visuele feedback timers en overslaan▁cosmetische▁rendering▁wacht.▁Bevroren UI penselen▁worden▁hergebruikt en▁gelanceerd applicatie▁proces wrappers▁worden▁verwijderd.

▁Downloaden **DeckPilot-Setup.exe** of update van de app.▁Profielen,▁kalibratie- en opstartvoorkeuren▁worden▁bewaard. Er is▁geen firmware-flits▁nodig.

Validatie▁omvat▁instellingen /▁geschiedenis / cache▁regressies en Windows-installatie, update, picker, drag-and-drop,▁korte/lange▁actie▁bewerken en▁minimaliseren /▁herstellen▁controles.▁Dit▁zijn▁structurele optimalisaties,▁niet▁een▁gemeten claim van▁een▁bepaalde CPU of RAM te▁besparen op▁elke PC.

> ▁Dit is▁een▁automatische▁vertaling van de▁volledige release notes.▁Het▁Engelse▁origineel is de▁referentie.

DeckPilot 1.6.4 ▁lost▁zoekresultaten▁vervanging▁tijdens drag-and-drop en stabiliseert Windows▁verificatie.

- ▁Bewaar▁bestaande▁zoekknoppen▁wanneer de debounce timer of de▁geïnstalleerde-app-catalogus▁dezelfde▁resultaten▁oplevert,▁zodat▁een drag-bron▁niet▁onverwacht▁wordt▁verwijderd.
- Defer▁zoekresultaten▁vervanging▁terwijl▁een▁muis▁interactie of OLE drag is in▁uitvoering.
- Windows UI-controles▁maken▁gebruik van▁fysieke cursorcoördinaten, focusbesturingen voor▁het meten▁ervan, en scan▁het▁volledige scrollbereik in▁plaats van▁een vast▁aantal▁lijnstappen. Native▁muisslepen▁blijven▁vereist;▁mislukte▁opdrachten▁worden▁niet▁opnieuw▁geprobeerd om▁een▁pas te▁verkrijgen.
- Windows-instellingen▁slaat de▁speciale atomaire▁vervanging API,▁zodat▁een▁bestaande▁lezer▁zijn▁oude snapshot▁kan▁behouden▁zonder▁het▁blokkeren van de▁volgende▁opslag.▁Een▁kern▁regressie▁controleert▁drie▁opeenvolgende▁vervangingen met▁een▁gedeelde▁lezer open▁gehouden.
- ▁Instellingen▁lezers in Windows-tests▁gebruiken UTF-8 en atomaire▁bestandsvervanging▁toestaan. Snapshot opening▁wacht▁tot 500 ms▁alleen voor Windows▁delen/lock▁schendingen▁tijdens de▁vervanging;▁andere▁leesfouten en▁aanhoudende▁sloten▁falen de test.▁Drie undo/redo▁cycli▁houden▁ook▁een▁oude snapshot open om te▁controleren▁dat▁slaat▁slagen▁zonder▁het▁wijzigen van▁bestaande▁lezers.
- ▁Achtergrond-opstartcontroles▁wachten▁tot WPF▁klaar is met▁het▁verbergen van▁het▁venster,▁dan▁moet▁het▁proces▁draaiend▁blijven▁zonder▁zichtbaar▁hoofdvenster.
- GitHub-acties upgraden▁naar Node.js 24 ▁versies en installeer▁dezelfde Arduino CLI 1.2.2 ▁uit▁zijn▁vooraf▁geverifieerde checksum-archief.

▁Downloaden **DeckPilot-Setup.exe** of update van de app.▁Profielen en▁kalibratie▁worden▁bewaard. Er is▁geen firmware▁flits▁nodig.

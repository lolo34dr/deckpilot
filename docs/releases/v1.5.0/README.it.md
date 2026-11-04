> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.5.0 aggiunge i controlli Spotify-specific, la ricerca di azione e drag-and-drop, e ripara la registrazione di avvio di Windows.

- **Spotify** play/pause, indirizzo successivo e precedente solo la sessione dei supporti desktop Spotify, mantenendo la riproduzione del browser/YouTube separata. Apri Spotify e avvia la riproduzione una volta. Spotify in un browser non è supportato; non c'è alcun fallback multimediale globale se Spotify manca o rifiuta un comando.
- Ricerca di azioni e applicazioni installate per nome o parole chiave come volume, skip, Spotify o Discord. Fare clic sui risultati o trascinarli su una chiave.
- Trascinare le azioni di catalogo/ricerca sui tasti, scambiare le azioni di due chiavi senza modificare la calibrazione, o rilasciare un link eseguibile, scorciatoia, cartella o HTTP(S).
- Startup mantiene una preferenza salvata per utente, ripara i percorsi mancanti/stale, rileva le bandiere disabili di Windows note e supporta la riattivazione esplicita.

La costruzione richiede Windows 10 versione 2004 o più tardi, o Windows 11. Scarica **DeckPilot-Setup.exe** o aggiornamento da DeckPilot. Profili e taratura sono conservati; non è necessario alcun flash firmware. Per riparare un'avvio precedentemente bloccato, controllare il suo stato in **Préférences de l’application** e abilitare **Lancer avec Windows** di nuovo.

La convalida include l'isolamento simulato di Spotify/browser, i controlli di ricerca e impostazioni, i test di riparazione di Windows installer/startup, i controlli reali di drag-and-drop del mouse e di aggiornamento/UI nativo.

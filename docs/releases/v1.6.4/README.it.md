> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.6.4 corregge la sostituzione dei risultati di ricerca durante il drag-and-drop e stabilizza la verifica di Windows.

- Mantenere i pulsanti di ricerca esistenti quando il timer debounce o il catalogo installato-app produce gli stessi risultati, quindi una fonte di trascinamento non è staccata inaspettatamente.
- Deferire la sostituzione di ricerca-risultato mentre un'interazione del mouse o la resistenza OLE è in corso.
- I controlli dell'interfaccia utente di Windows utilizzano le coordinate fisiche del cursore, i controlli di messa a fuoco prima di misurarli, e la scansione dell'intervallo di scorrimento completo invece di assumere un numero fisso di incrementi di linea.
- Le impostazioni di Windows salvano l'utilizzo dell'API di sostituzione atomica dedicata in modo che un lettore esistente possa conservare la sua vecchia snapshot senza bloccare il salvataggio successivo.
- I lettori delle impostazioni nei test di Windows utilizzano UTF-8 e consentire la sostituzione del file atomico. Apertura istantaneo aspetta fino a 500 ms solo per la condivisione di Windows / bloccare le violazioni durante la sostituzione; altri errori di lettura e serrature persistenti falliscono il test. Tre cicli di annullare / eseguire anche mantenere una vecchia snapshot aperta per verificare che salva successo senza modificare i lettori esistenti.
- I controlli di avvio dello sfondo aspettano che WPF finisca di nascondere la sua finestra, quindi richiedono che il processo rimanga in esecuzione senza finestra principale visibile.
- Azioni GitHub per Node.js 24 versioni e installa lo stesso Arduino CLI 1.2.2 dal suo archivio a monte verificato.

Scarica **DeckPilot-Setup.exe** o l'aggiornamento dall'app. I profili e la calibrazione sono conservati. Non è necessario alcun flash firmware.

> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.7.0 riduce il lavoro ripetuto durante la modifica dei profili, la ricerca di applicazioni e l'esecuzione in background.

- Risparmio di impostazioni invariate non sostituisce più il file o crea backup. I contenuti del disco effettivo sono ancora controllati, quindi le modifiche esterne, le cancellazioni e i risparmi falliti non sono nascosti.
- La cronologia dei profili mantiene le istantanee dei modelli staccati. Le modifiche non modificate e annullano/redo evitano i round-trip di JSON; l'editing d'azione utilizza i confronti dei valori invece della serializzazione ripetuta.
- Ricerca applicazione e picker condividono una cache di catalogo di cinque minuti e un compito di scoperta. Discovery corre fuori WPF; Refresh ancora costringe una nuova scansione. Ogni raccoglitore possiede il suo filtro e mantiene i risultati attuali utilizzabili durante il aggiornamento.
- Sostituire il permanente 100 ms bordo sondaggio UI con le notifiche di connessione ordinate. Keyboard-hook callbacks coda lavoro visivo invece di rendere dodici chiavi sincronizzato.
- Lo stato dell'audio sonda ogni due secondi mentre visibile e ogni dieci secondi quando nascosto/minimizzato, rinfrescante immediatamente sulla riapertura e dopo i comandi audio.
- Le spazzole UI congelate vengono riutilizzate e vengono smaltiti i wrapper del processo di applicazione lanciati.

Scarica **DeckPilot-Setup.exe** I profili, le preferenze di calibrazione e di avvio sono conservati e non è richiesto alcun flash firmware.

La validazione include impostazioni/regressioni di storia/cache e installazione di Windows, aggiornamento, picker, drag-and-drop, editing di azione breve/lungo e minimizzare/ripristinare i controlli.

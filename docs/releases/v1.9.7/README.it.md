> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.9.7 corregge Atelier disinstallare mentre l'app è aperta. Atelier 1.0.6 corregge anche il suo manifesto DPI di Windows, che ha impedito all'applicazione installata di lanciare.

DeckPilot 1.9.6 corregge il manifesto DPI di Windows che ha impedito DeckPilot Atelier dall'apertura dopo l'installazione.

DeckPilot 1.9.5 include la diagnostica di avvio Atelier dettagliata nel controllo di rilascio di Windows.

DeckPilot 1.9.4 migliora il test di fumo di avvio Atelier Windows per segnalare i dettagli dell'applicazione crash.

DeckPilot 1.9.3 corregge i metadati di Atelier per leggere il manifesto del firmware dall'archivio firmware in bundle.

DeckPilot 1.9.2 corregge il passo dell'imballaggio di rilascio di Atelier e migliora la sua diagnostica di costruzione.

DeckPilot 1.9.1 migliora l'avvio dell'installatore Atelier e aggiunge i controlli automatici dell'aggiornamento dell'app e del firmware.

- L'installatore lancia Atelier nella sessione utente firmata di Windows e ferma/riavvia in modo sicuro per gli aggiornamenti.
- Atelier controlla GitHub all'avvio e a richiesta, annuncia nuovi Atelier costruisce o firmware in bundle, quindi scarica un programma di installazione verificato contro la SHA-256 file.
- Firmware è solo lampeggiato dopo che l'utente sceglie l'azione flash; un aggiornamento app non scrive mai a una scheda collegata da solo.

L'installatore Atelier lancia anche nella sessione Windows dell'utente firmata dopo una nuova installazione. Il flusso di lavoro di rilascio apre ora l'app installata, quindi verifica l'aggiornamento e la disinstallazione durante l'esecuzione.

DeckPilot 1.9.0 aggiunge DeckPilot Atelier, un'applicazione di officina di Windows separata per lampeggiare nuovi macropad e controllare tutti 12 Le chiavi.

- Installare il firmware incluso sulle schede Leonardo / Pro Micro ATmega32U4 con il bootloader Caterina.
- Eseguire controlli guidati a breve pressione e a lunga pressione per ogni chiave fisica, registrare la sua mappatura F13-F24 e salvare un rapporto di controllo della qualità.
- Scarica **DeckPilot-Atelier-Setup.exe** da questo rilascio. L'originale DeckPilot configurazione e firmware rimangono disponibili separatamente.

DeckPilot 1.8.0 aggiunge uno spazio di lavoro rinfrescato e modi più veloci per costruire e utilizzare i tuoi profili.

- Clearer charcoal/teal interface, prima libreria di azione, croccante 3 × 4 chiavi, colori di accento chiave e la preferenza di movimento ridotto.
- Tre modelli di profili modificabili: Musica e audio, Desktop e produttività, e Creazione.
- Azioni del profilo: passare a un profilo chiamato, il prossimo o il profilo precedente da una chiave.
- Azioni di Windows: aprire impostazioni comuni o Task Manager, o bloccare esplicitamente il PC.
- Copiare / tagliare / incollare / ripristinare intere chiavi, tra cui lunghe presse e colori, con undo/redo e un app-local clipboard.
- Catturare una scorciatoia da tastiera direttamente; Escape cancella e le chiavi di macropad rimangono riservate durante la cattura.
- Ricerca d'azione Ctrl+K e una storia chiara e semplice dell'ultima sessione 30 azioni.
- Fissare il prompt del firmware automatico in modo da accettare che si apre il firmware manager. I gesti in attesa vengono cancellati attraverso le modifiche del profilo.

Scarica **DeckPilot-Setup.exe** o aggiornamento dall'app. Vengono conservati profili, preferenze di calibrazione e avvio esistenti. **1.1.1**; non è necessario alcun flash per queste caratteristiche.

La convalida include la compatibilità del core/navigation/import/template e l'installazione nativa di Windows, l'aggiornamento, il picker, il drag-and-drop, undo/redo, la creazione di modelli, le azioni del profilo, la cattura di scorciatoie, la modifica di appunti, il layout e minimizzare / ripristinare i controlli.

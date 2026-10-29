> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.6.1 corregge il rendering della tastiera morbida e aggiunge il recupero del profilo, i controlli audio di Windows e le presse brevi / lunghe.

- Testo del tastierino di formato nativo: rimosso gli effetti dell'ombra dell'intero-deck e del contenuto, aggiunta per-monitor consapevolezza DPI e le icone di app desktop più grandi.
- Undo/redo per le modifiche del profilo, tra cui drag-and-drop (50 modifiche per sessione app); i campi di testo mantengono nativi Ctrl+Z/Ctrl+Y.
- Importazione/esportazione del profilo 20 Le importazioni aggiungono nuove copie; ripristinano e annullano la calibrazione. Le azioni importate non vengono mai eseguite automaticamente.
- Il microfono globale di Windows si attiva con un indicatore di stato-bar persistente e lo stato di endpoint dal vivo. Tutti gli endpoint di cattura attivi vengono disattivati / non modificati insieme.
- Toggle tra due dispositivi di uscita collegati scelti, per tutti i ruoli audio predefiniti di Windows. I dispositivi mancanti non riescono senza scegliere silenziosamente un'altra uscita.
- Due azioni configurate per chiave con un 200–3000 Soglia ms. Corto viene eseguito sul rilascio, a lungo sostituisce breve e viene eseguito una volta. Le azioni immediate esistenti e le chiavi F13-F24 gratuite mantengono il loro comportamento.

Scarica **DeckPilot-Setup.exe** Le scelte di endpoint audio possono essere necessarie per la regolazione dopo l'importazione di profili su un altro PC. Lo stato di endpoint di Windows differisce dal muto di un'applicazione o da un interruttore di mte hardware; le applicazioni di uscita fissa non possono seguire le modifiche di uscita predefinite.

Validazione: test core profile/gesture/audio (audio endpoint simulati), test di installazione di Windows/update/UI, mouse nativo drag undo/redo e editing a lunga azione.

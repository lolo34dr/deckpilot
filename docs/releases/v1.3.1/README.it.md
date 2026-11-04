> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 1.3.1 migliora la connessione USB e controlla il firmware macropad all'avvio.

- cruscotto di connessione con ricollegamento automatico, firmware rilevato, stato del pulsante vivo / diagnostica USB e selezione esplicita della scheda quando diversi dispositivi sono presenti.
- Avviare i controlli del firmware e aggiornare le proposte, con un 24-ora ritardo del promemoria e un'opzione per disabilitare le notifiche del firmware.
- Nuovo firmware **1.1.0**: messaggi di protocollo rilegati, token di sessione freschi, risposte battito cardiaco correlate e eventi key-state. La mappatura F13-F24 rimane invariata, compreso l'uso standalone.
- Firmware manager rilascia automaticamente la porta seriale del monitor prima dell'identificazione/flashing e ricollega dopo. 1.0.0 e le query di identità legacy rimangono sostenute.
- I profili possono essere selezionati dal menu del vassoio; Ctrl+S salva le modifiche; la diagnostica di connessione può essere copiata per il supporto.

Gli aggiornamenti firmware vengono consegnati in app releases; i controlli di avvio confrontano la versione rilevata contro il bundle incluso. **Firmware 1.1.0 ha ancora bisogno di un primo test fisico-board:** firmware firmware firmware firmware 1.0.0 e la procedura flash precedente sono stati convalidati dal proprietario, mentre questo nuovo protocollo è coperto da test host e compilazione AVR.

Scarica **DeckPilot-Setup.exe** o aggiornamento dall'app. Profili, preferenze di calibrazione e avvio vengono mantenuti. DeckPilot prima di utilizzare Arduino IDE o un altro monitor seriale; l'apertura del firmware manager in-app rilascia automaticamente la connessione.

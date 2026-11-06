> Questa è una traduzione automatica delle note di rilascio complete. L'originale inglese è il riferimento.

DeckPilot 2.1.6 e DeckPilot Atelier 1.0.12 rendere il firmware di Atelier cancellare esplicita e recuperabile.

- Spostare la cancellazione del firmware distruttivo in operazioni avanzate collassate e richiedono la digitazione `EFFACER` prima di scrivere.
- Spiegare che la scheda smette di enumerare come una tastiera dopo la cancellazione, mostrare i passaggi di recupero RST / GND e mantenere l'azione di recupero di Atelier disponibile dopo la cancellazione o un flash interrotto.
- Resta da fare 1.1.1N. DeckPilot è necessaria la modifica del firmware hardware.

La convalida include i controlli firmware e app, oltre all'automazione UI di Windows per il controllo avanzato delle cancellazioni, la sicurezza senza dispositivi e la guida di recupero RST/GND.

DeckPilot 2.1.5 leviga l'audio-dispositivo rinfresca e aggiunge una schermata di avvio di marca.

- Scopri i microfoni fisici e VB-CABLE i retries automatici condividono un'istantanea del dispositivo recente; il pulsante di aggiornamento manuale esegue sempre una scansione fresca.
- Ridurre l'inquinamento audio-stato di Windows ripetuto e semplificare il percorso di conversione monomicrofono comune.
- Mostrare il DeckPilot banner mentre l'applicazione interattiva inizia. avvio silenzioso con Windows rimane in background.
- Atelier rimane 1.0.11 e il firmware rimane 1.1.1.

La validazione include i controlli di scansione/cache di sfondo, i controlli di conversione audio, le build di Windows, l'aggiornamento/disinstallazione dell'installatore, i controlli nativi splash/update e application-picker.

DeckPilot 2.1.4 avvia automaticamente il microfono virtuale configurato con l'app, inclusa l'avvio di sfondo dopo l'accesso di Windows.

- L'avvio automatico del microfono della scheda audio è abilitato per impostazione predefinita per le impostazioni esistenti e nuove. VB-CABLE; non è necessaria alcuna attivazione manuale dopo il riavvio del PC quando DeckPilot inizia con Windows.
- Se i dispositivi non sono pronti all'accesso, o l'audio si ferma dopo una disconnessione, DeckPilot ogni cinque secondi mentre l'attivazione automatica è attivata.
- Un arresto esplicito disabilita e salva la preferenza di avvio automatico; abilitarlo di nuovo dal Soundboard pannello o il pulsante di attivazione. I clip audio non vengono mai riprodotti automaticamente.
- Atelier rimane 1.0.11 e il firmware rimane 1.1.1.

La convalida include i controlli di migrazione/rotondo-trip delle impostazioni e un controllo utente di Windows nativo per la preferenza di avvio automatico persistito, oltre ai controlli audio e installatori esistenti.

DeckPilot 2.1.3 riduce la latenza del microfono della scheda audio e rimuove i blocchi di conversione di un secondo dal percorso audio dal vivo.

- Utilizzare l'output-driven, piccolo-blocco di risampling per il microfono e clip audio; i tassi di campionamento corrispondenti bypassa completamente.
- Richiedi evento-driven 20 ms cattura e 40 ms buffer di riproduzione invece del precedente 100 impostazioni ms. Latenza effettiva dipende da Windows, i dispositivi audio e VB-CABLE.
- Bound microfono buffering a 100 ms e conservare i recenti frame completi dopo uno stallo di riproduzione invece di mettere in coda il vecchio discorso.
- Supporta ingressi PCM/float mono, stereo, multicanale e Windows estensibili senza la Media Foundation un secondo di lettura.
- Mantenere l'elaborazione Discord e il routing invariato. Atelier rimane 1.0.11 e il firmware rimane 1.1.1.

La validazione include controlli di regressione per il campionamento di dimensioni, la conversione dei canali, i formati estensibili, il recupero in esecuzione, la freschezza di troppopieno, la cattura / il completamento concomitante di acquisizione e clip, accanto ai test di installazione e applicazione Windows esistenti.

DeckPilot 2.1.2 e DeckPilot Atelier 1.0.11 fissare la sicurezza dell'officina, l'affidabilità del suono e i suoni del profilo portatile.

- Atelier può preparare tavole Leonardo/Caterina che non rispondono ancora DeckPilot protocollo; i guasti seriali fermano ancora il flashing e la posizione USB, il bootloader e la firma MCU rimangono verificati.
- Ogni ispezione inizia con i risultati vuoti. I report mantengono la scheda e il firmware testati, e i callback USB stanti non possono influenzare una nuova sessione.
- Chiusura Atelier durante la scrittura del firmware o la verifica è bloccata.
- Le esportazioni di profilati e i backup automatici dei profili possono conservare i file MP3/WAV di riferimento (32 MiB audio limit). Importa copia clip incorporati in una cartella gestita e rimostra riferimenti d'azione brevi / lunghi. Le importazioni del profilo Legacy rimangono supportate; l'audio di backup mancante viene segnalato esplicitamente.
- La scheda audio rilascia clip finiti, aggiorna gli indicatori di riproduzione e segnala i flussi audio interrotti. VB-CABLE endpoints sono riconosciuti attraverso la loro identità adattatore.
- Corretto tradotto link README e conservare i nomi dei dispositivi tecnici durante la traduzione. VB-CABLE requisito.

La convalida include i controlli di regressione del nucleo, i test del protocollo del firmware, i test di traduzione, la costruzione di Windows, DeckPilot e Atelier installazione / upgrade / disinstallazione controlli, e nativo interfaccia / aggiornamento controlli. Il primo-flash fisico e il comportamento di disconnessione audio richiedono ancora l'hardware del proprietario. 1.1.1; non è necessario alcun aggiornamento firmware.

DeckPilot 2.1.1 migliora l'installazione e gli aggiornamenti:

- Controllo automatico per VB-CABLE durante la prima installazione e aggiornamenti, e scaricare e lanciare l'installatore firmato ufficiale quando il driver è mancante.
- Mostra l'avvio di Windows, i controlli automatici di aggiornamento e le preferenze di movimento ridotto durante la configurazione e gli aggiornamenti, con le scelte esistenti preselezionate e conservate.
- Mantenere il microfono fisico selezionato e richiedere la configurazione del microfono solo quando necessario.
- Fissare l'inizializzazione dell'installatore e il rilevamento delle installazioni esistenti.

Scarica **DeckPilot-Setup.exe** o accettare l'aggiornamento in DeckPilot. L'approvazione dell'amministratore di Windows è necessaria. I profili esistenti e la calibrazione sono conservati. DeckPilot Atelier rimane 1.0.10 e il firmware rimane a 1.1.1; entrambi sono inclusi nel rilascio condiviso.

Convalida: i controlli di base e la creazione di Windows, l'aggiornamento/disinstallazione dell'installatore e le prove di applicazione native vengono eseguite prima della pubblicazione del rilascio.

DeckPilot 2.1.0 mantiene l'interfaccia nera profonda e colori personalizzabili, e percorre la soundboard attraverso VB-Audio VB-CABLE Gli utenti selezionano solo il loro microfono fisico; l'endpoint di riproduzione virtuale viene fissato in modo che le clip vocali e MP3/WAV non possano essere inviate accidentalmente ai diffusori. DeckPilot installer può scaricare il funzionario VB-CABLE pacchetto, verifica il suo editore Authenticode e avvia la configurazione del driver del fornitore; il pacchetto non viene ridistribuito. CABLE Output come il microfono. Atelier rimane in versione 1.0.10.

DeckPilot 1.9.11 e DeckPilot Atelier 1.0.10 aggiungere l'hardware pre-flash e la diagnostica del firmware, un confronto della versione del firmware, un'azione di recupero guidata dopo le scritture interrotte, il flash multi-board sequenziale con un rapporto di batch, e un controllo del layout fisico-position-to-F13-F24 in ogni report di controllo chiave.

DeckPilot 1.9.11 include lo stesso aggiornamento di Atelier nel rilascio dell'installatore condiviso.

DeckPilot 1.9.10 ripristina i rilasci condivisi contenenti entrambi DeckPilot e DeckPilot Atelier 1.0.9. Entrambe le applicazioni controllano ancora una volta lo stesso ultimo rilascio GitHub.

Se installato Atelier 1.0.8 dal canale di rilascio separato, installare DeckPilot-Atelier-Setup.exe da questa versione una volta per tornare agli aggiornamenti condivisi. DeckPilot può utilizzare il loro aggiornamento esistente.

DeckPilot Atelier 1.0.7 aggiunge una cancellazione programma-area confermata che cancella tutto 28 KiB dell'utente flash, lascia un loop di ripristino sicuro, e conserva Caterina e EEPROM. Reset rimane disabilitato fino a quando non viene selezionata una scheda fisica.

DeckPilot 1.9.7 corregge Atelier disinstallare mentre l'app è aperta. Atelier 1.0.6 corregge anche il suo manifesto DPI di Windows, che ha impedito all'applicazione installata di lanciare.

DeckPilot 1.9.6 corregge il manifesto DPI di Windows che ha impedito DeckPilot Atelier dall'apertura dopo l'installazione.

DeckPilot 1.9.5 include la diagnostica di avvio Atelier dettagliata nel controllo di rilascio di Windows.

DeckPilot 1.9.4 migliora il test di fumo di avvio Atelier Windows per segnalare i dettagli dell'applicazione crash.

DeckPilot 1.9.3 corregge i metadati di Atelier per leggere il manifesto del firmware dall'archivio firmware in bundle.

DeckPilot 1.9.2 corregge il passo dell'imballaggio di rilascio di Atelier e migliora la sua diagnostica di costruzione.

DeckPilot 1.9.1 migliora l'avvio dell'installatore Atelier e aggiunge i controlli automatici dell'aggiornamento dell'app e del firmware.

- L'installatore lancia Atelier nella sessione utente firmata di Windows e ferma/riavvia in modo sicuro per gli aggiornamenti.
- Atelier controlla GitHub all'avvio e a richiesta. Annuncia più recente Atelier costruisce o firmware in bundle, quindi scarica un installatore verificato contro il pubblicato SHA-256 file.
- Firmware è solo lampeggiato dopo che l'utente sceglie l'azione flash; un aggiornamento app non scrive mai a una scheda collegata da solo.

L'installatore Atelier lancia anche nella sessione Windows dell'utente firmata dopo una nuova installazione. Il flusso di lavoro di rilascio apre ora l'app installata, quindi verifica l'aggiornamento e la disinstallazione durante l'esecuzione.

DeckPilot 1.9.0 aggiunge DeckPilot Atelier, un app di officina di Windows separata per lampeggiare nuovi macropad e controllare tutti 12 Le chiavi.

- Installare il firmware incluso sulle schede Leonardo / Pro Micro ATmega32U4 con il bootloader Caterina.
- Eseguire controlli guidati a breve pressione e a lunga pressione per ogni chiave fisica, registrare la sua mappatura F13-F24 e salvare un rapporto di controllo della qualità.
- Scarica **DeckPilot-Atelier-Setup.exe** da questo rilascio. L'originale DeckPilot configurazione e firmware rimangono disponibili separatamente.

DeckPilot 1.8.0 aggiunge uno spazio di lavoro rinfrescato e modi più veloci per costruire e utilizzare i tuoi profili.

- Clearer charcoal/teal interface, prima libreria di azione, croccante 3 × 4 chiavi, colori di accento chiave e la preferenza di movimento ridotto.
- Tre modelli di profili modificabili: Musica e audio, Desktop e produttività, e Creazione.
- Azioni del profilo: passare a un profilo chiamato, il prossimo o il profilo precedente da una chiave.
- Azioni di Windows: aprire impostazioni comuni o Task Manager, o bloccare esplicitamente il PC.
- Copia/taglio/incolla/reset interi tasti, comprese le presse lunghe e i colori, con undo/redo e un app-local clipboard.
- Catturare una scorciatoia da tastiera direttamente; Escape cancella e le chiavi di macropad rimangono riservate durante la cattura.
- Ricerca d'azione Ctrl+K e una storia chiara e semplice dell'ultima sessione 30 azioni.
- Fissare il prompt del firmware automatico in modo da accettare che si apre il firmware manager. I gesti in attesa vengono cancellati attraverso le modifiche del profilo.

Scarica **DeckPilot-Setup.exe** o aggiornamento dall'app. Vengono conservati profili, preferenze di calibrazione e avvio esistenti. **1.1.1**; non è necessario alcun flash per queste caratteristiche.

La convalida include la compatibilità del core/navigation/import/template e l'installazione nativa di Windows, l'aggiornamento, il picker, il drag-and-drop, undo/redo, la creazione del modello, le azioni del profilo, la cattura di scorciatoie, l'editing di appunti, il layout e minimizzare/ripristinare i controlli.

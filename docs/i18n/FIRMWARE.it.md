> Questa è una traduzione automatica della documentazione completa del firmware. L'originale inglese è il riferimento. Comandi e identificatori sono conservati esattamente.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware firmware firmware firmware

Per la configurazione hardware testata: **Pro Micro ATmega32U4, 5 V / 16 MHz**, usando il **Arduino Leonardo** profilo di bordo e un **Caterina / AVR109** bootloader. USB-C descrive il connettore, non la compatibilità della scheda. Lo schizzo originale del proprietario è stato caricato con successo con il 1200-Reset e AVR109 a vuoto 57600 baud. Il proprietario ha anche confermato successo in-app flashing, firmware 1.0.0 identificazione e funzionamento di tutti 12 pulsanti su quella scheda di riferimento.

<a id="wiring-and-behavior"></a>
## Cablaggio e comportamento

Ogni interruttore collega un perno a GND. Le pull-up interne sono abilitate. Perni **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** invio **F13 fino a F24** in quell'ordine. 30 Il comportamento della tastiera, i profili di Windows e la calibrazione rimangono compatibili; il rollover della tastiera USB rimane limitato dalla libreria di tastiera di Arduino. Il firmware non introduce le modalità USB per pulsante o i controlli dei media nativi.

Pinocchina **1** tenuto a GND all'avvio disabilita il funzionamento della tastiera, mantenendo il failsafe originale. Il nucleo USB può ancora supportare l'ingresso del bootloader.

L'attuale interfaccia seriale USB CDC risponde `DECKPILOT?\n` a) 115200 baud con:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

Non è richiesta alcuna connessione seriale per il normale funzionamento della tastiera. L'identità è un controllo di compatibilità, non autenticazione dell'hardware crittografico. Windows riporta ancora gli identificatori USB Arduino Leonardo; molte altre schede condividono questi identificatori.

<a id="install-from-deckpilot"></a>
## Installa da DeckPilot

1. Mantenere lo schizzo di recupero fornito (`firmware/Original/Original.ino`) e una configurazione di caricamento Arduino funzionante come opzione di recupero. Tenere RST e GND fisicamente accessibile.
2. Chiudere Arduino IDE, Serial Monitor e altri strumenti seriali. Scollegare altre schede Leonardo.
3. Aprire **Firmware du macropad…**, scegliere il dispositivo corretto e confermare la scheda e il cablaggio. Uno schizzo originale non identificato può essere installato solo dopo una conferma esplicita.
4. Confermare l'installazione e lasciare il cavo USB collegato fino al completamento. DeckPilot mentre la finestra di dialogo del firmware è aperta; gli eventi ordinari F13-F24 raggiungono ancora Windows.

Il firmware e Windows AVRDUDE 8.0 sono in bundle all'interno dell'app pubblicata. No Arduino IDE o installazione del driver separata viene eseguita da DeckPilot. Windows deve già esporre la porta seriale USB della scheda, come ha fatto per il caricamento di successo del proprietario. Le nuove versioni del firmware vengono consegnate con gli aggiornamenti dell'app da GitHub; l'aggiornamento dell'app fa **No.** ogni flash richiede la conferma.

DeckPilot assegni SHA.256, controllo record Intel HEX, presenza del vettore di reset, sovrapposizioni e **28672- Ciao.** corrisponde alla posizione fisica USB attraverso le porte runtime e bootloader, utilizza AVR109 a 57600 baud, richiede la verifica di scrittura di AVRDUDE, quindi controlla l'identità e la versione del firmware in esecuzione. Non scrive fusibili, EEPROM o il bootloader. `2341:8036` / `2A03:8036` e Caterina `2341:0036` / `2A03:0036` Se Windows non può fornire una connessione fisica corrispondente, il flash è bloccato piuttosto che indovinare una porta.

Flashing non è atomico: un cavo di disconnessione o perdita di potenza può lasciare il firmware dell'applicazione incompleto. Non c'è alcun backup automatico del firmware o rollback. Se necessario, ricaricare lo schizzo originale con Arduino IDE impostato su **Arduino Leonardo**. A seconda del bootloader della scheda, collegando brevemente RST a GND due volte può esporre una porta di bootloader temporanea; caricare mentre quella porta è disponibile. Un bootloader danneggiato richiede hardware ISP e non può essere riparato da questo aggiornamento USB.

L'ultimo registro di processo AVRDUDE completato è memorizzato in `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## Costruzioni

Su Windows, dalla root del repository, installare Arduino CLI **1.2.2** e .NET SDK 8, poi correre:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

I perni di script Arduino AVR core **1.8.6**, tastiera **1.0.6**, e AVRDUDE a monte **8.0**. I pacchetti gestiti da CLI utilizzano la validazione del pacchetto di controllo di Arduino; gli archivi binari/source AVRDUDE hanno un esplicito SHA-256 perni nella sceneggiatura. `artifacts/firmware/DeckPilot-firmware.zip` contiene il HEX, manifesto, strumento di Windows, licenza e corrispondente archivio sorgente AVRDUDE a monte. L'editoria CI richiede il bundle; uno sviluppatore ordinario costruire senza che possa eseguire l'applicazione ma non può lampeggiare.

L'originale 12- Button sketch è stato adattato da David Madison **Flusso economico** progetto, diritto d'autore 2018, MIT. Il suo avviso rimane in `DeckPilot.ino`. DeckPilot le aggiunte utilizzano la licenza MIT del repository. **AVRDUDE è un software separato sotto GPL-2.0- o tardi.**, con il suo avviso / licenza e fonte in bundle; la licenza MIT del repository non sostituisce quella licenza. https://github.com/avrdudes/avrdude/tree/v8.0 per istruzioni di costruzione a monte e avvisi di dipendenza.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 e protocollo USB 2

Firmware 1.1.1 aggiunge un collegamento diagnostico di sola lettura pur mantenendo la stessa mappatura della tastiera F13-F24 e pin-1 avvio failsafe. **Il firmware coperto del test fisico del proprietario 1.0.0. Firmware 1.1.1 ha test di protocollo automatizzati e una costruzione AVR, ma ha ancora bisogno del suo primo test sulla scheda reale.** I profili e la calibrazione rimangono sul PC e non devono essere ricreati per questo aggiornamento firmware.

La nuova app identifica la scheda collegata, mantiene la sua porta seriale aperta, ricollega dopo aver smontato, e controlla la sua versione firmware all'avvio e dopo la riconnessione.Gli aggiornamenti firmware vengono consegnati con le versioni app: i controlli di avvio confrontano il firmware connesso con il bundle convalidato incluso nell'app installata.

Una volta per sessione dell'app, un vecchio firmware rilevato richiede 24-ora di ritardo del promemoria quando posticipato. Le notifiche possono essere disabilitate sotto **Connexion et diagnostic**Gli schizzi non noti di Arduino, i dispositivi disconnessi e le selezioni ambigue di più carte non attivano il flash automatico o i prompt di aggiornamento. L'installazione di firmware richiede sempre la conferma. L'apertura del gestore del firmware sospende il monitor e aspetta che la porta seriale venga rilasciata; il monitoraggio riprende dopo la chiusura della finestra di dialogo.

**Per un caricamento manuale da Arduino IDE, smettere DeckPilot dalla sua icona del vassoio prima.** La chiusura della finestra principale lo nasconde solo, quindi continua a possedere la connessione seriale. DeckPilot e un altro monitor seriale non può utilizzare la stessa porta simultaneamente. Se un altro strumento possiede già la porta, l'app segnala il problema e le ripetizioni senza influenzare le chiavi della tastiera ordinaria.

Il nuovo `DeckPilotProtocol.h` è la stessa attuazione esercitata da `tests/firmware-protocol.cpp`. Utilizza buffer di dimensione fissa, l'elaborazione di input limitato e lo spazio di trasmissione USB disponibile; la scansione del pulsante non è mai volutamente ritardata in attesa di un host. L'ultima maschera del pulsante in attesa è carbonizzata sotto la pressione posteriore. Non ci sono comandi per eseguire macro, scrivere EEPROM, disabilitare la tastiera, modificare i profili o il firmware flash su questo protocollo diagnostico.

Attorno 115200 baud, le linee terminano con LF (CRLF è accettato). I token di sessione contengono esattamente otto caratteri esadecimali maiuscoli. Tutti i numeri sono decimali non firmati. Esempi:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` pubblicità 12 chiavi e bit di capacità: 1 = battito cardiaco/istantanea di stato, 2 = eventi chiave-stato. `PONG` riecheggia il contatore di richiesta, poi riporta uptime in millisecondi, sequenza di stato e la 12- Maschera a bottone a bit. `KEYS` report sequenza e maschera; bit 0 è F13 e bit 11 è F24, indipendente dalla calibrazione fisica. Sessioni e contatori di richiesta respingono risposte stanti. 32-bit wraparound. Le chiavi rimangono solo dati diagnostici: le azioni app funzionano ancora una volta attraverso il percorso della tastiera esistente, quindi un evento seriale non può eseguire la stessa azione due volte.

Le richieste di battito cardiaco vengono normalmente inviate ogni due secondi. L'app si riconnette se manca una risposta corrispondente 1.5 secondi; il firmware smette di diagnostica non sollecitata dopo sei secondi senza battito cardiaco o quando la connessione seriale si chiude. 1.0.0 rimane supportato attraverso la query di identità legacy, senza diagnostica chiave dal vivo. DeckPilot le app possono ancora leggere l'identità legacy del firmware 1.1.1.

La versione sorgente in `DeckPilotProtocol.h` fornisce la versione del manifesto di rilascio. I pacchetti del firmware pubblicati ora contengono anche lo schizzo/header corrente sotto `source/DeckPilot`, lo schizzo di recupero e DeckPilot Patente MIT.

Il collegamento diagnostico utilizza deliberatamente `Serial.dtr()` piuttosto che `bool(Serial)`: Nucleo AVR Arduino 1.8.6 inserisce un 10 ms ritardo nel `Serial_::operator bool()`. Lettura DTR mantiene direttamente il percorso diagnostico dall'aggiunta di quella pausa ad ogni ciclo key-scan. Il test del protocollo host elimina la conversione implicita bool in modo che questo modello di blocco non possa essere reintrodotto nell'implementazione del protocollo condiviso inosservato.

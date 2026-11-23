> Questa è una traduzione automatica della documentazione completa del firmware. L'originale inglese è il riferimento. Comandi e identificatori sono conservati esattamente.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware firmware firmware firmware

Per la configurazione hardware testata: **Pro Micro ATmega32U4, 5 V / 16 MHz**, usando il **Arduino Leonardo** profilo di bordo e un **Caterina / AVR109** bootloader. USB-C descrive il connettore, non la compatibilità della scheda. Lo schizzo originale del proprietario è stato caricato con successo con il 1200-Reset completo e AVR109 a 57600 baud. Il proprietario ha anche confermato il successo in-app flashing, firmware 1.0.0 identificazione e funzionamento di tutti 12 pulsanti su quella scheda di riferimento.

<a id="wiring-and-behavior"></a>
## Cablaggio e comportamento

Ogni interruttore collega un perno a GND. I pull-up interni sono abilitati. **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** invio **F13 fino a F24** in quell'ordine. 30 ms debounce gestisce la pressa e rilascia in modo indipendente. 1.1.2 utilizza un bit per chiave nel suo rapporto della tastiera USB in modo da poter rappresentare tutte le dodici chiavi detenute.

Pinocchina **1** tenuto a GND all'avvio disabilita il funzionamento della tastiera, mantenendo il failsafe originale. Il nucleo USB può ancora supportare l'ingresso del bootloader.

L'attuale interfaccia seriale USB CDC risponde `DECKPILOT?\n` a) 115200 baud con:

```
DECKPILOT|1|promicro-32u4-16mhz|1.2.0
```

Non è richiesta alcuna connessione seriale per il normale funzionamento della tastiera. L'identità è un controllo di compatibilità, non autenticazione dell'hardware crittografico. Windows riporta ancora gli identificatori USB Arduino Leonardo; molte altre schede condividono questi identificatori.

<a id="hybrid-input-mode-120"></a>
## Modalità di ingresso ibrido (1.2.0)

Entrambe le schede utilizzano lo stesso firmware. L'applicazione li identifica con l'istanza del dispositivo Windows USB e apre una connessione seriale per scheda. **Macropads**, fare clic su una chiave sulla scheda di ogni scheda per abilitare DeckPilot modalità e configura il suo profilo. **Calibratore** una scheda senza un numero di serie USB univoco può ottenere un'istanza diversa del dispositivo quando si sposta in un'altra presa USB.

Dopo una stretta di mano DP2 pubblicizza la capacità 4, l'app invia `DP2|INPUT|<session>`. La scheda risponde con `DP2|INPUT|<session>` quando tutte le chiavi e il rapporto della tastiera vengono rilasciati, o `DP2|BUSY|<session>` una volta attivata, la scheda invia ordinata `KEYS` cornici a partire da sequenza 1 e sopprime F13–F24. Client diagnostici che semplicemente inviano `HELLO` Tenere il normale comportamento della tastiera. Dropping DTR o manca il battito cardiaco locazione per sei secondi ripristina la tastiera dopo che vengono rilasciati i pulsanti detenuti. Una coda di evento completa termina la sessione di ingresso seriale con `DP2|LOST|<session>` invece di fondere silenziosamente gli eventi di stampa e di rilascio.

Test fisico su Windows:

1. Flash e verificare ogni scheda individualmente con **Firmware du macropad…**, poi collegare entrambe le schede.
2. Aprire **Macropads** e fare clic sul primo pulsante sulla scheda di ogni dispositivo. Assegnare un'azione diversa a ogni pulsante. Verificare che l'editor nomina la porta COM selezionata e che ogni scheda visualizza il proprio profilo.
3. Premere la stessa posizione su entrambi i mazzi insieme, poi ripetere con brevi e lunghe presse, ogni azione dovrebbe funzionare una volta sul suo profilo assegnato.
4. Scollegare un mazzo tenendo premuto un pulsante, verificare l'altro risponde ancora, e riconnettere il primo alla sua presa USB originale. DeckPilot dal vassoio di sistema e confermare una nuova pressa invia la corrispondente chiave F13–F24.

<a id="install-from-deckpilot"></a>
## Installa da DeckPilot

1. Mantenere lo schizzo di recupero fornito (`firmware/Original/Original.ino`) e una configurazione di caricamento Arduino funzionante come opzione di recupero. Tenere RST e GND fisicamente accessibile.
2. Chiudere Arduino IDE, Serial Monitor e altri strumenti seriali. Scollegare altre schede Leonardo.
3. Aprire **Firmware du macropad…**, scegliere il dispositivo corretto e confermare la scheda e il cablaggio. Un bozzetto originale non identificato può essere installato solo dopo una conferma esplicita.
4. Confermare l'installazione e lasciare il cavo USB collegato fino al completamento. DeckPilot mentre la finestra di dialogo del firmware è aperta; gli eventi ordinari F13-F24 raggiungono ancora Windows.

Il firmware e Windows AVRDUDE 8.0 sono in bundle all'interno dell'app pubblicata. No Arduino IDE o installazione del driver separata viene eseguita da DeckPilot. Windows deve già esporre la porta seriale USB della scheda, come ha fatto per il caricamento di successo del proprietario. Le nuove versioni del firmware sono fornite con gli aggiornamenti dell'app da GitHub; l'aggiornamento dell'app fa **No.** ogni flash richiede la conferma.

DeckPilot Atelier mantiene **Effacer le firmware de la carte** sotto le operazioni avanzate collassate, richiede la digitazione `EFFACER`# Sgombera tutto # 28 KiB di applicazione flash con un loop di reset verificato, e ferma F13–F24 e normale enumerazione USB. Caterina rimane disponibile, ma la scheda non apparirà di nuovo in Atelier fino a quando non viene messa manualmente in modalità bootloader. **RST** a **GND due volte rapidamente**, quindi selezionare la sua porta temporanea Arduino Leonardo / bootloader COM e utilizzare l'azione di recupero di Atelier per reinstallare DeckPilot. Atelier visualizza queste istruzioni e mantiene il recupero abilitato dopo la cancellazione o un flash interrotto. Il bootloader e EEPROM sono conservati; questa non è una cancellazione full-chip. I dati del profilo rimangono sul PC.

DeckPilot assegni SHA.256, controllo record Intel HEX, presenza del vettore di reset, sovrapposizioni e **28672- Ciao.** corrisponde alla posizione fisica USB attraverso le porte runtime e bootloader, utilizza AVR109 a 57600 baud, richiede la verifica di scrittura di AVRDUDE, quindi controlla l'identità e la versione del firmware in esecuzione. Non scrive fusibili, EEPROM o il bootloader. `2341:8036` / `2A03:8036` e Caterina `2341:0036` / `2A03:0036` Se Windows non può fornire una connessione fisica corrispondente, il flash è bloccato piuttosto che indovinare una porta.

Flashing non è atomico: un cavo di disconnessione o perdita di potenza può lasciare il firmware dell'applicazione incompleto. Non c'è alcun backup automatico del firmware o rollback. Se necessario, ricaricare lo schizzo originale con Arduino IDE impostato su **Arduino Leonardo**. A seconda del bootloader della scheda, collegando brevemente RST a GND due volte può esporre una porta di bootloader temporanea; caricare mentre quella porta è disponibile. Un bootloader danneggiato richiede hardware ISP e non può essere riparato da questo aggiornamento USB.

L'ultimo registro AVRDUDE completato dell'app principale è memorizzato in `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`. Atelier mantiene l'installazione e il ripristino dei registri `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log` e `last-reset.log`.

<a id="build"></a>
## Costruzioni

Su Windows, dalla root del repository, installare Arduino CLI **1.2.2** e .NET SDK 8, poi correre:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

I perni di script Arduino AVR core **1.8.6**, tastiera **1.0.6**, e AVRDUDE a monte **8.0**. I pacchetti gestiti da CLI utilizzano la validazione del pacchetto di controllo di Arduino; gli archivi binari/source AVRDUDE hanno un esplicito SHA-256 perni nella sceneggiatura. `artifacts/firmware/DeckPilot-firmware.zip` contiene DeckPilot HEX, ha generato l'installazione dell'area dell'applicazione HEX e il suo generatore, i checksum, lo strumento di Windows, la licenza e corrispondente archivio sorgente AVRDUDE a monte. L'editoria CI richiede il bundle; una costruzione dello sviluppatore ordinario senza che possa eseguire l'applicazione, ma non può lampeggiare o ripristinare una scheda.

L'originale 12- Button sketch è stato adattato da David Madison **Streaming a basso costo** progetto, diritto d'autore 2018, MIT. Il suo avviso rimane in `DeckPilot.ino`. DeckPilot le aggiunte utilizzano la licenza MIT del repository. **AVRDUDE è un software separato sotto GPL-2.0- o tardi.**, con il suo avviso / licenza e fonte bundle; la licenza MIT repository non sostituisce quella licenza. https://github.com/avrdudes/avrdude/tree/v8.0 per istruzioni di costruzione a monte e avvisi di dipendenza.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 e protocollo USB 2

Firmware 1.1.1 aggiunge un collegamento diagnostico di sola lettura pur mantenendo la stessa mappatura della tastiera F13-F24 e pin-1 avvio failsafe. **Il firmware coperto del test fisico del proprietario 1.0.0. Firmware 1.1.1 ha test di protocollo automatizzati e una costruzione AVR, ma ha ancora bisogno del suo primo test sulla scheda reale.** I profili e la calibrazione rimangono sul PC e non devono essere ricreati per questo aggiornamento firmware.

La nuova applicazione identifica la scheda collegata, mantiene la porta seriale aperta, si ricollega dopo aver smontato, e controlla la sua versione firmware all'avvio e dopo la riconnessione.Gli aggiornamenti firmware vengono consegnati con le versioni app: i controlli di avvio confrontano il firmware connesso con il bundle convalidato incluso nell'app installata.

Una volta per sessione di app, con un vecchio firmware rilevato richiede 24-ora di ritardo del promemoria quando posticipato. Le notifiche possono essere disabilitate sotto **Connexion et diagnostic**Gli schizzi non noti di Arduino, i dispositivi disconnessi e le selezioni ambigue di più carte non attivano il flash automatico o i prompt di aggiornamento. L'installazione di firmware richiede sempre la conferma. L'apertura del gestore del firmware sospende il monitor e aspetta che venga rilasciata la porta seriale; il monitoraggio riprende dopo la chiusura della finestra di dialogo.

**Per un caricamento manuale da Arduino IDE, smettere DeckPilot dalla sua icona del vassoio prima.** La chiusura della finestra principale lo nasconde solo, quindi continua a possedere la connessione seriale. DeckPilot e un altro monitor seriale non può utilizzare la stessa porta simultaneamente. Se un altro strumento possiede già la porta, l'app segnala il problema e le ripetizioni senza influenzare le chiavi della tastiera ordinaria.

La modalità diagnostica in `DeckPilotProtocol.h` utilizza buffer a dimensione fissa, l'elaborazione di input limitato e lo spazio di trasmissione USB disponibile. L'ultima maschera diagnostica in attesa è carbonizzata sotto backpressure. 1.2.0 aggiunge la modalità di ingresso seriale opt-in sopra.

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

`INFO` pubblicità 12 chiavi e bit di funzionalità: 1 = battito cardiaco/istantanea di stato, 2 = eventi chiave-stato, 4 = ingresso seriale opt-in. `PONG` eco il contatore di richiesta e segnala uptime, sequenza e maschera pulsante per la diagnostica. `KEYS` segnala sequenza e maschera; bit 0 corrisponde a F13 e bit 11 a F24, indipendente dalla calibrazione fisica. Le sessioni e i contatori di richiesta rifiutano le risposte stanti. In modalità di ingresso seriale, le azioni seguono l'ordine `KEYS` cornici.

Le richieste di battito cardiaco vengono normalmente inviate ogni due secondi. L'app ricollega se manca una risposta corrispondente 1.5 secondi; il firmware smette di diagnostica non sollecitata dopo sei secondi senza battito cardiaco o quando la connessione seriale si chiude. 1.0.0 rimane supportato attraverso la query di identità legacy, senza diagnostica chiave dal vivo. DeckPilot le app possono ancora leggere l'identità legacy del firmware 1.1.1.

La versione sorgente in `DeckPilotProtocol.h` fornisce la versione del manifesto di rilascio. I pacchetti del firmware Pubblicato ora contengono anche lo schizzo/intestazione corrente sotto `source/DeckPilot`, lo schizzo di recupero e DeckPilot Patente MIT.

Il collegamento diagnostico utilizza deliberatamente `Serial.dtr()` piuttosto che `bool(Serial)`: Nucleo AVR Arduino 1.8.6 inserisce un 10 ms ritardo nel `Serial_::operator bool()`. Lettura DTR mantiene direttamente il percorso diagnostico dall'aggiunta di quella pausa ad ogni ciclo key-scan. Il test del protocollo host elimina la conversione implicita bool in modo che questo modello di blocco non possa essere reintrodotto nell'implementazione del protocollo condiviso inosservato.

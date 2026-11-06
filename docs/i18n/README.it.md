> Questa è una traduzione automatica del README completo. L'originale inglese è il riferimento. L'interfaccia di applicazione etichette e comandi sono conservati esattamente.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="DeckPilot banner" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="logo DeckPilot" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Un compagno di Windows per il tuo 12- Macropad di fai da te.<br>
  Configurare le chiavi, le applicazioni di lancio, il supporto di controllo e i profili di commutazione.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Scarica l'ultima versione</a>
   · 
  <a href="#getting-started">Iniziare</a>
   · 
  <a href="#development">Sviluppo</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt="Ultimo rilascio"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt="Scarica per Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe"><img src="https://img.shields.io/badge/Download-DeckPilot_Atelier-16856A?style=flat-square&logo=windows" alt="Scarica DeckPilot Atelier"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="Licenza MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Stato di configurazione di Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt="GitHub scarica il rilascio"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

---

DeckPilot configura i macropad Arduino Leonardo compatibili che già inviano **F13–F24** come ingressi tastiera USB, disposti in **3 righe di 4 chiavi**. L'ordine fisico chiave può essere arbitrario: una calibrazione guidata mappa ogni pulsante alla sua posizione.

**Le funzioni della tastiera F13-F24 non necessitano di ulteriori modifiche del driver o del firmware. Soundboard la miscelazione richiede la firma VB-CABLE driver audio, installato da Impostazione quando manca.** L'interfaccia dell'applicazione è attualmente in francese. Scegli una lingua sopra per un README tradotto completo, incluso il riferimento tecnico e la cronologia delle versioni. Le traduzioni vengono generate automaticamente; questo originale inglese è il riferimento.

<a id="features"></a>
## Caratteristiche

- **Calibrazione guidata:** identificare tutti 12 pulsanti fisici senza cambiare il macropad.
- **Picker di applicazione installato:** ricerca Avviare le scorciatoie del menu e il catalogo delle applicazioni di Windows, comprese le voci di Microsoft Store.
- **Azioni personalizzate:** lanciare applicazioni, aprire cartelle o siti web, inviare scorciatoie e controllare i supporti.
- **Tasti liberi:** tenere F13-F24 disponibile per le scorciatoie configurate direttamente in un gioco o in un'altra applicazione.
- **Profili locali:** creare, duplicare, rinominare, eliminare e selezionare i profili. La calibrazione è condivisa tra i profili.
- **Profili preconfezionati:** avviare con i modelli di musica, produttività o creazione, quindi personalizzarli. Passare a un profilo specifico, successivo o precedente da una chiave fisica.
- **Elaborazione più veloce:** copiare / tagliare / incollare / ripristinare i tasti completi, registrare le scorciatoie della tastiera, scegliere i colori accenti chiave, e annullare le modifiche.
- **Strumenti di Windows:** aprire le impostazioni del suono, del display, del Bluetooth e della rete, Task Manager o bloccare la sessione.
- **Interfaccia scura compatta:** icone vettoriali, icone di applicazione dove disponibili, e animazioni sottili che rispettano le preferenze di animazione di Windows.
- **Funzionamento dello sfondo:** Tenga. DeckPilot nell'area di notifica, con l'avvio al login di Windows abilitato per impostazione predefinita (può essere disabilitato).
- **Aggiornamenti automatici:** approvare una nuova release, poi lasciare DeckPilot scaricare, verificare, sostituire e riavviarsi.

<a id="getting-started"></a>
## Iniziare

<a id="requirements"></a>
### Requisiti

- Windows 10 o Windows 11, **x64 x**.
- Un macropad Arduino Leonardo compatibile 12 chiavi distinte da **F13 a F24**.
- Una connessione dati USB.

I download pubblicati includono il runtime .NET. **Non è necessario installare .NET per l'uso DeckPilot.**

<a id="install-and-launch"></a>
### Installare e lanciare

1. Scarica **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** e eseguire l'installer. Windows chiede l'autorizzazione dell'amministratore per installare **Program Files\DeckPilot**.
2. L'installatore ti permette di scegliere se DeckPilot inizia con Windows, controlla gli aggiornamenti delle app automaticamente e riduce le animazioni. I controlli di avvio e di aggiornamento sono accesi per impostazione predefinita; le animazioni ridotte sono spente. VB-CABLE autista richiesto dal Soundboard e lo installa automaticamente se mancante. È necessaria una connessione internet per il controllo del driver.
3. Il programma di installazione aggiunge DeckPilot a Windows **App installate** e crea una scorciatoia del menu Start. Una scorciatoia del desktop è selezionata per impostazione predefinita.
4. Collegare il macropad e aprire DeckPilot dalla sua scorciatoia.
5. Seguire la calibrazione da sinistra a destra **quattro colonne**, poi la riga successiva, per **tre righe**. Ogni pulsante deve inviare una chiave diversa F13–F24.

Quando si aggiorna dall'ex 4- layout freccia, DeckPilot conserva profili/azioni e richiede una nuova calibrazione per le posizioni corrette. L'installazione su una configurazione portatile esistente preserva anche le impostazioni in `%APPDATA%\DeckPilot`; lasciare la vecchia copia se la configurazione ti chiede di farlo.

Un portatile [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) e [CAPITOLO](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) Per l'uso portatile, mantenere l'eseguibile in una cartella personale scrivibile.

Disinstallare da Windows **App installate**. Disinstallazione rimuove il programma e le sue scorciatoie, mantenendo i profili personali in modo che una reinstallazione successiva possa riutilizzarli.

L'eseguibile single-file estrae le sue dipendenze in bundle nella cache .NET dell'utente al primo lancio, che può richiedere un po 'più. Gli eseguibili pubblicati sono attualmente non firmati, quindi Windows può visualizzare un avviso SmartScreen.

<a id="configure-a-key"></a>
### Configurare una chiave

1. Selezionare una chiave nella griglia.
2. Scegli un'azione nella barra laterale destra.
3. Configurare le impostazioni sotto la griglia e salvare. La commutazione di tasti o profili consente anche di salvare le impostazioni della chiave corrente.

| Azione | Comportamento |
| --- | --- |
| Tasto libero | Passa F13–F24 attraverso normalmente. Assegnalo direttamente nel tuo gioco o applicazione. |
| Applicazione | Avvia un'applicazione installata o un programma selezionato manualmente o una scorciatoia. |
| Cartella | Apre una cartella in Windows Explorer. |
| Sito web | Apre un indirizzo HTTP o HTTPS nel browser predefinito. |
| Taglio di scelta rapida | Invia una combinazione chiave alla finestra attiva. |
| Media | Controlla la riproduzione, le tracce, il volume o il muto. |
| Spotify | Controlla la riproduzione Spotify e regola solo il volume di sessione audio di Windows di Spotify. |
| Applicazione DeckPilot | Mostra l'esistente DeckPilot finestra, compreso quando è in esecuzione nell'area di notifica. |

Per le azioni di applicazione, aprire il raccoglitore di applicazione e cercare per nome. Il selettore di file manuale rimane disponibile, con argomenti di programma facoltativi sotto opzioni avanzate. Inserisci argomenti in base alla sintassi dell'applicazione di destinazione.

Tenere premuto un pulsante innesca un'azione configurata solo una volta al rilascio. In modalità free-key, gli eventi normali di ripetizione della tastiera rimangono disponibili.

<a id="manage-profiles"></a>
### Gestione dei profili

Aprire la sezione di gestione del profilo nella barra laterale destra per creare, duplicare, rinominare o eliminare un profilo. Utilizzare il selettore sopra la griglia chiave per scegliere il profilo attivo.

La calibrazione si applica a tutti i profili e può essere ripetuta dalla barra laterale.

<a id="background-operation"></a>
### Funzionamento dello sfondo

Chiusura della finestra nasconde DeckPilot Fare doppio clic sull'icona dell'area di notifica vicino all'orologio per riaprirlo, oppure fare clic con il pulsante destro del mouse sull'icona e scegliere il comando quit per fermarlo completamente.

L'avvio di Windows è abilitato per impostazione predefinita per l'utente corrente. **Lancer avec Windows**; aggiornamenti app successivi conservano tale scelta. L'installazione e i successivi lanci rinfrescano un ingresso abilitato al percorso eseguibile corrente.

<a id="installed-applications"></a>
## Applicazioni installate

Il raccoglitore legge scorciatoie dai menu di avvio dell'utente corrente e di livello di sistema, quindi li integra con `Get-StartApps` risultati dal catalogo di applicazioni di Windows.

I nomi duplicati sono raggruppati, con le voci del menu Start che prendono la priorità. Applicazioni e programmi portatili senza scorciatoie registrate possono ancora essere selezionati manualmente. Se il catalogo di Windows non è disponibile, le abbreviazioni del menu Start leggibili rimangono utilizzabili.

Windows lancia le scorciatoie selezionate. `shell:AppsFolder`. Gli argomenti opzionali si applicano a programmi basati su file e scorciatoie; sono nascosti per le voci di AppsFolder.

<a id="automatic-updates"></a>
## Aggiornamenti automatici

Disponibile da subito **0.2.0**, i controlli automatici di aggiornamento vengono eseguiti all'avvio dopo la calibrazione, quindi ogni sei ore. Possono essere disabilitati nella barra laterale.

Quando si trova un nuovo rilascio stabile GitHub, DeckPilot chiede se aggiornare. **Copie installate** scaricare e SHA-256 verifica della verifica `DeckPilot-Setup.exe`, quindi avviare l'installatore con l'avvio corrente, l'aggiornamento automatico e le preferenze di riduzione preselezionate. È possibile modificarle durante l'aggiornamento. Windows chiede l'autorizzazione dell'amministratore per aggiornare i file di programma. DeckPilot con grazia quando pronto, conserva profili e configurazione, controlla e installa il firmato VB-CABLE se manca, e rilancia l'app. Cancellare l'installatore lascia aperta l'app. Impostazione rifiuta di interrompere una sessione firmware aperta.

**Copia portatile** conservare il meccanismo di aggiornamento esistente. Dopo la conferma, esso:

1. Scarica l'eseguibile con il supporto per la segnalazione dei progressi e la cancellazione.
2. Verifica la sua SHA-256 hash contro `SHA256SUMS.txt` dallo stesso rilascio di GitHub.
3. Salva le impostazioni e prepara l'helper di aggiornamento.
4. Esiti, sostituisce l'eseguibile, e riavvia.
5. Mantiene il precedente eseguibile fino a quando il nuovo processo conferma l'inizializzazione di successo; se l'avvio fallisce, ripristina e rilancia la versione precedente.

La scelta di aggiornare posticipa in seguito i prompt automatici per 24 ore. I controlli manuali bypassano quel rinvio.

I download utilizzano HTTPS, con reindirizzamenti limitati agli host GitHub attesi. I file temporanei vengono memorizzati in `%LOCALAPPDATA%\DeckPilot\Updates`; i download di età superiore a sette giorni vengono ripuliti. La voce di avvio di Windows continua a utilizzare lo stesso percorso eseguibile dopo un aggiornamento.

Le copie portatili richiedono una cartella personale scrivibile. Le copie installate utilizzano l'installatore per le directory protette; DeckPilot app continua a funzionare senza diritti di amministratore.

**Aggiornamento da 0.1.x?** Smettere la vecchia versione dall'area di notifica e sostituire manualmente l'eseguibile con l'ultima versione una volta. Quelle versioni precedenti non contengono l'aggiornamento. DeckPilot.

<a id="repository-rename-compatibility"></a>
### Compatibilità del repository

Versione **1.0.1** prepara la mossa da `lolo34dr/streamdeck-diy` a `lolo34dr/deckpilot`. Il repository è ora `lolo34dr/deckpilot`. I controlli di aggiornamento utilizzano il nuovo nome e mantengono la compatibilità con il nome originale. I download di rilascio vengono convalidati contro questi due percorsi di repository esatti.

Uso **1.0.1 o successivamente** con il repository rinominato. Versioni 0.2.0 e 1.0.0 convalidare rigorosamente i percorsi di asset originali e potrebbe essere necessario un aggiornamento manuale. Scarica l'eseguibile corrente dalla pagina di releases se una versione precedente non può aggiornarsi.

<a id="settings-and-backups"></a>
## Impostazioni e backup

I profili e la calibrazione vengono memorizzati al di fuori della cartella dell'applicazione:

```text
%APPDATA%\DeckPilot\settings.json
```

Smettetela. DeckPilot prima di copiare questo file per eseguire il backup o ripristinare la configurazione. DeckPilot conserva una copia con una `.backup-<date>` suffisso prima di usare una nuova configurazione.

Non è richiesto nessun account. Aggiornare i controlli contattare GitHub e può essere disabilitato nell'interfaccia.

<a id="development"></a>
## Sviluppo

Installare il **.NET 8 SDK**, versione 8.0.415 o una nuova versione compatibile consentita da `global.json`. Visual Studio 2022 con il carico di lavoro di sviluppo del desktop .NET è facoltativo.

Eseguire questi comandi dalla root del repository:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

Anche la libreria di base e i controlli di console vengono eseguiti su Linux. L'interfaccia WPF e le API di input di Windows richiedono Windows.

<a id="validation"></a>
### Validazione

La console controlla l'uscita con uno stato non zero sulla prima affermazione fallita.

- Mappatura delle chiavi, ripetizioni di chiavi in mano, release, commutazione del profilo e copia dell'azione.
- Convalida delle impostazioni, persistenza e migrazione da configurazioni precedenti.
- Ricerca delle applicazioni e gestione degli obiettivi.
- Selezione di rilascio, checksum, download troncati o corrotti, reindirizzamenti e cancellazione.
- Sostituzione eseguibile, conservazione di backup e rollback.

Il controllo file-lock viene eseguito solo su Windows.

GitHub Azioni funziona anche `tests/windows-update-smoke.ps1` Avvia l'helper dell'aggiornamento, esce dal processo originale, verifica la conferma della sostituzione e dell'avvio, verifica la conservazione delle impostazioni, quindi apre il picker dell'applicazione installato tramite UI Automation e salva una selezione. Esercita anche gli eseguibili rinominati e i percorsi che contengono gli spazi. Questo script è riservato all'account di prova GitHub Actions usa e getta. `tests/windows-installer-smoke.ps1`: installazione di file di programma reale, scorciatoie di menu desktop/Start, avvio predefinito, aggiornamento con l'applicazione aperta, conservazione di avvio disabili, e disinstallazione senza eliminare i profili.

<a id="publish-a-standalone-windows-folder"></a>
### Pubblica una cartella di Windows standalone

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Distribuire l'archivio completo. Il flusso di lavoro di Windows build produce anche questo pacchetto.

<a id="publish-a-github-release"></a>
### Pubblica un rilascio di GitHub

Aggiornare la versione dell'applicazione e `.github/release-notes.md`, quindi spingere una corrispondenza `v*` tag, come ad esempio `v1.0.0`. Il flusso di lavoro di rilascio verifica che il tag corrisponde alla versione del progetto ed esegue la validazione prima di pubblicare:

| Scarica | Contenuto |
| --- | --- |
| `DeckPilot.exe` | Applicazione di Windows x64 autocontenuto, a singolo file. |
| `DeckPilot-win-x64.zip` | Completa cartella di applicazione autocontenuto. |
| `SHA256SUMS.txt` | SHA...256 hashes per entrambi i download. |

Per Windows ARM64, pubblicare separatamente con `-r win-arm64` e convalidare la macchina di destinazione.

<a id="hardware-checks-on-windows"></a>
## Controlli hardware su Windows

Con Leonardo collegato:

- Calibrare tutti 12 le posizioni in un ordine arbitrario F13-F24. Confermare i tasti duplicati sono rifiutati e la cancellazione preserva la mappatura precedente.
- Assegnare una chiave gratuita in un gioco e controllare che il gioco lo riceve normalmente.
- Avviare un programma configurato, quindi tenere premuto il pulsante: dovrebbe lanciare una volta fino al rilascio. Includere i percorsi contenenti spazi e argomenti opzionali.
- Prova una cartella, un sito web, una scorciatoia in un'applicazione ordinaria e controlli multimediali.
- Inviare F13–F24 attraverso un'azione di scelta rapida e confermare che non c'è alcun loop DeckPilot azioni.
- Creare due profili con diverse azioni, passare tra loro, smettere e rilanciare. I profili e la calibrazione dovrebbero persistere.
- Chiudere la finestra, attivare un'azione, quindi riaprire e smettere attraverso l'icona dell'area di notifica. DeckPilot uscite, tutte le chiavi devono passare normalmente.
- Abilitare l'avvio e il test di Windows dopo la firma di nuovo; disabilitare in seguito se indesiderato.

<a id="current-limitations"></a>
## Limiti attuali

- L'ascolto chiave è globale della sessione di Windows. I tasti fisici F13–F24 da un'altra tastiera seguono la stessa configurazione. Gli eventi della tastiera iniettati di software vengono ignorati per prevenire i loop di azione.
- Alcuni giochi, sistemi anti-cheat, o programmi che utilizzano l'ingresso diretto del dispositivo possono ancora ricevere le chiavi intercettate. La modalità Free-key rimane disponibile. Lo schermo di accesso, il desktop sicuro e altre sessioni sono al di fuori del controllo dell'applicazione.
- Windows può bloccare le scorciatoie inviate alle applicazioni in esecuzione con privilegi di amministratore. DeckPilot esegue senza diritti di amministratore e non bypassa questa restrizione.
- Rilasciare Ctrl, Alt, Shift e Windows prima di avviare un'azione di scelta rapida. Combinazioni come Alt+Tab rimangono soggette al comportamento di Windows.
- Scollegare il rilevamento, i profili automatici per gioco e un installatore firmato sono miglioramenti futuri.

<a id="project-structure"></a>
## Struttura del progetto

| Sentiero | Oggetto |
| --- | --- |
| `src/DeckPilot.Core` | Impostazioni, stato chiave, ricerca, convalida di rilascio di GitHub, download verificati e operazioni di sostituzione. |
| `src/DeckPilot.Windows` | Interfaccia WPF, catalogo delle applicazioni, icone, azioni di Windows, integrazione di area di notifica e helper di aggiornamento. |
| `tests/DeckPilot.Checks` | Controlli eseguibili senza un quadro di prova esterno. |
| `tests/windows-update-smoke.ps1` | Aggiornamento nativo di Windows e test di fumo di applicazione-picker utilizzato da CI. |
| `deckpilotlogo.png` | Applicazione logo. |
| `deckpilotbanner.png` | Bandiera del progetto. |

<a id="license-and-credit"></a>
## Licenza e credito

DeckPilot è rilasciato sotto il **[Licenza MIT](../../LICENSE)**. È possibile utilizzare, copiare, modificare e ridistribuirlo, anche commercialmente, fino a quando si mantiene il copyright e le note di licenza richieste dalla licenza. Il denoiser audio RNNoise bundle è un componente di terze parti separato sotto la sua licenza di stile BSD; vedi [`third-party/RNNoise/COPYING`](../../third-party/RNNoise/COPYING) e [`third-party/RNNoise/NOTICE.md`](../../third-party/RNNoise/NOTICE.md).

Se si costruisce su DeckPilot, un piccolo credito visibile e un link a questo repository sarebbe apprezzato; questo credito aggiuntivo è facoltativo; mantenere le notifiche MIT è ancora necessario.

Credito consigliato:

> Sulla base di [DeckPilot](https://github.com/lolo34dr/deckpilot) di [Lolo34d](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Aggiornamenti firmware Macropad

Per la preparazione del dispositivo e i controlli di qualità dell'officina, installare il separato [DeckPilot Atelier](../../docs/FACTORY.md) strumento. Il suo installatore è pubblicato come **[DeckPilot-Atelier-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe)** accanto all'applicazione principale. Può lampeggiare il firmware incluso su una scheda Leonardo/Caterina, quindi controllare brevi e lunghe presse su tutti 12 chiavi fisiche ed esportare un rapporto.

DeckPilot 1.1.0 include un **Firmware du macropad…** dialogo e firmware 1.0.0 per il Pro Micro ATmega32U4 **5 V / 16 MHz**, utilizzando la configurazione Leonardo/Caterina confermata dal registro di caricamento del proprietario. Lo strumento di flashing Windows in bundle non richiede Arduino IDE. La selezione e la conferma del dispositivo sono obbligatori; gli aggiornamenti dell'app non lampeggiano mai silenziosamente la scheda.

**Il proprietario ha confermato successo in-app flashing, firmware 1.0.0 identificazione e tutto 12 pulsanti sulla scheda di riferimento.** Mantenere lo schizzo originale e l'accesso a RST/GND per il recupero. [documentazione del firmware](FIRMWARE.it.md) per la compatibilità, la validazione, il recupero, i passaggi di costruzione e le licenze di terze parti. Le modalità USB di Per-button e i controlli di media nativi non fanno parte di questa versione del firmware.

<a id="building-the-windows-installer"></a>
### Costruire il programma di installazione di Windows

Dopo la produzione `artifacts/standalone/DeckPilot.exe` con il pacchetto firmware incorporato, installare Inno Setup 6 e correre `./installer/build.ps1` dalla radice del repository. L'output è `artifacts/installer/DeckPilot-Setup.exe`. CI costruisce e testa questo installatore prima di collegarlo a un rilascio. L'installatore utilizza un ID di applicazione stabile in modo che le versioni future aggiornino la stessa installazione.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Controllo del firmware e del cruscotto di connessione (1.3.0)

The **Votre macropad** scheda mostra lo stato di connessione, il firmware rilevato e qualsiasi aggiornamento bundle disponibile. **Connexion et diagnostic** I sistemi diagnostici non copiano le azioni del profilo o gli identificatori seriali USB. Il monitor si ricollega dopo l'unplugging e dà automaticamente la sua porta seriale mentre il gestore del firmware è aperto. Le azioni della tastiera F13-F24 continuano a funzionare con il firmware 1.0.0 e senza il collegamento diagnostico.

I controlli firmware di avvio sono abilitati per impostazione predefinita. Dopo che il controllo e la calibrazione dell'aggiornamento dell'app sono fuori strada, un vecchio firmware identificato può richiedere di aprire l'installazione; 24 Le schede sconosciute e i candidati non selezionati multipli non vengono lampeggiati automaticamente. Le versioni firmware sono in bundle con gli aggiornamenti delle app da GitHub, piuttosto che recuperati da un feed firmware indipendente.

Firmware **1.1.1** Questi eventi animano i pulsanti mappati e aggiornano la diagnostica; non invocano mai le azioni una seconda volta. La sua prima validazione fisica-board è ancora necessaria, anche se il firmware precedente 1.0.0 e la procedura flash sono stati confermati dal proprietario. [documentazione di protocollo e compatibilità](FIRMWARE.it.md).

<a id="keypad-interface-140"></a>
## Interfaccia della tastiera (1.4.0)

Il più grande 3 × 4 Ogni chiave mostra la sua icona principale, l'etichetta, la mappatura F13-F24 e una piccola icona di tipo azione; gli strumenti mantengono l'etichetta completa e la posizione. Premere il feedback, le azioni in esecuzione, la spedizione di successo e gli errori hanno apparizioni distinte, compresi i simboli piuttosto che il colore da solo.

Scegli una chiave per modificarla. **Options avancées · fichier et arguments** mantiene i percorsi manuali delle applicazioni e gli argomenti di lancio crollati per impostazione predefinita, mentre le destinazioni essenziali della cartella e del sito rimangono espanse. **Tester l’action** salva e invia l'azione selezionata sul PC corrente; un indicatore di successo conferma l'invio, non il completamento all'interno di un'altra app.

Quando il macropad è scollegato, **Hors ligne** e l'indicazione di accompagnamento spiegano che i profili possono ancora essere modificati e le azioni testate. La configurazione viene memorizzata sul PC e rimane disponibile durante le disconnessioni e le riconnessioni.

Il menu delle aree di notifica ora passa direttamente i profili. **Ctrl+S** I file di configurazione non validi vengono supportati prima che venga utilizzata una nuova configurazione. L'installazione dell'app, le icone del menu desktop/Start, le preferenze di avvio e l'aggiornamento automatico esistente vengono mantenuti.

Per gli upload manuali di Arduino IDE, **Qui. DeckPilot dal suo menu di vassoio** per liberare la porta seriale. Chiusura della finestra principale lo mantiene in esecuzione in background.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, ricerca azione e drag-and-drop (1.5.0)

**Spotify** è separato da **Multimédia**. Il suo gioco/pausa, i comandi successivi e precedenti si rivolgono alla sessione multimediale di Windows dell'app desktop Spotify, anche mentre un browser sta giocando a YouTube. 5Spotify deve essere aperto e avere una sessione di riproduzione disponibile; avviare una traccia una volta se necessario. Spotify in un browser non è supportato. Le sessioni Missing Spotify producono un errore, senza alcuna ricaduta sui tasti multimediali globali o su un altro giocatore. DeckPilot. Windows 10 versione 2004 o più tardi, o Windows 11, è richiesto da questa costruzione.

Uso **Rechercher une action ou une application** per quesiti come `volume`, `skip`, `Spotify` L'indice installato-app carica sulla prima ricerca e viene memorizzato nella cache per cinque minuti; la selezione manuale dei file rimane disponibile. Fare clic su un risultato per configurare la chiave selezionata o trascinarla su un'altra chiave.

Trascinare un'azione dal catalogo o dai risultati della ricerca su una chiave per assegnarla. **scambiare le loro azioni**; la taratura fisica rimane invariata. Puoi anche lasciarne uno `.exe`, `.lnk`, `.appref-ms`, la cartella o l'URL HTTP(S) su una chiave. Dropping configura solo la chiave e non esegue mai l'azione.

L'avvio di Windows ora mantiene una preferenza esplicita per l'utente, ripara una voce Run stale o mancante, e segnala le bandiere note di avvio di Windows disabilitano. **Lancer avec Windows** chiarisce il noto DeckPilot disattivare la bandiera per l'utente corrente. I lanci di sfondo rimangono nascosti anche prima della calibrazione, e i lanci di sfondo duplicati escono tranquillamente. **Préférences de l’application**, controllare lo stato dell'avvio e riattivare l'opzione se Windows l'ha bloccato. Un vero e proprio sign-out/sign-in sul PC dell'utente rimane il controllo finale per le politiche di Windows o le restrizioni specifiche della macchina.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Rendering tastiera affilata, recupero del profilo e controlli audio (1.6.1)

Il tastierino ora rende a dimensioni native WPF invece di scalare il testo attraverso un Viewbox. Effetti di ombra principale / chiave e animazioni di scala di testo sono stati rimossi; scorrere di finestre più piccole piuttosto che ridurre la tastiera. L'applicazione dichiara per-monitor DPI consapevolezza e utilizza le icone di shell di Windows più grandi per applicazioni desktop.

**Annuler / Rétablir** ripristina le modifiche dei profili, compresi gli swap drag-and-drop, le impostazioni di azione, le importazioni e la creazione/delezione del profilo. 50 Ctrl+Z / Ctrl+Y operare su profili al di fuori dei campi di testo; i campi di testo mantengono il proprio undo/redo.

Sottotitolo **Gérer les profils**, esportare il profilo corrente o tutti i profili come `.deckpilot.json`, importare profili come nuove copie, o ripristinare un backup di tutti i profili. Importa non esegue mai azioni e non sostituisce mai la calibrazione del PC. I profili contengono percorsi di azione, argomenti e ID endpoint audio, che possono essere necessari per la regolazione su un altro PC.

Prima di salvare i profili modificati, DeckPilot mantiene i profili precedenti in `%APPDATA%\DeckPilot\Backups` (20 Le salvazioni preferenze non creano backup dei profili. Un guasto di backup impedisce la sovrascrittura delle impostazioni attuali. Ripristino sostituisce i profili dopo la conferma e può essere annullato; la calibrazione rimane invariata.

**Microphone Windows** La barra di stato persistente e la scheda di Windows Audio mostrano lo stato attivo/mutato/mixed/non disponibile, aggiornato ogni due secondi e dopo i comandi dell'app. Un aggiornamento multi-endpoint fallito tenta di ripristinare gli stati di muto precedenti. Questi indicatori descrivono lo stato di endpoint di Windows, non un interruttore hardware o un pulsante muto di un'applicazione.

**Changer de sortie audio** Un obiettivo mancante dà un errore senza scegliere un altro dispositivo. Le applicazioni con il proprio output fisso potrebbero non seguire il default di Windows. Gli ID endpoint importati vengono visualizzati come non disponibili fino ad adattarsi al PC corrente.

Per configurare **appui court / appui long**, prima scegliere un'azione configurata, abilitare l'opzione a due azioni, selezionare il gesto per modificare e impostare una soglia da 200 a 3000 ms. Senza questa modalità, le azioni mantengono un comportamento di key-down immediato. Con esso, le azioni corte vengono eseguite sul rilascio prima della soglia; le azioni lunghe vengono eseguite una volta alla soglia (o sul rilascio se il timer non ha ancora eseguito). Una lunga azione non gestisce mai anche il breve. Le chiavi di passaggio Free F13-F24 non possono usare la modalità dual-action.

I controlli core coprono i limiti di ripetizione/rilascio, le detenzioni simultanee, la cancellazione, la cronologia del profilo/scambio/backup e il rollback audio utilizzando endpoint falsi. Windows UI controlla l'esercizio effettivo di drag undo/redo, configurando una lunga azione e soglia, le dimensioni della tastiera nativo dopo il ridimensionamento e l'indicatore del microfono.

<a id="search-and-ci-reliability-fixes-164"></a>
## Correzioni di affidabilità di ricerca e CI (1.6.4)

I risultati di ricerca equivalenti mantengono i pulsanti WPF esistenti. Il catalogo installato-app e il timer debunce differiscono la sostituzione dei risultati mentre un pulsante di ricerca viene catturato o una resistenza è in corso. Questo impedisce che un pulsante sorgente venga rimosso durante un'interazione.

I controlli di fumo di Windows continuano a eseguire trascinamenti nativo del mouse e convalidare le assegnazioni salvate, swap, undo/redo ed entrambi i gesti di stampa. Il client di prova utilizza le coordinate del cursore fisico per abbinare i limiti di automazione UI indipendentemente dal DPI, si concentra prima di misurare e scansiona tutte le percentuali di scorrimento.

Costruire e rilasciare i flussi di lavoro utilizzare Node.js 24 azioni. Il compilatore Arduino rimane a CLI 1.2.2; CI scarica il suo archivio di Windows con un pinned SHA-256 checksum. Le versioni firmware e toolchain sono invariate.

Le impostazioni lette nei test di Windows permettono esplicitamente di leggere / scrivere / cancellare la condivisione e la parse UTF-8 Questo impedisce al test stesso di negare la sostituzione del file atomico dell'app. Il test di storia mantiene una vecchia snapshot aperta su tre cicli di undo/redo e verifica che rimanga intatta mentre le impostazioni live cambiano.

Su Windows, i file di impostazioni esistenti sono impegnati con `File.Replace` piuttosto che overwrite-mode `File.Move`. Questo supporta i lettori che permettono la cancellazione mantenendo una vecchia snapshot completa. La suite di base verifica tre sostituzioni con un lettore tenuto aperto; i controlli di core di Windows vengono eseguiti prima della compilazione del firmware e la pubblicazione di tali guasti di persistenza vengono rilevati in anticipo.

Un lettore di istantanee di prova può aspettare fino a 500 ms per la condivisione di Windows / sbloccare le violazioni mentre `ReplaceFile` commit. L'attesa si applica solo all'apertura dell'istantanea, non all'esecuzione o alla riesecuzione di un'azione di applicazione. Altri errori, JSON malformato e serrature persistenti rimangono guasti.

<a id="runtime-and-editing-optimizations-170"></a>
## Ottimizzazione di runtime e editing (1.7.0)

Risparmio di impostazioni identiche controlla il file corrente, ma salta la sostituzione e il lavoro di backup. `ButtonAction` ha l'uguaglianza di valore (compresa la lunga azione); i controlli sporchi dell'editor evitano la serializzazione di JSON. La storia mantiene le istantanee del modello staccate, convalida nuovi stati, ed evita la parsing di JSON durante l'undo/redo. 50-edit limit e 20 i backup dei profili rimangono.

Ricerca e picker condividono un catalogo immutabile installato-app per cinque minuti (15 secondi per un risultato parziale). I consumatori concorrenti condividono una scansione; la chiusura di un raccoglitore non annulla un altro consumatore. Refresh bypassa la cache, e ogni raccoglitore ha un proprio filtro / vista. Scansione e catalogo parsing eseguire il thread UI; la filtrazione del picker è debounced da 120 ms. La ricerca di azione ricontrolla questa cache dopo il suo debounce e quando ritorna dal raccoglitore, quindi è disponibile anche un catalogo aggiornato.

Gli aggiornamenti dell'interfaccia utente del bordo ora seguono le notifiche di connessione invece di svegliare ogni 100 ms. Le notifiche di disconnessione ordinate cancellano ancora le detenzioni in sospeso. I ganci della tastiera agganciano il lavoro visivo della coda, mentre la soppressione e il temporizzazione del gesto rimangono sincroni. Le animazioni di feedback nascoste/minimizzate arrestano e utilizzano un intervallo di polling audio di dieci secondi invece di due secondi; la riapertura o l'emissione di un comando audio richiede lo stato fresco immediatamente.

Queste modifiche riducono il lavoro specificato; non stabiliscono una percentuale di CPU/RAM dipendente dalla macchina. La commutazione audio hardware nativo e il comportamento di chiave fisica richiedono ancora la validazione su un PC Windows con il macropad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Strumenti di spazio di lavoro e interfaccia rinfrescata (1.8.0)

L'interfaccia carbone mette in primo luogo la libreria di azione, separa i controlli del profilo dalla dimensione nativa croccante 3 × 4 Gli accenti chiave distinguono le proprie categorie; la selezione, la messa a fuoco della tastiera e il feedback d'azione rimangono separati. **Réduire les animations** nelle preferenze integra l'impostazione di animazione di Windows. Le finestre più piccole conservano l'accesso alla pergamena invece di restringere il testo chiave.

- **Modelli di profilo:** sotto: **Gérer les profils**, aggiungere musica e audio, desktop e produttività, o Creazione. Ogni modello crea un profilo modificabile separato e non esegue mai le sue azioni. I profili esistenti e la calibrazione fisica sono mantenuti. Tastiere utilizzare l'applicazione attiva, i cui binding possono differire.
- **Tasti del profilo:** assegna assegnare assegna assegna assegna assegna assegna assegna as assegna as assegna as assegna as assegna as assegna as assegna as as assegna as assegna as as assegna as as assegna as as assegna as as as assegna as as as as as assegna as as assegna as as as as as as as as as as as as as as as as assegna assegna as as as as as as assegna as as as as as assegna assegna as as as as assegna as as as as as as as assegna assegna as as as as as as as as as as as as as assegna assegna assegna as as as as as as as as as as as as as as as as as as as as as as as as as assegna as as as as as as as as as assegna as as as as as as as as as as as as as as as assegna as as as assegna as as as as as as assegna assegna as as as as as assegna as as as as as as as as as as as as as as as as as as as as as as as as as as assegna as as as as as as as as as as as as as assegna as as as assegna assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna assegna as as as as as as assegna as as as as assegna as as as assegna assegna assegna assegna as as as as assegna assegna as as as as as assegna assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna assegna as as assegna assegna assegna assegna as as as assegna assegna assegna as as as as as as as as as as as as as as as as as as as as as as assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna assegna as as as as as as as as as as as as as as as as as assegna as as as as as as as as as as as as as as as as as as as assegna as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as as assegna as as as as as as as **Changer de profil**, quindi selezionare un profilo di nome o un ciclo in avanti/indietro. I riferimenti tra i profili vengono ristampati quando si importa un set o si duplica un profilo di auto-riferencing. Una destinazione mancante segnala un errore e può essere sostituita nell'editor.
- **Comandi di Windows:** uso **Commandes Windows** per aprire impostazioni comuni o Task Manager, o bloccare esplicitamente il PC. I comandi provengono da un elenco fisso, senza script da inserire. L'azione di blocco richiede la firma di nuovo in Windows.
- **Tastiera completa:** Fare clic con il pulsante destro del mouse su una chiave per copiare, tagliare, incollare o resettarlo, tra cui l'azione lunga e l'accento. Ctrl+C/X/V e cancellare il lavoro mentre una chiave del mazzo ha messa a fuoco della tastiera; i campi di testo ordinari mantengono le scorciatoie di editing. DeckPilot e viene eliminato all'uscita. Queste modifiche supportano undo/redo.
- **Registratore di scelta rapida:** selezionare un'azione della tastiera e fare clic **Enregistrer un raccourci**. Premere una combinazione supportata, quindi salvarla. Escape annulla; F13–F24 è riservato al macropad durante la registrazione. Le combinazioni riservate di Windows possono ancora richiedere i controlli manuali.
- **Accesso rapido:** Ctrl+K o il pulsante Libreria concentra la ricerca di azione. **Activité récente** mantiene l'ultimo 30 azioni in memoria per questa sessione, senza memorizzare argomenti di programma o percorsi di destinazione. Può essere cancellato in qualsiasi momento. Una spedizione di successo non garantisce il completamento nell'app di destinazione.

Il firmware rimane **1.1.1**. L'accettazione di un'offerta firmware automatico ora apre correttamente il firmware manager; la precedente guardia modale ha impedito che il handoff. L'identità del firmware esistente, battito cardiaco, ricollegamento e controlli flash rimangono in vigore.

<a id="microphone-noise-suppression-220"></a>
## Soppressione del rumore del microfono (2.2.0)

**Soundboard · micro virtuel** ora include la soppressione del rumore locale RNNoise per il microfono fisico selezionato. **Filtrer le bruit du microfono** e regolare **Intensità**. DeckPilot filtra la voce prima di mescolare nelle clip della scheda sonora, quindi le clip rimangono invariate e non vengono rimosse dal filtro del rumore. **CABLE Output** come il suo microfono, compresi i giochi e le app a catena vocale; i programmi impostati per utilizzare il microfono fisico direttamente non passano attraverso DeckPilot.

L'audio viene elaborato localmente in DeckPilot. Non è registrato o inviato sulla rete. RNNoise funziona in 10 Ms, 48 frame mono kHz; questa dimensione della cornice non è una garanzia di latenza end-to-end, che dipende anche da Windows, i dispositivi e VB-CABLE. Questo aggiornamento non richiede un flash firmware. Il componente RNNoise nativo è coperto dalla propria licenza di stile BSD, nota sopra.

<a id="soundboard-return-and-on-screen-sound-notifications-219"></a>
## Soundboard ritorno e notifiche audio sullo schermo (2.1.9)

In **Soundboard · micro virtuel**, abilitare **Entendre aussi les children du soundboard** per ascoltare clip attraverso l'auricolare o altoparlanti, nonché il microfono virtuale. Scegliere l'output fisico e impostare il volume di ritorno separato da 0 a 100%. Questo ritorno è disattivato per impostazione predefinita e salvato su questo PC. **solo clip audio**; il vostro microfono fisico non viene mai aggiunto all'uscita dell'altoparlante. Il microfono virtuale invia ancora la vostra voce e clip a VB-CABLE.

Se l'altoparlante selezionato si disconnette, il ritorno si ferma mentre il microfono virtuale continua. Ricollegalo, aggiorna l'elenco dei dispositivi, o scegli un'altra uscita. Le cuffie aiutano a impedire al microfono fisico di raccogliere clip acustica.

Il ritorno dell'altoparlante utilizza ora il formato audio di Windows pubblicizzato del dispositivo selezionato, compresi i formati estesi utilizzati da alcuni auricolari USB, e rientra dalla riproduzione guidata eventi se un driver lo rifiuta.

DeckPilot Atelier rimane 1.0.12 e il firmware rimane 1.1.1. Non è necessario alcun flash firmware. Il routing effettivo dell'altoparlante dipende ancora dal dispositivo audio di Windows e deve essere controllato sul PC del proprietario.

<a id="soundboard-speaker-return-218"></a>
## Soundboard ritorno dell'altoparlante2.1.8)

In **Soundboard · micro virtuel**, abilitare **Entendre aussi les children du soundboard** per ascoltare clip attraverso l'auricolare o altoparlanti, nonché il microfono virtuale. Scegliere l'output fisico e impostare il volume di ritorno separato da 0 a 100%. Questo ritorno è disattivato per impostazione predefinita e salvato su questo PC. **solo clip audio**; il vostro microfono fisico non viene mai aggiunto all'uscita dell'altoparlante. Il microfono virtuale invia ancora la vostra voce e clip a VB-CABLE.

Se l'altoparlante selezionato si disconnette, il ritorno si ferma mentre il microfono virtuale continua. Ricollegalo, aggiorna l'elenco dei dispositivi, o scegli un'altra uscita. Le cuffie aiutano a impedire al microfono fisico di raccogliere clip acustica.

<a id="open-deckpilot-and-profile-notifications-217"></a>
## Aprire DeckPilot e notifiche del profilo (2.1.7)

Sottotitolo **Ouvrir una domanda**, scegliere il built-in **DeckPilot** ingresso o click **Affettare « Ouvrir DeckPilot »**. Questa azione porta l'esistente DeckPilot finestra sul davanti; il suo obiettivo rimane valido dopo un aggiornamento o una reinstallazione dell'app.

Cambiare i profili mostra una breve, click-through notifica con il nome del profilo attivo. **Préférences de l’application**, è possibile disattivarlo, scegliere qualsiasi angolo, o impostare una posizione orizzontale e verticale personalizzata. **Effettuare prenotazioni online** mostra immediatamente il risultato. La notifica segue lo schermo contenente l'app in primo piano e appare anche mentre DeckPilot È nascosto. Può apparire oltre i giochi senza confini / conviviali; esclusivi giochi a schermo intero possono coprire normali overlay di Windows.

<a id="deckpilot-210-fixed-signed-virtual-microphone-routing"></a>
## DeckPilot 2.1.0: instradamento del microfono virtuale firmato fisso

DeckPilot ora utilizza un'interfaccia nera profonda per impostazione predefinita. In **Préférences de l’application**, cambiare lo sfondo, i colori del pannello e dell'accento utilizzando `#RRGGBB` valori; il tuo tema viene salvato su questo PC.

The **Soundboard · micro virtuel** pannello consente di selezionare il microfono fisico e clip MP3/WAV. DeckPilot esegue automaticamente il mix al VB-Audio VB-CABLE riproduzione endpoint; in Discord o un gioco, scegliere **`CABLE Output`** come il microfono. Durante l'installazione e gli aggiornamenti, DeckPilot assegni VB-CABLE scarica il pacchetto ufficiale su HTTPS e verifica l'editore di Authenticode prima di eseguire l'installatore silenzioso del driver. Ciò richiede una connessione internet e un'autorizzazione dell'amministratore. DeckPilot non fa il bundle o ridistribuisce i file VB-Audio. Se il driver manca in seguito, è possibile avviare lo stesso programma di installazione controllato dalla firma dal Soundboard pannello. Il motore audio utilizza NAudio (MIT; vedi [avviso di terze parti](../../third-party/NAudio-LICENSE.txt)). VB-CABLE è un driver Windows con licenza separata, firmato, non un componente MIT.

<a id="deckpilot-atelier-190"></a>
## DeckPilot Atelier (Atelier)1.9.0)

Atelier ora controlla la scheda selezionata e il firmware prima di lampeggiare, confronta le versioni installate e in bundle, e offre una riprovazione verificata dopo le scritture interrotte. **Mode atelier en série...** diagnostica e lampeggia tutte le schede rilevate in modo sequenziale e può esportare un rapporto batch. 12- test chiave verifica il fisico 01→12 il layout, registra la mappatura F13–F24 e include i risultati firmware e per-key nel report di qualità.


Il separato **DeckPilot Atelier** L'installatore di Windows prepara nuovi macropad DIY, rileva le schede Leonardo/Caterina, convalida e lampeggia il firmware in bundle senza sovrascrivere il bootloader, quindi guida un fisico 3×4 controllo di tutti 12 pulsanti. Ogni pulsante deve passare una breve pressione e una presa di almeno 700 ms. Le registrazioni degli strumenti che il segnale F13-F24 appartiene ad ogni posizione fisica, bandiere segnali multipli o inconsistenti, e esporta un rapporto timestamped. `DeckPilot-Atelier-Setup.exe` un bootloader Caterina deve essere già presente; lo strumento non installa un bootloader mancante.

DeckPilot Atelier controlla GitHub per l'app e aggiornamenti in bundle-firmware su startup e su richiesta.Gli aggiornamenti richiedono la conferma e l'utilizzo del rilascio SHA-256 manifest. Firmware non è mai lampeggiato da un aggiornamento automatico; dopo gli aggiornamenti di Atelier, scegliere l'azione del firmware quando si è pronti a programmare la scheda collegata.

Atelier 1.0.7 aggiunge un confermato **Effetto del programma DeckPilot** azione. Sgombera il 28 Area di applicazione KiB con istruzioni di arresto sicuro verificate, quindi la scheda smette di agire come una tastiera preservando Caterina per il recupero e il futuro lampeggiante. Lascia il bootloader, EEPROM e profili Windows intatti.

Versione 1.9.1 Il suo aggiornamento può installare un nuovo pacchetto Atelier o firmware bundle, quindi rilanciare l'app. Il rilascio di Windows verifica il primo lancio, l'aggiornamento e la disinstallazione per l'installatore Atelier.

Versione 1.9.2 migliora la fase di confezionamento Atelier Windows e segnala la sua esatta posizione di guasto in CI.

Versione 1.9.3 corregge il modo in cui il pacchetto di Windows legge la versione del firmware inclusa, quindi il controllo di aggiornamento di Atelier traccia il firmware spedito nel programma di installazione.

Il controllo di rilascio Atelier ora segnala la diagnostica di avvio se Windows non riesce ad aprire la finestra installata.

DeckPilot 1.9.6 Corregge il manifesto Windows DPI di Atelier in modo che l'applicazione installata venga lanciata correttamente su Windows 10 e 11.

DeckPilot 1.9.7 Corregge la disinstallazione di Atelier durante l'esecuzione. Il test di rilascio di Windows verifica anche il lancio, l'aggiornamento in sostituzione e la disinstallazione.

DeckPilot 1.9.8 aggiunge un'esplicita e confermata Atelier reset che sostituisce il programma utente della scheda con uno schizzo vuoto verificato e conserva il bootloader Caterina.

La validazione copre le impostazioni legacy, la convalida dell'accento, la navigazione del profilo e la remapping di riferimento, i modelli portatili, il modello di Windows nativo/undo/redo e il commutatore del profilo, la registrazione di scorciatoia, copia/paste/reset, le impostazioni persistenza, l'installazione/aggiornamento, il ridimensionamento del layout e una cattura effettiva dell'interfaccia utente.

<a id="profile-sound-files-and-workshop-safety"></a>
### File audio profilo e sicurezza officina

Le esportazioni di profilati includono le clip MP3/WAV di riferimento (fino a 32 MiB di audio per pacchetto). Importa copia audio incorporato in DeckPilotI backup del profilo automatico utilizzano lo stesso formato; l'audio non disponibile viene registrato come mancante piuttosto che silenziosamente sostituito. Mantenere i file sorgente se si supera il limite di esportazione. I file del profilo legacy rimangono supportati. Rimozione di una clip che è ancora assegnato a un'azione breve o lunga è bloccata.

Atelier accetta tavole che non rispondono ancora DeckPilot protocollo, mentre controlla ancora la posizione USB, Caterina bootloader e ATmega32U4 firma prima della programmazione. Ogni sessione di test inizia con i risultati vuoti, e il suo rapporto rimane associato alla scheda testata.

<a id="live-microphone-latency"></a>
### Latenza del microfono dal vivo

DeckPilot 2.1.3 processi audio dal vivo in piccoli blocchi di uscita-driven. Richiede 20 ms acquisizione microfono guidato da eventi e 40 ms buffer di riproduzione. La coda del microfono è legata a 100 ms e scarta le cornici complete più vecchie se le bancarelle di riproduzione; questo evita la costruzione di secondi di discorso ritardato. Queste sono le impostazioni del buffer del software, non una garanzia di latenza end-to-end misurata. VB-CABLE e l'applicazione di ricezione contribuiscono anche ritardo. Le impostazioni di isolamento vocale discord sono invariate e possono sopprimere le clip della scheda audio perché riceve il segnale misto.

<a id="automatic-virtual-microphone-startup"></a>
### Avvio automatico del microfono virtuale

DeckPilot 2.1.4 attiva automaticamente il microfono virtuale quando il microfono fisico selezionato e VB-CABLE sono disponibili. Questo funziona anche in background dopo Windows sign-in quando **Iniziare con Windows** è abilitato. I dispositivi mancanti vengono riattivati ogni cinque secondi. Bloccare il microfono virtuale salva esplicitamente l'avvio automatico come disabilitato; riattivarlo nel Soundboard pannello. Le clip vengono giocate solo quando richiesto.

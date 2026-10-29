> ▁Esta é▁uma▁tradução▁automática da▁documentação▁completa do firmware. O original em▁inglês é a▁referência.▁Os▁comandos e▁identificadores são▁preservados▁exatamente.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

▁Apenas para a▁configuração de hardware▁testada: **Pro Micro ATmega32U4, 5 V / 16 MHz**,▁utilizando **Arduino Leonardo** ▁perfil do▁tabuleiro e a **Caterina / AVR109** bootloader. USB- C▁descreve o▁conector,▁não a▁compatibilidade do▁tabuleiro. O▁esboço original do▁proprietário▁foi▁carregado com▁sucesso com o 1200-baud reset e AVR109 em 57600 baud. O▁proprietário▁também▁confirmou o▁sucesso flashing no▁aplicativo, firmware 1.0.0 ▁identificação e▁funcionamento de▁todos 12 ▁botões▁nesse▁quadro de▁referência.

<a id="wiring-and-behavior"></a>
## Fiação e▁comportamento

▁Cada▁interruptor▁conecta um pino▁ao GND. As▁pull-ups▁internas▁estão▁habilitadas. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** ▁enviar **F13▁até F24** por▁essa▁ordem. 30 Ms debounce▁lida com▁imprensa e▁liberação▁independentemente.▁Comportamento do▁teclado,▁perfis do▁lado do Windows e▁calibração▁permanecem▁compatíveis; a capotagem do▁teclado USB▁permanece▁limitada▁pela▁biblioteca do▁Teclado de Arduino. O firmware▁ainda▁não▁introduz▁modos USB por▁botão ou▁controles de▁mídia nativa.

Pino **1** Mantido em GND na▁inicialização desativa a▁operação do▁teclado,▁preservando o▁sistema de▁segurança original. O▁núcleo USB▁ainda▁pode▁suportar a▁entrada do▁carregador de▁inicialização.▁Não conecte VCC▁ao GND.

A interface▁serial USB CDC▁atual▁responde `DECKPILOT?\n` em 115200 baud com:

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

▁Nenhuma▁ligação▁serial é▁necessária para a▁operação normal do▁teclado. A▁identidade é▁uma▁verificação de▁compatibilidade,▁não▁uma▁autenticação de hardware▁criptográfica. O Windows▁ainda▁reporta▁os▁identificadores USB do Arduino Leonardo;▁muitas▁outras▁placas▁partilham▁esses▁identificadores.

<a id="install-from-deckpilot"></a>
## ▁Instalar de DeckPilot

1. ▁Manter o▁esboço de▁recuperação▁fornecido (`firmware/Original/Original.ino`) e▁uma▁configuração de upload Arduino▁trabalhando▁como▁uma▁opção de▁recuperação.▁Mantenha RST e GND▁fisicamente▁acessível.
2. ▁Fechar Arduino IDE, Monitor▁Serial e▁outras▁ferramentas▁seriais. Desconecte▁outras▁placas Leonardo.
3. ▁Abrir **Firmware du macropad…**,▁escolha o▁dispositivo▁correto e▁confirme a▁placa e fiação. Um▁esboço original▁não▁identificado▁só▁pode ser▁instalado▁após▁confirmação▁explícita.
4. ▁Confirme a▁instalação e▁deixe o▁cabo USB▁conectado▁até a▁conclusão. As▁ações do▁teclado são▁suspensas DeckPilot ▁enquanto a▁janela de firmware▁está▁aberta;▁os▁eventos▁normais F13–F24▁ainda▁chegam▁ao Windows.

O firmware e o AVRDUDE do Windows 8.0 são▁empacotados▁dentro do▁aplicativo▁publicado.▁Nenhum IDE Arduino ou▁instalação de driver▁separada é▁realizada por DeckPilot. O Windows▁já▁deve▁expor a▁porta▁serial USB do▁conselho,▁como▁fez para o upload▁bem▁sucedido do▁proprietário. Novas▁versões de firmware são▁entregues com▁atualizações de▁aplicativo do GitHub;▁atualização do▁aplicativo▁faz **▁não** hardware flash▁automaticamente.▁Cada flash▁requer▁confirmação.

DeckPilot ▁Verifica o SHA-256, Checksums de▁registro Intel HEX,▁presença do▁vetor de reset, sobreposições e o **28672-Adeus.** o▁limite da▁aplicação▁antes da redefinição. Corresponde à▁localização▁física USB▁através das▁portas 'runtime' e 'bootloader',▁usa o AVR109 em 57600 baud,▁requer▁verificação de▁escrita do AVRDUDE, em▁seguida,▁verifica a▁identidade e▁versão do firmware em▁execução.▁Ele▁não▁escreve fusíveis, EEPROM ou o▁carregador de boot.▁Apenas IDs Leonardo `2341:8036` / `2A03:8036` e Caterina `2341:0036` / `2A03:0036` são▁aceitos. Se o Windows▁não▁pode▁fornecer▁uma▁conexão▁física▁correspondente,▁piscando é▁bloqueado em▁vez de▁adivinhar▁uma▁porta.

Flashing▁não é atômico:▁uma desconexão de▁cabo ou▁perda de▁energia▁pode▁deixar o firmware do▁aplicativo▁incompleto.▁Não▁há backup ou rollback de firmware▁automático. Se▁necessário, re-upload o▁esboço original com o Arduino IDE▁configurado para **Arduino Leonardo**.▁Dependendo do▁carregador de boot da▁placa,▁conectando▁brevemente RST▁ao GND▁duas▁vezes▁pode▁expor▁uma▁porta de bootloader▁temporária; upload▁enquanto▁essa▁porta▁está▁disponível. Um▁carregador de boot▁danificado▁requer hardware ISP e▁não▁pode ser▁reparado por▁este▁atualizador USB.▁Não▁escreva▁uma▁diferente▁placa ou▁variante do▁relógio.

O▁último▁registo de▁processo AVRDUDE▁concluído é▁armazenado em `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## Compilar

No Windows, a▁partir da▁raiz do▁repositório,▁instalar Arduino CLI **1.2.2** e .NET SDK 8, em▁seguida,▁correr:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

O script pins Arduino AVR core **1.8.6**,▁Teclado **1.0.6**, e AVRDUDE a▁montante **8.0**▁Os▁pacotes▁geridos por CLI▁utilizam a▁validação da▁soma de▁verificação de▁pacotes do Arduino;▁os▁arquivos▁binários/fonte do AVRDUDE▁têm SHA-256 pins no▁guião. `artifacts/firmware/DeckPilot-firmware.zip` ▁contém o HEX,▁manifesto,▁ferramenta Windows,▁licença e▁correspondente upstream AVRDUDE▁arquivo▁fonte.▁Publicação CI▁requer o▁pacote; um▁desenvolvedor▁comum▁construir▁sem▁ele▁pode▁executar o▁aplicativo,▁mas▁não▁pode flash.

O original 12- O▁esboço do▁botão▁foi▁adaptado do David Madison. **Barato de▁fluxo** ▁projeto, copyright 2018, MIT.▁Seu▁aviso▁permanece em `DeckPilot.ino`. DeckPilot as adições▁usam a▁licença do▁repositório MIT. **AVRDUDE é software▁separado▁sob GPL-2.0-ou▁depois**, com o seu▁aviso/licença e▁código-fonte▁empacotado; a▁licença do▁repositório MIT▁não▁substitui▁essa▁licença. https://github.com/avrdudes/avrdude/tree/v8.0 para▁instruções de▁compilação a▁montante e▁avisos de▁dependência.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 e▁protocolo USB 2

Firmware 1.1.1 ▁adiciona▁uma▁ligação de▁diagnóstico▁apenas para▁leitura,▁mantendo o▁mesmo▁mapeamento de▁teclado F13-F24 e pin-1 ▁Segurança de▁arranque. **O▁teste▁físico do▁proprietário▁cobriu firmware 1.0.0. Firmware 1.1.1 ▁tem▁testes▁automatizados de▁protocolo e▁uma▁compilação AVR,▁mas▁ainda▁precisa de seu primeiro▁teste na▁placa real.** Perfis e▁calibração▁permanecem no PC e▁não▁precisam ser recriados para esta▁atualização de firmware.

O▁novo▁aplicativo▁identifica o▁cartão▁conectado,▁mantém▁sua▁porta▁serial▁aberta, reconecta▁após▁desconectar e▁verifica▁sua▁versão de firmware na▁inicialização e▁após a reconexão. As▁atualizações do Firmware são▁entregues com▁versões de▁aplicativos: as▁verificações de▁inicialização▁comparam o firmware▁conectado com o▁pacote▁validado▁incluído na▁aplicação▁instalada. As▁verificações de▁atualização do▁aplicativo▁ainda▁obtêm▁versões▁mais▁recentes do GitHub.▁Não▁há▁nenhum canal de download▁separado▁somente de firmware.

Um firmware▁mais▁antigo▁detectado▁pede▁uma▁vez por▁sessão de▁aplicação, com▁uma 24-▁hora de▁atraso do▁lembrete▁quando adiado. As▁notificações▁podem ser▁desabilitadas em **Connexion et diagnostic**.▁Os▁esboços▁desconhecidos do Arduino,▁os▁dispositivos▁desconectados e as seleções ambíguas de▁múltiplos▁cartões▁não desencadeiam▁avisos▁automáticos de▁piscamento ou▁atualização. A▁instalação do Firmware▁requer▁sempre▁confirmação.▁Abrindo o▁gerenciador de firmware▁suspende o monitor e▁espera que a▁porta▁serial▁seja▁liberada; o▁monitoramento▁retoma▁após o▁encerramento da▁janela.

**Para um upload manual do Arduino IDE,▁saia DeckPilot do▁ícone da▁bandeja primeiro.** ▁Fechar a▁janela principal▁só a▁esconde, por▁isso▁continua a▁possuir a▁ligação em▁série. DeckPilot e▁outro monitor▁serial▁não▁pode▁usar a▁mesma▁porta▁simultaneamente. Se▁outra▁ferramenta▁já▁possui a▁porta, o▁aplicativo▁relata o▁problema e▁retorna▁sem▁afetar as▁teclas de▁teclado▁comuns.

A▁nova `DeckPilotProtocol.h` é a▁mesma▁execução▁exercida por `tests/firmware-protocol.cpp`.▁Ele▁usa buffers de▁tamanho▁fixo,▁processamento de▁entrada▁limitado e▁espaço de▁transmissão USB▁disponível; a▁digitalização de▁botão▁nunca é▁deliberadamente▁adiada▁enquanto▁espera por▁uma▁máquina. A▁máscara de▁botão▁pendente▁mais▁recente é coalesced▁sob contrapressão.▁Não▁há▁comandos para▁executar macros,▁escrever EEPROM,▁desativar o▁teclado,▁modificar▁perfis ou firmware flash sobre▁este▁protocolo de▁diagnóstico.

Em 115200 baud, as▁linhas▁terminam com LF (o CRLF é▁aceito).▁Os tokens de▁sessão▁contêm▁exatamente▁oito▁caracteres hexadecimais▁maiúsculas.▁Todos▁os▁números são decimais▁não▁assinados.▁Exemplos:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` ▁anúncios 12 ▁chaves e▁bits de▁capacidade: 1 = snapshot▁batimento▁cardíaco/estado, 2 =▁eventos de▁estado-chave. `PONG` ecoa o▁contador de requisições, em▁seguida,▁relata tempo de inatividade em milissegundos, 12-▁Máscara de▁botão de▁bits. `KEYS` ▁relata▁sequência e▁máscara; bit 0 é F13 e bit 11 é F24,▁independente da▁calibração▁física. Sessões e▁contadores de▁pedidos▁rejeitam▁respostas defasadas. 32-bit wraparound. As▁chaves▁permanecem▁apenas▁dados de▁diagnóstico: as▁ações do▁aplicativo▁ainda são▁executadas▁uma▁vez▁pelo▁caminho do▁teclado▁existente, de▁modo que um▁evento▁serial▁não▁pode▁executar a▁mesma▁ação▁duas▁vezes.

▁Os▁pedidos de▁batimento▁cardíaco são▁normalmente▁enviados a▁cada▁dois▁segundos. A▁aplicação▁liga- se de▁novo se▁faltar▁uma▁resposta▁correspondente para 1.5 ▁segundos; o firmware▁pára▁diagnósticos▁não▁solicitados▁após▁seis▁segundos▁sem▁batimento▁cardíaco ou▁quando a▁conexão▁serial▁fecha. Firmware 1.0.0 ▁permanece▁suportado▁através da▁consulta de▁identidade legada,▁sem▁diagnósticos de▁chave▁viva. DeckPilot apps▁ainda▁podem▁ler a▁identidade legada do firmware 1.1.1.

A▁versão original em `DeckPilotProtocol.h` ▁fornece a▁versão do▁manifesto de▁lançamento.▁Os▁pacotes de firmware▁publicados▁agora▁também▁contêm o▁esboço/header▁atual▁sob `source/DeckPilot`, o▁esboço de▁recuperação e o DeckPilot ▁Licença MIT.

A▁ligação▁diagnóstica▁utiliza▁deliberadamente `Serial.dtr()` em▁vez de `bool(Serial)`: Arduino AVR core 1.8.6 ▁insere a 10 ▁atraso em ms `Serial_::operator bool()`. A▁leitura do DTR impede▁diretamente o▁caminho▁diagnóstico de▁adicionar▁essa▁pausa a▁cada▁ciclo▁chave-scan. O▁teste do▁protocolo host▁elimina a▁conversão bool▁implícita para que▁este▁padrão de▁bloqueio▁não▁possa ser▁reintroduzido na▁implementação do▁protocolo▁compartilhado despercebido.

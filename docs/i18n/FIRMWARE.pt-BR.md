> Esta é uma tradução automática da documentação completa do firmware. O original em inglês é a referência. Os comandos e identificadores são preservados exatamente.

<a id="deckpilot-firmware"></a>
# DeckPilot firmware

Apenas para a configuração de hardware testada: **Pro Micro ATmega32U4, 5 V / 16 MHz**, utilizando **Arduino Leonardo** perfil do tabuleiro e uma **Caterina / AVR109** bootloader. USB- C descreve o conector, não a compatibilidade da placa. O esboço original do proprietário foi carregado com sucesso com o 1200-baud reset e AVR109 em 57600 baud. O proprietário também confirmou o sucesso flashing no aplicativo, firmware 1.0.0 identificação e funcionamento de todos 12 botões nesse quadro de referência.

<a id="wiring-and-behavior"></a>
## Fiação e comportamento

Cada interruptor conecta um pino ao GND. As pull-ups internas estão habilitadas. Pins **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** enviar **F13 até F24** por essa ordem. 30 ms debounce manuseia imprensa e liberação independente. 1.1.2 usa um bit por chave em seu relatório de teclado USB para que ele possa representar todas as doze teclas mantidas. Perfis do Windows e calibração usar os mesmos códigos de chaves.

Pin **1** Mantido em GND na inicialização desabilita a operação do teclado, preservando a segurança de falhas original. O núcleo USB ainda pode suportar a entrada do carregador de inicialização. Não conecte VCC ao GND.

A interface serial USB CDC atual responde `DECKPILOT?\n` em 115200 baud com:

```
DECKPILOT|1|promicro-32u4-16mhz|1.2.0
```

Nenhuma conexão serial é necessária para a operação normal do teclado. A identidade é uma verificação de compatibilidade, não autenticação de hardware criptográfica. O Windows ainda relata os identificadores Arduino Leonardo USB; muitas outras placas compartilham esses identificadores.

<a id="hybrid-input-mode-120"></a>
## Modo de entrada híbrido (1.2.0)

Ambas as placas usam o mesmo firmware. A aplicação identifica-as pela sua instância de dispositivo USB Windows e abre uma ligação serial por placa. **Macropads**, clique em uma chave no cartão de cada placa para habilitar DeckPilot mode e configure o seu perfil. Use **Calibrador** nesse cartão se sua ordem física do botão difere. Uma placa sem um número de série único USB pode obter uma instância diferente do dispositivo quando movido para outro soquete USB. Mantenha cada uma dessas placas em seu soquete atribuído.

Depois de um aperto de mão DP2 anuncia capacidade 4, o aplicativo envia `DP2|INPUT|<session>`. O conselho responde com `DP2|INPUT|<session>` quando todas as teclas e o relatório do teclado são liberados, ou `DP2|BUSY|<session>` se uma tecla ainda estiver carregada. Uma vez activada, o tabuleiro envia encomendado `KEYS` frames com início na sequência 1 e suprime F13–F24. Clientes de diagnóstico que apenas enviam `HELLO` manter o comportamento normal do teclado. Se largar o DTR ou se faltar o aluguer do batimento cardíaco durante seis segundos, restaura o teclado depois de quaisquer botões suspensos serem libertados. Uma fila completa de eventos termina a sessão de entrada em série com `DP2|LOST|<session>` Em vez de se fundirem silenciosamente os acontecimentos de imprensa e de divulgação.

Teste físico em Windows:

1. Flash e verificar cada placa individualmente com **Firmware du macropad…**, em seguida, conecte ambas as placas.
2. Abrir **Macropads** e clique no primeiro botão no cartão de cada dispositivo. Atribua uma ação diferente a cada botão. Verifique se o editor nomeia a porta COM selecionada e que cada cartão exibe seu próprio perfil.
3. Pressione a mesma posição em ambos os decks juntos, então repita com prensas curtas e longas. Cada ação deve ser executada uma vez em seu perfil atribuído.
4. Desligue um deck enquanto segura um botão, verifique se o outro ainda responde e reconecte o primeiro ao seu soquete USB original. DeckPilot A partir da bandeja do sistema e confirmar que uma nova prensa envia a tecla F13–F24 correspondente.

<a id="install-from-deckpilot"></a>
## Instalar de DeckPilot

1. Manter o esboço de recuperação fornecido (`firmware/Original/Original.ino`) e uma configuração de upload Arduino trabalhando como uma opção de recuperação. Mantenha RST e GND fisicamente acessível.
2. Feche o IDE Arduino, Monitor Serial e outras ferramentas seriais. Desconecte outras placas Leonardo.
3. Abrir **Firmware du macropad…**, escolha o dispositivo correto e confirme a placa e fiação. Um esboço original não identificado só pode ser instalado após confirmação explícita.
4. Confirme a instalação e deixe o cabo USB conectado até a conclusão. As ações do teclado são suspensas em DeckPilot enquanto a janela de firmware está aberta; os eventos F13-F24 normais ainda chegam ao Windows.

O firmware e o Windows AVRDUDE 8.0 são empacotados dentro da aplicação publicada. Nenhum IDE Arduino ou instalação de driver separada é executado por DeckPilot. O Windows já deve expor a porta serial USB da placa, como fez para o upload bem sucedido do proprietário. Novas versões de firmware são entregues com atualizações de aplicativo do GitHub; atualização do aplicativo faz **não** hardware flash automaticamente. Cada flash requer confirmação.

DeckPilot Atelier mantém **Effacer le firmware da carta** sob operações avançadas colapsadas. Requer digitação `EFFACER`, limpa tudo 28 KiB de flash de aplicação com um loop de reset verificado, e pára F13-F24 e enumeração USB normal. Caterina permanece disponível, mas o cartão não aparecerá novamente em Atelier até que seja colocado manualmente no modo bootloader. Mantenha-o conectado, brevemente conecte **RST** para **GND duas vezes rapidamente**, em seguida, selecione sua porta temporária Arduino Leonardo / bootloader COM e use a ação de recuperação de Atelier para reinstalar DeckPilot. Atelier exibe estas instruções e mantém a recuperação habilitada após apagamento ou um flash interrompido. O carregador de boot e EEPROM são preservados; esta não é uma apagar full-chip. Dados de perfil permanece no PC.

DeckPilot Verifica o SHA-256, Checksums de registro Intel HEX, presença do vetor de reset, sobreposições e **28672-Adeus.** O limite da aplicação antes da redefinição. Corresponde à localização física USB nas portas de execução e de arranque, usando o AVR109 em 57600 baud, requer a verificação de escrita do AVRDUDE, então verifica a identidade e versão do firmware em execução. Ele não escreve fusíveis, EEPROM ou o carregador de boot. Apenas IDs Leonardo `2341:8036` / `2A03:8036` e Caterina `2341:0036` / `2A03:0036` são aceitos. Se o Windows não pode fornecer uma conexão física correspondente, piscando é bloqueado em vez de adivinhar uma porta.

O flash não é atômico: uma desconexão ou perda de energia do cabo pode deixar o firmware do aplicativo incompleto. Não há backup ou rollback de firmware automáticos. Se necessário, re-upload o esboço original com o Arduino IDE configurado para **Arduino Leonardo**. Dependendo do carregador de boot do tabuleiro, conectar brevemente RST ao GND duas vezes pode expor uma porta de bootloader temporária; upload enquanto essa porta está disponível. Um carregador de boot danificado requer hardware ISP e não pode ser reparado por este atualizador USB. Não escreva uma diferente placa ou variante do relógio.

O último registo AVRDUDE da aplicação principal está armazenado em `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`. Atelier mantém instalar e reiniciar logs dentro `%LOCALAPPDATA%\DeckPilotFactory\last-flash.log` e `last-reset.log`.

<a id="build"></a>
## Compilar

No Windows, a partir da raiz do repositório, instalar Arduino CLI **1.2.2** e .NET SDK 8, em seguida, corra:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

O script pins Arduino AVR core **1.8.6**, Teclado **1.0.6**, e AVRDUDE a montante **8.0**. Pacotes gerenciados por CLI usam a validação da soma de verificação de pacotes de Arduino; arquivos binários/fonte AVRDUDE têm SHA-256 pinos no guião. `artifacts/firmware/DeckPilot-firmware.zip` contém a DeckPilot HEX, área de aplicação gerada redefinir HEX e seu gerador, checksums, ferramenta do Windows, licença e correspondente upstream AVRDUDE arquivo de fonte. Publicação CI requer o pacote; um desenvolvedor comum construir sem ele pode executar o aplicativo, mas não pode flash ou redefinir um tabuleiro.

O original 12- O esboço do botão foi adaptado do David Madison. **Barato de Fluxo** projeto, copyright 2018, MIT. Seu aviso permanece em `DeckPilot.ino`. DeckPilot as adições usam a licença do repositório MIT. **AVRDUDE é um software separado sob GPL-2.0-ou mais tarde**, com o seu aviso/licença e código-fonte empacotado; a licença do repositório MIT não substitui essa licença. https://github.com/avrdudes/avrdude/tree/v8.0 para instruções de compilação a montante e avisos de dependência.

<a id="firmware-111-and-usb-protocol-2"></a>
## Firmware 1.1.1 e protocolo USB 2

Firmware 1.1.1 adiciona uma ligação de diagnóstico apenas para leitura, mantendo o mesmo mapeamento de teclado F13-F24 e pin-1 Segurança de falha de arranque. **O teste físico do proprietário cobriu firmware 1.0.0. Firmware 1.1.1 tem testes automatizados de protocolo e uma compilação AVR, mas ainda precisa de seu primeiro teste na placa real.** Perfis e calibração permanecem no PC e não precisam ser recriados para esta atualização de firmware.

O novo aplicativo identifica o cartão conectado, mantém sua porta serial aberta, reconecta após desconectar e verifica sua versão de firmware na inicialização e após a reconexão. As atualizações de firmware são entregues com versões de aplicativos: as verificações de inicialização comparam o firmware conectado com o pacote validado incluído no aplicativo instalado. As verificações de atualização de aplicativos ainda obtêm versões mais recentes do GitHub. Não há nenhum canal de download separado somente de firmware.

Um firmware mais antigo detectado pede uma vez por sessão de aplicação, com 24- hora de atraso da chamada de atenção quando adiada. As notificações podem ser desactivadas em **Connexion et diagnostic**. Os esboços desconhecidos do Arduino, os dispositivos desconectados e as seleções ambíguas de múltiplos cartões não desencadeiam avisos automáticos de piscamento ou atualização. A instalação do Firmware requer sempre confirmação. Abrindo o gerenciador de firmware suspende o monitor e espera que a porta serial seja liberada; o monitoramento retoma após o encerramento da janela.

**Para um upload manual do Arduino IDE, saia DeckPilot do ícone da bandeja primeiro.** Fechar a janela principal só a esconde, por isso continua a possuir a ligação em série. DeckPilot e outro monitor serial não pode usar a mesma porta simultaneamente. Se outra ferramenta já possui a porta, o aplicativo relata o problema e retorna sem afetar as teclas de teclado comuns.

O modo de diagnóstico em `DeckPilotProtocol.h` usa buffers de tamanho fixo, processamento de entrada limitado e espaço de transmissão USB disponível. A última máscara de diagnóstico pendente é coalesced sob contrapressão. Firmware 1.2.0 adiciona o modo de entrada serial opt-in acima.

Em 115200 baud, as linhas terminam com LF (o CRLF é aceito). Os tokens de sessão contêm exatamente oito caracteres hexadecimais maiúsculas. Todos os números são decimais não assinados. Exemplos:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` anúncios 12 chaves e bits de capacidade: 1 = snapshot batimento cardíaco/estado, 2 = eventos de estado-chave, 4 = entrada de série opt-in. `PONG` ecoa o contador de requisição e reporta tempo de funcionamento, sequência e máscara de botão para diagnósticos. `KEYS` relata sequência e máscara; bit 0 corresponde a F13 e bit 11 para F24, independentemente da calibração física. Sessões e contadores de pedidos rejeitam respostas antigas. No modo de entrada serial, as ações seguem os pedidos `KEYS` Quadros.

Os pedidos de batimento cardíaco são normalmente enviados a cada dois segundos. A aplicação liga- se novamente se faltar uma resposta correspondente para 1.5 segundos; o firmware pára diagnósticos não solicitados após seis segundos sem batimento cardíaco ou quando a conexão serial fecha. Firmware 1.0.0 permanece suportado através da consulta de identidade legado, sem diagnósticos de chave viva. DeckPilot apps ainda podem ler a identidade legada do firmware 1.1.1.

A versão original em `DeckPilotProtocol.h` fornece a versão do manifesto de lançamento. Os pacotes de firmware publicados agora também contêm o esboço/header atual sob `source/DeckPilot`, o esboço de recuperação e o DeckPilot Licença do MIT.

A ligação diagnóstica utiliza deliberadamente `Serial.dtr()` em vez de `bool(Serial)`: Arduino AVR core 1.8.6 insere a 10 atraso ms em `Serial_::operator bool()`. A leitura do DTR impede diretamente o caminho diagnóstico de adicionar essa pausa a cada ciclo chave-scan. O teste de protocolo host elimina a conversão bool implícita para que este padrão de bloqueio não possa ser reintroduzido na implementação de protocolo compartilhado despercebido.

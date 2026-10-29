>  Esta é uma tradução automática do README completo. O original em inglês é a referência. As etiquetas e comandos da interface da aplicação são preservados exatamente.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="Banner do DeckPilot" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="Logótipo do DeckPilot" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Um companheiro de Windows para o seu 12- Macropad chave DIY.<br>
   Configure suas chaves, inicie aplicativos, controle de mídia e mude de perfil.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"> Baixar a versão mais recente</a>
   · 
  <a href="#getting-started"> Começar</a>
   · 
  <a href="#development"> Desenvolvimento</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt=" Última versão"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt=" Baixar para Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt=" Licença MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Estado de compilação do Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt=" Transferências de versões do GitHub"></a>
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

DeckPilot  configura macropads compatíveis com o Arduino Leonardo que já enviam **F13–F24**  como entradas de teclado USB, dispostos em **3  linhas de 4  chaves**. A ordem da chave física pode ser arbitrária: uma calibração guiada mapeia cada botão para sua posição.

**Não são necessárias alterações adicionais de driver ou firmware.** A interface do aplicativo está atualmente em francês. Escolha uma língua acima para um README traduzido completo, incluindo a referência técnica e o histórico de versões. As traduções são geradas automaticamente; este original em inglês é a referência.

<a id="features"></a>
## Características

- **Calibração guiada:**  identificar todos 12  botões físicos sem religar o seu macropad.
- **Recolhedor de aplicativos instalado:**  procurar Iniciar atalhos de menu e o catálogo de aplicativos do Windows, incluindo entradas da Microsoft Store. A seleção manual de arquivos permanece disponível.
- **Acções personalizadas:**  lançar aplicativos, abrir pastas ou sites, enviar atalhos e controlar mídia.
- **Chaves livres:**  Mantenha F13–F24 disponível para atalhos configurados diretamente em um jogo ou outra aplicação.
- **Perfis locais:**  criar, duplicar, renomear, excluir e selecionar perfis. Calibração é compartilhada entre perfis.
- **Perfis prontos:**  comece com modelos de Música, Produtividade ou Criação, depois personalize-os. Mude para um perfil específico, próximo ou anterior a partir de uma chave física.
- **Edição mais rápida:**  copiar/cortar/colar/repor teclas completas, gravar atalhos de teclado, escolher cores de acento de teclas e desfazer alterações.
- **Ferramentas das janelas:**  abrir som, exibição, configurações de Bluetooth e rede, Gerenciador de tarefas, ou bloquear a sessão.
- **Interface escura compacta:**  ícones vetoriais, ícones de aplicativos onde disponíveis, e animações sutis que respeitam as preferências de animação do Windows.
- **Operação de fundo:**  manter DeckPilot na área de notificação, com inicialização no Windows sign-in ativado por padrão (pode ser desativado).
- **Atualizações automáticas:**  aprovar uma nova versão, em seguida, deixe DeckPilot  baixar, verificar, substituir e reiniciar a si mesmo.

<a id="getting-started"></a>
## Começar

<a id="requirements"></a>
### Requisitos

-  Janelas 10 ou Windows 11, **x64**.
- Um envio de macropad compatível com o Arduino Leonardo 12  chaves distintas de **F13 a F24**.
-  Uma conexão de dados USB.

 Os downloads publicados incluem o tempo de execução .NET. **Você não precisa instalar .NET para usar DeckPilot.**

<a id="install-and-launch"></a>
### Instalar e lançar

1.  Transferir **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** e executar o instalador. Windows pede permissão de administrador para instalar em **Program Files\DeckPilot**.
2. O instalador adiciona DeckPilot para Windows **Aplicações instaladas**, cria um atalho de menu Iniciar, e seleciona o atalho de desktop e as opções de inicialização do Windows por padrão. Você pode desmarcar essas tarefas opcionais.
3.  Conecte seu macropad e abra DeckPilot do seu atalho.
4.  Siga a calibração da esquerda para a direita através **quatro colunas**, em seguida, a próxima linha, para **três linhas**. Cada botão deve enviar uma chave F13–F24 diferente.

 Ao atualizar a partir do primeiro 4- disposição da linha, DeckPilot  preserva perfis/ações e solicita uma nova calibração para as posições corrigidas. Instalar sobre uma configuração portátil existente também preserva as configurações em `%APPDATA%\DeckPilot`; saia da cópia antiga se a configuração lhe pedir.

Um portátil [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) e [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip)  permanecer disponível para compatibilidade. Para uso portátil, mantenha o executável em uma pasta pessoal.

Desinstalar a partir de janelas **Aplicações instaladas**. Desinstalação remove o programa e seus atalhos, mantendo os perfis pessoais para que uma reinstalação posterior possa reutilizá-los.

O executável de arquivo único extrai suas dependências empacotadas no cache .NET do usuário no primeiro lançamento, o que pode demorar um pouco mais. Os executáveis publicados estão atualmente sem assinatura, para que o Windows possa exibir um aviso SmartScreen.

<a id="configure-a-key"></a>
### Configurar uma chave

1.  Selecione uma chave na grade.
2.  Escolha uma ação na barra lateral direita.
3.  Configure suas configurações abaixo da grade e salve. Alternar chaves ou perfis também salva as configurações da chave atual.

|  Acção |  Comportamento |
| --- | --- |
|  Chave livre |  Passa por F13–F24 normalmente. Atribua-o diretamente em seu jogo ou aplicação. |
|  Aplicação |  Lança uma aplicação instalada ou um programa ou atalho manualmente seleccionado. |
|  Pasta |  Abre uma pasta no Windows Explorer. |
|  Sítio Web |  Abre um endereço HTTP ou HTTPS em seu navegador padrão. |
| Atalho do teclado |  Envia uma combinação de teclas para a janela activa. |
|  Mídia | Controla a reprodução, faixas, volume ou mudo. |

Para as ações do aplicativo, abra o seletor de aplicativos e pesquise pelo nome. O seletor de arquivos manual permanece disponível, com argumentos de programa opcionais sob opções avançadas. Digite os argumentos de acordo com a sintaxe do aplicativo alvo.

Se carregar num botão, irá activar uma acção configurada apenas uma vez até à libertação. No modo de chave livre, os eventos normais de repetição do teclado permanecem disponíveis.

<a id="manage-profiles"></a>
### Gerenciar perfis

 Abra a seção de gerenciamento de perfil na barra lateral direita para criar, duplicar, renomear ou excluir um perfil. Use o seletor acima da grade de chaves para escolher o perfil ativo. Pelo menos um perfil deve permanecer.

A calibração aplica-se a todos os perfis e pode ser repetida a partir da barra lateral.

<a id="background-operation"></a>
### Operação de fundo

 Fechando as cortinas da janela DeckPilot  enquanto mantém as suas acções activas. Carregue duas vezes no ícone da área de notificação perto do relógio para o reabrir ou carregue com o botão direito do rato no ícone e escolha o comando sair para o parar completamente.

A inicialização do Windows está habilitada por padrão para o usuário atual. Você pode desativá- lo com **Lancer avec Windows**; atualizações posteriores do aplicativo preservar essa escolha. Instalação e lançamentos subsequentes atualizar uma entrada habilitada para o caminho executável atual.

<a id="installed-applications"></a>
## Aplicações instaladas

O selecionador lê atalhos dos menus Iniciar do usuário atual e de todo o sistema, e depois os complementa com `Get-StartApps`  resultados do catálogo de aplicações Windows.

A pesquisa ignora os casos e os acentos. Os nomes duplicados são agrupados, tendo prioridade os itens do menu Iniciar. Os aplicativos e programas portáteis sem atalhos registrados ainda podem ser selecionados manualmente. Se o catálogo do Windows não estiver disponível, os atalhos do menu Iniciar legíveis permanecem utilizáveis.

O Windows lança os atalhos seleccionados. Os itens de catálogo usam `shell:AppsFolder`. Argumentos opcionais se aplicam a programas e atalhos baseados em arquivos; eles estão ocultos para entradas do AppsFolder.

<a id="automatic-updates"></a>
## Actualizações automáticas

 Disponível desde **0.2.0**, verificações automáticas de atualização executadas na inicialização após a calibração, em seguida, a cada seis horas. Eles podem ser desativados na barra lateral. Uma verificação manual permanece disponível.

 Quando uma versão mais recente do GitHub estável é encontrada, DeckPilot  pergunta se deseja atualizar. **Cópias instaladas** download e SHA-256  verificar `DeckPilot-Setup.exe`, em seguida, iniciar o instalador. Windows pede permissão de administrador para atualizar arquivos de programa. O instalador fecha DeckPilot  graciosamente quando estiver pronto, preserva perfis e preferências de inicialização, instala a nova versão e libera o aplicativo. Cancelar o pedido de permissão do Windows deixa o aplicativo aberto. A configuração se recusa a interromper uma sessão de firmware aberta.

**Cópias portáteis** reter o mecanismo de atualização existente. Após confirmação, ele:

1. Transfere o executável com o suporte de relatórios de progresso e cancelamento.
2.  Verifica o seu SHA-256 hash contra `SHA256SUMS.txt` do mesmo lançamento do GitHub.
3.  Salva suas configurações e prepara o helper de atualização.
4. Sai, substitui o executável e reinicia.
5.  Mantém o executável anterior até que o novo processo confirme a inicialização bem-sucedida; se a inicialização falhar, ele restaura e libera a versão anterior.

 Escolhendo para atualizar mais tarde adia as instruções automáticas para 24  Verificações manuais ignoram o adiamento.

 Os downloads usam HTTPS, com redirecionamentos restritos às máquinas do GitHub esperadas. Os arquivos temporários são armazenados em `%LOCALAPPDATA%\DeckPilot\Updates`; os downloads com mais de sete dias são limpos. A entrada de inicialização do Windows continua a usar o mesmo caminho executável após uma atualização.

 Cópias portáteis requerem uma pasta pessoal escrita. Cópias instaladas usam o instalador para diretórios protegidos; o DeckPilot app em si continua a ser executado sem direitos de administrador.

**Atualizando de 0.1.x?**  Sair da versão antiga da área de notificação e substituir manualmente o executável pela versão mais recente uma vez. Essas versões mais antigas não contêm o updater. As actualizações posteriores podem ser instaladas a partir de dentro DeckPilot.

<a id="repository-rename-compatibility"></a>
### Repositório renomear compatibilidade

 Versão **1.0.1**  prepara a mudança de `lolo34dr/streamdeck-diy` para `lolo34dr/deckpilot`. O repositório está agora `lolo34dr/deckpilot`. As verificações de atualização usam o novo nome e mantêm a compatibilidade com o nome original. Os downloads de versão são validados com estes dois caminhos exatos do repositório.

 Utilização **1.0.1 ou mais tarde** com o repositório renomeado. Versions 0.2.0 e 1.0.0 valide rigorosamente os caminhos originais dos ativos e pode precisar de uma atualização manual. Baixe o executável atual da página de lançamentos se uma versão antiga não puder se atualizar.

<a id="settings-and-backups"></a>
## Configurações e backups

 Os perfis e calibração são armazenados fora da pasta de aplicação:

```text
%APPDATA%\DeckPilot\settings.json
```

 Sair DeckPilot  antes de copiar este arquivo para fazer backup ou restaurar sua configuração. Se um arquivo de configuração não puder ser lido, DeckPilot  preserva uma cópia com uma `.backup-<date>` sufixo antes de usar uma nova configuração.

 Não é necessária nenhuma conta. Actualizar as verificações contacte o GitHub e poderá ser desactivado na interface.

<a id="development"></a>
## Desenvolvimento

 Instalar o **.NET 8 SDK**, versão 8.0.415 ou uma nova versão compatível permitida por `global.json`. Estúdio Visual 2022 com a carga de trabalho de desenvolvimento de desktop .NET é opcional.

Executa estes comandos da raiz do repositório:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

A biblioteca principal e as verificações de console também são executadas no Linux. A interface WPF e as APIs de entrada do Windows requerem Windows.

<a id="validation"></a>
### Validação

A consola verifica a saída com um estado não nulo na primeira asserção mal- sucedida. Elas cobrem:

- Mapeamento de chaves, repetições de teclas mantidas, lançamentos, mudança de perfil e cópia de ação.
-  Configuração validação, persistência e migração de configurações antigas.
- Pesquisa de aplicações e tratamento de alvos.
- Release seleção, checksums, truncados ou corrompidos downloads, redirecionamentos e cancelamento.
-  Substituição executável, preservação de backup e rollback.

A verificação de bloqueio de arquivos é executada apenas no Windows.

As Acções do GitHub também são executadas `tests/windows-update-smoke.ps1` contra o executável autônomo. Ele inicia o helper de atualização, sai do processo original, verifica a substituição e a confirmação de inicialização, verifica a preservação das configurações, então abre o coletor de aplicativos instalado através da UI Automation e salva uma seleção. Ele também exercita executáveis renomeados e caminhos contendo espaços. Este script é reservado para a conta de teste descartável do GitHub Actions. CI também executa `tests/windows-installer-smoke.ps1`: instalação real de arquivos de programa, atalhos de menu desktop/Iniciar, inicialização padrão, atualização com o aplicativo aberto, preservação de inicialização desativada e desinstalação sem excluir perfis.

<a id="publish-a-standalone-windows-folder"></a>
### Publicar uma pasta independente do Windows

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

 Distribua o arquivo completo. O fluxo de trabalho de compilação do Windows também produz este pacote.

<a id="publish-a-github-release"></a>
### Publicar uma versão do GitHub

 Atualizar a versão do aplicativo e `.github/release-notes.md`, em seguida, empurre uma correspondência `v*` tag, como `v1.0.0`. O fluxo de trabalho de lançamento verifica que a tag corresponde à versão do projeto e executa validação antes de publicar:

|  Transferir | Sumário |
| --- | --- |
| `DeckPilot.exe` |  Aplicativo auto-suficiente, de arquivo único Windows x64. |
| `DeckPilot-win-x64.zip` |  Completar a pasta de aplicação auto-suficiente. |
| `SHA256SUMS.txt` | SHA...256 hashes para ambos os downloads. |

Para Windows ARM64, publicar separadamente com `-r win-arm64` e validar na máquina alvo.

<a id="hardware-checks-on-windows"></a>
## Verificação de hardware no Windows

Com o Leonardo ligado:

- Calibrar tudo 12  posições em uma ordem arbitrária F13-F24. Confirme que as chaves duplicadas são rejeitadas e cancelar preserva o mapeamento anterior.
- Atribua uma chave livre em um jogo e verifique se o jogo recebe normalmente.
-  Inicie um programa configurado e, em seguida, segure o botão: ele deve lançar uma vez até a liberação. Inclua caminhos contendo espaços e argumentos opcionais.
-  Teste uma pasta, um site, um atalho em uma aplicação comum e controles de mídia.
-  Envie F13–F24 através de uma ação de atalho e confirme que não há nenhum loop de DeckPilot  acções.
-  Criar dois perfis com ações diferentes, alternar entre eles, sair e relançar. Perfis e calibração devem persistir.
-  Fecha a janela, desencadeia uma ação, então reabre e saia através do ícone área de notificação. Uma vez DeckPilot  saídas, todas as chaves devem passar normalmente.
- Activar o arranque do Windows e testar depois de iniciar sessão novamente; desactiva-o depois se não o desejar.

<a id="current-limitations"></a>
## Limitações atuais

- A escuta de chaves é global para a sessão do Windows. As teclas F13-F24 físicas de outro teclado seguem a mesma configuração. Eventos de teclado injetados por software são ignorados para evitar loops de ação.
-  Alguns jogos, sistemas anti-fraude ou programas usando entrada direta de dispositivo ainda podem receber chaves interceptadas. O modo de chave livre permanece disponível. A tela de entrada, área de trabalho segura e outras sessões estão fora do controle da aplicação.
- O Windows pode bloquear atalhos enviados para aplicativos em execução com privilégios de administrador. DeckPilot  corre sem direitos de administrador e não ignora esta restrição.
- Release Ctrl, Alt, Shift e Windows antes de ativar uma ação de atalho. Combinações como Alt+Tab permanecem sujeitas ao comportamento do Windows.
- A detecção de desconexão, os perfis automáticos por jogo e um instalador assinado são melhorias futuras. Os perfis são atualmente selecionados manualmente.

<a id="project-structure"></a>
## Estrutura do projecto

|  Localização |  Objecto |
| --- | --- |
| `src/DeckPilot.Core` |  Configurações, estado chave, pesquisa, validação de versão do GitHub, downloads verificados e transações de substituição. |
| `src/DeckPilot.Windows` | Interface WPF, catálogo de aplicativos, ícones, ações do Windows, integração de área de notificação e assistente de atualização. |
| `tests/DeckPilot.Checks` |  Verificações executáveis sem uma estrutura de teste externa. |
| `tests/windows-update-smoke.ps1` |  Atualizar o Windows nativo e o teste de fumaça do coletor de aplicativos usado pelo CI. |
| `deckpilotlogo.png` | Logótipo da aplicação. |
| `deckpilotbanner.png` | Banner do projecto. |

<a id="license-and-credit"></a>
## Licença e crédito

DeckPilot é liberado sob o **[ Licença MIT](../../LICENSE)**. Você pode usá-lo, copiar, modificar e redistribuí-lo, inclusive comercialmente, desde que você mantenha os direitos autorais e avisos de licença exigidos pela licença.

Se você construir sobre DeckPilot, um pequeno crédito visível e um link para este repositório seriam apreciados. Este crédito adicional é opcional; manter os avisos MIT ainda é necessário.

 Crédito sugerido:

> Baseada em [DeckPilot](https://github.com/lolo34dr/deckpilot) por [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Atualizações de firmware do Macropad

DeckPilot 1.1.0  inclui a **Firmware du macropad…**  diálogo e firmware 1.0.0 para o Pro Micro ATmega32U4 **5 V / 16 MHz**, usando a configuração Leonardo/Caterina confirmada pelo log de upload do proprietário. A ferramenta de flashing do Windows empacotada não requer Arduino IDE. A seleção e confirmação de dispositivos são obrigatórias; atualizações de aplicativos nunca flash silenciosamente o quadro. Mapeamentos, perfis e calibração existentes F13-F24 permanecem compatíveis.

**O proprietário confirmou o sucesso flashing no aplicativo, firmware 1.0.0  identificação e tudo 12  botões no quadro de referência.**  Mantenha o esboço original e acesso a RST / GND para recuperação. Firmware piscando não pode ser rolado para trás automaticamente. Veja [ documentação do firmware](FIRMWARE.pt-BR.md) para compatibilidade, validação, recuperação, etapas de construção e licenças de terceiros. Os modos USB por botão e controles de mídia nativa não fazem parte desta versão de firmware.

<a id="building-the-windows-installer"></a>
### Construindo o instalador do Windows

 Após a produção `artifacts/standalone/DeckPilot.exe` com o pacote de firmware incorporado, instalar Inno Setup 6 e correr `./installer/build.ps1` da raiz do repositório. O resultado é `artifacts/installer/DeckPilot-Setup.exe`. O CI constrói e testa este instalador antes de anexá- lo a uma versão. O instalador usa um ID de aplicação estável para que futuras versões atualizem a mesma instalação.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Verificação do painel de conexão e firmware (1.3.0)

A **Votre macropad** O cartão mostra o estado de conexão, o firmware detectado e qualquer atualização disponível em pacotes. **Connexion et diagnostic**  expõe a seleção do tabuleiro quando vários Leonardos estão conectados, o estado da ligação USB ao vivo, as contagens dos botões físicos, as preferências de notificação de firmware e um resumo diagnóstico copiável. Os diagnósticos não copiam as ações do perfil ou os identificadores seriais USB. O monitor reconecta- se depois de desligar e dá a sua porta serial automaticamente enquanto o gerenciador de firmware está aberto. As ações do teclado F13–F24 continuam a funcionar com firmware 1.0.0 e sem a ligação diagnóstica.

As verificações de firmware de inicialização estão habilitadas por padrão. Após a verificação e calibração da atualização da aplicação estarem fora do caminho, um firmware identificado mais antigo pode pedir para abrir a instalação; adiar os lembretes 24  horas. Placas desconhecidas e vários candidatos não selecionados não são flashed automaticamente. Releases de Firmware são empacotados com atualizações de aplicativo do GitHub, em vez de obtido de uma fonte de firmware independente.

Firmware **1.1.1**  adiciona identificação da sessão, respostas cardíacas correlacionadas, mensagens delimitadas e eventos físicos de estado- chave. Estes eventos animam os botões mapeados e atualizam os diagnósticos; eles nunca invocam as ações uma segunda vez. Sua primeira validação de placa física ainda é necessária, embora o firmware anterior 1.0.0 e o procedimento flash foram confirmados pelo proprietário. [ documentação de protocolo e compatibilidade](FIRMWARE.pt-BR.md).

<a id="keypad-interface-140"></a>
## Interface de teclado (1.4.0)

O maior 3 × 4 O teclado segue o gabinete físico e as teclas-caps pretas. Cada tecla mostra o seu ícone principal, a legenda, o mapeamento F13-F24 e um pequeno ícone do tipo de ação; as dicas de ferramentas mantêm a etiqueta completa e a posição. Pressione o feedback, as ações de execução, o envio bem sucedido e os erros têm aparências distintas, incluindo símbolos em vez de cores sozinho. As animações seguem a preferência de animação do Windows.

 Escolha uma chave para editá-la. **Options avancées · fichier et arguments**  mantém os caminhos manuais da aplicação e os argumentos de lançamento colapsados por omissão, enquanto os destinos essenciais da pasta e do website permanecem expandidos. As configurações de colapsamento não limpam os seus valores. **Tester l’action**  salva e envia a ação selecionada no PC atual; um indicador de sucesso confirma despacho, não completa dentro de outra aplicação. Os atalhos do teclado visam a janela ativa. Teclas F13-F24 livres não têm ação de teste.

 Quando o macropad estiver desconectado, **Hors ligne** e a dica que a acompanha explica que os perfis ainda podem ser editados e as ações testadas. A configuração é armazenada no PC e permanece disponível ao longo de desconexão e reconexões. Estas melhorias de interface funcionam com o firmware existente e não requerem flash adicional.

O menu de área de notificação agora muda de perfil diretamente. **Ctrl+S**  salva a configuração atual do botão. Os arquivos de configuração inválidos são copiados antes de uma nova configuração ser usada. A instalação da aplicação, os ícones do menu desktop/Iniciar, as preferências de inicialização e o atualizador automático são retidos.

Para uploads manuais do Arduino IDE, **sair DeckPilot a partir do menu da bandeja** para libertar a porta serial. Fechar a janela principal mantém-na a correr em segundo plano.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, pesquisa de ação e arrastar- e- soltar (1.5.0)

**Spotify** é separado de **Multimédia**. Seus comandos de reprodução/pausa, próximo e anterior, endereçam-se à sessão de mídia do aplicativo do Spotify no Windows, mesmo enquanto um navegador estiver reproduzindo o YouTube. O Spotify deve estar aberto e ter uma sessão de reprodução disponível; inicie uma faixa uma vez, se necessário. O Spotify em um navegador não é suportado. Faltando ou recusando as sessões do Spotify produzem um erro, sem retorno às teclas de mídia globais ou outro player. Nenhuma autorização da conta do Spotify, assinatura Premium ou token armazenado é necessário DeckPilot. Windows 10  versão 2004 ou mais tarde, ou Windows 11, é exigido por esta construção.

 Utilização **Rechercher une action ou une application** para consultas como `volume`, `skip`, `Spotify` ou o nome de uma aplicação instalada. O índice de aplicações instaladas carrega na primeira pesquisa e fica em 'cache' durante cinco minutos; a selecção manual de ficheiros permanece disponível. Carregue num resultado para configurar a chave seleccionada ou arraste- a para outra tecla.

Arraste uma acção do catálogo ou dos resultados da pesquisa para uma chave para a atribuir. Arraste uma chave configurada para outra para **trocar as suas acções**; a calibração física permanece inalterada. Você também pode soltar um `.exe`, `.lnk`, `.appref-ms`, pasta ou URL HTTP( S) em uma chave. Soltar apenas configura a chave e nunca executa a ação. Complete qualquer configuração em falta no editor.

A inicialização do Windows agora mantém uma preferência explícita por usuário, repara uma entrada de execução incompleta ou ausente, e relatórios conhecidos da inicialização do Windows desativam bandeiras. **Lancer avec Windows**  limpa o conhecido DeckPilot desactivar a opção para o utilizador actual. Os lançamentos de fundo permanecem escondidos mesmo antes da calibração, e os lançamentos de fundo duplicados saem silenciosamente. **Préférences de l’application**, verifique o status de inicialização e reativar a opção se o Windows bloqueou-o. Um verdadeiro sign-out/sign-in no PC do usuário continua a verificação final para políticas do Windows ou restrições específicas da máquina.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## renderização de teclado afiado, recuperação de perfil e controles de áudio (1.6.1)

O teclado agora renderiza em tamanhos WPF nativos em vez de escalar texto através de uma Viewbox. Efeitos de sombra pai/chave e animações de escala de texto foram removidos; janelas menores rolar em vez de diminuir o teclado. O aplicativo declara consciência de DPI por monitor e usa ícones de shell maiores do Windows para aplicativos de desktop.

**Annuler / Rétablir**  restaura edições de perfil, incluindo trocas de arrastar e soltar, configurações de ação, importações e criação/deleção de perfil. 50 edits during the app session. Ctrl+Z / Ctrl+Y opera em perfis fora dos campos de texto; os campos de texto mantêm sua própria desfazer/refazer. Calibração, preferências de inicialização e ações executadas não são desfeitas.

Embaixo **Gérer les profils**, exportar o perfil atual ou todos os perfis como `.deckpilot.json`, importar perfis como novas cópias, ou restaurar um backup de todos os perfis. Importar nunca executa ações e nunca substitui a calibração do PC. Os perfis contêm caminhos de ação, argumentos e IDs de endpoint de áudio, que podem precisar de ajuste em outro PC. Formatos desconhecidos, arquivos superdimensionados e configurações de gestos malformados são recusados.

 Antes de salvar perfis alterados, DeckPilot  mantém os perfis anteriores em `%APPDATA%\DeckPilot\Backups` (20  versões, deduplicadas pelo conteúdo). As poupanças apenas de preferência não criam cópias de segurança de perfil. Uma falha de backup impede a substituição das configurações atuais. A restauração substitui os perfis após a confirmação e pode ser desfeita; a calibração permanece inalterada.

**Microphone Windows** comuta o estado mudo em todos os parâmetros de captura do Windows atualmente ativos. A barra de estado persistente e o cartão do Audio Windows mostram estado ativo/mutilado/indisponível, atualizado a cada dois segundos e após os comandos de aplicativos. Uma atualização de vários pontos falhou para restaurar estados mudos anteriores. Estes indicadores descrevem o estado de terminal do Windows, não um switch de hardware ou o próprio botão mudo de uma aplicação.

**Changer de sortie audio**  alterna entre dois dispositivos de saída selecionados e conectados, para os padrões de console, multimídia e comunicações do Windows. Um alvo ausente dá um erro sem escolher outro dispositivo. Os aplicativos com seu próprio resultado fixo podem não seguir o padrão do Windows. IDs de endpoint importados são mostrados como indisponíveis até serem adaptados ao PC atual. Alterações de roteamento falhadas tentam restaurar os padrões anteriores.

Para configurar **appui court / appui long**, primeiro escolha uma ação configurada, habilite a opção de duas ações, selecione o gesto para editar e defina um limiar de 200 para 3000 ms. Sem este modo, as acções mantêm o comportamento imediato de 'chave- para- baixo'. Com ele, as acções curtas são executadas na libertação antes do limiar; as acções longas são executadas uma vez no limiar (ou na libertação se o temporizador ainda não tiver sido executado). Uma acção longa nunca executa também a curta. As chaves de passagem F13- F24 livres não podem usar o modo de dupla- acção. Os gestos pendentes são cancelados em alterações de perfil/ configuração, calibração, operações de firmware, alterações de sessão e suspensão. Não é necessário flash de firmware; os relatórios de teclas/chave- up do teclado existentes fornecem os gestos.

As verificações principais cobrem limites de repetição/libertação, retenções simultâneas, cancelamento, histórico de perfil/troca/backups e retorno de áudio usando endpoints falsos. A interface do Windows verifica o exercício de arrastar/refazer real, configurando uma ação longa e limiar, dimensões nativas do teclado após o redimensionamento e o indicador do microfone. Os controladores de microfone reais, a chave de ouvido/alto-falante e os compartimentos físicos ainda precisam de validação no PC do proprietário.

<a id="search-and-ci-reliability-fixes-164"></a>
## Correcções de pesquisa e fiabilidade da IC (1.6.4)

 Os resultados de pesquisa equivalentes mantêm os seus botões WPF existentes. O catálogo de aplicativos instalado e desactivar a substituição do resultado do temporizador enquanto um botão de pesquisa é capturado ou um arrasto está em andamento. Isto impede que um botão fonte seja removido durante uma interação.

As verificações de fumo do Windows continuam a executar arrastamentos nativos do rato e a validar tarefas salvas, trocas, desfazer/ refazer e ambos os gestos de imprensa. O cliente de teste usa coordenadas físicas do cursor para corresponder aos limites de Automação da UI, independentemente do DPI, foca- se antes de medir e verifica todas as percentagens de rolagem. Uma verificação de regressão também verifica que as pesquisas equivalentes preservam o mesmo botão acessível. As asserções de arranque do plano de fundo aguardam pelo manipulador carregado do WPF e requerem cinco observações consecutivas de janelas ocultas enquanto o aplicativo permanece vivo. Falhas reportam a fase, os alvos do cursor, o DPI e a geometria de rolagem final.

Build and release workflows use Node.js 24  ações. O compilador Arduino permanece em CLI 1.2.2; CI baixa seu arquivo do Windows com um SHA-256 checksum. O firmware e suas versões da cadeia de ferramentas estão inalterados.

As configurações lidas nos testes do Windows explicitamente permitem o compartilhamento de leitura/escrita/delete e processar UTF-8  depois de fechar o leitor. Isto impede que o próprio teste negue a substituição do arquivo atômico do aplicativo. O teste de histórico mantém um instantâneo antigo aberto durante três ciclos de desfazer/refazer e verifica se ele permanece intacto enquanto as configurações ao vivo mudam.

No Windows, os arquivos de configurações existentes são comprometidos com `File.Replace` em vez de substituir o modo `File.Move`. Isto suporta os leitores que permitem a exclusão enquanto retém um instantâneo antigo completo. O conjunto central testa três substituições com um leitor aberto; as verificações do núcleo do Windows são executadas antes da compilação de firmware e da publicação, pelo que as falhas de persistência são detectadas precocemente.

Um leitor de instantâneos de teste pode esperar até 500 ms para o compartilhamento/travamento do Windows enquanto `ReplaceFile` commits. A espera se aplica apenas à abertura do instantâneo, nunca execução ou re- execução de uma ação da aplicação. Outros erros, JSON malformado e bloqueios persistentes permanecem falhas.

<a id="runtime-and-editing-optimizations-170"></a>
## Otimização de execução e edição (1.7.0)

A gravação de configurações idênticas verifica o arquivo atual, mas ignora a substituição e o trabalho de backup. Edições/deleção externas e erros falhados ainda são detectados. `ButtonAction`  tem igualdade de valor (incluindo a ação longa); as verificações sujas do editor evitam a serialização do JSON. O histórico mantém instantâneos desvinculados do modelo, valida novos estados e evita a análise do JSON durante o desfazer/refazer. 50-reduzir o limite e 20 As cópias de segurança do perfil permanecem.

 Pesquisar e escolher compartilhar um imutável catálogo de aplicativos instalados por cinco minutos (15  segundos para um resultado parcial). Os consumidores simultâneos partilham uma pesquisa; fechar um captador não cancela outro consumidor. Actualizar ignora a 'cache', e cada captador tem o seu próprio filtro/visualização. A digitalização e a análise do catálogo são executadas fora do tópico UI; a filtragem do captador é deduzida por 120 ms. A pesquisa de ação verifica novamente este cache após sua defloração e ao retornar do catador, então um catálogo atualizado também está disponível lá.

 Atualizações de interface de bordo agora seguir notificações de conexão em vez de acordar cada 100 ms. As notificações de desconexão ordenadas ainda cancelam as esperas pendentes. Os ganchos do teclado fazem filas de trabalho visual, enquanto a supressão e o timing do gesto permanecem síncronos. As janelas ocultas/minimizadas param as animações de feedback e usam um intervalo de sondagem de áudio de dez segundos em vez de dois segundos; reabrindo ou emitindo um comando de áudio solicitam um estado fresco imediatamente. As atualizações de áudio pendentes partilham um ciclo e incluem as solicitações que chegam durante uma leitura existente. Os instantâneos de áudio inalterados e pincéis congelados evitam a atribuição de UI repetidas. Os invólucros de processo devolvidos ao lançar uma aplicação são eliminados sem fechar o programa lançado.

 Essas mudanças reduzem o trabalho especificado; elas não estabelecem uma porcentagem de CPU/RAM independente de máquina. A mudança de áudio de hardware nativo e o comportamento da chave física ainda requerem validação em um PC Windows com o macropad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Ferramentas de espaço de trabalho e interface atualizada (1.8.0)

A interface carvão coloca a biblioteca de ação em primeiro lugar, separa controles de perfil do tamanho nativo nítido 3 × 4  teclado e configurações de ação de grupos em um cartão. Acentos de teclas distinguem suas próprias categorias; seleção, foco do teclado e feedback de ação permanecem separados. **Réduire les animations**  nas preferências completa a configuração de animação do Windows. Janelas menores retêm o acesso de rolagem em vez de encolher o texto da chave.

- **Modelos de perfil:** em baixo **Gérer les profils**, adicione Música e áudio, Desktop e produtividade, ou Criação. Cada modelo cria um perfil editável separado e nunca executa suas ações. Perfis existentes e calibração física são retidos. Os atalhos do teclado usam a aplicação ativa, cujas ligações podem diferir.
- **Chaves de perfil:**  atribuir **Changer de profil**, então selecione um perfil chamado ou ciclo para frente/para trás. As referências entre perfis são remapeadas ao importar um conjunto ou duplicar um perfil de autorreferência. Um destino em falta reporta um erro e pode ser substituído no editor.
- **Comandos do Windows:**  Utilização **Commandes Windows** para abrir configurações comuns ou Gerenciador de tarefas, ou explicitamente bloquear o PC. Os comandos vêm de uma lista fixa, sem script para entrar. A ação de bloqueio requer a assinatura de volta no Windows.
- **Área de transferência de teclas completa:**  Carregue com o botão direito numa tecla para a copiar, cortar, colar ou redefinir, incluindo a acção longa e o acento. Ctrl+C/X/V e Apagar o trabalho enquanto uma tecla do baralho tem a focagem do teclado; os campos de texto normais mantêm os seus atalhos de edição. A área de transferência da tecla fica dentro DeckPilot e está limpo ao sair. Estas edições suportam desfazer/refazer.
- **Gravador de atalhos:**  selecione uma ação do teclado e clique **Enregistrer un raccourci**. Pressione uma combinação suportada, em seguida, salvá-lo. Escape cancela; F13-F24 é reservado para o macropad durante a gravação. Combinações reservadas ao Windows ainda pode exigir os controles manuais.
- **Acesso rápido:** Ctrl+K ou o botão Biblioteca foca a pesquisa de ação. **Activité récente**  mantém o último 30 As ações enviadas/falhadas na memória para esta sessão, sem armazenar os argumentos do programa ou os caminhos de destino. Ela pode ser limpa a qualquer momento. Um envio bem- sucedido não garante a conclusão no aplicativo alvo.

O firmware permanece **1.1.1**. Aceitar uma oferta de firmware automática agora abre corretamente o gerenciador de firmware; o guarda modal anterior impediu essa transferência. Identidade de firmware existente, batimentos cardíacos, reconexões e verificações flash permanecem no local. Novos recursos de espaço de trabalho não precisam de um flash de firmware.

A validação abrange configurações legadas, validação de sotaques, navegação de perfil e remapeamento de referência, modelos portáteis, modelo/mundo/redo nativo do Windows e mudança de perfil, gravação de atalhos, cópia/colar/reset, persistência de configurações, instalação/atualização, redimensionamento de layout e uma captura real de UI. O comportamento específico do hardware ainda requer o macropad e dispositivos de áudio em um PC Windows.

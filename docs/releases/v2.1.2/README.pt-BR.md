> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.2 e DeckPilot Atelier 1.0.11 corrigir a segurança da oficina, confiabilidade da placa de som e sons de perfil portáteis.

- Atelier pode preparar placas Leonardo/Caterina que ainda não respondem DeckPilot protocolo; falhas seriais ainda param de piscar e o local USB, bootloader e assinatura MCU permanecem verificados.
- Cada inspeção começa com resultados vazios. Relatórios retêm a placa e firmware testados, e chamadas de retornos USB não podem afetar uma nova sessão.
- O fechamento do Atelier durante a escrita ou verificação do firmware é bloqueado.
- Exportações de perfil e backups de perfil automáticos podem reter arquivos MP3/WAV referenciados (32 Limite de áudio MiB). Importa copia clipes incorporados em uma pasta gerenciada e remapeia referências de ação curta/longa. Importações de perfil legado permanecem suportadas; falta de áudio de backup é relatado explicitamente.
- A placa de som libera clipes acabados, atualiza os indicadores de reprodução e reporta fluxos de áudio interrompidos. VB-CABLE endpoints são reconhecidos através de sua identidade adaptadora.
- Correct traduzido README links e preservar nomes de dispositivos técnicos durante a tradução. VB-CABLE requisito.

Validação inclui verificações de regressão do núcleo, testes de protocolo de firmware, testes de tradução, Windows build, DeckPilot e verificação de instalação/atualização/ininstalação do Atelier e verificação de interface/atualização nativa. O comportamento físico do primeiro flash e da desconexão de áudio ainda requer o hardware do proprietário. 1.1.1; nenhuma atualização de firmware é necessária.

DeckPilot 2.1.1 melhora a instalação e atualizações:

- Verificar automaticamente se VB-CABLE durante a primeira instalação e atualizações, e baixar e lançar o instalador oficial assinado quando o driver está faltando.
- Mostrar a inicialização do Windows, verificações automáticas de atualização e preferências de movimento reduzido durante a configuração e atualizações, com as escolhas existentes pré-selecionadas e preservadas.
- Mantenha o microfone físico selecionado e peça a configuração do microfone apenas quando necessário.
- Corrigir a inicialização do instalador e a detecção das instalações existentes.

Transferir **DeckPilot- Setup.exe** ou aceitar a atualização em DeckPilot. A aprovação do administrador do Windows é necessária. Perfis e calibração existentes são preservados. DeckPilot Atelier permanece em 1.0.10 e firmware permanece em 1.1.1; ambos estão incluídos no lançamento compartilhado.

Validação: verificações principais e compilação do Windows, atualização/desinstalação do instalador e testes nativos de aplicativos executados antes da publicação de lançamento. Comportamento de hardware de áudio ainda requer testes no PC do usuário.

DeckPilot 2.1.0 mantém a interface preta profunda e cores personalizáveis, e roteia a placa de som através do VB-Audio VB-CABLE automaticamente. Os usuários selecionam apenas o microfone físico; o endpoint de reprodução virtual é fixo para que os clipes de voz e MP3/WAV não possam ser enviados acidentalmente para alto-falantes. DeckPilot instalador pode baixar o oficial VB-CABLE pacote, verifique o seu editor Authenticode e inicie a configuração do driver do fornecedor; o pacote não é redistribuído. Em Discórdia ou um jogo, selecione CABLE Output como o microfone. Atelier permanece na versão 1.0.10.

DeckPilot 1.9.11 e DeckPilot Atelier 1.0.10 adicionar diagnósticos de hardware e firmware pré-flash, uma comparação de versão de firmware, uma ação de recuperação guiada após escrita interrompida, flashing sequencial de múltiplas placas com um relatório de lote, e uma verificação de layout físico-position-to-F13-F24 em cada relatório de controle-chave.

DeckPilot 1.9.11 inclui a mesma atualização do Atelier na versão compartilhada do instalador.

DeckPilot 1.9.10 restaura lançamentos compartilhados contendo ambos DeckPilot e DeckPilot Atelier 1.0.9. Ambos os aplicativos mais uma vez verificar o mesmo lançamento GitHub mais recente.

Se você instalou o Atelier 1.0.8 a partir do canal de lançamento separado, instalar DeckPilot-Atelier-Setup.exe desta versão uma vez para retornar a atualizações compartilhadas. DeckPilot pode usar o seu atualizador existente.

DeckPilot Atelier 1.0.7 adiciona uma área de programa confirmada apagar que limpa tudo 28 KiB de flash de usuário, deixa um loop de reset seguro, e preserva Caterina e EEPROM. Reiniciar permanece desativado até que uma placa física seja selecionada.

DeckPilot 1.9.7 corrige a desinstalação do Atelier enquanto o aplicativo está aberto. Atelier 1.0.6 também corrige seu manifesto de DPI do Windows, o que impediu o aplicativo instalado de lançar.

DeckPilot 1.9.6 corrige o manifesto de DPI do Windows que impediu DeckPilot Atelier de abertura após instalação.

DeckPilot 1.9.5 inclui diagnósticos detalhados de inicialização do Atelier na verificação de lançamento do Windows.

DeckPilot 1.9.4 melhora o teste de fumaça de inicialização do Atelier Windows para reportar os detalhes da falha de aplicação.

DeckPilot 1.9.3 corrige os metadados de compilação do Atelier para ler o manifesto de firmware do arquivo de firmware empacotado.

DeckPilot 1.9.2 corrige a etapa de liberação da embalagem do Atelier e melhora seus diagnósticos de construção.

DeckPilot 1.9.1 melhora a inicialização do instalador Atelier e adiciona verificações automáticas de atualização de aplicativo e firmware.

- O instalador lança o Atelier na sessão de usuário do Windows iniciada e o para/recomeça com segurança para atualizações.
- O Atelier verifica o GitHub na inicialização e na demanda. Ele anuncia novas construções do Atelier ou firmware empacotado, então baixa um instalador verificado contra o SHA-256 Arquivo.
- Firmware é apenas flashed depois que o usuário escolhe a ação flash; uma atualização de aplicativo nunca escreve para uma placa conectada por conta própria.

O instalador do Atelier também lança na sessão do Windows do usuário iniciada após uma nova instalação. O fluxo de trabalho de lançamento agora abre o aplicativo instalado, em seguida, verifica a atualização e desinstalar enquanto ele está em execução.

DeckPilot 1.9.0 adds DeckPilot Atelier, um aplicativo de oficina do Windows separado para piscar novos macropads e verificar tudo 12 Chaves.

- Instale o firmware incluído em placas Leonardo / Pro Micro ATmega32U4 com o bootloader Caterina.
- Execute verificações guiadas de curto-pressão e longa-pressão para cada chave física, grave seu mapeamento F13-F24 e salve um relatório de controle de qualidade.
- Transferir **DeckPilot- Atelier- Setup.exe** desta versão. O original DeckPilot a configuração e o firmware permanecem disponíveis separadamente.

DeckPilot 1.8.0 adiciona um espaço de trabalho atualizado e maneiras mais rápidas de construir e usar seus perfis.

- Limpar a interface carvão/leal, biblioteca de ação primeiro, crocante 3 × 4 chaves, cores de acento chave e preferência de movimento reduzido.
- Três modelos de perfil editáveis: Música e áudio, Desktop e produtividade e Criação.
- Ações de perfil: mude para um perfil nomeado, o próximo ou o perfil anterior de uma chave. Os conjuntos de perfil importados mantêm seus links internos.
- Ações do Windows: abrir configurações comuns ou Gerenciador de tarefas, ou bloquear explicitamente o PC.
- Copiar/cortar/colar/reset chaves inteiras, incluindo longas prensas e cores, com desfazer/refazer e uma área de transferência local-aplicativo.
- Capture um atalho de teclado diretamente; Escape cancela e as teclas do macropad permanecem reservadas durante a captura.
- Ctrl+K pesquisa de ação e um histórico desobstruído, somente de sessão do último 30 acções.
- Corrigir o prompt de firmware automático de modo a aceitá-lo abre o gerenciador de firmware. Os gestos pendentes são cancelados através de alterações de perfil.

Transferir **DeckPilot- Setup.exe** ou atualização do aplicativo. Perfis existentes, calibração e preferências de inicialização são preservadas. Firmware permanece **1.1.1**; nenhum flash é necessário para essas características.

A validação inclui as verificações de compatibilidade/navegação/importação/template e instalação nativa do Windows, atualização, picador, arrastar e soltar, desfazer/refazer, criação de modelo, ações de perfil, captura de atalhos, edição de área de transferência, layout e minimização/restore. O comportamento físico do macropad e do dispositivo de áudio ainda depende do hardware do proprietário do Windows.

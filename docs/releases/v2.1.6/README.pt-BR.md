> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.6 e DeckPilot Atelier 1.0.12 Faça o firmware do Atelier apagar explícito e recuperável.

- Mova o firmware destrutivo apagar em operações avançadas colapsadas e exija digitação `EFFACER` antes de escrever.
- Explique que a placa pára de enumerar como um teclado após a rasura, mostrar os passos de recuperação RST/GND exatos duplo-reset, e manter a ação de recuperação de Atelier disponível após a rasura ou um flash interrompido.
- Firmware permanece 1.1.1; não DeckPilot mudança de firmware de hardware é necessária.

A validação inclui verificações de firmware e aplicativo, além de automação de interface do Windows para o controle de apagar avançado, segurança sem dispositivo e orientação de recuperação RST/GND. O reset em si ainda requer a placa física.

DeckPilot 2.1.5 suaviza o áudio-dispositivo atualiza e adiciona uma tela de inicialização de marca.

- Descubra microfones físicos e VB-CABLE fora do tópico UI. Repetições automáticas compartilham um instantâneo recente do dispositivo; o botão de atualização manual realiza sempre uma nova varredura.
- Reduza as pesquisas repetidas de estado de áudio do Windows e agite o caminho comum de conversão mono-microfone.
- Mostrar a DeckPilot banner enquanto a aplicação interactiva começa. A inicialização silenciosa com o Windows fica em segundo plano.
- Restos de atelier 1.0.11 e o firmware permanece 1.1.1.

A validação inclui verificações de regressão de varredura/cache de fundo, verificações de conversão de áudio, compilações do Windows, atualização/desinstalação do instalador, splash/atualização nativa e verificação do coletor de aplicativos. O uso de CPU de áudio real e latência ainda dependem do PC e dispositivos de áudio.

DeckPilot 2.1.4 inicia o microfone virtual configurado automaticamente com o aplicativo, incluindo a inicialização de fundo após o login no Windows.

- A inicialização automática do microfone de placa de som está ativada por padrão para as configurações existentes e novas. Ele usa o microfone físico selecionado e VB-CABLE; nenhuma ativação manual é necessária após reiniciar o PC quando DeckPilot começa com Windows.
- Se os dispositivos não estiverem prontos para iniciar sessão ou parar o áudio após uma desconexão, DeckPilot retorna a cada cinco segundos enquanto a ativação automática está ativada.
- Uma paragem explícita desactiva e salva a preferência de arranque automático; activa- a de novo a partir da Soundboard painel ou o botão de ativação. Os clipes de áudio nunca são reproduzidos automaticamente.
- Restos de atelier 1.0.11 e o firmware permanece 1.1.1.

A validação inclui configurações de migração/cheques de ida e volta e uma verificação nativa da interface do Windows para a preferência de início automático persistente, além das verificações de áudio e instalador existentes. A inicialização/reconexão do dispositivo físico ainda depende do hardware do proprietário.

DeckPilot 2.1.3 reduz a latência do microfone da placa de som e remove os blocos de conversão de um segundo do caminho de áudio ao vivo.

- Use reamostragem de pequeno bloco orientada para saída para o microfone e clipes de som; as taxas de amostragem correspondentes ignoram completamente a reamostragem.
- Pedido orientado para o evento 20 captura de ms e 40 buffers de reprodução ms em vez dos anteriores 100 Configurações ms. A latência real depende do Windows, dos dispositivos de áudio e VB-CABLE.
- Tampão de microfone fechado para 100 ms e reter quadros completos recentes após uma parada de reprodução em vez de enfileirar o discurso antigo. As falhas curtas adicionam apenas o silêncio solicitado pela leitura actual.
- Suporte entradas mono, estéreo, multicanal e Windows extensíveis PCM/float sem a Media Foundation um segundo lido à frente.
- Mantenha o processamento e roteamento da Discord inalterados. 1.0.11 e o firmware permanece 1.1.1.

Validação inclui verificações de regressão para reamostrar tamanhos de blocos, conversão de canal, formatos extensíveis, recuperação de underrun, frescura de transbordamento, captura concorrente / playback e acabamento do clipe, juntamente com o instalador Windows existente e testes de aplicação. Nenhuma figura de latência de hardware é reivindicada; verifique a melhoria com seu microfone e driver de áudio.

DeckPilot 2.1.2 e DeckPilot Atelier 1.0.11 corrigir a segurança da oficina, confiabilidade da placa de som e sons de perfil portáteis.

- Atelier pode preparar placas Leonardo/Caterina que ainda não respondem DeckPilot protocolo; falhas seriais ainda param de piscar e o local USB, bootloader e assinatura MCU permanecem verificados.
- Cada inspeção começa com resultados vazios. Relatórios retêm a placa e firmware testados, e callbacks USB obsoletos não podem afetar uma nova sessão.
- O fechamento do Atelier durante a escrita ou verificação do firmware é bloqueado.
- Exportações de perfil e backups de perfil automáticos podem reter arquivos MP3/WAV referenciados (32 Limite de áudio MiB). Importa copia clipes incorporados em uma pasta gerenciada e remape referências de ação curta/longa. Importações de perfil legado permanecem suportadas; áudio de backup faltando é relatado explicitamente.
- A placa de som libera clipes acabados, atualiza os indicadores de reprodução e reporta fluxos de áudio interrompidos. VB-CABLE endpoints são reconhecidos através de sua identidade adaptadora.
- Corrigir traduzido README links e preservar nomes de dispositivos técnicos durante a tradução. VB-CABLE requisito.

A validação inclui verificações de regressão do núcleo, testes de protocolo de firmware, testes de tradução, compilação do Windows, DeckPilot e verificação de instalação/atualização/ininstalação do Atelier, e verificação de interface/atualização nativa. O comportamento físico de primeiro flash e de desconexão de áudio ainda requer o hardware do proprietário. 1.1.1; nenhuma atualização de firmware é necessária.

DeckPilot 2.1.1 melhora a instalação e as atualizações:

- Verificar automaticamente se VB-CABLE durante a primeira instalação e atualizações, e baixar e lançar o instalador oficial assinado quando o driver está faltando.
- Mostrar a inicialização do Windows, verificações automáticas de atualização e preferências de movimento reduzido durante a configuração e atualizações, com as escolhas existentes pré-selecionadas e preservadas.
- Mantenha o microfone físico selecionado e peça a configuração do microfone apenas quando necessário.
- Corrigir a inicialização do instalador e detecção de instalações existentes.

Baixar **DeckPilot- Setup.exe** ou aceitar a atualização em DeckPilot. A aprovação do administrador do Windows é necessária. Perfis e calibração existentes são preservados. DeckPilot Atelier permanece em 1.0.10 e firmware permanece em 1.1.1; ambos estão incluídos no lançamento compartilhado.

Validação: verificações principais e compilação do Windows, atualização/desinstalação do instalador e testes nativos de aplicativos executados antes da publicação de lançamento. Comportamento de hardware de áudio ainda requer testes no PC do usuário.

DeckPilot 2.1.0 mantém a interface preta profunda e cores personalizáveis, e roteia a placa de som através do VB-Audio VB-CABLE automaticamente. Os usuários selecionam apenas o microfone físico; o ponto final de reprodução virtual é fixo para que a voz e clipes MP3/WAV não possam ser enviados acidentalmente para alto-falantes. DeckPilot instalador pode baixar o oficial VB-CABLE pacote, verifique o seu editor Authenticode e inicie a configuração do driver do fornecedor; o pacote não é redistribuído. Em Discord ou um jogo, selecione CABLE Output como o microfone. Atelier permanece na versão 1.0.10.

DeckPilot 1.9.11 e DeckPilot Atelier 1.0.10 adicionar diagnósticos de hardware e firmware pré-flash, uma comparação de versão de firmware, uma ação de recuperação guiada após escrita interrompida, piscamento sequencial de múltiplas placas com um relatório de lote e uma verificação de layout físico-position-to-F13-F24 em cada relatório de controle de chaves.

DeckPilot 1.9.11 inclui a mesma atualização do Atelier na versão compartilhada do instalador.

DeckPilot 1.9.10 restaura lançamentos compartilhados contendo ambos DeckPilot e DeckPilot Atelier 1.0.9. Ambos os aplicativos mais uma vez verificar o mesmo lançamento GitHub mais recente.

Se você instalou o Atelier 1.0.8 do canal de lançamento separado, instalar DeckPilot-Atelier-Setup.exe desta versão uma vez para retornar a atualizações compartilhadas. DeckPilot pode usar o seu atualizador existente.

DeckPilot Atelier 1.0.7 adiciona uma área de programa confirmada apagar que limpa tudo 28 KiB do flash do usuário, deixa um loop de reset seguro, e preserva Caterina e EEPROM. Reiniciar permanece desativado até que uma placa física seja selecionada.

DeckPilot 1.9.7 corrige a desinstalação do Atelier enquanto o aplicativo está aberto. Atelier 1.0.6 também corrige seu manifesto de DPI do Windows, o que impediu o aplicativo instalado de lançar.

DeckPilot 1.9.6 corrige o manifesto de DPI do Windows que impediu DeckPilot Atelier de abertura após instalação.

DeckPilot 1.9.5 inclui diagnósticos detalhados de inicialização do Atelier na verificação de lançamento do Windows.

DeckPilot 1.9.4 melhora o teste de fumaça de inicialização do Atelier Windows para reportar detalhes de falha de aplicação.

DeckPilot 1.9.3 corrige os metadados de compilação do Atelier para ler o manifesto de firmware a partir do arquivo de firmware empacotado.

DeckPilot 1.9.2 corrige a etapa de liberação da embalagem do Atelier e melhora seus diagnósticos de construção.

DeckPilot 1.9.1 melhora a inicialização do instalador Atelier e adiciona verificações automáticas de atualização de aplicativo e firmware.

- O instalador lança o Atelier na sessão de usuário do Windows iniciada e para/reinicia com segurança para atualizações.
- O Atelier verifica o GitHub na inicialização e sob demanda. Anuncia novas construções do Atelier ou firmware empacotado, então baixa um instalador verificado contra o SHA-256 Arquivo.
- Firmware só é flashed depois que o usuário escolhe a ação flash; uma atualização de aplicativo nunca escreve para um tabuleiro conectado por conta própria.

O instalador do Atelier também lança na sessão do Windows do usuário iniciada após uma nova instalação. O fluxo de trabalho de lançamento agora abre o aplicativo instalado, então verifica a atualização e desinstala enquanto ele está em execução.

DeckPilot 1.9.0 adds DeckPilot Atelier, um aplicativo de oficina do Windows separado para piscar novos macropads e verificar tudo 12 Chaves.

- Instale o firmware incluído em placas Leonardo / Pro Micro ATmega32U4 com o bootloader Caterina.
- Execute verificações guiadas de curto-pressão e longa-pressão para cada chave física, grave seu mapeamento F13-F24 e salve um relatório de controle de qualidade.
- Baixar **DeckPilot- Atelier- Setup.exe** desta versão. O original DeckPilot a configuração e o firmware permanecem disponíveis separadamente.

DeckPilot 1.8.0 adiciona um espaço de trabalho atualizado e maneiras mais rápidas de construir e usar seus perfis.

- Limpar a interface carvão/leal, biblioteca de ação primeiro, crocante 3 × 4 chaves, cores de acento-chave e preferência de movimento reduzido.
- Três modelos de perfil editáveis: Música e áudio, Desktop e produtividade e Criação.
- Ações de perfil: mude para um perfil nomeado, o próximo ou o perfil anterior a partir de uma chave. Os conjuntos de perfil importados mantêm seus links internos.
- Ações do Windows: abrir configurações comuns ou Gerenciador de tarefas, ou explicitamente bloquear o PC.
- Copiar/cortar/colar/repor chaves inteiras, incluindo longas prensas e cores, com desfazer/refazer e uma área de transferência local do aplicativo.
- Capture um atalho de teclado diretamente; Escape cancela e as teclas do macropad permanecem reservadas durante a captura.
- Pesquisa de ação Ctrl+K e um histórico clearable, somente de sessão do último 30 acções.
- Corrigir o prompt de firmware automático de modo a aceitá- lo abre o gerenciador de firmware. Os gestos pendentes são cancelados através de mudanças de perfil.

Baixar **DeckPilot- Setup.exe** ou atualização do aplicativo. Perfis existentes, calibração e preferências de inicialização são preservadas. Firmware permanece **1.1.1**; nenhum flash é necessário para essas características.

A validação inclui verificações de compatibilidade/navegação/importação/template do núcleo e instalação nativa do Windows, atualização, picker, arrastar e soltar, desfazer/refazer, criação de modelo, ações de perfil, captura de atalhos, edição de área de transferência, layout e minimização/restauração de verificações. O comportamento físico do macropad e do dispositivo de áudio ainda depende do hardware do proprietário do Windows.

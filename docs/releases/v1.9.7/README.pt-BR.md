> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

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

A validação inclui as verificações de compatibilidade/navegação/importação/template do núcleo e a instalação nativa do Windows, atualização, picker, arrastar e soltar, desfazer/refazer, criação de modelo, ações de perfil, captura de atalhos, edição da área de transferência, layout e minimização/restore. O comportamento físico do macropad e do dispositivo de áudio ainda depende do hardware do proprietário do Windows.

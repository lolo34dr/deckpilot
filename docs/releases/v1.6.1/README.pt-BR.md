> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 1.6.1 corrige renderização de teclado macio e adiciona recuperação de perfil, controles de áudio do Windows e prensas curtas / longas.

- Texto de teclado de tamanho nativo: removeu a escala de deck inteiro e os efeitos de sombra de conteúdo, adicionou consciência de DPI por monitor e ícones maiores de aplicativos de desktop. Janelas pequenas rolam em vez de teclas encolhendo.
- Desfazer/refazer para edições de perfil, incluindo arrastar- e- soltar (50 alterações por sessão de aplicação); os campos de texto mantêm nativo Ctrl+Z/Ctrl+Y.
- Importação/exportação de perfis e 20 backups automáticos de versão anterior. Importações adicionar novas cópias; restaurar e desfazer a calibração preservar. As ações importadas nunca são executadas automaticamente.
- O microfone global do Windows alterna com um indicador de barra de estado persistente e estado de endpoint ao vivo. Todos os endpoints de captura ativos são silenciados/desmudados juntos.
- Alterna entre dois dispositivos de saída conectados escolhidos, para todas as funções de áudio padrão do Windows. Os dispositivos em falta falham sem escolher silenciosamente outra saída.
- Duas ações configuradas por chave com uma 200–3000 ms liminar. Curtas corridas no lançamento, longa substitui curta e roda uma vez. As ações imediatas existentes e as teclas F13-F24 livres mantêm seu comportamento. Nenhum flash de firmware necessário.

Baixar **DeckPilot- Setup.exe** ou atualização do aplicativo. Perfis e calibração são preservados. As opções de endpoint de áudio podem precisar de ajuste após a importação de perfis em outro PC. O estado de endpoint do Windows difere do próprio mudo de um aplicativo ou de um switch de hardware mudo; aplicativos de saída fixa podem não seguir as alterações de saída padrão.

Validação: principais testes de perfil/gestura/áudio (endpoints de áudio simulados), instalador/atualização do Windows/UI testes, arrastar o mouse nativo desfazer/refazer e edição de longa ação. Suportes de teclas físicas, drivers de microfone reais e headset/alto-falante ainda precisam de confirmação no hardware do proprietário.

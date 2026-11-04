> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 1.7.0 reduz o trabalho repetido ao editar perfis, encontrar aplicações e correr em segundo plano.

- A gravação de configurações inalteradas não substitui mais o arquivo ou cria backups. O conteúdo real do disco ainda está selecionado, então as edições externas, as exclusões e as salvares falhadas não estão escondidas.
- O histórico do perfil mantém instantâneos desvinculados do modelo. Edições inalteradas e desfazer/refazer evitam viagens de ida e volta do JSON; a edição de ações usa comparações de valores em vez de serialização repetida.
- Pesquisa e seleção de aplicativos compartilham uma cache de catálogo de cinco minutos e uma tarefa de descoberta. Discovery corre fora do WPF; Atualizar ainda força uma nova varredura. Cada coletor possui seu filtro e mantém os resultados atuais utilizáveis durante a atualização.
- Substituir o permanente 100 enquete de interface de placa ms com notificações de conexão ordenadas. Keyboard-hook callbacks trabalho visual fila em vez de renderizar doze chaves síncrona.
- As sondagens de estado de áudio a cada dois segundos, enquanto visíveis e a cada dez segundos, quando ocultas/minimizadas, são refrescantes imediatamente após a reabertura e após os comandos de áudio. As requisições de actualização sobrepostas são coalescidas sem deixar cair a atualização pós-comando.
- Janelas ocultas parar temporizadores de feedback visual e pular renderização estética espera. Escovas de UI congeladas são reutilizadas e lançadas embalagens de processo de aplicação são eliminados.

Baixar **DeckPilot- Setup.exe** ou atualização a partir do aplicativo. Perfis, calibração e preferências de inicialização são preservadas. Nenhum flash de firmware é necessário.

A validação inclui configurações/história/regressões de cache e instalação do Windows, atualização, picador, arrastar e soltar, edição de ação curta/longa e minimizar/restaurar verificações. Estas são otimizações estruturais, não uma reivindicação medida de uma determinada CPU ou gravação de RAM em cada PC.

>  Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 1.6.4  corrige a substituição do resultado de busca durante o drag-and-drop e estabiliza a verificação do Windows.

-  Mantenha os botões de pesquisa existentes quando o temporizador ou o catálogo de aplicativos instalado produz os mesmos resultados, de modo que uma fonte de arrasto não é descolada inesperadamente.
- Define a substituição do resultado de pesquisa enquanto uma interação com o mouse ou o OLE drag está em andamento.
-  Verificações de interface do Windows usam coordenadas físicas do cursor, controles de foco antes de medi- las, e escaneia o intervalo de rolagem completo em vez de assumir um número fixo de incrementos de linha. Os arrastos nativos do mouse permanecem necessários; atribuições falhadas não são tentadas novamente para obter um passe.
- As configurações do Windows salvam o uso da API de substituição atômica dedicada para que um leitor existente possa reter seu instantâneo antigo sem bloquear o próximo salvamento. Uma regressão central verifica três substituições sucessivas com um leitor compartilhado mantido aberto.
-  Os leitores de configurações nos testes do Windows usam UTF-8 e permitir a substituição de arquivos atômicos. 500 ms somente para violação de compartilhamento/bloqueio do Windows durante a substituição; outros erros de leitura e bloqueios persistentes falham no teste. Três ciclos de desfazer/refazer também mantêm um instantâneo antigo aberto para verificar se salva sucesso sem modificar os leitores existentes.
-  Verificações de background-startup esperar para WPF para terminar de esconder sua janela, em seguida, exigir o processo para permanecer em execução sem janela principal visível.
-  Atualizar ações do GitHub para Node.js 24  versões e instalar o mesmo Arduino CLI 1.2.2 a partir do seu arquivo a montante verificado pela soma de verificação.

 Transferir **DeckPilot- Setup.exe** ou atualização do aplicativo. Perfis e calibração são preservados. Nenhum flash de firmware é necessário.

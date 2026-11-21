> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.4 inicia o microfone virtual configurado automaticamente com o aplicativo, incluindo a inicialização de fundo após o login no Windows.

- A inicialização automática do microfone de placa de som está ativada por padrão para as configurações existentes e novas. Ele usa o microfone físico selecionado e VB-CABLE; nenhuma ativação manual é necessária após reiniciar o PC quando DeckPilot começa com Windows.
- Se os dispositivos não estiverem prontos para iniciar sessão ou parar o áudio após uma desconexão, DeckPilot retorna a cada cinco segundos enquanto a ativação automática está ativada.
- Uma paragem explícita desactiva e salva a preferência de arranque automático; activa- a de novo a partir da Soundboard painel ou o botão de ativação. Os clipes de áudio nunca são reproduzidos automaticamente.
- Restos de atelier 1.0.11 e o firmware permanece 1.1.1.

A validação inclui configurações de migração/cheques de ida e volta e uma verificação nativa da interface do Windows para a preferência de início automático persistente, além das verificações de áudio e instalador existentes. A inicialização/reconexão do dispositivo físico ainda depende do hardware do proprietário.

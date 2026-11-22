> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.3 reduz a latência do microfone da placa de som e remove os blocos de conversão de um segundo do caminho de áudio ao vivo.

- Use reamostragem de pequenos blocos orientado a saída para o microfone e clipes de som; as taxas de amostragem correspondentes ignoram completamente a reamostragem.
- Pedido orientado para o evento 20 captura de ms e 40 buffers de reprodução ms em vez dos anteriores 100 Configurações ms. A latência real depende do Windows, dos dispositivos de áudio e VB-CABLE.
- Tampão de microfone fechado para 100 ms e reter quadros completos recentes após uma parada de reprodução em vez de enfileirar o discurso antigo. As falhas curtas adicionam apenas o silêncio solicitado pela leitura actual.
- Suporte entradas mono, estéreo, multicanal e Windows extensíveis PCM/float sem a Media Foundation um segundo lido à frente.
- Mantenha o processamento e roteamento da Discórdia inalterados. 1.0.11 e o firmware permanece 1.1.1.

A validação inclui verificações de regressão para reamostrar tamanhos de bloco, conversão de canal, formatos extensíveis, recuperação de underrun, frescura de transbordamento, captura/reprodução simultânea e conclusão do clipe, juntamente com o instalador do Windows e testes de aplicação existentes. Nenhuma figura de latência de hardware é reivindicada; verifique a melhoria com seu microfone e driver de áudio.

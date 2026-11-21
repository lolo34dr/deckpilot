> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.2.0 adiciona supressão de ruído de microfone local ajustável, mantendo os clipes de placa de som intocados.

- RNNoise filtra o microfone físico selecionado antes DeckPilot Misturas em clipes de placa de som. Clips ignorar o filtro e permanecer claro no microfone virtual.
- Escolha qualquer aplicativo que use **CABLE Output** como seu microfone – jogos e aplicativos de voz-chat também recebem a voz filtrada e mistura de placa de som. Apps usando o microfone físico diretamente bypass DeckPilotÉ o filtro.
- Activar ou desactivar a filtragem e definir a sua 0–100% de concentração em **Soundboard · micro virtuel**. O processamento de áudio permanece neste PC; nenhum flash de firmware é necessário.
- A biblioteca nativa RNNoise é construída para Windows x64 e incorporada em DeckPilot A sua licença de terceiros do tipo BSD está incluída no repositório de código-fonte.

A 10 ms RNNoise frame tamanho não garante latência de ponta a ponta. Windows, os dispositivos de áudio e VB-CABLE Teste a qualidade e latência do microfone com a configuração pretendida.

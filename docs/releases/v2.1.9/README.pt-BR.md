> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.9 corrige o monitoramento de alto-falantes em dispositivos cujo formato de áudio Windows é extensível, incluindo alguns fones de ouvido USB, e adiciona uma notificação de sobreposição de placa de som.

- O retorno do alto-falante mantém o formato exato de mistura WASAPI da saída selecionada, converte amostras de clipe para o seu PCM ou codificação flutuante, e repete sem reprodução orientada por eventos quando um driver rejeita esse modo.
- O início de um clipe de placa de som mostra o seu nome na sobreposição configurável na tela. A preferência de notificação de perfil existente, a colocação e o controle de configuração de movimento reduzido ambos os tipos de notificação.
- O retorno ainda contém apenas clipes de placa de som; o microfone físico fica fora da mistura de alto-falantes e continua para o microfone virtual.

DeckPilot Atelier permanece em 1.0.12 e o firmware permanece 1.1.1. Nenhum flash de firmware é necessário. A compatibilidade do alto-falante ainda deve ser verificada com o dispositivo de áudio do proprietário do Windows real.

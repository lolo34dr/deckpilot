> Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot 2.1.2 e DeckPilot Atelier 1.0.11 corrigir segurança da oficina, confiabilidade da placa de som e sons de perfil portáteis.

- Atelier pode preparar placas Leonardo/Caterina que ainda não respondem DeckPilot protocolo; falhas seriais ainda param de piscar e o local USB, bootloader e assinatura MCU permanecem verificados.
- Cada inspeção começa com resultados vazios. Relatórios retêm a placa e firmware testados, e callbacks USB obsoletos não podem afetar uma nova sessão.
- O fechamento do Atelier durante a escrita ou verificação de firmware é bloqueado.
- Exportações de perfil e backups automáticos de perfil podem reter arquivos MP3/WAV referenciados (32 Limite de áudio MiB). Importa copia clipes incorporados em uma pasta gerenciada e remapeia referências de ação curta/longa. Importações de perfil legado permanecem suportadas; áudio de backup faltando é relatado explicitamente.
- A placa de som libera clipes acabados, atualiza os indicadores de reprodução e reporta fluxos de áudio interrompidos. VB-CABLE endpoints são reconhecidos através de sua identidade adaptadora.
- Corrigir traduzido README links e preservar nomes de dispositivos técnicos durante a tradução. VB-CABLE requisito.

A validação inclui verificações de regressão do núcleo, testes de protocolo de firmware, testes de tradução, compilação do Windows, DeckPilot e verificação de instalação/atualização/ininstalação do Atelier e verificação de interface/atualização nativa. O comportamento físico de primeiro flash e de desconexão de áudio ainda exigem o hardware do proprietário. 1.1.1; nenhuma atualização de firmware é necessária.

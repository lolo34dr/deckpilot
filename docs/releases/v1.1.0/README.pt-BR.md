>  Esta é uma tradução automática das notas de lançamento completas. O original em inglês é a referência.

DeckPilot  agora pode instalar firmware no micropad DIY Pro diretamente a partir do aplicativo.

- Novo **Firmware du macropad…**  diálogo com a seleção e confirmação do dispositivo antes de piscar.
- Firmware agrupado **1.0.0**  preserva F13–F24 e acrescenta um DeckPilot  resposta de identidade/versão sobre a série USB.
- AVRDUDE agrupado: nenhuma instalação do Arduino IDE necessária para atualizações normais.
-  Verificação de firmware e limites de memória validados, correspondência física de conexão USB, verificação de gravação e verificação de versão pós-flash.
- Perfis de PC existentes e calibração são preservados. Atualizar o aplicativo nunca flashes de hardware automaticamente.

**O primeiro teste de hardware ainda é necessário:** o procedimento de upload Caterina do firmware original é confirmado; o novo firmware/updater ainda deve ser testado no macropad real. Para Pro Micro **ATmega32U4, 5 V / 16 MHz**, ligado **2–10, 14, 15, 16**, com IDs Leonardo USB e bootloader Caterina apenas. Mantenha o esboço original e acesso RST / GND para recuperação. Sem backup de firmware ou rollback automático.

 Janelas 10/11 x64. Baixar **DeckPilot.exe** ou **DeckPilot-win-x64.zip**. Os firmware / ferramentas estão incorporados no aplicativo e também disponíveis em **DeckPilot- Firmware.zip**; AVRDUDE tem sua própria licença GPL e fonte empacotada.

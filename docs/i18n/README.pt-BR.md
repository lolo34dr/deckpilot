<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">O companheiro Windows para seu macropad de 12 teclas.</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&amp;color=78b7a0" alt="Latest release"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&amp;logo=windows" alt="Download for Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="MIT license"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Windows build status"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&amp;color=70a7e8" alt="GitHub release downloads"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

O DeckPilot configura F13–F24 em macropads compatíveis com Arduino Leonardo, organizados em 3 linhas de 4 teclas. A calibração guiada identifica as posições mesmo com a fiação em outra ordem.

## Recursos

- Abrir aplicativos instalados, pastas e sites ou enviar atalhos de teclado.
- Controlar o Spotify separadamente dos controles de mídia do Windows, silenciar microfones e alternar a saída de áudio.
- Criar perfis e usar modelos de Música, Produtividade ou Criação; trocar de perfil por uma tecla.
- Configurar toques curtos e longos, escolher cores e capturar combinações de teclas.
- Arrastar e soltar, copiar/recortar/colar, desfazer/refazer, importar/exportar e recuperar backups automáticos.

## Instalação

Requer Windows 10 versão 2004 ou posterior, ou Windows 11, x64, e cabo USB de dados. O runtime .NET está incluído.

1. Baixe DeckPilot-Setup.exe no link acima e instale. O Windows solicita permissão de administrador para Program Files. Atalhos na Área de Trabalho/menu Iniciar e inicialização com o Windows são oferecidos por padrão.
2. Conecte o macropad e calibre da esquerda para a direita: quatro teclas por linha, três linhas. Cada tecla deve enviar um F13–F24 diferente.

## Uso

Selecione uma tecla, escolha uma ação na biblioteca, configure as opções e salve. É possível editar e testar sem conectar a placa. Teclas livres preservam F13–F24 para jogos e outros aplicativos.

Fechar a janela mantém o DeckPilot na área de notificação. Use o menu do ícone para sair completamente. O Spotify exige o aplicativo Windows aberto com uma sessão de reprodução; não controla o YouTube como alternativa.

## Atualizações e firmware

Atualizações são verificadas na inicialização e a cada seis horas. Após sua aprovação, são baixadas e verificadas. O instalador pode solicitar autorização do Windows. Perfis e calibração são preservados.

Nenhum driver extra nem gravação de firmware é necessário se o macropad já envia F13–F24. O firmware DeckPilot compatível acrescenta identificação e diagnóstico; leia o guia de hardware antes de gravar.

## Documentação e licença

Esta página traduz o guia do usuário. A interface está atualmente em francês; a referência técnica e o histórico estão em inglês.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

Licença MIT: permite usar, copiar, modificar e redistribuir o software mantendo os avisos de copyright e licença.

[MIT LICENSE](../../LICENSE)

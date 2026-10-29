<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">El compañero de Windows para tu macropad de 12 teclas.</p>

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

DeckPilot configura F13–F24 en macropads compatibles con Arduino Leonardo, con 3 filas de 4 teclas. La calibración guiada identifica su posición aunque el cableado siga otro orden.

## Funciones

- Abrir aplicaciones instaladas, carpetas y sitios web, o enviar atajos de teclado.
- Controlar Spotify por separado de los controles multimedia de Windows, silenciar micrófonos y cambiar la salida de audio.
- Crear perfiles y usar plantillas de Música, Productividad o Creación; cambiar de perfil desde una tecla.
- Configurar pulsaciones cortas y largas, elegir colores y capturar combinaciones de teclas.
- Arrastrar y soltar, copiar/cortar/pegar, deshacer/rehacer, importar/exportar y recuperar copias automáticas.

## Instalación

Requiere Windows 10 versión 2004 o posterior, o Windows 11, x64, y un cable USB de datos. El runtime de .NET está incluido.

1. Descarga DeckPilot-Setup.exe desde el enlace superior e instálalo. Windows solicita permisos de administrador para Program Files. Se ofrecen por defecto accesos directos en Escritorio/Inicio y el inicio con Windows.
2. Conecta el macropad y calibra de izquierda a derecha: cuatro teclas por fila, tres filas. Cada tecla debe enviar un F13–F24 distinto.

## Uso

Selecciona una tecla, elige una acción de la biblioteca, configura sus opciones y guarda. Puedes editar y probar acciones sin conectar la placa. Una tecla libre conserva F13–F24 para juegos u otras aplicaciones.

Cerrar la ventana mantiene DeckPilot en la bandeja del sistema. Usa el menú del icono para salir por completo. Spotify necesita su aplicación de Windows abierta con una sesión de reproducción; no se sustituye por YouTube.

## Actualizaciones y firmware

Se buscan actualizaciones al iniciar y cada seis horas. Con tu aprobación se descargan y verifican. El instalador puede pedir autorización de Windows. Se conservan los perfiles y la calibración.

No se necesita otro controlador ni flashear un macropad que ya envía F13–F24. El firmware compatible de DeckPilot añade identificación y diagnóstico; consulta la guía de hardware antes de flashear.

## Documentación y licencia

Esta página traduce la guía de usuario. La interfaz está actualmente en francés; la referencia técnica detallada y el historial están en inglés.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

Licencia MIT: permite usar, copiar, modificar y redistribuir el software conservando los avisos de copyright y licencia.

[MIT LICENSE](../../LICENSE)

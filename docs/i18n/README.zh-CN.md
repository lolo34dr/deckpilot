<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">适用于12键宏键盘的Windows配套应用。</p>

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

DeckPilot可配置兼容Arduino Leonardo的宏键盘所发送的F13–F24。布局为3行4列。即使接线顺序不同，也可以通过引导校准确定每个按键的位置。

## 功能

- 启动已安装的应用、打开文件夹和网站，或发送键盘快捷键。
- 独立控制Spotify，不影响Windows通用媒体控制；支持麦克风静音和音频输出切换。
- 创建配置文件，使用音乐、效率或创作模板，并通过按键切换配置。
- 设置短按和长按、选择按键颜色、录制快捷键。
- 支持拖放、复制／剪切／粘贴、撤销／重做、导入／导出和自动备份。

## 安装

需要Windows 10版本2004或更高版本，或Windows 11，x64架构，以及支持数据传输的USB线。已包含.NET运行时。

1. 通过上方链接下载并运行DeckPilot-Setup.exe。安装到Program Files时，Windows会请求管理员权限。默认提供桌面／开始菜单快捷方式和开机启动选项。
2. 连接宏键盘，按从左到右、每行四键、共三行的顺序校准。每个按键必须发送不同的F13–F24键值。

## 使用

选择一个按键，从动作库选择动作，设置选项并保存。未连接设备时也能编辑和测试。自由按键保留F13–F24，可直接在游戏和其他应用中绑定。

关闭窗口后，DeckPilot仍在通知区域运行。通过托盘图标菜单可完全退出。Spotify控制需要打开Windows版Spotify并建立播放会话，不会转而控制YouTube。

## 更新与固件

应用在启动时及每六小时检查更新。经你同意后下载并校验。安装程序可能请求Windows授权。配置文件和校准信息会保留。

如果宏键盘已经发送F13–F24，就不需要额外驱动或刷写固件。兼容的DeckPilot固件提供身份识别和诊断功能；刷写前请阅读硬件指南。

## 文档与许可

本页为用户指南译文。应用界面目前为法语；详细技术参考和版本历史为英语。

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

采用MIT许可证：保留版权声明和许可文本即可使用、复制、修改和再分发。

[MIT LICENSE](../../LICENSE)

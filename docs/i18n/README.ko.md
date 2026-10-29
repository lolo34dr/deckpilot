<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">12키 매크로패드를 위한 Windows 앱.</p>

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

DeckPilot은 Arduino Leonardo 호환 매크로패드의 F13–F24를 설정합니다. 배열은 3행 4열입니다. 배선 순서가 달라도 안내에 따라 각 키의 위치를 보정할 수 있습니다.

## 기능

- 설치된 앱, 폴더, 웹사이트를 열고 키보드 단축키를 전송합니다.
- Windows 미디어 제어와 별도로 Spotify를 제어하고 마이크 음소거 및 오디오 출력 전환을 지원합니다.
- 음악·생산성·창작 템플릿으로 프로필을 만들고 키로 전환합니다.
- 짧게 누르기와 길게 누르기, 키 색상, 단축키 기록을 설정합니다.
- 끌어서 놓기, 복사·잘라내기·붙여넣기, 실행 취소·다시 실행, 가져오기·내보내기, 자동 백업을 지원합니다.

## 설치

Windows 10 버전 2004 이상 또는 Windows 11 x64와 데이터 전송용 USB 케이블이 필요합니다. .NET 런타임은 포함되어 있습니다.

1. 위 링크에서 DeckPilot-Setup.exe를 다운로드하여 설치합니다. Program Files 설치에는 관리자 권한이 필요합니다. 바탕 화면·시작 메뉴 바로가기와 Windows 시작 시 실행이 기본으로 제안됩니다.
2. 매크로패드를 연결하고 왼쪽에서 오른쪽으로 한 행에 네 키씩, 세 행을 보정합니다. 각 키는 서로 다른 F13–F24를 보내야 합니다.

## 사용법

키를 선택하고 라이브러리에서 작업을 골라 옵션을 설정한 뒤 저장합니다. 보드가 연결되지 않아도 편집하고 테스트할 수 있습니다. 자유 키는 게임과 다른 앱에서 F13–F24로 사용할 수 있습니다.

창을 닫아도 알림 영역에서 계속 실행됩니다. 완전히 종료하려면 아이콘 메뉴를 사용하세요. Spotify는 Windows 앱이 열려 있고 재생 세션이 있어야 합니다. 대신 YouTube를 제어하지 않습니다.

## 업데이트와 펌웨어

시작 시와 6시간마다 업데이트를 확인합니다. 승인하면 다운로드하고 검증합니다. 설치 프로그램이 Windows 권한을 요청할 수 있습니다. 프로필과 보정은 유지됩니다.

이미 F13–F24를 보내는 매크로패드는 추가 드라이버나 펌웨어 쓰기가 필요하지 않습니다. 호환 DeckPilot 펌웨어는 식별 및 진단 기능을 추가합니다. 쓰기 전에 하드웨어 가이드를 확인하세요.

## 문서와 라이선스

이 페이지는 사용자 가이드 번역입니다. 앱 인터페이스는 현재 프랑스어이며 자세한 기술 문서와 변경 이력은 영어입니다.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

MIT 라이선스에 따라 저작권 및 라이선스 고지를 유지하면 사용·복사·수정·재배포할 수 있습니다.

[MIT LICENSE](../../LICENSE)

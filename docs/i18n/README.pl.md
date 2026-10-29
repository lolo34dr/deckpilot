<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">Aplikacja Windows do Twojego macropada z 12 klawiszami.</p>

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

DeckPilot konfiguruje F13–F24 na macropadach zgodnych z Arduino Leonardo, w układzie 3 rzędów po 4 klawisze. Kalibracja rozpoznaje położenie nawet przy innej kolejności przewodów.

## Funkcje

- Uruchamianie zainstalowanych aplikacji, otwieranie folderów i stron oraz wysyłanie skrótów klawiaturowych.
- Sterowanie Spotify niezależnie od multimediów Windows, wyciszanie mikrofonów i zmiana wyjścia audio.
- Profile i szablony Muzyka, Produktywność lub Tworzenie; zmiana profilu jednym klawiszem.
- Krótkie i długie naciśnięcie, kolory klawiszy oraz nagrywanie kombinacji klawiszy.
- Przeciąganie, kopiowanie/wycinanie/wklejanie, cofanie/ponawianie, import/eksport i automatyczne kopie zapasowe.

## Instalacja

Wymagany jest Windows 10 w wersji 2004 lub nowszej albo Windows 11, x64, oraz kabel USB do transmisji danych. Środowisko .NET jest dołączone.

1. Pobierz DeckPilot-Setup.exe z linku powyżej i zainstaluj. Windows poprosi o uprawnienia administratora do Program Files. Domyślnie proponowane są skróty na pulpicie/w menu Start i uruchamianie z Windows.
2. Podłącz macropad i kalibruj od lewej do prawej: cztery klawisze w rzędzie, trzy rzędy. Każdy musi wysyłać inne F13–F24.

## Obsługa

Wybierz klawisz, akcję z biblioteki, ustaw opcje i zapisz. Edycja i testowanie działają także bez podłączonej płytki. Wolne klawisze zachowują F13–F24 dla gier i innych aplikacji.

Zamknięcie okna pozostawia DeckPilot w zasobniku systemowym. Zakończ program z menu ikony. Spotify wymaga otwartej aplikacji Windows z sesją odtwarzania; nie steruje zastępczo YouTube.

## Aktualizacje i firmware

Aktualizacje są sprawdzane przy starcie i co sześć godzin. Po zatwierdzeniu są pobierane i weryfikowane. Instalator może poprosić o zgodę Windows. Profile i kalibracja zostają zachowane.

Dodatkowy sterownik ani wgrywanie firmware nie są potrzebne, jeśli macropad już wysyła F13–F24. Zgodny firmware DeckPilot dodaje identyfikację i diagnostykę; przed wgrywaniem przeczytaj instrukcję sprzętu.

## Dokumentacja i licencja

Ta strona jest tłumaczeniem instrukcji użytkownika. Interfejs aplikacji jest obecnie francuski; dokumentacja techniczna i historia zmian są angielskie.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

Licencja MIT pozwala używać, kopiować, zmieniać i rozpowszechniać program z zachowaniem informacji o prawach autorskich i licencji.

[MIT LICENSE](../../LICENSE)

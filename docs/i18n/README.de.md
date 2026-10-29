<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">Der Windows-Begleiter für dein Makropad mit 12 Tasten.</p>

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

DeckPilot konfiguriert F13–F24 auf Arduino-Leonardo-kompatiblen Makropads mit 3 Reihen und 4 Spalten. Die geführte Kalibrierung ordnet auch unterschiedlich verkabelte Tasten richtig zu.

## Funktionen

- Installierte Anwendungen, Ordner und Websites öffnen oder Tastenkombinationen senden.
- Spotify unabhängig von den Windows-Medientasten steuern, Mikrofone stummschalten und die Audioausgabe wechseln.
- Profile erstellen, Vorlagen für Musik, Produktivität oder kreative Arbeit nutzen und per Taste wechseln.
- Kurzen und langen Tastendruck konfigurieren, Tastenfarben wählen und Tastenkombinationen aufzeichnen.
- Drag-and-drop, Kopieren/Ausschneiden/Einfügen, Rückgängig/Wiederholen, Import/Export und automatische Sicherungen.

## Installation

Benötigt werden Windows 10 ab Version 2004 oder Windows 11, jeweils x64, und ein USB-Datenkabel. Die .NET-Laufzeit ist enthalten.

1. DeckPilot-Setup.exe über den Download-Link laden und installieren. Windows fordert Administratorrechte für Program Files an. Desktop-/Startmenü-Verknüpfungen und der Windows-Autostart werden standardmäßig angeboten.
2. Makropad anschließen und von links nach rechts kalibrieren: vier Tasten pro Reihe, drei Reihen. Jede Taste muss eine andere Taste von F13 bis F24 senden.

## Bedienung

Eine Taste auswählen, eine Aktion aus der Bibliothek zuweisen, Optionen festlegen und speichern. Bearbeiten und Testen sind auch ohne angeschlossenes Gerät möglich. Freie Tasten behalten F13–F24 für Spiele und andere Programme.

Beim Schließen bleibt DeckPilot im Infobereich aktiv. Über dessen Menü lässt sich das Programm vollständig beenden. Spotify benötigt die geöffnete Windows-App mit einer Wiedergabesitzung; YouTube wird nicht ersatzweise gesteuert.

## Updates und Firmware

Updates werden beim Start und alle sechs Stunden gesucht. Nach deiner Zustimmung wird das Update heruntergeladen und geprüft. Der Installer kann eine Windows-Berechtigung anfordern. Profile und Kalibrierung bleiben erhalten.

Für ein Makropad, das bereits F13–F24 sendet, sind kein zusätzlicher Treiber und kein Flashen nötig. Kompatible DeckPilot-Firmware ergänzt Geräteerkennung und Diagnose. Vor dem Flashen die Hardware-Anleitung lesen.

## Dokumentation und Lizenz

Diese Seite übersetzt die Benutzeranleitung. Die App-Oberfläche ist derzeit Französisch; technische Referenz und Versionshistorie sind Englisch.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

MIT-Lizenz: Nutzung, Kopieren, Änderung und Weitergabe sind unter Beibehaltung des Copyright- und Lizenzhinweises erlaubt.

[MIT LICENSE](../../LICENSE)

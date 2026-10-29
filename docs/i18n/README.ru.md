<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">Приложение Windows для вашего макропада с 12 клавишами.</p>

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

DeckPilot настраивает F13–F24 на макропадах, совместимых с Arduino Leonardo: 3 ряда по 4 клавиши. Пошаговая калибровка определяет положения даже при произвольном порядке подключения.

## Возможности

- Запуск установленных приложений, открытие папок и сайтов, отправка сочетаний клавиш.
- Управление Spotify отдельно от мультимедийных команд Windows, отключение микрофонов и переключение аудиовыхода.
- Профили и шаблоны для музыки, работы и творчества; переключение профиля клавишей.
- Короткое и долгое нажатие, цвета клавиш и запись сочетаний.
- Перетаскивание, копирование/вырезание/вставка, отмена/повтор, импорт/экспорт и автоматические резервные копии.

## Установка

Нужны Windows 10 версии 2004 или новее либо Windows 11, x64, и USB-кабель с передачей данных. Среда .NET включена.

1. Скачайте DeckPilot-Setup.exe по ссылке выше и установите. Windows запросит права администратора для Program Files. По умолчанию предлагаются ярлыки на рабочем столе/в меню «Пуск» и запуск с Windows.
2. Подключите макропад и калибруйте слева направо: четыре клавиши в каждом из трёх рядов. Каждая должна отправлять отдельную F13–F24.

## Использование

Выберите клавишу, действие в библиотеке, задайте параметры и сохраните. Редактировать и проверять действия можно без подключённой платы. Свободные клавиши сохраняют F13–F24 для игр и других приложений.

После закрытия окна DeckPilot остаётся в области уведомлений. Для полного выхода используйте меню значка. Spotify требует открытого приложения Windows с сеансом воспроизведения; переключения на YouTube нет.

## Обновления и прошивка

Обновления проверяются при запуске и каждые шесть часов. После согласия они загружаются и проверяются. Установщик может запросить разрешение Windows. Профили и калибровка сохраняются.

Дополнительный драйвер и перепрошивка не нужны, если макропад уже отправляет F13–F24. Совместимая прошивка DeckPilot добавляет идентификацию и диагностику; перед прошивкой прочитайте руководство по оборудованию.

## Документация и лицензия

Эта страница переводит руководство пользователя. Интерфейс приложения сейчас на французском; подробная техническая документация и история версий — на английском.

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

Лицензия MIT разрешает использование, копирование, изменение и распространение с сохранением уведомления об авторских правах и лицензии.

[MIT LICENSE](../../LICENSE)

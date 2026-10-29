<p align="center"><img src="../../deckpilotlogo.png" alt="DeckPilot" width="96"></p>

<h1 align="center">DeckPilot</h1>

<p align="center">12キーのマクロパッド向けWindowsアプリ。</p>

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

DeckPilotはArduino Leonardo互換マクロパッドのF13〜F24を設定します。配置は3行×4列です。配線の順番が異なっても、ガイド付きの位置合わせで各キーを識別できます。

## 機能

- インストール済みアプリ、フォルダー、ウェブサイトを開き、キーボードショートカットを送信。
- Windowsのメディア操作とは別にSpotifyを操作し、マイクのミュートや音声出力の切り替えに対応。
- 音楽・作業・制作のテンプレートからプロファイルを作成し、キーで切り替え。
- 短押し・長押し、キーの色、ショートカットの記録を設定。
- ドラッグ＆ドロップ、コピー・切り取り・貼り付け、元に戻す・やり直す、インポート・エクスポート、自動バックアップ。

## インストール

Windows 10 バージョン2004以降、またはWindows 11のx64環境と、データ通信対応USBケーブルが必要です。.NETランタイムは同梱されています。

1. 上のリンクからDeckPilot-Setup.exeをダウンロードして実行します。Program Filesへのインストールには管理者の許可が必要です。デスクトップ・スタートメニューのショートカットとWindows起動時の自動実行が初期選択されています。
2. マクロパッドを接続し、左から右へ、各行4キー・全3行の順で位置合わせします。各キーは異なるF13〜F24を送信する必要があります。

## 使い方

キーを選び、ライブラリからアクションを選択して設定を保存します。ボードを接続していなくても編集・テストできます。未割り当てのキーはゲームなどでF13〜F24として使えます。

ウィンドウを閉じても通知領域で動作します。完全に終了するにはアイコンのメニューを使います。Spotifyの操作には、Windows版アプリを開いて再生セッションを開始してください。代わりにYouTubeを操作することはありません。

## 更新とファームウェア

起動時と6時間ごとに更新を確認します。承認後にダウンロードして検証します。インストーラーがWindowsの許可を求める場合があります。プロファイルと位置合わせは保持されます。

すでにF13〜F24を送信するマクロパッドなら、追加ドライバーや書き換えは不要です。互換DeckPilotファームウェアは識別・診断機能を追加します。書き込む前にハードウェアガイドを確認してください。

## ドキュメントとライセンス

このページはユーザーガイドの翻訳です。アプリの表示言語は現在フランス語です。詳細な技術資料と変更履歴は英語です。

[English technical reference](../../README.md#development) · [Firmware & hardware](../../firmware/README.md) · [Release notes](https://github.com/lolo34dr/deckpilot/releases)

MITライセンス。著作権表示とライセンス文を保持することで、使用・コピー・変更・再配布ができます。

[MIT LICENSE](../../LICENSE)

# Contributing

DeckPilot is maintained around a real DIY macropad and Windows setup. I can review a focused report faster when it says what device and version were used and how to reproduce the behavior.

## Report a bug or request a change

Use the issue forms for a [bug](https://github.com/lolo34dr/deckpilot/issues/new?template=bug_report.yml) or a [feature request](https://github.com/lolo34dr/deckpilot/issues/new?template=feature_request.yml). English and French are both fine. Before opening a new report, check the [existing issues](https://github.com/lolo34dr/deckpilot/issues).

For a macropad problem, include the DeckPilot version, Windows version, board model, firmware version, and a short reproduction. For soundboard problems, include the audio endpoint names and whether VB-CABLE is installed. Remove personal paths and other private details from logs or screenshots.

## Changes and commits

Keep a change focused on one user problem. Explain why it is needed, what you changed, and how you checked it. Do not mix generated translations or old release notes into a new release's notes.

Commit subjects in this repository are short, imperative English sentences, such as `Fit macropad controls on small screens`. Keep generated translation commits separate; GitHub Actions uses a bot identity for those updates.

# Full documentation translations

The workflow translates the entire root README, including development, limitations and historical sections, into all 12 configured languages. Code, link destinations, language navigation and exact French UI labels are retained. Stable heading IDs preserve links.

Every published release is also translated. Its original English body is preserved, full translations are saved under docs/releases/<tag>, and collapsible translations are added to its GitHub release page. Tags, binaries and assets are never modified. Concurrent edits are protected. Oversized bodies fail rather than being truncated.

Translations are automatic and labelled accordingly. English remains the reference; technical wording should be reviewed. The application UI stays French. LICENSE remains the original MIT legal text.

Triggers: README/tooling changes, release publication/edits, manual dispatch, and successful completion of the Windows release workflow. The latter covers releases created using GitHub Actions tokens. No additional secret or paid service is required. Generated documentation commits use [skip ci].

CTranslate2 and SentencePiece run official Argos English-to-target models locally on isolated runners. All 12 languages must finish before publishing. Changes to the source during a run cause publication to fail rather than publish stale text.

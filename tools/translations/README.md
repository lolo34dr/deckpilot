# French release-note translations

The English root README, French overview, user guide and firmware summary are maintained as project documentation. The workflow only translates release notes into French; English remains the canonical release text. Code, Markdown formatting, technical names and exact French interface labels are preserved where possible.

When the workflow refreshes release pages, it keeps the original English text and embeds the French translation when it fits. If a release body is too long, it links to the complete French note in the repository. Tags, binaries and assets are never modified. Concurrent edits are protected.

French release translations are automatic and marked as such. The README, user guides and firmware summary are maintained by hand. The application UI stays French. LICENSE remains the original MIT legal text.

Triggers: release-note or translation-tool changes, release publication/edits, manual dispatch, and successful completion of the Windows release workflow. The latter covers releases created using GitHub Actions tokens. No additional secret or paid service is required. Generated translation commits use [skip ci].

CTranslate2 and SentencePiece run the official Argos English-to-French model locally on an isolated runner. Changes to release notes during a run are checked before publication so stale text is not sent to GitHub.

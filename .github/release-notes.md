DeckPilot 1.7.0 reduces repeated work while editing profiles, finding applications and running in the background.

- Saving unchanged settings no longer replaces the file or creates backups. Actual disk contents are still checked, so external edits, deletions and failed saves are not hidden.
- Profile history keeps detached model snapshots. Unchanged edits and undo/redo avoid JSON round-trips; action editing uses value comparisons instead of repeated serialization.
- Application search and picker share a five-minute catalog cache and one discovery task. Discovery runs outside WPF; Refresh still forces a new scan. Each picker owns its filter and keeps current results usable during refresh.
- Replace the permanent 100 ms board UI poll with ordered connection notifications. Keyboard-hook callbacks queue visual work instead of rendering twelve keys synchronously.
- Audio status polls every two seconds while visible and every ten seconds when hidden/minimized, refreshing immediately on reopen and after audio commands. Overlapping refresh requests are coalesced without dropping the post-command update.
- Hidden windows stop visual feedback timers and skip cosmetic rendering waits. Frozen UI brushes are reused and launched application process wrappers are disposed.

Download **DeckPilot-Setup.exe** or update from the app. Profiles, calibration and startup preferences are preserved. No firmware flash is required.

Validation includes settings/history/cache regressions and Windows installation, update, picker, drag-and-drop, short/long action editing and minimize/restore checks. These are structural optimizations, not a measured claim of a particular CPU or RAM saving on every PC.

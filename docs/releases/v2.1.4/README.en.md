DeckPilot 2.1.4 starts the configured virtual microphone automatically with the app, including background startup after Windows sign-in.

- Automatic soundboard microphone startup is enabled by default for existing and new settings. It uses the selected physical microphone and VB-CABLE; no manual activation is needed after restarting the PC when DeckPilot starts with Windows.
- If devices are not ready at sign-in, or audio stops after a disconnect, DeckPilot retries every five seconds while automatic activation is enabled.
- An explicit stop disables and saves the automatic-start preference; enable it again from the Soundboard panel or the activation button. Audio clips are never played automatically.
- Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes settings migration/round-trip checks and a native Windows UI check for the persisted automatic-start preference, plus the existing audio and installer checks. Physical device startup/reconnection still depends on the owner's hardware.

DeckPilot 2.1.9 fixes speaker monitoring on devices whose Windows audio format is extensible, including some USB headsets, and adds a soundboard overlay notification.

- The speaker return keeps the selected output's exact WASAPI mix format, converts clip samples to its PCM or float encoding, and retries without event-driven playback when a driver rejects that mode.
- Starting a soundboard clip shows its name in the configurable on-screen overlay. The existing profile notification preference, placement and reduced-motion setting control both notification types.
- The return still contains only soundboard clips; the physical microphone stays out of the speaker mix and continues to the virtual microphone.

DeckPilot Atelier remains at 1.0.12 and firmware remains 1.1.1. No firmware flash is needed. Speaker compatibility must still be checked with the owner's actual Windows audio device.

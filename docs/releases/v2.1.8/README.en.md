DeckPilot 2.1.8 adds an optional speaker return for Soundboard clips.

- In **Soundboard · micro virtuel**, enable **Entendre aussi les sons du soundboard**, choose a physical speaker or headset output, and set its independent 0–100% volume. The return is off by default and saved across launches.
- Only soundboard clips are sent to this second output. The physical microphone continues to go to the virtual microphone, and is never mixed into the speaker return. Clips still go to VB-CABLE at their original volume.
- Changing the return volume does not restart the virtual microphone. A disconnected speaker output stops only the return; the virtual microphone keeps running. Refresh devices or choose another output to restore it.

DeckPilot Atelier remains at 1.0.12 and firmware remains 1.1.1. No firmware flash is needed. Actual speaker routing should be checked on the owner's Windows PC with its audio devices.

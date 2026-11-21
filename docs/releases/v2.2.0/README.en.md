DeckPilot 2.2.0 adds adjustable, local microphone noise suppression while keeping soundboard clips untouched.

- RNNoise filters the selected physical microphone before DeckPilot mixes in soundboard clips. Clips bypass the filter and remain clear in the virtual microphone.
- Choose any application that uses **CABLE Output** as its microphone—games and voice-chat applications alike receive the filtered voice and soundboard mix. Apps using the physical mic directly bypass DeckPilot's filter.
- Enable or disable filtering and set its 0–100% strength in **Soundboard · micro virtuel**. Audio processing stays on this PC; no firmware flash is required.
- The RNNoise native library is built for Windows x64 and embedded into DeckPilot and its installer. Its BSD-style third-party license is included in the source repository.

The 10 ms RNNoise frame size does not guarantee end-to-end latency. Windows, the audio devices and VB-CABLE also affect delay. Test microphone quality and latency with the intended setup.

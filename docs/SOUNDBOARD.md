# Soundboard and microphone

The soundboard is an optional part of DeckPilot. The macropad still works without it.

## Setup

DeckPilot mixes local MP3/WAV clips with a selected microphone and sends the mix to the **CABLE Input** endpoint. In a game or voice-chat app, choose **CABLE Output** as the microphone.

Setup checks for the signed VB-CABLE driver and installs it from the vendor's package if it is missing. This requires an internet connection and Windows administrator approval. DeckPilot verifies the package publisher; it does not bundle the driver.

## Clips

Add or replace a clip from **Audio**. DeckPilot copies it to `%APPDATA%\DeckPilot\Sounds`, so moving or deleting the original file does not break playback. To move an older clip into managed storage, choose its file again from the clip's **…** menu. Clips can be assigned to a macropad action.

Profile exports can include referenced clips. The package allows up to 32 MiB of audio; clips that do not fit are marked as unavailable instead of being replaced silently.

## Microphone filter and speaker return

**Filtrer le bruit du microphone** applies RNNoise locally to the physical microphone before DeckPilot mixes in soundboard clips. Clips bypass the filter. The filter does not record or upload audio. Its 10 ms processing frame is not an end-to-end latency guarantee; Windows and the audio devices also affect delay.

The optional speaker return plays soundboard clips through a selected headset or speaker as well as through the virtual microphone. It does not play the physical microphone through the speakers. Headphones reduce the chance that a microphone will pick up a clip acoustically.

The virtual microphone can start automatically when DeckPilot starts if the selected microphone and VB-CABLE are available. Stop it explicitly to disable automatic startup; it can be enabled again in **Audio**.

Audio is processed on the PC. No firmware update is required. The soundboard uses [NAudio under MIT](../third-party/NAudio-LICENSE.txt) and [RNNoise under its BSD-style license](../third-party/RNNoise/NOTICE.md).

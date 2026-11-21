DeckPilot 2.1.3 reduces soundboard microphone latency and removes the one-second conversion blocks from the live audio path.

- Use output-driven, small-block resampling for the microphone and sound clips; matching sample rates bypass resampling entirely.
- Request event-driven 20 ms capture and 40 ms playback buffers instead of the previous 100 ms settings. Actual latency depends on Windows, the audio devices and VB-CABLE.
- Bound microphone buffering to 100 ms and retain recent complete frames after a playback stall instead of queueing old speech. Short underruns add only the silence requested by the current read.
- Support mono, stereo, multichannel and Windows extensible PCM/float inputs without the Media Foundation one-second read-ahead.
- Keep Discord processing and routing unchanged. Atelier remains 1.0.11 and firmware remains 1.1.1.

Validation includes regression checks for resampling block sizes, channel conversion, extensible formats, underrun recovery, overflow freshness, concurrent capture/playback and clip completion, alongside the existing Windows installer and application tests. No hardware latency figure is claimed; verify the improvement with your microphone and audio driver.

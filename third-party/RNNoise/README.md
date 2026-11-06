DeckPilot vendor snapshot: RNNoise
==================================

This is the native RNNoise source from Yellow-Dog-Man/rnnoise commit
`f0b73e38c029660bc6e010b583b406f6470f8a1b`, built as a small Windows x64 DLL.
RNNoise is a recurrent-neural-network voice denoiser based on the Xiph.Org
project. Its BSD-style license and copyright notices are in `COPYING`;
`NOTICE.md` records the source revision and DeckPilot integration.

Build on Windows with Visual Studio C++ tools and CMake:

```powershell
./third-party/RNNoise/build.ps1
```

The script writes `artifacts/native/win-x64/rnnoise.dll`. The app embeds that
DLL and extracts the verified embedded bytes into DeckPilot's per-user native
cache when the filter is first used. No driver installation or microphone
network service is involved.

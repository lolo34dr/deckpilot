# RNNoise notice

The native noise suppression engine is a vendored snapshot of
[Yellow-Dog-Man/rnnoise](https://github.com/Yellow-Dog-Man/rnnoise) at commit
`f0b73e38c029660bc6e010b583b406f6470f8a1b` (2025-11-23), based on the
[Xiph.Org RNNoise](https://github.com/xiph/rnnoise) project. The native source
is under the BSD-style license in [`COPYING`](COPYING). The snapshot includes
the trained model data used by that project.

DeckPilot wraps RNNoise directly from C# and embeds the compiled Windows DLL in
the application. It does not install a kernel driver or send microphone audio
over the network.

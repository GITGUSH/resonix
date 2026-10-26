# 01 - Vision

> Draft for owner review. Rewrite any sentence that does not sound like you.

## What Resonix is

Resonix is a Windows desktop music player built for people who study music. It plays local audio files like any modern player, and adds practice tools on top: slowing a track down without changing its pitch, changing pitch without changing speed, looping a section, marking parts of a song and viewing the waveform.

## Who it is for

- Musicians learning songs by ear.
- Anyone who wants a fast, local, offline music library without accounts or subscriptions.

## Principles

- **Local first.** Playback of local files works offline. No account, no server, no telemetry required for the core features.
- **Study tools are the differentiator.** A plain player is the baseline (the MVP). The Studio mode is what makes Resonix worth building.
- **Open source.** Licensed under GPL-3.0.
- **Engineering quality is part of the product.** Layered architecture, tests, CI and documentation from the start, because the project is also a learning exercise.

## What Resonix is not

- Not a streaming service. Online sources are optional, future, and limited to what their providers legally allow.
- Not a DAW or audio editor. It does not record or export edited audio (not planned).
- Not cross-platform for now. Windows only (see ADR-0001).

## Success criteria

- The MVP plays a real music library reliably and can be installed by someone other than the author.
- Studio mode changes speed and pitch independently with acceptable sound quality (validated in the DSP spike before building the mode).

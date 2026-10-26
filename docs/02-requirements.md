# 02 - Requirements

Status key follows the development log: BACKLOG, READY, IN_PROGRESS, TESTING, DONE, BLOCKED.

## MVP (Phases 1 to 4)

| # | Requirement | Phase |
|---|---|---|
| 1 | Open local audio files | 1 |
| 2 | Play tracks | 1 |
| 3 | Pause and resume | 1 |
| 4 | Stop playback | 1 |
| 5 | Seek forward and backward | 1 |
| 6 | Change volume | 1 |
| 7 | Show playback progress | 1 |
| 8 | Import directories | 2 |
| 9 | Read metadata | 2 |
| 10 | Persist a library | 2 |
| 11 | Search tracks | 2 |
| 12 | Create playlists | 3 |
| 13 | Manage a play queue | 3 |
| 14 | Shuffle | 3 |
| 15 | Repeat modes | 3 |
| 16 | Save settings | 3 to 4 |
| 17 | Handle errors properly | all |

The phase column is the intended starting phase, not a commitment to the exact split. Revisit when each phase begins.

## After the MVP

| Feature | Phase |
|---|---|
| Validate DSP technology (time-stretch, pitch shift) in a spike | 5 |
| Studio mode: speed and pitch control, A/B loop, markers, waveform, equalizer, saved study settings | 6 |
| External providers: metadata lookup, playlist import, audio source resolution | 7 |
| Distribution: installer, release process | 8 |
| Instrument separation by AI | not scheduled |

## Non-functional requirements

- **Offline:** local playback and library work without network access.
- **No accounts:** no sign-in or external server for essential features.
- **Responsive UI:** imports, database work and audio processing must not block the interface.
- **Dark theme:** fixed dark theme. A light theme is not planned until there is a reason.
- **Resource handling:** audio resources are released correctly; no leaks across track changes.
- **No audio in the database:** only metadata and file paths are stored.
- **Provider independence:** the app must not depend on a single online provider.

## Constraints

- Windows only.
- .NET 10, Avalonia UI (see ADR-0001).
- GPL-3.0 licensing, which affects which libraries can be bundled.

## Open questions

- Audio engine for Studio mode (decided in ADR-0002, after the DSP spike).
- Library file identity: path only, or path plus size and date, or content hash. Decide in Phase 2.
- Accent-insensitive search strategy for Portuguese titles. Decide in Phase 2.

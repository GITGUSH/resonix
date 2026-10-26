# Development log

Newest session first. Each entry should let someone resume the project without the conversation history.

## Session 1 (2026-10-24 to 2026-10-26): Phase 0

Status: F0-01 to F0-07 done. F0-08 (base documentation) in progress.

### Task status

| ID | Task | Status |
|---|---|---|
| F0-01 | Install and validate tools | DONE |
| F0-02 | GitHub repository, LICENSE, README | DONE |
| F0-03 | Solution and six projects | DONE |
| F0-04 | Central build config, package versions, SDK pin | DONE |
| F0-05 | Empty Avalonia window, dark theme | DONE |
| F0-06 | Composition root and Serilog | DONE |
| F0-07 | CI in GitHub Actions | DONE |
| F0-08 | Base documentation | IN_PROGRESS |
| F0-09 | DSP spike (time-boxed, disposable) | BACKLOG |
| F0-10 | ADR-0002: audio engine | BACKLOG |

### F0-07 details

- Workflow: `.github/workflows/ci.yml`, job `build-and-test`, `windows-latest`, Release, timeout 15 minutes, read-only token permissions.
- Verified red and green: a deliberately failing test (`Assert.Fail`) turned the check red in a throwaway PR, which was closed without merging.
- `main` is protected: pull request and passing check required.
- CI job took about 1m15s. Most of that is installing the SDK (31s) and restoring packages (17s).
- The runner installed SDK 10.0.403 because `latestPatch` picks the newest patch in the same feature band. The local SDK was updated to 10.0.403 to match.
- Actions versions in use: `actions/checkout@v7`, `actions/setup-dotnet@v6`.

### Problems found this session

- Compiler error CS0118: `Resonix.Application` namespace shadows `Avalonia.Application`. Fixed by writing `Avalonia.Application`.
- Analyzer errors after enabling warnings as errors: CA1852 (seal `Program`), CA1305 (culture in logging sinks), CA1848 and CA1873 (logger usage). All fixed in code.
- Six empty files named `src/Resonix.*` and `tests/Resonix.*` were committed in the first commit by mistake. Removed when real project folders were created.
- Git pager (`less`) swallowed later commands and a stray file named `tatus --short` was created. Fixed with `git config --global pager.diff false`. The stray file was deleted.

### Decisions

See ADR-0001.

### Pending

- Delete five merged remote branches (`feat/f0-03...` to `feat/f0-07...`) and enable automatic deletion of head branches in repository settings.
- Review and merge the F0-08 documentation pull request.

### Next task

F0-09: DSP spike. Compare LibVLCSharp against NAudio plus SoundTouch (and other candidates such as Rubber Band, since GPL-3.0 allows it) on sound quality, latency, CPU, .NET compatibility, seek precision, real-time processing, licensing and distribution. Also verify that LibVLCSharp.Avalonia supports the Avalonia version in use (12.1.4) before counting it as a candidate.

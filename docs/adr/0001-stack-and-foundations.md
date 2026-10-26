# ADR-0001: Stack and foundations

- Status: Accepted
- Date: 2026-10-26

## Context

Resonix is a long-lived desktop application built by one developer who also wants to learn from the project. The foundations below were fixed during Phase 0, before any feature code. The audio engine is deliberately not part of this ADR: it depends on a DSP spike (see ADR-0002, planned).

## Decision

| Area | Decision | Reason |
|---|---|---|
| Name | Resonix (earlier working name: MusicBox) | Chosen by the owner before the first project was created, so no rename cost. |
| Platform | Windows only | Single target keeps audio and distribution simple. Avalonia stays as the UI toolkit, but cross-platform is not a goal. |
| License | GPL-3.0 | Open source project. Leaves the choice of DSP libraries open, including GPL ones. Accepts that some companies avoid reusing GPL code. |
| Runtime | .NET 10 | LTS runtime. |
| UI | Avalonia 12.x, MVVM with CommunityToolkit.Mvvm | Planned in the original brief. Toolkit is not installed yet. |
| Test framework | xUnit v3 on Microsoft.Testing.Platform | New projects should not start on v2. |
| Dependency injection | `Microsoft.Extensions.DependencyInjection`, plain `ServiceCollection`, no Generic Host | The Host brings its own lifecycle that competes with Avalonia's, and many dependencies we do not use. Cheap to migrate later because inner layers only see interfaces. |
| Logging | Serilog in Desktop only; `ILogger<T>` everywhere else | Inner layers do not depend on a logging library. |
| Local data | `%LocalAppData%\Resonix` for logs and settings | `%AppData%` roams on corporate networks, and our data contains machine-specific paths. |
| Settings format | JSON file | Simple and editable. SQLite is reserved for relational data. |
| Build rules | Warnings as errors, latest-recommended analysis, Central Package Management | Turning on strict rules later, with hundreds of accumulated warnings, is much more expensive. |
| SDK | Pinned to 10.0.402 with `rollForward: latestPatch` | Security patches are picked up automatically. Local SDK is kept at the same patch as CI. |
| CI | GitHub Actions on `windows-latest`, Release configuration | Only Windows matters, and Release exercises the code path users will run. |
| Workflow | Pull requests into protected `main`, Conventional Commits | CI becomes a real gate. |

## Consequences

- Strict analyzers fail the build on style and performance rules (for example CA1305, CA1848, CA1873). We fix the code, we do not suppress the rule. If a rule creates real friction, we decide with a new ADR.
- A newly published SDK patch or NuGet vulnerability advisory can turn CI red without any code change. That is intended: we learn about it.
- Choosing GPL-3.0 means proprietary relicensing of the whole project would require agreement from all contributors.
- Windows-only means no Linux or macOS CI. If that changes, this ADR is superseded.
- Per-layer registration extensions, the MVVM toolkit and persistence are intentionally not set up yet (YAGNI).

# 03 - Architecture

Describes what exists in the repository today. Planned parts are marked as such.

## Solution layout

```
src/
  Resonix.Domain
  Resonix.Application
  Resonix.Infrastructure
  Resonix.Desktop
tests/
  Resonix.UnitTests
  Resonix.IntegrationTests
Resonix.slnx
```

Domain, Application and Infrastructure are empty class libraries at this point. Desktop contains the Avalonia shell, the composition root and logging setup.

## Layers

| Project | Responsibility |
|---|---|
| Domain | Entities, business rules, enums, value objects. No dependency on UI or persistence frameworks. |
| Application | Use cases, application services and contracts. Coordinates work without depending on concrete implementations. |
| Infrastructure | Persistence, audio playback, metadata reading, external integrations. |
| Desktop | Views, view models, styles, and the composition root. |

## Project references

```
Domain           -> (none)
Application      -> Domain
Infrastructure   -> Application, Domain
Desktop          -> Application, Infrastructure
UnitTests        -> Domain, Application, Desktop
IntegrationTests -> Infrastructure, Application, Domain
```

Desktop references Infrastructure only to wire the dependency container. If that reference starts to leak into view models, extract a separate host project.

## Rules

- References only point inward as shown above. The compiler enforces this.
- Create an interface only when there is a concrete need: decoupling, testing, or swapping an implementation. No interface per class.
- Dependencies arrive through constructors. Calling the container (`GetService`, `GetRequiredService`) outside the composition root is the service locator anti-pattern and is not allowed.
- No business rules in code-behind.
- Only Desktop knows Serilog. Other layers log through `ILogger<T>` from `Microsoft.Extensions.Logging`.

## Composition root

- `Program.cs` configures the static Serilog logger first, then starts Avalonia inside `try/catch/finally`. A startup crash is logged as fatal, and `Log.CloseAndFlush()` runs on exit.
- `CompositionRoot.cs` builds a `ServiceCollection` (no Generic Host) with `ValidateScopes` and `ValidateOnBuild`, so registration errors fail at startup.
- `App.axaml.cs` resolves `MainWindow` from the container and disposes the provider on exit. This is the only place that resolves services by hand.
- Per-layer registration extensions (`AddApplication`, `AddInfrastructure`) are not created yet. They arrive with the first real service.

## Logging

- Logs go to `%LocalAppData%\Resonix\logs`, one file per day, 14 days kept.
- Minimum level is Information (Debug in Debug builds). `Microsoft` and `Avalonia` namespaces are limited to Warning.
- Log formatting uses the invariant culture, so log files do not depend on the user's Windows locale.
- Code analyzer rule CA1848 is on: use `[LoggerMessage]` source-generated methods instead of calling `LogInformation` directly.

## Build conventions

- `Directory.Build.props`: nullable and implicit usings on, warnings as errors, `AnalysisLevel` set to latest-recommended.
- `Directory.Packages.props`: Central Package Management. Package versions live only here. Avalonia packages share one `AvaloniaVersion` property.
- `global.json`: SDK pinned to 10.0.402 with `rollForward: latestPatch`, and the test runner set to Microsoft.Testing.Platform (needed by xUnit v3).
- `TargetFramework` stays in each project file, because the audio engine may require a Windows-specific target in some projects.
- `AvaloniaUI.DiagnosticsSupport` is only included in Debug builds.

## Known pitfall

The namespace `Resonix.Application` shadows `Avalonia.Application` inside `namespace Resonix.Desktop`. The base class of `App` and any use of `Application.Current` must be written as `Avalonia.Application`.

## Planned, not decided

- Audio engine abstraction and implementation (ADR-0002, after the DSP spike).
- Persistence model and migrations (Phase 2).
- Settings file format: JSON in `%LocalAppData%\Resonix` (decided, not implemented).

## Continuous integration

GitHub Actions workflow `CI` runs on every push to `main` and on pull requests to `main`: restore, build and test in Release on `windows-latest`, with the SDK taken from `global.json`. The `main` branch requires a pull request and a passing check.

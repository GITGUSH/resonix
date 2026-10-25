using Avalonia;
using Serilog;
using System;

namespace Resonix.Desktop;

internal sealed class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        Log.Logger = LoggingSetup.CreateLogger();

        try
        {
            Log.Information("Resonix iniciando");
            var exitCode = BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            Log.Information("Resonix encerrado (código {ExitCode})", exitCode);
            return exitCode;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Falha fatal no Resonix");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}
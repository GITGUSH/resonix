using System;
using System.IO;
using Serilog;
using Serilog.Events;
using System.Globalization;

namespace Resonix.Desktop;

internal static class LoggingSetup
{
    public static string LogDirectory { get; } = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Resonix",
        "logs");

    public static ILogger CreateLogger()
    {
        var configuration = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Avalonia", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(
                path: Path.Combine(LogDirectory, "resonix-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                formatProvider: CultureInfo.InvariantCulture,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}");

#if DEBUG
        configuration.MinimumLevel.Debug().WriteTo.Debug(formatProvider: CultureInfo.InvariantCulture);
#else
        configuration.MinimumLevel.Information();
#endif

        return configuration.CreateLogger();
    }
}
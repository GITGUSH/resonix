using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Resonix.Desktop;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = CompositionRoot.Build();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = services.GetRequiredService<MainWindow>();
            desktop.Exit += (_, _) => services.Dispose();

           var logger = services.GetRequiredService<ILogger<App>>();
            LogMainWindowCreated(logger);
        }

        base.OnFrameworkInitializationCompleted();
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Janela principal criada pelo container")]
    private static partial void LogMainWindowCreated(ILogger logger);
}
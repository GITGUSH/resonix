using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Resonix.Desktop;

internal static class CompositionRoot
{
    public static ServiceProvider Build()
    {
        var services = new ServiceCollection();

        services.AddLogging(builder => builder.AddSerilog());

        services.AddTransient<MainWindow>();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        });
    }
}
using System.Runtime.Versioning;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Browser;
using Avalonia.Logging;
using AvaloniaUI.Ribbon.Demo;

internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
            .WithInterFont()
            .LogToTrace(LogEventLevel.Debug)
            .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}

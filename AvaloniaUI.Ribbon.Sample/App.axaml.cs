using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;

using AvaloniaUI.Ribbon.Samples.Views;

namespace AvaloniaUI.Ribbon.Samples
{
    public class App : Application
    {
        public static IThemeManager? ThemeManager;

        internal static IClassicDesktopStyleApplicationLifetime Lifetime => Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

        public override void Initialize()
        {
            ThemeManager = new FluentThemeManager();
            ThemeManager.Initialize(this);
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                Lifetime.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}

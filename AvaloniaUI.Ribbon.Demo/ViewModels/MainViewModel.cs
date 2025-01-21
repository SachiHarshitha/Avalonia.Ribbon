using System;
using Avalonia.Controls;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaUI.Ribbon.Demo.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string _help = "Help requested!";

    [ObservableProperty] private string _lastActionText = "none";
    
    [ObservableProperty] private Orientation _ribbonOrientation = Orientation.Horizontal;

    [ObservableProperty] private SystemDecorations _selectedDecoration;

    [ObservableProperty] private bool _showContextualGroup1 = true;

    [ObservableProperty] private bool _showContextualGroup2;

    [ObservableProperty] private bool _showContextualGroup3;

    [ObservableProperty] private bool _switchOrientation = true;

    [ObservableProperty] private bool _switchTheme = true;
    public string Greeting => "Welcome to Avalonia!";

    public MainViewModel()
    {
            
    }
    public void HelpCommand(object parameter)
    {
        Console.WriteLine(Help);
        LastActionText = Help;
    }

    public void OnClickCommand(object parameter)
    {
        var paramString = "[NO CONTENT]";

        if (parameter != null)
        {
            if (parameter is string str)
                paramString = str;
            else
                paramString = parameter.ToString();
        }

        Console.WriteLine("OnClickCommand invoked: " + paramString);
        LastActionText = paramString;
    }

    /// <summary>
    ///     Called when [switch theme changed].
    /// </summary>
    /// <param name="value">if set to <c>true</c> [value].</param>
    partial void OnSwitchThemeChanged(bool value)
    {
        switch (value)
        {
            case true:
                App.ThemeManager.Switch(0);
                break;

            case false:
                App.ThemeManager.Switch(1);
                break;
        }
    }

    /// <summary>
    ///     Handle Orientation Change event
    /// </summary>
    /// <param name="value"></param>
    partial void OnSwitchOrientationChanged(bool value)
    {
        if (value)
        {
            RibbonOrientation = Orientation.Horizontal;
        }
        else
        {
            RibbonOrientation = Orientation.Vertical;
        }
    }
}
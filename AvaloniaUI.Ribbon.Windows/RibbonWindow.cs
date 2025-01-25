using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.VisualTree;

namespace AvaloniaUI.Ribbon.Windows;

[TemplatePart("PART_TitleBar", typeof(Control))]
public class RibbonWindow : Window
{
    public static readonly StyledProperty<bool> LeftSideCaptionButtonsProperty =
        AvaloniaProperty.Register<RibbonWindow, bool>(nameof(LeftSideCaptionButtons), UseLeftSideCaptionButtons());

    public static readonly StyledProperty<Orientation> OrientationProperty =
        StackPanel.OrientationProperty.AddOwner<RibbonWindow>();

    public static readonly StyledProperty<QuickAccessToolbar> QuickAccessToolbarProperty =
        DesktopRibbon.QuickAccessToolbarProperty.AddOwner<RibbonWindow>();

    public static readonly StyledProperty<DesktopRibbon> RibbonProperty =
        AvaloniaProperty.Register<RibbonWindow, DesktopRibbon>("Ribbon");

    public static readonly StyledProperty<IBrush> TitleBarBackgroundProperty =
        AvaloniaProperty.Register<RibbonWindow, IBrush>(nameof(TitleBarBackground));

    public static readonly StyledProperty<IBrush> TitleBarForegroundProperty =
        AvaloniaProperty.Register<RibbonWindow, IBrush>(nameof(TitleBarForeground));

    private bool _titlebarSecondClick;

    static RibbonWindow()
    {
        OrientationProperty.OverrideDefaultValue<RibbonWindow>(Orientation.Horizontal);

        RibbonProperty.Changed.AddClassHandler<RibbonWindow>(
            (sender, e) => sender.RefreshRibbon(e.OldValue, e.NewValue));
        QuickAccessToolbarProperty.Changed.AddClassHandler<RibbonWindow>((sender, e) =>
            sender.RefreshQat(e.OldValue, e.NewValue));
        SystemDecorationsProperty.Changed.AddClassHandler<RibbonWindow>((sender, arg) =>
        {
            if (arg.NewValue is SystemDecorations systemDecorations)
                switch (systemDecorations)
                {
                    case SystemDecorations.Full:
                        sender.ExtendClientAreaToDecorationsHint = false;
                        break;

                    case SystemDecorations.None:
                        sender.ExtendClientAreaToDecorationsHint = true;
                        break;
                }
        });
    }

    public RibbonWindow()
    {
        ExtendClientAreaTitleBarHeightHint = 35;
        ExtendClientAreaToDecorationsHint = true;
        ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.SystemChrome;
        TransparencyLevelHint = new List<WindowTransparencyLevel> { WindowTransparencyLevel.AcrylicBlur };
        this.GetObservable(WindowStateProperty)
            .Subscribe(x =>
            {
                PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                PseudoClasses.Set(":fullscreen", x == WindowState.FullScreen);
            });

        this.GetObservable(IsExtendedIntoWindowDecorationsProperty)
            .Subscribe(x =>
            {
                if (!x)
                {
                    SystemDecorations = SystemDecorations.Full;
                    TransparencyLevelHint = new List<WindowTransparencyLevel> { WindowTransparencyLevel.Blur };
                }
            });
        RefreshRibbon(null, Ribbon);
        RefreshQat(null, QuickAccessToolbar);
    }

    public bool LeftSideCaptionButtons
    {
        get => GetValue(LeftSideCaptionButtonsProperty);
        set => SetValue(LeftSideCaptionButtonsProperty, value);
    }

    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }


    public QuickAccessToolbar QuickAccessToolbar
    {
        get => GetValue(QuickAccessToolbarProperty);
        set => SetValue(QuickAccessToolbarProperty, value);
    }

    public DesktopRibbon Ribbon
    {
        get => GetValue(RibbonProperty);
        set => SetValue(RibbonProperty, value);
    }


    public IBrush TitleBarBackground
    {
        get => GetValue(TitleBarBackgroundProperty);
        set => SetValue(TitleBarBackgroundProperty, value);
    }

    public IBrush TitleBarForeground
    {
        get => GetValue(TitleBarForegroundProperty);
        set => SetValue(TitleBarForegroundProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(RibbonWindow);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var window = this;
        ExtendClientAreaChromeHints =
            ExtendClientAreaChromeHints.PreferSystemChrome |
            ExtendClientAreaChromeHints.OSXThickTitleBar;
        try
        {
            var titleBar = GetControl<Control>(e, "PART_TitleBar");
            titleBar.PointerPressed += (sender, ep) =>
            {
                if (_titlebarSecondClick)
                    window.WindowState = WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                else
                    window.BeginMoveDrag(ep);

                if (!_titlebarSecondClick)
                {
                    _titlebarSecondClick = true;

                    var secondClickTimer = new Timer(250);
                    secondClickTimer.Elapsed += (_, _) =>
                    {
                        _titlebarSecondClick = false;
                        secondClickTimer.Stop();
                    };
                    secondClickTimer.Start();
                }
            };

            /*try
            {
                SetupSide("Left_top", StandardCursorType.LeftSide, WindowEdge.West, ref e);
                SetupSide("Left_mid", StandardCursorType.LeftSide, WindowEdge.West, ref e);
                SetupSide("Left_bottom", StandardCursorType.LeftSide, WindowEdge.West, ref e);
                SetupSide("Right_top", StandardCursorType.RightSide, WindowEdge.East, ref e);
                SetupSide("Right_mid", StandardCursorType.RightSide, WindowEdge.East, ref e);
                SetupSide("Right_bottom", StandardCursorType.RightSide, WindowEdge.East, ref e);
                SetupSide("Top", StandardCursorType.TopSide, WindowEdge.North, ref e);
                SetupSide("Bottom", StandardCursorType.BottomSide, WindowEdge.South, ref e);
                SetupSide("TopLeft", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest, ref e);
                SetupSide("TopRight", StandardCursorType.TopRightCorner, WindowEdge.NorthEast, ref e);
                SetupSide("BottomLeft", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest, ref e);
                SetupSide("BottomRight", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast, ref e);
            }
            catch { }

            GetControl<Button>(e, "PART_MinimizeButton").Click += delegate
            {
                window.WindowState = WindowState.Minimized;
            };
            GetControl<Button>(e, "PART_MaximizeButton").Click += delegate
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            };
            GetControl<Button>(e, "PART_CloseButton").Click += delegate
            {
                window.Close();
            };*/
        }
        catch (KeyNotFoundException)
        {
        }
    }

    private static bool UseLeftSideCaptionButtons()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return true;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //TODO: See if there's any sane way of getting  the user's Window manager/decorator/etc and its configuration, and deciding or guessing based on that
            return false;

        //on Windows
        return false;
    }

    private T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : class
    {
        return e.NameScope.Get<T>(name);
    }

    private void RefreshQat(object oldValue, object newValue)
    {
        if (oldValue != null && oldValue is QuickAccessToolbar oldQat)
            oldQat.Ribbon = null;

        if (newValue != null && newValue is QuickAccessToolbar newQat)
        {
            newQat.Ribbon = Ribbon;

            if (Ribbon != null)
                Ribbon.QuickAccessToolbar = newQat;
        }
        else if (Ribbon != null)
        {
            Ribbon.QuickAccessToolbar = null;
        }
    }

    private void RefreshRibbon(object oldValue, object newValue)
    {
        if (oldValue != null && oldValue is DesktopRibbon oldRibbon)
        {
            oldRibbon.QuickAccessToolbar = null;
            oldRibbon.ClearValue(DesktopRibbon.OrientationProperty);
        }

        if (newValue != null && newValue is DesktopRibbon newRibbon)
        {
            newRibbon.QuickAccessToolbar = QuickAccessToolbar;
            newRibbon[!DesktopRibbon.OrientationProperty] = this[!OrientationProperty];

            if (QuickAccessToolbar != null)
                QuickAccessToolbar.Ribbon = newRibbon;
        }
        else if (QuickAccessToolbar != null)
        {
            QuickAccessToolbar.Ribbon = null;
        }
    }

    private void SetupSide(string name, StandardCursorType cursor, WindowEdge edge, ref TemplateAppliedEventArgs e)
    {
        var control = e.NameScope.Get<Control>(name);
        control.Cursor = new Cursor(cursor);
        control.PointerPressed += (_, ep) =>
        {
            if (this.GetVisualRoot() is Window window)
                window.BeginResizeDrag(edge, ep);
        };
    }
}
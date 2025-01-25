using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml.Templates;
using AvaloniaUI.Ribbon.Contracts;
using AvaloniaUI.Ribbon.Helpers;
using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon;

public class RibbonButton : Button, IRibbonControl, ICanAddToQuickAccess
{
    public static readonly StyledProperty<bool> CanAddToQuickAccessProperty =
        AvaloniaProperty.Register<RibbonButton, bool>(nameof(CanAddToQuickAccess), true);

    public static readonly StyledProperty<IControlTemplate> IconProperty =
        AvaloniaProperty.Register<RibbonButton, IControlTemplate>(nameof(Icon));

    public static readonly StyledProperty<IControlTemplate> LargeIconProperty =
        AvaloniaProperty.Register<RibbonButton, IControlTemplate>(nameof(LargeIcon));

    public static readonly AvaloniaProperty<RibbonControlSize> MaxSizeProperty;
    public static readonly AvaloniaProperty<RibbonControlSize> MinSizeProperty;

    public static readonly StyledProperty<IControlTemplate> QuickAccessIconProperty =
        AvaloniaProperty.Register<RibbonButton, IControlTemplate>(nameof(QuickAccessIcon));

    public static readonly StyledProperty<IControlTemplate> QuickAccessTemplateProperty =
        AvaloniaProperty.Register<RibbonButton, IControlTemplate>(nameof(QuickAccessTemplate));

    public static readonly AvaloniaProperty<RibbonControlSize> SizeProperty;

    static RibbonButton()
    {
        RibbonControlHelper<RibbonButton>.SetProperties(out SizeProperty, out MinSizeProperty, out MaxSizeProperty);
        FocusableProperty.OverrideDefaultValue<RibbonButton>(false);
    }

    public IControlTemplate Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public IControlTemplate LargeIcon
    {
        get => GetValue(LargeIconProperty);
        set => SetValue(LargeIconProperty, value);
    }

    public IControlTemplate QuickAccessIcon
    {
        get => GetValue(QuickAccessIconProperty);
        set => SetValue(QuickAccessIconProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(RibbonButton);

    public bool CanAddToQuickAccess
    {
        get => GetValue(CanAddToQuickAccessProperty);
        set => SetValue(CanAddToQuickAccessProperty, value);
    }

    public IControlTemplate QuickAccessTemplate
    {
        get
        {
            var value = GetValue(QuickAccessTemplateProperty);
            var controlTemplate = value as ControlTemplate;
            return value;
        }
        set => SetValue(QuickAccessTemplateProperty, value);
    }

    public RibbonControlSize MaxSize
    {
        get => (RibbonControlSize)GetValue(MaxSizeProperty);
        set => SetValue(MaxSizeProperty, value);
    }

    public RibbonControlSize MinSize
    {
        get => (RibbonControlSize)GetValue(MinSizeProperty);
        set => SetValue(MinSizeProperty, value);
    }

    public RibbonControlSize Size
    {
        get => (RibbonControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }
}
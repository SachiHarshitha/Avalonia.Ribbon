using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using AvaloniaUI.Ribbon.Contracts;
using AvaloniaUI.Ribbon.Helpers;
using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon;

public class SplitButtonControl : SplitButton, IRibbonControl, ICanAddToQuickAccess
{
    static SplitButtonControl()
    {
        RibbonControlHelper<SplitButtonControl>.SetProperties(out SizeProperty, out MinSizeProperty,
            out MaxSizeProperty);
    }

    public IControlTemplate Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsDropDownOpen
    {
        get => GetValue(IsDropDownOpenProperty);
        set => SetValue(IsDropDownOpenProperty, value);
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

    public bool CanAddToQuickAccess
    {
        get => GetValue(CanAddToQuickAccessProperty);
        set => SetValue(CanAddToQuickAccessProperty, value);
    }

    public IControlTemplate QuickAccessTemplate
    {
        get => GetValue(QuickAccessTemplateProperty);
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
        set
        {
            SetValue(SizeProperty, value);

            switch (value)
            {
                case RibbonControlSize.Large:
                    //TODO: Figure out a way to optimize this.
                    Application.Current.Resources.TryGetResource("LargeSplitButton", null, out var theme);
                    if (theme as ControlTheme != null)
                        Theme = theme as ControlTheme;
                    break;

                case RibbonControlSize.Small:
                    break;

                case RibbonControlSize.Medium:
                    break;
            }
        }
    }

    //protected override Type StyleKeyOverride => typeof(SplitButton);

    #region Static Properties

    public static readonly StyledProperty<bool> CanAddToQuickAccessProperty =
        RibbonButton.CanAddToQuickAccessProperty.AddOwner<SplitButton>();

    public static readonly StyledProperty<IControlTemplate> IconProperty =
        RibbonButton.IconProperty.AddOwner<SplitButton>();

    public static readonly StyledProperty<bool> IsDropDownOpenProperty =
        ComboBox.IsDropDownOpenProperty.AddOwner<SplitButton>();

    public static readonly StyledProperty<IControlTemplate> LargeIconProperty =
        RibbonButton.LargeIconProperty.AddOwner<SplitButton>();

    public static readonly AvaloniaProperty<RibbonControlSize> MaxSizeProperty;
    public static readonly AvaloniaProperty<RibbonControlSize> MinSizeProperty;

    public static readonly StyledProperty<IControlTemplate> QuickAccessIconProperty =
        RibbonButton.QuickAccessIconProperty.AddOwner<RibbonToggleButton>();

    public static readonly StyledProperty<IControlTemplate> QuickAccessTemplateProperty =
        RibbonButton.QuickAccessTemplateProperty.AddOwner<SplitButton>();

    public static readonly AvaloniaProperty<RibbonControlSize> SizeProperty;

    #endregion Static Properties
}
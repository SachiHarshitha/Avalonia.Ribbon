using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using AvaloniaUI.Ribbon.Contracts;
using AvaloniaUI.Ribbon.Helpers;
using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon;

public class RibbonDropDownButton : ItemsControl, IRibbonControl, ICanAddToQuickAccess
{
    static RibbonDropDownButton()
    {
        RibbonControlHelper<RibbonDropDownButton>.SetProperties(out SizeProperty, out MinSizeProperty,
            out MaxSizeProperty);
    }

    #region Static Properties

    public static readonly StyledProperty<bool> CanAddToQuickAccessProperty =
        RibbonButton.CanAddToQuickAccessProperty.AddOwner<RibbonDropDownButton>();

    public static readonly StyledProperty<object> ContentProperty =
        ContentControl.ContentProperty.AddOwner<RibbonDropDownButton>();

    public static readonly StyledProperty<IControlTemplate> IconProperty =
        RibbonButton.IconProperty.AddOwner<RibbonDropDownButton>();

    public static readonly StyledProperty<bool> IsDropDownOpenProperty =
        ComboBox.IsDropDownOpenProperty.AddOwner<RibbonDropDownButton>();

    public static readonly StyledProperty<IControlTemplate> LargeIconProperty =
        RibbonButton.LargeIconProperty.AddOwner<RibbonDropDownButton>();

    public static readonly AvaloniaProperty<RibbonControlSize> MaxSizeProperty;
    public static readonly AvaloniaProperty<RibbonControlSize> MinSizeProperty;

    public static readonly StyledProperty<IControlTemplate> QuickAccessIconProperty =
        RibbonButton.QuickAccessIconProperty.AddOwner<RibbonToggleButton>();

    public static readonly StyledProperty<IControlTemplate> QuickAccessTemplateProperty =
        RibbonButton.QuickAccessTemplateProperty.AddOwner<RibbonDropDownButton>();

    public static readonly AvaloniaProperty<RibbonControlSize> SizeProperty;

    #endregion Static Properties

    #region Properties

    public bool CanAddToQuickAccess
    {
        get => GetValue(CanAddToQuickAccessProperty);
        set => SetValue(CanAddToQuickAccessProperty, value);
    }

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
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

    public IControlTemplate QuickAccessIcon
    {
        get => GetValue(QuickAccessIconProperty);
        set => SetValue(QuickAccessIconProperty, value);
    }

    public IControlTemplate QuickAccessTemplate
    {
        get => GetValue(QuickAccessTemplateProperty);
        set => SetValue(QuickAccessTemplateProperty, value);
    }

    public RibbonControlSize Size
    {
        get => (RibbonControlSize)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    #endregion Properties

    //TODO:
    /*protected override IItemContainerGenerator CreateItemContainerGenerator()
    {
        return new ItemContainerGenerator<RibbonDropDownItemPresenter>(this, RibbonDropDownItemPresenter.ContentProperty, RibbonDropDownItemPresenter.ContentTemplateProperty);
    }*/
}
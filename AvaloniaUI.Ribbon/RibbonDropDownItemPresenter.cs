using System;
using Avalonia.Controls;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon;

public class RibbonDropDownItemPresenter : Button, IRibbonCommand
{
    /*public static readonly StyledProperty<IControlTemplate> IconProperty = RibbonControlItem.IconProperty.AddOwner<RibbonControlItemPresenter>();
    public IControlTemplate Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }*/

    protected override Type StyleKeyOverride => typeof(RibbonDropDownItemPresenter);
}
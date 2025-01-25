using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using AvaloniaUI.Ribbon.Contracts;
using VisualExtensions = Avalonia.VisualTree.VisualExtensions;

namespace AvaloniaUI.Ribbon.Windows;

public class QuickAccessItem : ContentControl
{
    public static readonly StyledProperty<ICanAddToQuickAccess> ItemProperty =
        AvaloniaProperty.Register<QuickAccessItem, ICanAddToQuickAccess>(nameof(Item));

    public ICanAddToQuickAccess Item
    {
        get => GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(QuickAccessItem);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        e.NameScope.Find<MenuItem>("PART_RemoveFromQuickAccessToolbar")!.Click += (_, _) =>
            VisualExtensions.FindAncestorOfType<QuickAccessToolbar>(this)?.RemoveItem(Item);
    }
}
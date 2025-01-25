using System;
using System.Collections.Specialized;
using System.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using AvaloniaUI.Ribbon.Helpers;

namespace AvaloniaUI.Ribbon;

public class RibbonContextualTabGroup : HeaderedItemsControl
{
    static RibbonContextualTabGroup()
    {
        IsVisibleProperty.Changed.AddClassHandler<RibbonContextualTabGroup>((sender, e) =>
        {
            if (e.NewValue is bool visible && !visible)
                sender.SwitchToNextVisibleTab();
        });
        ItemsSourceProperty.Changed.AddClassHandler<RibbonContextualTabGroup>((sender, args) =>
        {
            if (args.OldValue is INotifyCollectionChanged oldSource)
                oldSource.CollectionChanged -= sender.ItemsCollectionChanged;
            if (args.NewValue is INotifyCollectionChanged newSource)
                newSource.CollectionChanged += sender.ItemsCollectionChanged;
        });
    }

    public RibbonContextualTabGroup()
    {
        Items.CollectionChanged += ItemsCollectionChanged;
    }

    protected override Type StyleKeyOverride => typeof(RibbonContextualTabGroup);

    private void SwitchToNextVisibleTab()
    {
        var rbn = (Ribbon)RibbonControlExtensions.GetParentRibbon(this);
        if (rbn != null && Items.Contains(rbn.SelectedItem))
        {
            var selIndex = rbn.SelectedIndex;

            rbn.CycleTabs(false);

            if (selIndex == rbn.SelectedIndex)
                rbn.CycleTabs(true);
        }
        /*var selectableItems = ((IAvaloniaList<object>)rbn.Items).OfType<RibbonTab>().Where(x => x.IsVisible && x.IsEnabled);
        RibbonTab targetTab = null;
        foreach (RibbonTab tab in selectableItems)
        {
            if (((IAvaloniaList<object>)Items).Contains(tab))
                break;

            targetTab = tab;
        }

        if (targetTab == null)
        {
            selectableItems = selectableItems.Reverse();

            foreach (RibbonTab tab in selectableItems)
            {
                if (((IAvaloniaList<object>)Items).Contains(tab))
                    break;

                targetTab = tab;
            }
        }
        int index = ((IAvaloniaList<object>)rbn.Items).IndexOf(targetTab);
        rbn.SelectedIndex = index;
        //if (index > 0)
        */
    }

    private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems != null)
            foreach (var tab in e.OldItems.OfType<RibbonTab>())
                tab.IsContextual = false;

        if (e.NewItems != null)
            foreach (var tab in e.NewItems.OfType<RibbonTab>())
                tab.IsContextual = true;
    }
}
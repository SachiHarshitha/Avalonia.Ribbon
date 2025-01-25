using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.VisualTree;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon;

// TemplatePart attribute specifying the part "MenuPopup" required for this control
[TemplatePart("MenuPopup", typeof(Popup), IsRequired = true)]
public sealed class RibbonMenu : ItemsControl, IRibbonMenu
{
    // AvaloniaProperty for TopDockedGroupedItems, a collection of RibbonMenuItem groups
    public static readonly DirectProperty<RibbonMenu, IEnumerable<IGrouping<string, RibbonMenuItem>>>
        TopDockedGroupedItemsProperty =
            AvaloniaProperty.RegisterDirect<RibbonMenu, IEnumerable<IGrouping<string, RibbonMenuItem>>>(
                nameof(TopDockedGroupedItems),
                o => o.TopDockedGroupedItems);

    // AvaloniaProperty for BottomDockedGroupedItems, a collection of RibbonMenuItem groups
    public static readonly DirectProperty<RibbonMenu, IEnumerable<IGrouping<string, RibbonMenuItem>>>
        BottomDockedGroupedItemsProperty =
            AvaloniaProperty.RegisterDirect<RibbonMenu, IEnumerable<IGrouping<string, RibbonMenuItem>>>(
                nameof(BottomDockedGroupedItems),
                o => o.BottomDockedGroupedItems);

    // Content Property for the RibbonMenu
    public static readonly StyledProperty<object> ContentProperty =
        ContentControl.ContentProperty.AddOwner<RibbonMenu>();

    // AvaloniaProperty for the IsMenuOpen state
    public static readonly StyledProperty<bool> IsMenuOpenProperty =
        AvaloniaProperty.Register<RibbonMenu, bool>(nameof(IsMenuOpen));

    // AvaloniaProperty for SelectedItemContent
    public static readonly StyledProperty<object> SelectedItemContentProperty =
        AvaloniaProperty.Register<RibbonMenu, object>(nameof(SelectedItemContent));

    // AvaloniaProperty for SelectedSubItems
    public static readonly StyledProperty<object> SelectedSubItemsProperty =
        AvaloniaProperty.Register<RibbonMenu, object>(nameof(SelectedSubItems));

    // Default panel template used when no other template is specified
    private static readonly FuncTemplate<Panel> DefaultPanel = new(() => new StackPanel());

    // Private fields to hold the grouped items for top and bottom docks
    private IEnumerable<IGrouping<string, RibbonMenuItem>> _bottomDockedGroupedItems;

    private IEnumerable<IGrouping<string, RibbonMenuItem>> _topDockedGroupedItems;

    static RibbonMenu()
    {
        // Class handler for IsMenuOpenProperty when it changes
        IsMenuOpenProperty.Changed.AddClassHandler<RibbonMenu>((sender, e) => { });

        // Class handler for ItemsSourceProperty when it changes
        ItemsSourceProperty.Changed.AddClassHandler<RibbonMenu>((x, e) => x.ItemsChanged(e));
    }

    // Public getter and setter for TopDockedGroupedItems
    public IEnumerable<IGrouping<string, RibbonMenuItem>> TopDockedGroupedItems
    {
        get => _topDockedGroupedItems;
        private set => SetAndRaise(TopDockedGroupedItemsProperty, ref _topDockedGroupedItems, value);
    }

    // Public getter and setter for BottomDockedGroupedItems
    public IEnumerable<IGrouping<string, RibbonMenuItem>> BottomDockedGroupedItems
    {
        get => _bottomDockedGroupedItems;
        private set => SetAndRaise(BottomDockedGroupedItemsProperty, ref _bottomDockedGroupedItems, value);
    }

    // Public getter and setter for Content
    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    // Public getter and setter for SelectedItemContent
    public object SelectedItemContent
    {
        get => GetValue(SelectedItemContentProperty);
        set => SetValue(SelectedItemContentProperty, value);
    }

    // Public getter and setter for SelectedSubItems
    public object SelectedSubItems
    {
        get => GetValue(SelectedSubItemsProperty);
        set => SetValue(SelectedSubItemsProperty, value);
    }

    // Public getter and setter for IsMenuOpen
    public bool IsMenuOpen
    {
        get => GetValue(IsMenuOpenProperty);
        set => SetValue(IsMenuOpenProperty, value);
    }

    // Constructor: Called when the template is applied
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Fetch the Popup named "MenuPopup" from the template
        var menuPopup = e.NameScope.Find<Popup>("MenuPopup");
        if (menuPopup != null)
        {
            // Add event handlers for Popup events
            menuPopup.Closed -= PopupOnClosed;
            menuPopup.Closed += PopupOnClosed;
            menuPopup.Opened -= Popup_Opened;
            menuPopup.Opened += Popup_Opened;
        }

        Console.WriteLine($"{menuPopup}, Menu popup is found.");

        // Update grouped items and reset item hover events
        UpdateGroupedItems();
        ResetItemHoverEvents();
    }

    /// <summary>
    ///     Handles the Popup Opened event.
    ///     Adjusts the Popup's position and size based on the top level window's size.
    /// </summary>
    private void Popup_Opened(object sender, EventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return;

        var menuPopup = sender as Popup;
        if (menuPopup == null) return;

        var descendants = topLevel.GetVisualDescendants();
        var titleBar = descendants.FirstOrDefault(x => x is TitleBar);
        var ribbon = descendants.FirstOrDefault(x => x is Ribbon) as Ribbon;
        if (ribbon == null) return;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            menuPopup.Height = topLevel.Bounds.Height - titleBar?.Bounds.Height ?? 0;
            menuPopup.Placement = PlacementMode.LeftEdgeAlignedTop;
            if (ribbon.Orientation == Orientation.Horizontal)
            {
                menuPopup.Width = ribbon.Bounds.Width;
                menuPopup.HorizontalOffset = -1 * menuPopup.PlacementTarget?.Bounds.Width ?? 0;
            }
            else
            {
                menuPopup.Width = topLevel.Bounds.Width;
                menuPopup.HorizontalOffset = -40;
            }

            if (titleBar != null)
            {
            }
        }
        else
        {
            menuPopup.Height = topLevel.ClientSize.Height;
            menuPopup.Width = topLevel.ClientSize.Width;
        }

        // Set the horizontal offset
        //menuPopup.HorizontalOffset = -1 * menuPopup.PlacementTarget?.Bounds.Width ?? 0;
    }

    /// <summary>
    ///     Handles the Popup Closed event.
    /// </summary>
    private void PopupOnClosed(object sender, EventArgs e)
    {
    }

    // Called when the items collection changes
    private void ItemsChanged(AvaloniaPropertyChangedEventArgs args)
    {
        UpdateGroupedItems();
        ResetItemHoverEvents();

        // Unsubscribe from old collection changes, if applicable
        if (args.OldValue is INotifyCollectionChanged oldSource)
            oldSource.CollectionChanged -= ItemsCollectionChanged;
        if (args.NewValue is INotifyCollectionChanged newSource)
            newSource.CollectionChanged += ItemsCollectionChanged;
    }

    // Resets item hover events for each item
    private void ResetItemHoverEvents()
    {
        foreach (var item in Items.OfType<RibbonMenuItem>())
        {
            item.Click -= Item_Clicked;
            item.Click += Item_Clicked;
        }
    }

    /// <summary>
    ///     Handles the Item Clicked event.
    ///     Updates the selected item content based on the clicked item.
    /// </summary>
    private void Item_Clicked(object sender, RoutedEventArgs e)
    {
        var item = sender as RibbonMenuItem;
        if (item == null) return;

        SelectedItemContent = item.Content;
    }

    // Updates grouped items based on top-docked and bottom-docked criteria
    private void UpdateGroupedItems()
    {
        // Group items for the TopDocked section
        TopDockedGroupedItems = Items.OfType<RibbonMenuItem>()
            .Where(x => x.IsTopDocked && !x.IsBottomDocked)
            .GroupBy(x => string.IsNullOrWhiteSpace(x.Group) ? "Ungrouped" : x.Group)
            .ToList();

        // Group items for the BottomDocked section
        BottomDockedGroupedItems = Items.OfType<RibbonMenuItem>()
            .Where(x => x.IsBottomDocked)
            .GroupBy(x => string.IsNullOrWhiteSpace(x.Group) ? "Ungrouped" : x.Group)
            .ToList();

        // Set flags for the last item in each group
        try
        {
            SetIsLastItemFlag(TopDockedGroupedItems, false); // Top docked groups
            SetIsLastItemFlag(BottomDockedGroupedItems, true); // Bottom docked groups
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    // Resets the selection state for all items
    private void ResetSelection()
    {
        foreach (var item in Items.OfType<RibbonMenuItem>()) item.IsSelected = false;
    }

    // Handles collection changes for the items
    private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        ResetItemHoverEvents();
    }

    // Sets the IsLastItem flag for items in the grouped collection
    private void SetIsLastItemFlag(IEnumerable<IGrouping<string, RibbonMenuItem>> groupedItems, bool isBottomDocked)
    {
        var groupList = groupedItems.ToList();

        // Iterate over each group
        for (var groupIndex = 0; groupIndex < groupList.Count; groupIndex++)
        {
            var group = groupList[groupIndex];
            var itemList = group.ToList();

            // Set the IsLastItem flag for each item in the group
            for (var itemIndex = 0; itemIndex < itemList.Count; itemIndex++)
                itemList[itemIndex].IsLastItem = itemIndex == itemList.Count - 1;

            // If it's the last group and it's in the bottom docked section, hide the group
            if (isBottomDocked && groupIndex == groupList.Count - 1)
                foreach (var item in itemList)
                    item.IsLastItem = false; // Set visibility flag for the last group in the bottom dock
        }
    }

    // Destructor: Removes event handlers when the RibbonMenu is destroyed
    ~RibbonMenu()
    {
        if (ItemsSource is INotifyCollectionChanged collectionChanged)
            collectionChanged.CollectionChanged -= ItemsCollectionChanged;
    }
}
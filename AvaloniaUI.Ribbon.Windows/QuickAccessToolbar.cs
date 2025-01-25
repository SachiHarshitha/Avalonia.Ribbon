using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon.Windows;

[TemplatePart("PART_MoreButton", typeof(ToggleButton))]
public class QuickAccessToolbar : ItemsControl, INotifyPropertyChanged //, IKeyTipHandler
{
    public static bool GetIsChecked(MenuItem element)
    {
        return element.GetValue(IsCheckedProperty);
    }

    public static void SetIsChecked(MenuItem element, bool value)
    {
        element.SetValue(IsCheckedProperty, value);
    }

    public bool AddItem(ICanAddToQuickAccess item)
    {
        var contains = ContainsItem(item, out var obj);
        if (item == null || contains)
            return false;
        if (item.CanAddToQuickAccess)
        {
            (ItemsSource as ObservableCollection<QuickAccessItem>).Add(new QuickAccessItem { Item = item });
            return true;
        }

        return false;
    }

    public bool ContainsItem(ICanAddToQuickAccess item)
    {
        return ContainsItem(item, out var result);
    }

    public bool ContainsItem(ICanAddToQuickAccess item, out object result)
    {
        if (Items.OfType<ICanAddToQuickAccess>().Contains(item))
        {
            result = Items.OfType<ICanAddToQuickAccess>().First();
            return true;
        }

        if (Items.OfType<QuickAccessItem>().Any(x => x.Item == item))
        {
            result = Items.OfType<QuickAccessItem>().First(x => x.Item == item);
            return true;
        }

        result = null;
        return false;
    }

    public void MoreFlyoutMenuItemCommand(object parameter)
    {
        if (parameter is ICanAddToQuickAccess item)
        {
            if (!AddItem(item))
                RemoveItem(item);
        }
        else if (parameter is Action cmd)
        {
            cmd();
        }
    }

    public bool RemoveItem(ICanAddToQuickAccess item)
    {
        var contains = ContainsItem(item, out var obj);
        if (item == null || !contains)
            return false;
        var items = (ItemsSource as ObservableCollection<QuickAccessItem>).ToList();
        return (ItemsSource as ObservableCollection<QuickAccessItem>).Remove(items.First(x => x.Item == item));
        /*
            Items.Remove(items.First(x =>
            {
                if (x == item)
                    return true;
                else if (x is QuickAccessItem itm && itm.Item == item)
                    return true;

                return false;
            }));
            */
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var more = e.NameScope.Find<ToggleButton>("PART_MoreButton");
        if (more is null)
            return;
        var morCtx = more.ContextMenu;

        var moreCmdItem = new MenuItem
        {
            //Header =  new DynamicResourceExtension()., //"More commands...",
            IsEnabled = false //[!IsEnabledProperty] = this.GetObservable(RibbonProperty).Select(x => x != null).ToBinding(),
        };
        moreCmdItem.Classes.Add(FIXED_ITEM_CLASS);
        moreCmdItem[!HeaderedSelectingItemsControl.HeaderProperty] =
            moreCmdItem.GetResourceObservable("AvaloniaRibbon.MoreQATCommands").ToBinding();

        if (morCtx is null)
            return;
        morCtx.Opened += (sneder, a) =>
        {
            if (more.IsChecked != true)
                more.IsChecked = true;

            ObservableCollection<object> morCtxItems = new();
            foreach (var rcm in RecommendedItems)
            {
                rcm.IsChecked = ContainsItem(rcm.Item);
                morCtxItems.Add(rcm);
            }

            morCtxItems.Add(new Separator());
            morCtxItems.Add(moreCmdItem);
            morCtxItems.Add(_collapseRibbonItem);
            morCtx.ItemsSource = morCtxItems;
        };

        morCtx.Closed += (sender, a) =>
        {
            if (more.IsChecked == true)
                more.IsChecked = false;
        };
        more.IsCheckedChanged += delegate
        {
            if (more.IsChecked == true)
                morCtx.Open(more);
            else if (more.IsChecked == false) morCtx.Close();
        };
    }

    #region Fields

    public static readonly AttachedProperty<bool> IsCheckedProperty =
        AvaloniaProperty.RegisterAttached<QuickAccessToolbar, MenuItem, bool>("IsChecked");

    public static readonly DirectProperty<QuickAccessToolbar, ObservableCollection<QuickAccessRecommendation>>
        RecommendedItemsProperty =
            AvaloniaProperty.RegisterDirect<QuickAccessToolbar, ObservableCollection<QuickAccessRecommendation>>(
                nameof(RecommendedItems), o => o.RecommendedItems, (o, v) => o.RecommendedItems = v);

    public static readonly StyledProperty<DesktopRibbon> RibbonProperty =
        AvaloniaProperty.Register<QuickAccessToolbar, DesktopRibbon>("Ribbon");

    private static readonly string FIXED_ITEM_CLASS = "quickAccessFixedItem";

    private readonly MenuItem _collapseRibbonItem = new();

    private ObservableCollection<QuickAccessRecommendation> _recommendedItems = new();

    #endregion Fields

    #region Constructors

    static QuickAccessToolbar()
    {
        RibbonProperty.Changed.AddClassHandler<QuickAccessToolbar>((sender, e) =>
        {
            if (sender.Ribbon != null)
                sender._collapseRibbonItem[!IsCheckedProperty] = sender.Ribbon[!DesktopRibbon.IsCollapsedProperty];
            else
                SetIsChecked(sender._collapseRibbonItem, false);
        });
    }

    public QuickAccessToolbar()
    {
        _collapseRibbonItem.Classes.Add(FIXED_ITEM_CLASS);
        //_collapseRibbonItem.Header = new DynamicResourceExtension("AvaloniaRibbon.MinimizeRibbon"); // "Minimize the Ribbon";
        _collapseRibbonItem[!HeaderedSelectingItemsControl.HeaderProperty] = _collapseRibbonItem
            .GetResourceObservable("AvaloniaRibbon.MinimizeRibbon").ToBinding();
        _collapseRibbonItem[!IsEnabledProperty] = this.GetObservable(RibbonProperty).Select(x => x != null).ToBinding();
        _collapseRibbonItem.Click += (_, _) =>
        {
            if (Ribbon != null)
                Ribbon.IsCollapsed = !Ribbon.IsCollapsed;
        };
        ItemsSource = new ObservableCollection<QuickAccessItem>();
    }

    #endregion Constructors

    #region Properties

    public ObservableCollection<QuickAccessRecommendation> RecommendedItems
    {
        get => _recommendedItems;
        set => SetAndRaise(RecommendedItemsProperty, ref _recommendedItems, value);
    }

    public DesktopRibbon Ribbon
    {
        get => GetValue(RibbonProperty);
        set => SetValue(RibbonProperty, value);
    }

    protected override Type StyleKeyOverride => typeof(QuickAccessToolbar);

    #endregion Properties

    /*protected override void ItemsChanged(AvaloniaPropertyChangedEventArgs e)
    {
        base.ItemsChanged(e);
        RefreshItems();
    }

    protected override void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        base.ItemsCollectionChanged(sender, e);
        RefreshItems();
    }

    void RefreshItems()
    {
        panel.Children.Clear();

        foreach (Control itm in ((AvaloniaList<object>)Items).OfType<Control>())
            panel.Children.Add(itm);
    }*/

    /*private protected override ItemContainerGenerator CreateItemContainerGenerator()
    {
        return new ItemContainerGenerator<QuickAccessItem>(this, QuickAccessItem.ItemProperty, QuickAccessItem.ContentTemplateProperty);
    }*/
}
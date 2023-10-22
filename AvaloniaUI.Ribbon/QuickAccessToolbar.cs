using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Styling;

using AvaloniaUI.Ribbon.Contracts;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace AvaloniaUI.Ribbon
{
    public class QuickAccessItem : ContentControl
    {
        public static readonly StyledProperty<ICanAddToQuickAccess> ItemProperty = AvaloniaProperty.Register<QuickAccessItem, ICanAddToQuickAccess>(nameof(Item), null);

        public ICanAddToQuickAccess Item
        {
            get => GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        protected override Type StyleKeyOverride => typeof(QuickAccessItem);

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            e.NameScope.Find<MenuItem>("PART_RemoveFromQuickAccessToolbar")!.Click += (_, _) => Avalonia.VisualTree.VisualExtensions.FindAncestorOfType<QuickAccessToolbar>(this)?.RemoveItem(Item);
        }
    }

    public class QuickAccessRecommendation : AvaloniaObject//INotifyPropertyChanged
    {
        public static readonly StyledProperty<bool?> IsCheckedProperty = ToggleButton.IsCheckedProperty.AddOwner<QuickAccessRecommendation>();
        public static readonly StyledProperty<ICanAddToQuickAccess> ItemProperty = QuickAccessItem.ItemProperty.AddOwner<QuickAccessRecommendation>();

        public bool? IsChecked
        {
            get => GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public ICanAddToQuickAccess Item
        {
            get => GetValue(ItemProperty);
            set => SetValue(ItemProperty, value);
        }

        /*void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;*/
    }

    [TemplatePart("PART_MoreButton", typeof(ToggleButton))]
    public class QuickAccessToolbar : ItemsControl//, IKeyTipHandler
    {
        #region Static Properties

        public static readonly AttachedProperty<bool> IsCheckedProperty = AvaloniaProperty.RegisterAttached<QuickAccessToolbar, MenuItem, bool>("IsChecked");
        public static readonly DirectProperty<QuickAccessToolbar, ObservableCollection<QuickAccessRecommendation>> RecommendedItemsProperty = AvaloniaProperty.RegisterDirect<QuickAccessToolbar, ObservableCollection<QuickAccessRecommendation>>(nameof(RecommendedItems), o => o.RecommendedItems, (o, v) => o.RecommendedItems = v);
        public static readonly StyledProperty<Ribbon> RibbonProperty = AvaloniaProperty.Register<QuickAccessToolbar, Ribbon>(nameof(Ribbon));

        #endregion Static Properties

        #region Fields

        private static readonly string FIXED_ITEM_CLASS = "quickAccessFixedItem";

        private MenuItem _collapseRibbonItem = new();

        private ObservableCollection<QuickAccessRecommendation> _recommendedItems = new ObservableCollection<QuickAccessRecommendation>();

        #endregion Fields

        #region Constructors

        static QuickAccessToolbar()
        {
            RibbonProperty.Changed.AddClassHandler<QuickAccessToolbar>((sender, e) =>
            {
                if (sender.Ribbon != null)
                    sender._collapseRibbonItem[!IsCheckedProperty] = sender.Ribbon[!Ribbon.IsCollapsedProperty];
                else
                    SetIsChecked(sender._collapseRibbonItem, false);
            });
        }

        public QuickAccessToolbar() : base()
        {
            _collapseRibbonItem.Classes.Add(FIXED_ITEM_CLASS);
            //_collapseRibbonItem.Header = new DynamicResourceExtension("AvaloniaRibbon.MinimizeRibbon"); // "Minimize the Ribbon";
            _collapseRibbonItem[!HeaderedSelectingItemsControl.HeaderProperty] = _collapseRibbonItem.GetResourceObservable("AvaloniaRibbon.MinimizeRibbon").ToBinding();
            _collapseRibbonItem[!IsEnabledProperty] = this.GetObservable(RibbonProperty).Select(x => x != null).ToBinding();
            _collapseRibbonItem.Click += (_, _) =>
            {
                if (Ribbon != null)
                    Ribbon.IsCollapsed = !Ribbon.IsCollapsed;
            };
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<QuickAccessRecommendation> RecommendedItems
        {
            get => _recommendedItems;
            set => SetAndRaise(RecommendedItemsProperty, ref _recommendedItems, value);
        }

        public Ribbon Ribbon
        {
            get => GetValue(RibbonProperty);
            set => SetValue(RibbonProperty, value);
        }

        protected override Type StyleKeyOverride => typeof(QuickAccessToolbar);

        #endregion Properties

        #region Methods

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
            bool contains = ContainsItem(item, out object obj);
            if ((item == null) || contains)
                return false;
            else
            {
                ICanAddToQuickAccess itm = item;
                if (obj is QuickAccessItem qai)
                    itm = qai.Item;

                if (itm.CanAddToQuickAccess)
                {
                    Items.Add(item);
                    //ItemsSource = Items.Append(item);
                    return true;
                }
            }

            return false;
        }

        public bool ContainsItem(ICanAddToQuickAccess item) => ContainsItem(item, out object result);

        public bool ContainsItem(ICanAddToQuickAccess item, out object result)
        {
            if (Items.OfType<ICanAddToQuickAccess>().Contains(item))
            {
                result = Items.OfType<ICanAddToQuickAccess>().First();
                return true;
            }
            else if (Items.OfType<QuickAccessItem>().Any(x => x.Item == item))
            {
                result = Items.OfType<QuickAccessItem>().First(x => x.Item == item);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public void MoreFlyoutMenuItemCommand(object parameter)
        {
            if (parameter is ICanAddToQuickAccess item)
            {
                if (!AddItem(item))
                    RemoveItem(item);
            }
            else if (parameter is Action cmd)
                cmd();
        }

        public bool RemoveItem(ICanAddToQuickAccess item)
        {
            bool contains = ContainsItem(item, out object obj);
            if ((item == null) || (!contains))
                return false;
            else
            {
                var items = Items.ToList();
                Items.Remove(items.First(x =>
                {
                    if (x == item)
                        return true;
                    else if ((x is QuickAccessItem itm) && (itm.Item == item))
                        return true;

                    return false;
                }));
                //ItemsSource = items;
                return true;
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            var more = e.NameScope.Find<ToggleButton>("PART_MoreButton");
            if (more is not { })
                return;
            var morCtx = more.ContextMenu;

            MenuItem moreCmdItem = new MenuItem()
            {
                //Header =  new DynamicResourceExtension()., //"More commands...",
                IsEnabled = false, //[!IsEnabledProperty] = this.GetObservable(RibbonProperty).Select(x => x != null).ToBinding(),
            };
            moreCmdItem.Classes.Add(FIXED_ITEM_CLASS);
            moreCmdItem[!HeaderedSelectingItemsControl.HeaderProperty] = moreCmdItem.GetResourceObservable("AvaloniaRibbon.MoreQATCommands").ToBinding();

            if (morCtx is not { })
                return;
            morCtx.Opened += (sneder, a) =>
            {
                if (more.IsChecked != true)
                    more.IsChecked = true;

                ObservableCollection<object> morCtxItems = new ObservableCollection<object>();
                foreach (QuickAccessRecommendation rcm in RecommendedItems)
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
            more.IsCheckedChanged += delegate (object sender, RoutedEventArgs args)
            {
                if (more.IsChecked == true)
                {
                    morCtx.Open(more);
                }
                else if (more.IsChecked == false)
                {
                    morCtx.Close();
                }
            };
        }

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

        #endregion Methods
    }
}
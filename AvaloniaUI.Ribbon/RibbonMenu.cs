using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using Avalonia.Threading;

using AvaloniaUI.Ribbon.Contracts;

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;

namespace AvaloniaUI.Ribbon
{
    [TemplatePart("MenuPopup", typeof(Popup))]
    public sealed class RibbonMenu : ItemsControl, IRibbonMenu
    {
        private IEnumerable _rightColumnItems = new AvaloniaList<object>();
        private RibbonMenuItem _previousSelectedItem = null;

        public static readonly StyledProperty<object> ContentProperty = ContentControl.ContentProperty.AddOwner<RibbonMenu>();

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly StyledProperty<bool> IsMenuOpenProperty = AvaloniaProperty.Register<RibbonMenu, bool>(nameof(IsMenuOpen), false);

        public bool IsMenuOpen
        {
            get => GetValue(IsMenuOpenProperty);
            set => SetValue(IsMenuOpenProperty, value);
        }

        public static readonly StyledProperty<object> SelectedSubItemsProperty = AvaloniaProperty.Register<RibbonMenu, object>(nameof(SelectedSubItems));

        public object SelectedSubItems
        {
            get => GetValue(SelectedSubItemsProperty);
            set => SetValue(SelectedSubItemsProperty, value);
        }

        public static readonly StyledProperty<bool> HasSelectedItemProperty = AvaloniaProperty.Register<RibbonMenu, bool>(nameof(HasSelectedItem), false);

        public bool HasSelectedItem
        {
            get => GetValue(HasSelectedItemProperty);
            set => SetValue(HasSelectedItemProperty, value);
        }

        public static readonly StyledProperty<string> RightColumnHeaderProperty = AvaloniaProperty.Register<RibbonMenu, string>(nameof(RightColumnHeader));

        public string RightColumnHeader
        {
            get => GetValue(RightColumnHeaderProperty);
            set => SetValue(RightColumnHeaderProperty, value);
        }

        public static readonly DirectProperty<RibbonMenu, IEnumerable> RightColumnItemsProperty = AvaloniaProperty.RegisterDirect<RibbonMenu, IEnumerable>(nameof(RightColumnItems), o => o.RightColumnItems, (o, v) => o.RightColumnItems = v);

        public IEnumerable RightColumnItems
        {
            get => _rightColumnItems;
            set => SetAndRaise(RightColumnItemsProperty, ref _rightColumnItems, value);
        }

        private static readonly FuncTemplate<Panel> DefaultPanel = new FuncTemplate<Panel>(() => new StackPanel());

        public static readonly StyledProperty<ITemplate<Panel>> RightColumnItemsPanelProperty = AvaloniaProperty.Register<RibbonMenu, ITemplate<Panel>>(nameof(RightColumnItemsPanel), DefaultPanel);

        public ITemplate<Panel> RightColumnItemsPanel
        {
            get => GetValue(RightColumnItemsPanelProperty);
            set => SetValue(RightColumnItemsPanelProperty, value);
        }

        public static readonly StyledProperty<IDataTemplate> RightColumnItemTemplateProperty = AvaloniaProperty.Register<RibbonMenu, IDataTemplate>(nameof(RightColumnItemTemplate));

        public IDataTemplate RightColumnItemTemplate
        {
            get => GetValue(RightColumnItemTemplateProperty);
            set => SetValue(RightColumnItemTemplateProperty, value);
        }

        static RibbonMenu()
        {
            IsMenuOpenProperty.Changed.AddClassHandler<RibbonMenu>(new Action<RibbonMenu, AvaloniaPropertyChangedEventArgs>((sender, e) =>
            {
                if (e.NewValue is bool boolean)
                {
                    if (boolean)
                    {
                        //sender.Focus();
                    }
                    else
                    {
                        sender.SelectedSubItems = null;
                        sender.HasSelectedItem = false;

                        if (sender._previousSelectedItem != null)
                            sender._previousSelectedItem.IsSelected = false;
                    }
                }
            }));

            ItemsSourceProperty.Changed.AddClassHandler<RibbonMenu>((x, e) => x.ItemsChanged(e));
        }

        public RibbonMenu()
        {
            /*LostFocus += (_, _) =>
            {
                IsMenuOpen = false;
            };*/
            /*this.FindAncestorOfType<VisualLayerManager>()*/
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            var popup = e.NameScope.Find<Popup>("MenuPopup");
            popup.Closed += PopupOnClosed;
        }

        private void PopupOnClosed(object sender, EventArgs e)
        {
        }

        private void ItemsChanged(AvaloniaPropertyChangedEventArgs args)
        {
            ResetItemHoverEvents();

            if (args.OldValue is INotifyCollectionChanged oldSource)
                oldSource.CollectionChanged -= ItemsCollectionChanged;
            if (args.NewValue is INotifyCollectionChanged newSource)
            {
                newSource.CollectionChanged += ItemsCollectionChanged;
            }
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ResetItemHoverEvents();
        }

        private void ResetItemHoverEvents()
        {
            foreach (RibbonMenuItem item in Items.OfType<RibbonMenuItem>())
            {
                item.PointerEntered -= Item_PointerEnter;
                item.PointerEntered += Item_PointerEnter;
            }
        }

        private void Item_PointerEnter(object sender, Avalonia.Input.PointerEventArgs e)
        {
            if ((sender is RibbonMenuItem item))
            {
                int counter = 0;
                Timer timer = new Timer(1);
                timer.Elapsed += (sneder, args) =>
                {
                    if (counter < 25)
                        counter++;
                    else
                    {
                        Dispatcher.UIThread.Post(() =>
                        {
                            if (item.IsPointerOver)
                            {
                                if (item.HasItems)
                                {
                                    SelectedSubItems = item.Items;
                                    HasSelectedItem = true;

                                    item.IsSelected = true;

                                    if (_previousSelectedItem != null)
                                        _previousSelectedItem.IsSelected = false;

                                    _previousSelectedItem = item;
                                }
                                else
                                {
                                    SelectedSubItems = null;
                                    HasSelectedItem = false;

                                    if (_previousSelectedItem != null)
                                        _previousSelectedItem.IsSelected = false;
                                }
                            }
                        });

                        timer.Stop();
                    }
                };
                timer.Start();
            }
        }

        ~RibbonMenu()
        {
            if (ItemsSource is INotifyCollectionChanged collectionChanged)
                collectionChanged.CollectionChanged -= ItemsCollectionChanged;
        }
    }
}
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.VisualTree;

using AvaloniaUI.Ribbon.Contracts;

using ReactiveUI;

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Timers;
using Avalonia.Controls.Chrome;
using Avalonia.Controls.Primitives.PopupPositioning;
using Avalonia.LogicalTree;

namespace AvaloniaUI.Ribbon
{
    [TemplatePart("MenuPopup", typeof(Popup))]
    [TemplatePart("PART_TabControl", typeof(TabControl))]
    public sealed class RibbonMenu : ItemsControl, IRibbonMenu
    {
        private Popup _popup;
        private TabControl _tabControl;
        
        public static readonly StyledProperty<object> HeaderProperty = AvaloniaProperty.Register<RibbonMenu,object>(nameof(Header));

        
        public static readonly StyledProperty<IEnumerable> ItemsCollectionProperty = AvaloniaProperty.Register<RibbonMenu, IEnumerable?>(nameof(ItemsCollection));

        public object ItemsCollection
        {
            get => GetValue(ItemsCollectionProperty);
            set => SetValue(ItemsCollectionProperty, value);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }
        
        public static readonly StyledProperty<bool> IsMenuOpenProperty = AvaloniaProperty.Register<RibbonMenu, bool>(nameof(IsMenuOpen), false);

        public bool IsMenuOpen
        {
            get => GetValue(IsMenuOpenProperty);
            set => SetValue(IsMenuOpenProperty, value);
        }
        
        static RibbonMenu()
        {
            IsMenuOpenProperty.Changed.AddClassHandler<RibbonMenu>(new Action<RibbonMenu, AvaloniaPropertyChangedEventArgs>((sender, e) =>
            {
               
            }));
            ItemsSourceProperty.Changed.AddClassHandler<RibbonMenu>((x, e) => x.ItemsChanged(e));
        }
        
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            _popup = e.NameScope.Find<Popup>("MenuPopup");
            _tabControl = e.NameScope.Find<TabControl>("PART_TabControl");
            ItemsCollection = Items;
            _popup.Loaded += Popup_Loaded;
            _popup.Opened += Popup_Opened;
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            RearrangePopup();
        }

        private void Popup_Loaded(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TopLevel.GetTopLevel(this).SizeChanged += TopLevel_SizeChanged;
        }

        private void TopLevel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           RearrangePopup();
        }

        private void RearrangePopup()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;
            
            var descendants =  topLevel.GetVisualDescendants();
            var titleBar = descendants.FirstOrDefault(x => x is TitleBar);
            var toggle = descendants.FirstOrDefault(x => x.Name == "MenuPopupToggle");
            var ribbon = descendants.FirstOrDefault(x => x is Ribbon);
            _popup.Height = topLevel.Bounds.Height - titleBar.Bounds.Height;
            //_popup.VerticalOffset = -1 *  titleBar.Bounds.Y;

            var buttonPoint = ribbon.PointToScreen(ribbon.Bounds.TopLeft);
            var popupPoint = _popup.PointToScreen(_popup.Bounds.TopLeft);
            var xDif = buttonPoint.X - popupPoint.X;
            var difference = buttonPoint.Y - popupPoint.Y;
            _popup.HorizontalOffset = -1 * toggle.Bounds.Width;

            var vector = new Vector();
            var canvas = _popup.Parent as Panel;
           // _popup.PlacementRect = canvas.Bounds;
        }
        

        private void ItemsChanged(AvaloniaPropertyChangedEventArgs args)
        {

            if (args.OldValue is INotifyCollectionChanged oldSource)
                oldSource.CollectionChanged -= ItemsCollectionChanged;
            if (args.NewValue is INotifyCollectionChanged newSource)
            {
                newSource.CollectionChanged += ItemsCollectionChanged;
            }
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
        }


        ~RibbonMenu()
        {
            if (ItemsSource is INotifyCollectionChanged collectionChanged)
                collectionChanged.CollectionChanged -= ItemsCollectionChanged;
        }
    }
}
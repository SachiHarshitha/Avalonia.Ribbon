using System;
using System.Collections.Specialized;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

namespace AvaloniaUI.Ribbon;

[TemplatePart("PART_ContentButton", typeof(Button))]
public class RibbonMenuItem : HeaderedItemsControl
{
    public static readonly StyledProperty<string> GroupProperty =
        AvaloniaProperty.Register<RibbonMenuItem, string>(nameof(Group));

    public static readonly StyledProperty<bool> IsTopDockedProperty =
        AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsTopDocked), true);

    public static readonly StyledProperty<bool> IsBottomDockedProperty =
        AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsBottomDocked));

    public static readonly StyledProperty<bool> IsLastItemProperty =
        AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsLastItem));

    public static readonly StyledProperty<object> ContentProperty =
        AvaloniaProperty.Register<RibbonMenuItem, object>(nameof(Content));

    public static readonly StyledProperty<object> IconProperty =
        AvaloniaProperty.Register<RibbonMenuItem, object>(nameof(Icon));

    public static readonly StyledProperty<bool> IsSubmenuOpenProperty =
        AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsSubmenuOpen));

    public static readonly StyledProperty<bool> IsSelectedProperty =
        AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsSelected));

    public static readonly StyledProperty<ICommand> CommandProperty = Button.CommandProperty.AddOwner<RibbonMenuItem>();


    public static readonly StyledProperty<object> CommandParameterProperty =
        Button.CommandParameterProperty.AddOwner<RibbonMenuItem>();

    public static readonly RoutedEvent<RoutedEventArgs> ClickEvent =
        RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

    static RibbonMenuItem()
    {
        ItemsSourceProperty.Changed.AddClassHandler<RibbonMenuItem>((x, e) => x.ItemsChanged(e));
    }

    public string Group
    {
        get => GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
    }

    public bool IsTopDocked
    {
        get => GetValue(IsTopDockedProperty);
        set => SetValue(IsTopDockedProperty, value);
    }

    public bool IsBottomDocked
    {
        get => GetValue(IsBottomDockedProperty);
        set => SetValue(IsBottomDockedProperty, value);
    }

    public bool IsLastItem
    {
        get => GetValue(IsLastItemProperty);
        set => SetValue(IsLastItemProperty, value);
    }

    public object Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public bool IsSubmenuOpen
    {
        get => GetValue(IsSubmenuOpenProperty);
        set => SetValue(IsSubmenuOpenProperty, value);
    }

    public bool IsSelected
    {
        get => GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    private void ItemsChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (args.OldValue is INotifyCollectionChanged oldSource)
            oldSource.CollectionChanged -= ItemsCollectionChanged;
        if (args.NewValue is INotifyCollectionChanged newSource) newSource.CollectionChanged += ItemsCollectionChanged;
    }

    private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
    }

    public event EventHandler<RoutedEventArgs> Click
    {
        add => AddHandler(ClickEvent, value);
        remove => RemoveHandler(ClickEvent, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        e.NameScope.Get<Button>("PART_ContentButton").Click += (_, _) =>
        {
            var f = new RoutedEventArgs(ClickEvent);
            RaiseEvent(f);
        };
    }
}
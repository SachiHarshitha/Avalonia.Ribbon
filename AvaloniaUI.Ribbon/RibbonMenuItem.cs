using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Avalonia.Automation;
using Avalonia.Automation.Peers;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml.Templates;

namespace AvaloniaUI.Ribbon
{

    [TemplatePart("PART_ContentButton", typeof(Button))]
    public class RibbonMenuItem : TabItem
    {
        protected override AutomationPeer OnCreateAutomationPeer() => new ListItemAutomationPeer(this);
        
        private void UpdateHeader(AvaloniaPropertyChangedEventArgs obj)
        {
            if (Header == null)
            {
                if (obj.NewValue is HeaderedContentControl headered)
                {
                    if (Header != headered.Header)
                    {
                        SetCurrentValue(HeaderProperty, headered.Header);
                    }
                }
                else
                {
                    if (!(obj.NewValue is Control))
                    {
                        SetCurrentValue(HeaderProperty, obj.NewValue);
                    }
                }
            }
            else
            {
                if (Header == obj.OldValue)
                {
                    SetCurrentValue(HeaderProperty, obj.NewValue);
                }
            }
        }

        private void RibbonMenuItemActivated(RoutedEventArgs args)
        {
            SetCurrentValue(IsSelectedProperty, true);
            args.Handled = true;
        }
    
        protected override Type StyleKeyOverride => typeof(RibbonMenuItem);

        static RibbonMenuItem()
        {
            SelectableMixin.Attach<RibbonMenuItem>(IsSelectedProperty);
            PressedMixin.Attach<RibbonMenuItem>();
            FocusableProperty.OverrideDefaultValue(typeof(RibbonMenuItem), true);
            DataContextProperty.Changed.AddClassHandler<RibbonMenuItem>((x, e) => x.UpdateHeader(e));
            AutomationProperties.ControlTypeOverrideProperty.OverrideDefaultValue<RibbonMenuItem>(AutomationControlType.TabItem);
            AutomationProperties.IsOffscreenBehaviorProperty.OverrideDefaultValue<RibbonMenuItem>(IsOffscreenBehavior.FromClip);
        }
        
        
        public static readonly StyledProperty<object> IconProperty = AvaloniaProperty.Register<RibbonMenuItem, object>(nameof(Icon));

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        

        public static readonly StyledProperty<ICommand> CommandProperty = Button.CommandProperty.AddOwner<RibbonMenuItem>();


        public ICommand Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty,value);
        }


        public static readonly StyledProperty<object> CommandParameterProperty = Button.CommandParameterProperty.AddOwner<RibbonMenuItem>();

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public static readonly RoutedEvent<RoutedEventArgs> ClickEvent = RoutedEvent.Register<Button, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

        public event EventHandler<RoutedEventArgs> Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            var button =  e.NameScope.Find<Button>("PART_ContentButton");
            if (button !=null)
               button.Click += (_, _) =>
                {
                    var f = new RoutedEventArgs(ClickEvent);
                    RaiseEvent(f);
                };
        }
    }
}

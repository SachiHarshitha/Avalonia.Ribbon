using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon;

public class RibbonGroupBox : HeaderedItemsControl
{
    #region Fields

    private ICommand _command;
    #endregion

    static RibbonGroupBox()
    {
        AffectsArrange<RibbonGroupBox>(DisplayModeProperty);
        AffectsMeasure<RibbonGroupBox>(DisplayModeProperty);
        AffectsRender<RibbonGroupBox>(DisplayModeProperty);

        CommandProperty = AvaloniaProperty.RegisterDirect<RibbonGroupBox, ICommand>(nameof(Command),
            button => button.Command, (button, command) => button.Command = command, enableDataValidation: true);
    }

    #region Static Properties

    public static readonly StyledProperty<object> CommandParameterProperty =
        AvaloniaProperty.Register<RibbonGroupBox, object>(nameof(CommandParameter));

    public static readonly DirectProperty<RibbonGroupBox, ICommand> CommandProperty;

    public static readonly StyledProperty<GroupDisplayMode> DisplayModeProperty =
        StyledProperty<RibbonGroupBox>.Register<RibbonGroupBox, GroupDisplayMode>(nameof(DisplayMode),
            GroupDisplayMode.Small);

    #endregion Static Properties

    #region Properties

    public event EventHandler Rearranged;

    public event EventHandler Remeasured;


    protected override Size ArrangeOverride(Size finalSize)
    {
        Rearranged?.Invoke(this, null);
        return base.ArrangeOverride(finalSize);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        Remeasured?.Invoke(this, null);
        return base.MeasureOverride(availableSize);
    }

    protected override Type StyleKeyOverride => typeof(RibbonGroupBox);

    public ICommand Command
    {
        get => _command;
        set => SetAndRaise(CommandProperty, ref _command, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public GroupDisplayMode DisplayMode
    {
        get => GetValue(DisplayModeProperty);
        set => SetValue(DisplayModeProperty, value);
    }

    #endregion
    
}
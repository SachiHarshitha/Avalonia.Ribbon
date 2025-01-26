using System.Windows.Input;

namespace AvaloniaUI.Ribbon.Contracts;

public interface IRibbonCommand
{
    public ICommand Command { get; set; }

    public object CommandParameter { get; set; }
}
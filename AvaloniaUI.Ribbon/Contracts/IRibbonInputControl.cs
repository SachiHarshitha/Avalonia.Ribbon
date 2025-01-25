using Avalonia.Controls.Templates;

namespace AvaloniaUI.Ribbon.Contracts;

public interface IRibbonInputControl : IRibbonControl
{
    public object Content { get; set; }

    public IControlTemplate Icon { get; set; }

    public IControlTemplate LargeIcon { get; set; }
}
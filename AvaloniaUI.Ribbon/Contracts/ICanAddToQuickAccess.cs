using Avalonia.Controls.Templates;

namespace AvaloniaUI.Ribbon.Contracts;

public interface ICanAddToQuickAccess
{
    IControlTemplate QuickAccessTemplate { get; set; }

    bool CanAddToQuickAccess { get; set; }

    public IControlTemplate QuickAccessIcon { get; set; }
}
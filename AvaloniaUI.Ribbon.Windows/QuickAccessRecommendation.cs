using Avalonia;
using Avalonia.Controls.Primitives;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon.Windows;

public class QuickAccessRecommendation : AvaloniaObject //INotifyPropertyChanged
{
    public static readonly StyledProperty<ICanAddToQuickAccess> ItemProperty =
        QuickAccessItem.ItemProperty.AddOwner<QuickAccessRecommendation>();

    public static readonly StyledProperty<bool?> IsCheckedProperty =
        ToggleButton.IsCheckedProperty.AddOwner<QuickAccessRecommendation>();

    public ICanAddToQuickAccess Item
    {
        get => GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public bool? IsChecked
    {
        get => GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /*void NotifyPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    public event PropertyChangedEventHandler PropertyChanged;*/
}
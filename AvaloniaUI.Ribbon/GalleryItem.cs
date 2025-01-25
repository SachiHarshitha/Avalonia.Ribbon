using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace AvaloniaUI.Ribbon;

public class GalleryItem : ListBoxItem
{
    public static readonly StyledProperty<IControlTemplate> IconProperty =
        RibbonButton.IconProperty.AddOwner<GalleryItem>();

    public static readonly StyledProperty<IControlTemplate> LargeIconProperty =
        RibbonButton.LargeIconProperty.AddOwner<GalleryItem>();

    public IControlTemplate Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public IControlTemplate LargeIcon
    {
        get => GetValue(LargeIconProperty);
        set => SetValue(LargeIconProperty, value);
    }
}
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;

namespace AvaloniaUI.Ribbon.Controls;

/// <summary>
///     Represents the In-Ribbon Gallery, a gallery-based control that exposes
///     a default subset of items directly in the Ribbon. Any remaining items
///     are displayed when a drop-down menu button is clicked
///     Reference : Fluent.Ribbon
/// </summary>
//[ContentProperty(nameof(Items))]
[TemplatePart(Name = "PART_ExpandButton", Type = typeof(ToggleButton))]
[TemplatePart(Name = "PART_DropDownButton", Type = typeof(ToggleButton))]
[TemplatePart(Name = "PART_Popup", Type = typeof(Popup))]
// [TemplatePart(Name = "PART_PopupContentControl", Type = typeof(ResizeableContentControl))]
[TemplatePart(Name = "PART_FilterDropDownButton", Type = typeof(DropDownButton))]
//[TemplatePart(Name = "PART_GalleryPanel", Type = typeof(GalleryPanel))]
[TemplatePart(Name = "PART_FakeImage", Type = typeof(Image))]
[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentControl))]
[TemplatePart(Name = "PART_PopupContentPresenter", Type = typeof(ContentControl))]
//[TemplatePart(Name = "PART_PopupResizeBorder", Type = typeof(FrameworkElement))]
[TemplatePart(Name = "PART_DropDownBorder", Type = typeof(Border))]
public class InRibbonGallery : TemplatedControl
{
}
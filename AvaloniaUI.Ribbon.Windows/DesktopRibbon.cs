using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon.Windows;

[TemplatePart("PART_CollapsedContentPopup", typeof(Popup))]
[TemplatePart("PART_SelectedGroupsHost", typeof(ItemsControl))]
[TemplatePart("PART_GroupsPresenterHolder", typeof(ContentControl))]
[TemplatePart("PART_PopupGroupsPresenterHolder", typeof(ContentControl))]
[TemplatePart("PART_ItemsPresenter", typeof(ItemsPresenter))]
[TemplatePart("PART_PinLastHoveredControlToQuickAccess", typeof(MenuItem))]
[TemplatePart("PART_ContentAreaContextMenu", typeof(ContextMenu))]
[TemplatePart("PART_CollapseRibbon", typeof(MenuItem))]
public class DesktopRibbon : Ribbon
{
    #region Static Properties

    public static readonly StyledProperty<QuickAccessToolbar> QuickAccessToolbarProperty =
        AvaloniaProperty.Register<DesktopRibbon, QuickAccessToolbar>(nameof(QuickAccessToolbar));

    #endregion Static Properties

    static DesktopRibbon()
    {
    }

    #region Properties

    public QuickAccessToolbar QuickAccessToolbar
    {
        get => GetValue(QuickAccessToolbarProperty);
        set => SetValue(QuickAccessToolbarProperty, value);
    }

    #endregion Properties

    protected override Type StyleKeyOverride => typeof(DesktopRibbon);

    #region Methods

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        UpdatePresenterLocation(IsCollapsed);

        var pinToQat = e.NameScope.Find<MenuItem>("PART_PinLastHoveredControlToQuickAccess");
        if (pinToQat is not null)
            pinToQat.Click += (_, _) =>
            {
                if (_rightClicked != null)
                    QuickAccessToolbar?.AddItem(_rightClicked);
            };

        if (_groupsHost is not null)
        {
            _groupsHost.PointerExited += (_, _) =>
            {
                if (!_ctxMenu.IsOpen)
                    _rightClicked = null;
            };
            _groupsHost.AddHandler(PointerReleasedEvent,
                (_, args) =>
                {
                    if (args.Source is Visual visual && pinToQat is not null)
                    {
                        var ctrl = visual.FindAncestorOfType<ICanAddToQuickAccess>();

                        _rightClicked = ctrl;

                        if (QuickAccessToolbar != null)
                            pinToQat.IsEnabled = _rightClicked != null && _rightClicked.CanAddToQuickAccess &&
                                                 !QuickAccessToolbar.ContainsItem(_rightClicked);
                        else
                            pinToQat.IsEnabled = false;
                    }
                }, handledEventsToo: true);
        }

        /*if (_popup is { })
        {
            _popup.LostFocus += (_, _) =>
            {
                if (IsOpen)
                {
                    Close();
                }
            };
        }*/
    }
    
    #endregion Methods
}
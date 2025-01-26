using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Layout;

namespace AvaloniaUI.Ribbon;

public class KeyTip
{
    private static readonly Dictionary<Control, Popup> _keyTips = new();

    private KeyTip()
    {
    }

    #region Static Properties

    public static readonly AttachedProperty<string> KeyTipKeysProperty =
        AvaloniaProperty.RegisterAttached<KeyTip, Control, string>("KeyTipKeys");

    public static readonly AttachedProperty<bool> ShowChildKeyTipKeysProperty =
        AvaloniaProperty.RegisterAttached<KeyTip, Control, bool>("ShowChildKeyTipKeys");

    #endregion

    #region Methods

    private static void KeyTip_Opened(object sender, EventArgs e)
    {
        var sned = sender as Popup;
        //sned.Host?.ConfigurePosition(sned.PlacementTarget, sned.Placement, new Point(sned.HorizontalOffset, sned.VerticalOffset));
    }

    public static Popup GetKeyTip(Control element)
    {
        if (_keyTips.TryGetValue(element, out var val))
            return val;
        var tipContent = new ContentControl
        {
            //Background = new SolidColorBrush(Colors.White),
            [!ContentControl.ContentProperty] = element[!KeyTipKeysProperty]
        };
        tipContent.Classes.Add("KeyTipContent");
        if (tipContent.Content != null)
            Debug.WriteLine("TEXT: " + tipContent.Content);
        
        var tip = new Popup
        {
            PlacementTarget = element,
            Placement = PlacementMode.Right,
            [!Popup.WidthProperty] = tipContent.GetObservable(Control.BoundsProperty).Select(x => x.Width).ToBinding(),
            [!Popup.HeightProperty] =
                tipContent.GetObservable(Control.BoundsProperty).Select(x => x.Height)
                    .ToBinding(), //tipContent[!Control.HeightProperty],
            VerticalAlignment = VerticalAlignment.Bottom,
            Child = tipContent
        };
        tip.Classes.Add("KeyTip");

        tipContent.InvalidateArrange();
        tipContent.InvalidateMeasure();
        tipContent.InvalidateVisual();

        tip.InvalidateArrange();
        tip.InvalidateMeasure();
        tip.InvalidateVisual();

        tip[!Popup.HorizontalOffsetProperty] =
            tipContent.GetObservable(Control.BoundsProperty).Select(x => x.Width * -1).ToBinding();

        if (element is not RibbonGroupBox)
            tip[!Popup.VerticalOffsetProperty] = element.GetObservable(Control.BoundsProperty).Select(x => x.Height)
                .CombineLatest(tipContent.GetObservable(Control.BoundsProperty), (x, y) => x - y.Height).ToBinding();
        else
            tip[!Popup.VerticalOffsetProperty] = element.GetObservable(Control.BoundsProperty).Select(x => x.Height)
                .CombineLatest(tipContent.GetObservable(Control.HeightProperty), (x, y) => x / 2 - y).ToBinding();

        ((ISetLogicalParent)tip).SetParent(element);

        tip.Opened += KeyTip_Opened;

        _keyTips.Add(element, tip);
        return _keyTips[element];
    }

    public static string GetKeyTipKeys(Control element)
    {
        return element.GetValue(KeyTipKeysProperty);
    }

    public static bool GetShowChildKeyTipKeys(Control element)
    {
        return element.GetValue(ShowChildKeyTipKeysProperty);
    }

    public static bool HasKeyTipKey(Control element, Key key)
    {
        var keys = GetKeyTipKeys(element);
        return HasKeyTipKeys(element) && keys.ToLowerInvariant().Contains(key.ToString().ToLowerInvariant());
    }

    public static bool HasKeyTipKeys(Control element)
    {
        var keys = GetKeyTipKeys(element);
        return !string.IsNullOrEmpty(keys) && !string.IsNullOrWhiteSpace(keys);
    }

    public static void SetKeyTipKeys(Control element, string value)
    {
        element.SetValue(KeyTipKeysProperty, value);
    }

    public static void SetShowChildKeyTipKeys(Control element, bool value)
    {
        element.SetValue(ShowChildKeyTipKeysProperty, value);
    }

    #endregion
}
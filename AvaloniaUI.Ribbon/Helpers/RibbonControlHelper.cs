using System;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using AvaloniaUI.Ribbon.Contracts;
using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon.Helpers;

public static class RibbonControlHelper<T> where T : Layoutable
{
    private static readonly AvaloniaProperty<RibbonControlSize> SizeProperty =
        AvaloniaProperty.Register<TemplatedControl, RibbonControlSize>("Size", RibbonControlSize.Large,
            coerce: CoerceSize);

    private static readonly AvaloniaProperty<RibbonControlSize> MinSizeProperty =
        AvaloniaProperty.Register<TemplatedControl, RibbonControlSize>("MinSize");

    private static readonly AvaloniaProperty<RibbonControlSize> MaxSizeProperty =
        AvaloniaProperty.Register<TemplatedControl, RibbonControlSize>("MaxSize", RibbonControlSize.Large);

    private static RibbonControlSize CoerceSize(AvaloniaObject obj, RibbonControlSize val)
    {
        if (obj is IRibbonControl ctrl)
        {
            if ((int)ctrl.MinSize > (int)val)
                return ctrl.MinSize;
            if ((int)ctrl.MaxSize < (int)val)
                return ctrl.MaxSize;
            return val;
        }

        throw new Exception("obj must be an IRibbonControl!");
    }

    public static void SetProperties(out AvaloniaProperty<RibbonControlSize> size,
        out AvaloniaProperty<RibbonControlSize> minSize, out AvaloniaProperty<RibbonControlSize> maxSize)
    {
        size = SizeProperty;
        minSize = MinSizeProperty;
        maxSize = MaxSizeProperty;

        minSize.Changed.AddClassHandler<T>((sender, args) =>
        {
            if ((int)args.NewValue > (int)(sender as IRibbonControl).Size)
                (sender as IRibbonControl).Size = (RibbonControlSize)args.NewValue;
        });

        maxSize.Changed.AddClassHandler<T>((sender, args) =>
        {
            if ((int)args.NewValue < (int)(sender as IRibbonControl).Size)
                (sender as IRibbonControl).Size = (RibbonControlSize)args.NewValue;
        });
    }
}
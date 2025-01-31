﻿using Avalonia.Controls;
using Avalonia.VisualTree;
using AvaloniaUI.Ribbon.Contracts;

namespace AvaloniaUI.Ribbon.Helpers;

public static class RibbonControlExtensions
{
    public static IRibbon GetParentRibbon(Control control)
    {
        return control.FindAncestorOfType<IRibbon>(true);
        /*IControl parentRbn = control.Parent;
        while ((!(parentRbn is Ribbon)) && (parentRbn != null))
        {
            parentRbn = parentRbn.Parent;
            /*if (parentRbn == null)
                break;*
        }

        if (parentRbn is Ribbon ribbon)
            return ribbon;
        else
            return null;*/
    }
}
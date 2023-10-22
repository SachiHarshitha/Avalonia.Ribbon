using Avalonia.Controls;

namespace AvaloniaUI.Ribbon.Helpers
{
    public static class RibbonControlExtensions
    {
        public static Ribbon GetParentRibbon(Control control)
        {
            return Avalonia.VisualTree.VisualExtensions.FindAncestorOfType<Ribbon>(control, true);
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
}
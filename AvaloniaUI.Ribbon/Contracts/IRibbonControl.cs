using AvaloniaUI.Ribbon.Models;

namespace AvaloniaUI.Ribbon.Contracts
{
    public interface IRibbonControl
    {
        RibbonControlSize Size
        {
            get;
            set;
        }

        RibbonControlSize MinSize
        {
            get;
            set;
        }

        RibbonControlSize MaxSize
        {
            get;
            set;
        }
    }
}
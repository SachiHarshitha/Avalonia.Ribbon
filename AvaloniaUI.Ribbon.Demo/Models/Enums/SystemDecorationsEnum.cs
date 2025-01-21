using System.ComponentModel;

namespace AvaloniaUI.Ribbon.Demo.Models.Enums
{
    public enum SystemDecorationsEnum
    {
        [Description("No decorations")] None,
        [Description("Window border without titlebar")] BorderOnly,
        [Description("Fully decorated (default)")] Full
    }
}
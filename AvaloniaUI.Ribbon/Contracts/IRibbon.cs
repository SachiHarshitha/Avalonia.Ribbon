namespace AvaloniaUI.Ribbon.Contracts;

public interface IRibbon : IKeyTipHandler
{
    public bool IsCollapsedPopupOpen { get; set; }

    public void Close();
}
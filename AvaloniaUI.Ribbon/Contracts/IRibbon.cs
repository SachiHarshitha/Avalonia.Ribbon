namespace AvaloniaUI.Ribbon.Contracts
{
    public interface IRibbon : IKeyTipHandler
    {
        public bool IsCollapsedPopupOpen { get; set; }

        public void Close();

        public void CycleTabs(bool forward);

        public object? SelectedItem { get; set; }

        public int SelectedIndex { get; set; }
    }
}
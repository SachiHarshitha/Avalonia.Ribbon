using Avalonia.Input;

namespace AvaloniaUI.Ribbon.Contracts
{
    public interface IKeyTipHandler
    {
        void ActivateKeyTips(Ribbon ribbon, IKeyTipHandler prev);

        bool HandleKeyTipKeyPress(Key key);
    }
}
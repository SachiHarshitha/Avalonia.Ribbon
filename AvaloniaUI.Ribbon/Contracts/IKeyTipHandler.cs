using Avalonia.Input;

namespace AvaloniaUI.Ribbon.Contracts
{
    public interface IKeyTipHandler
    {
        void ActivateKeyTips(IRibbon ribbon, IKeyTipHandler prev);

        bool HandleKeyTipKeyPress(Key key);
    }
}
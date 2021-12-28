using TatmanGames.ScreenUI.Interfaces;
using ILogger = TatmanGames.ScreenUI.Interfaces.ILogger;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// simple "service locator" for UI specific injectables
    /// </summary>
    public class UIServiceLocator
    {
        public IDialogEvents DialogEvents { get; set; }
        public IKeyboardHandler KeyboardHandler { get; set; }
        public ILogger Logger { get; set; }
        public IPopupHandler PopupHandler { get; set; }
        public IPopupEventsManager PopupEventsManager { get; set; }
        public static UIServiceLocator Instance { get; private set; }

        static UIServiceLocator()
        {
            UIServiceLocator.Instance = new UIServiceLocator();
        }
    }
}
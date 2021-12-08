using TatmanGames.ScreenUI.Interfaces;
using UnityEngine;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// As is, not sure if this class belongs here as its specific to the test scene more than something reusable
    /// </summary>
    public class GlobalKeyboardHandler : IKeyboardHandler
    {
        private GameObject dialog = null;

        public GlobalKeyboardHandler(GameObject dialog)
        {
            this.dialog = dialog;
        }
        
        public virtual bool HandleKeyPress(KeyCode c)
        {
            switch (c)
            {
                case KeyCode.B:
                    ServiceLocator.Instance.PopupHandler?.ShowDialog(dialog);
                    return true;
                    break;
                case KeyCode.Escape:
                    ServiceLocator.Instance.PopupHandler?.CloseDialog();
                    return true;
                    break;
                default:
                    break;
            }
            return false;
        }
    }
}
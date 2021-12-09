using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;


namespace TatmanGames.ScreenUI.Keyboard
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
        
        public virtual bool HandleKeyPress()
        {
            bool handled = false;
            IPopupHandler popupHandler = ServiceLocator.Instance.PopupHandler;
            if (Input.GetKeyDown(KeyCode.B))
            {
                popupHandler?.ShowDialog(dialog);
                handled = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popupHandler?.CloseDialog();
                handled = true;
            }

            return handled;
        }
    }
}
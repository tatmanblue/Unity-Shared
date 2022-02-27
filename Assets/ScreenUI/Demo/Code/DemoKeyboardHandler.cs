using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;


namespace TatmanGames.ScreenUI.Demo
{
    /// <summary>
    /// demonstrates implementing IKeyboardHandler
    /// </summary>
    public class DemoKeyboardHandler : IKeyboardHandler
    {
        private GameObject dialog = null;
        private GameObject popup = null;

        public DemoKeyboardHandler(GameObject dialog, GameObject popup)
        {
            this.dialog = dialog;
            this.popup = popup;
        }
        
        public virtual bool HandleKeyPress()
        {
            IPopupHandler popupHandler = GlobalServicesLocator.Instance.GetService<IPopupHandler>();
            TatmanGames.ScreenUI.Interfaces.ILogger logger = GlobalServicesLocator.Instance.GetService<TatmanGames.ScreenUI.Interfaces.ILogger>();
            if (Input.GetKeyDown(KeyCode.D))
            {
                popupHandler?.ShowDialog(dialog);
                return true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popupHandler?.CloseDialog();
                return true;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                popupHandler?.ShowPopup(popup);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                logger?.Log("setting resolution to 3840 x 2160");
                Screen.SetResolution(3840, 2160, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                logger?.Log("setting resolution to 1920 x 1080");
                Screen.SetResolution(1920, 1080, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                logger?.Log("setting resolution to 1024 x 768");
                Screen.SetResolution(1024, 768, true);
                return true;
            }
            
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit(0);
                return true;
            }

            return false;
        }
    }
}
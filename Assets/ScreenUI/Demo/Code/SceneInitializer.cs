using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.Keyboard;
using TatmanGames.ScreenUI.UI;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.ScreenUI.Demo
{
    /// <summary>
    /// Demonstrates how to set up the ScreenUI system
    /// </summary>

    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject gameMenuDialog;
        [SerializeField] private GameObject settingsDialog;
        [SerializeField] private GameObject toolbarPopup;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void Start()
        {
            var dialogEvents = new PopupEventsManager();
            
            ServicesLocator services = GlobalServicesLocator.Instance;
            services.AddService<IPopupHandler>(new PopupHandler(dialogEvents));
            services.AddService<IPopupEventsManager>(dialogEvents);
            services.AddService<IDialogEvents>(dialogEvents);
            services.AddService<IKeyboardHandler>(new DemoKeyboardHandler(gameMenuDialog, toolbarPopup));
            services.AddService<TatmanGames.Common.Interfaces.ILogger>(new DebugLogging());
            
            IPopupHandler popupHandler = GlobalServicesLocator.Instance.GetService<IPopupHandler>();
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
        }

        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            if ("quit" == buttonId)
                GlobalServicesLocator.Instance.GetService<IPopupHandler>()?.CloseDialog();
            else if ("settings" == buttonId && settingsDialog != null)
                GlobalServicesLocator.Instance.GetService<IPopupHandler>()?.ReplaceDialog(settingsDialog);
            else
                GlobalServicesLocator.Instance.GetService<ILogger>()?.LogWarning($"dialog command {buttonId} for dialog {dialogName} not handled.");
            
            return false;
        }

        private void Update()
        {
            GlobalServicesLocator.Instance.GetService<IKeyboardHandler>()?.HandleKeyPress();
        }
    }

}

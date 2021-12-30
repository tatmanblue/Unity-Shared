using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.Keyboard;
using TatmanGames.ScreenUI.Scene;
using TatmanGames.ScreenUI.UI;

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
            
            // TODO:  these should be dynamically determined
            // TODO: or this scene initializer should not be part of the "distribution" but an example
            UIServiceLocator.Instance.PopupHandler = new PopupHandler(dialogEvents);
            UIServiceLocator.Instance.PopupEventsManager = dialogEvents;
            UIServiceLocator.Instance.DialogEvents = dialogEvents;
            UIServiceLocator.Instance.KeyboardHandler = new DemoKeyboardHandler(gameMenuDialog, toolbarPopup);
            UIServiceLocator.Instance.Logger = new DebugLogging();
            
            IPopupHandler popupHandler = UIServiceLocator.Instance.PopupHandler;
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
        }

        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            if ("quit" == buttonId)
                UIServiceLocator.Instance.PopupHandler.CloseDialog();
            else if ("settings" == buttonId && settingsDialog != null)
                UIServiceLocator.Instance.PopupHandler.ReplaceDialog(settingsDialog);
            else
                UIServiceLocator.Instance.Logger.LogWarning($"dialog command {buttonId} for dialog {dialogName} not handled.");
            
            return false;
        }

        private void Update()
        {
            UIServiceLocator.Instance.KeyboardHandler.HandleKeyPress();
        }
    }

}

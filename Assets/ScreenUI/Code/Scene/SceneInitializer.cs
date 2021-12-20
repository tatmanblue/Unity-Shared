using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.Keyboard;
using TatmanGames.ScreenUI.UI;

namespace TatmanGames.ScreenUI.Scene
{
    /// <summary>
    /// TODO: is part of the demo
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
            ServiceLocator.Instance.PopupHandler = new PopupHandler(dialogEvents);
            ServiceLocator.Instance.PopupEventsManager = dialogEvents;
            ServiceLocator.Instance.DialogEvents = dialogEvents;
            ServiceLocator.Instance.KeyboardHandler = new GlobalKeyboardHandler(gameMenuDialog, toolbarPopup);
            ServiceLocator.Instance.Logger = new DebugLogging();
            
            IPopupHandler popupHandler = ServiceLocator.Instance.PopupHandler;
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
            
            dialogEvents.OnButtonPressed += DialogEventsOnButtonPressed;
        }

        private bool DialogEventsOnButtonPressed(string dialogName, string buttonId)
        {
            if ("quit" == buttonId)
                ServiceLocator.Instance.PopupHandler.CloseDialog();
            else if ("settings" == buttonId && settingsDialog != null)
                ServiceLocator.Instance.PopupHandler.ReplaceDialog(settingsDialog);
            else
                ServiceLocator.Instance.Logger.LogWarning($"dialog command {buttonId} for dialog {dialogName} not handled.");
            
            return false;
        }

        private void Update()
        {
            ServiceLocator.Instance.KeyboardHandler.HandleKeyPress();
        }
    }

}

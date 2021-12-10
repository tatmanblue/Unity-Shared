using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.Keyboard;
using TatmanGames.ScreenUI.UI;
using UnityEngine.UIElements;

namespace TatmanGames.ScreenUI.Scene
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject dialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        private void Start()
        {
            // TODO:  these should be dynamically determined
            // TODO: or this scene initializer should not be part of the "distribution" but an example
            ServiceLocator.Instance.PopupHandler = new PopupHandler();
            var dialogEvents = new PopupEventsManager();
            ServiceLocator.Instance.PopupEventsManager = dialogEvents;
            ServiceLocator.Instance.DialogEvents = dialogEvents;
            ServiceLocator.Instance.KeyboardHandler = new GlobalKeyboardHandler(dialog);
            
            IPopupHandler popupHandler = ServiceLocator.Instance.PopupHandler;
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
            
            dialogEvents.OnButtonPressed += DialogEventsOnOnButtonPressed;
        }

        private bool DialogEventsOnOnButtonPressed(string dialogName, string buttonId)
        {
            Debug.LogWarning($"button clicked {dialogName}/{buttonId}");
            return false;
        }

        private void Update()
        {
            ServiceLocator.Instance.KeyboardHandler.HandleKeyPress();
        }
    }

}

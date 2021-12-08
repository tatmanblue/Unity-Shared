using System.Collections;
using System.Collections.Generic;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;
using UnityEngine;

namespace TatmanGames.ScreenUI.Scene
{
    public class SceneInitializer : MonoBehaviour
    {
        private IPopupHandler popupHandler = null;
        [SerializeField] private GameObject dialog;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip openSound;
        [SerializeField] private AudioClip closeSound;

        void Start()
        {
            // TODO:  these should be dynamically determined
            // TODO: or this scene initializer should not be part of the "distribution" but an example
            ServiceLocator.Instance.PopupHandler = new PopupHandler();
            ServiceLocator.Instance.PopupEventsManager = new PopupEventsManager();
            ServiceLocator.Instance.KeyboardHandler = new GlobalKeyboardHandler(dialog);
            
            popupHandler = ServiceLocator.Instance.PopupHandler;
            popupHandler.Canvas = GetComponent<Canvas>();
            popupHandler.AudioSource = audioSource;
            popupHandler.OpenSound = openSound;
            popupHandler.CloseSound = closeSound;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                popupHandler.ShowDialog(dialog);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                popupHandler.CloseDialog();
            }
        }
    }
}

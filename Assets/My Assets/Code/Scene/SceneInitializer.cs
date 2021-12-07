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
            ServiceLocator.Instance.PopupHandler = new PopupHandler();
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

using System.Collections;
using System.Collections.Generic;
using TatmanGames.ScreenUI.Interfaces;
using TatmanGames.ScreenUI.UI;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private IPopupHandler popupHandler = null;
    public GameObject dialog;
    void Start()
    {
        ServiceLocator.Instance.PopupHandler = new PopupHandler();
        ServiceLocator.Instance.PopupHandler.Canvas = GetComponent<Canvas>();
        popupHandler = ServiceLocator.Instance.PopupHandler;
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

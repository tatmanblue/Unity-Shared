using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.ScreenUI
{
    public enum PopupEvents
    {
        DialogOpened,
        DialogClosed,
        PopupOpened,
        PopClosed
    }
    
    public delegate void DialogEvent(PopupEvents popupEvent, string dialogName);

    public interface IPopupEventsManager
    {
        event DialogEvent OnDialogEvent;
        void FireDialogOpenEvent(string dialogName);
        void FireDialogCloseEvent(string dialogName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.ScreenUI.Interfaces
{
    public enum PopupEvents
    {
        DialogOpened,
        DialogClosed,
        PopupOpened,
        PopClosed
    }
    
    public delegate void DialogEvent(PopupEvents popupEvent, string dialogName);

    public delegate bool DialogButtonEvent(string dialogName, string buttonId);

    /// <summary>
    /// sources interested in changes in dialogs, popups and their behaviors can
    /// subscribe to these events for notification.  See ServiceLocator
    /// </summary>
    public interface IDialogEvents
    {
        /// <summary>
        /// see PopupEvents enum
        /// </summary>
        event DialogEvent OnDialogEvent;
        /// <summary>
        /// a button an a dialog has been pressed aka clicked
        /// </summary>
        event DialogButtonEvent OnButtonPressed;
    }

    /// <summary>
    /// Handler for firing off Dialog open/closed events as well as button click events
    /// in a dialog.
    ///
    /// Use DialogHelper to find IPopupEventsManager and fire off button clicks (pressed)
    /// </summary>
    public interface IPopupEventsManager
    {
        void FireDialogOpenEvent(string dialogName);
        void FireDialogCloseEvent(string dialogName);
        void FireButtonPressedEvent(string dialogName, string buttonId);
    }
}

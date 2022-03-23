using TatmanGames.ScreenUI.Interfaces;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class PopupEventsManager : IPopupEventsManager, IDialogEvents
    {
        public event DialogEvent OnDialogEvent;
        public event DialogButtonEvent OnButtonPressed;
        
        public void FireDialogOpenEvent(string dialogName)
        {
            DialogEvent events = OnDialogEvent;
            if (null != events)
                events(PopupEvents.DialogOpened, dialogName);
        }

        public void FireDialogCloseEvent(string dialogName)
        {
            DialogEvent events = OnDialogEvent;
            if (null != events)
                events(PopupEvents.DialogClosed, dialogName);
        }

        public void FireButtonPressedEvent(string dialogName, string buttonId)
        {
            DialogButtonEvent events = OnButtonPressed;
            if (null != events)
                events(dialogName, buttonId);
        }
    }
}
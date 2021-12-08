namespace TatmanGames.ScreenUI.UI
{
    public class PopupEventsManager : IPopupEventsManager
    {
        public event DialogEvent OnDialogEvent;
        
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
    }
}
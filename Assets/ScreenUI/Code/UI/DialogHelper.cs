using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// TODO: is part of the demo
    /// Attach this to UI so that dialog events can be sent out as C# events
    /// </summary>
    public class DialogHelper : MonoBehaviour
    {
        public void DoButtonClick(string buttonId)
        {
            ServiceLocator.Instance.PopupEventsManager?.FireButtonPressedEvent(GetDialogName(this.gameObject), buttonId);
        }
        
        private string GetDialogName(GameObject which)
        {
            string dlgName = which.name;
            int indexOf = dlgName.IndexOf("(Clone)");
            return dlgName.Remove(indexOf);
        }
    }
}

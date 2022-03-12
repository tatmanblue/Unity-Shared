using System.Collections;
using System.Collections.Generic;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.ScreenUI.Interfaces;
using UnityEngine;

namespace TatmanGames.ScreenUI.UI
{
    /// <summary>
    /// Attach this to UI so that dialog events can be sent out as C# events
    /// </summary>
    public class DialogHelper : MonoBehaviour
    {
        public void DoButtonClick(string buttonId)
        {
            IPopupEventsManager service = GlobalServicesLocator.Instance.GetService<IPopupEventsManager>();
            service?.FireButtonPressedEvent(GetDialogName(this.gameObject), buttonId);
        }
        
        private string GetDialogName(GameObject which)
        {
            string dlgName = which.name;
            int indexOf = dlgName.IndexOf("(Clone)");
            if (0 >= indexOf)
                return dlgName;
            return dlgName.Remove(indexOf);
        }
    }
}

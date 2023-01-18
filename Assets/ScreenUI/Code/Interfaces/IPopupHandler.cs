using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.ScreenUI.Interfaces
{
    public interface IPopupHandler
    {
        bool KeepWorldSpace { get; set; }
        bool IsDialogActive { get; }
        AudioSource AudioSource { get; set; }
        AudioClip OpenSound { get; set; }
        AudioClip CloseSound { get; set; }
        Canvas Canvas { get; set; }
        Color BackgroundColor { get; set; }
        GameObject ShowDialog(GameObject dialog);
        GameObject ShowPopup(GameObject popup);
        GameObject ReplaceDialog(GameObject dialog);
        void CloseDialog();
    }
}
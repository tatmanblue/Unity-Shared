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
        void ShowDialog(GameObject dialog);
        void ShowPopup(GameObject popup);
        void ReplaceDialog(GameObject dialog);
        void CloseDialog();
    }
}
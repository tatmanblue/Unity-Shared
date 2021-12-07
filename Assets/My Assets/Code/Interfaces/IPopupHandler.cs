using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TatmanGames.ScreenUI.Interfaces
{
    public interface IPopupHandler
    {
        bool IsDialogActive { get; }
        AudioSource AudioSource { get; set; }
        AudioClip OpenSound { get; set; }
        AudioClip CloseSound { get; set; }
        Canvas Canvas { get; set; }
        Color BackgroundColor { get; set; }
        void ShowDialog(GameObject dialog);
        void CloseDialog();
    }
}
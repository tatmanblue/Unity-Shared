using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TatmanGames.ScreenUI.Interfaces;

namespace TatmanGames.ScreenUI.UI
{
    public class ServiceLocator
    {
        public IPopupHandler PopupHandler { get; set; }
        public static ServiceLocator Instance { get; private set; }

        static ServiceLocator()
        {
            ServiceLocator.Instance = new ServiceLocator();
        }
    }
}
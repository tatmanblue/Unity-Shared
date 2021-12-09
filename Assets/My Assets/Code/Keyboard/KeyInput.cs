using System;
using UnityEngine;

namespace TatmanGames.ScreenUI.Keyboard
{
    public delegate Action Action();
    public delegate Action Action<T>(T t);
        
    /// <summary>
    /// 
    /// </summary>
    public class KeyInput
    {
        public KeyCode Key { get; set; }
        public Action Execute { get; set; }
    }
}
using System;
using UnityEngine;

namespace TatmanGames.ScreenUI.Keyboard
{
    public delegate Action Action(KeyCode k);
    public delegate Action Action<T>(KeyCode k, T t);
    public delegate Action Action<T, Z>(KeyCode k, T t, Z z);
    
    /// <summary>
    /// 
    /// </summary>
    public class KeyInput
    {
        public KeyCode Key { get; set; }
        public Action Execute { get; set; }
    }
    
    public class KeyInput<T>
    {
        public KeyCode Key { get; set; }
        public Action<T> Execute { get; set; }
    }
    
    public class KeyInput<T, Z>
    {
        public KeyCode Key { get; set; }
        public Action<T, Z> Execute { get; set; }
    }
}
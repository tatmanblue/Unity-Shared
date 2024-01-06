using System;

namespace TatmanGames.DebugUI
{
    /// <summary>
    /// Please read: https://github.com/tatmanblue/Unity-Shared/blob/main/DEBUG-UI.md#creating-your-own-commands
    ///
    /// Add this attribute to any class that implements IDebugCommand and the engine
    /// will automatically load this command on initialization
    /// </summary>
    public class CommandAutoLoadAttribute : Attribute
    {
        public string Word { get; set; }
        public string Description { get; set; }
    }
}
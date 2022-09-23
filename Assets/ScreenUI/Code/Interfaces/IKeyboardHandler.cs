using UnityEngine;

namespace TatmanGames.ScreenUI.Interfaces
{
    /// <summary>
    /// Allows for separation of keyboard handling from GameObject, which in turn,
    /// allows for dynamic injection/substitution of keyboard handlers during game play
    /// </summary>
    public interface IKeyboardHandler
    {
        bool HandleKeyPress();
    }
}
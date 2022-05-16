namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// Injects PlayerPositionHandler onto the PlayerObject
    ///
    /// The PlayerPositionHandler allows for moving the player object around
    /// </summary>
    public class PlayerPositionCommand :  DebugCommand
    {
        public PlayerPositionCommand() : base()
        {
            Word = "PlayerPos";
            Description = "Manipulates player positions. ";

        }
    }
}
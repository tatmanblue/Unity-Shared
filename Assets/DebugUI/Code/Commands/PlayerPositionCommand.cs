namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// Injects PlayerPositionHandler onto the PlayerObject
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
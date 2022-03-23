using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// TODO: this isnt fully implemented yet
    /// </summary>
    public class DebugLogging :  DebugCommand
    {
        public DebugLogging() : base()
        {
            Word = "DebugLog";
            Description = "Replaces IDebugger with DebugLogging and output does to unity console";
        }
    }
}
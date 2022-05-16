using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DebugLoggingCommand :  DebugCommand
    {
        public DebugLoggingCommand() : base()
        {
            Word = "DebugLog";
            Description = "Replaces IDebugger with DebugLogging and output does to unity console";
        }
    }
}
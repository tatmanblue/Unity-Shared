namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// TODO: this isn't fully implemented yet
    /// </summary>
    public class NoLoggingCommand : DebugCommand
    {
        public NoLoggingCommand() : base()
        {
            Word = "NoLog";
            Description = "Replaces IDebugger with NoLogging thereby eliminating all debug logs";
        }

    }
}
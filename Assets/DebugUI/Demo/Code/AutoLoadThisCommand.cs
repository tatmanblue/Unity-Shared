using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI.Demo
{
    [CommandAutoLoad(Word = "Auto", Description = "Shows how autoload works")]
    public class AutoLoadThisCommand : DebugCommand
    {
        public AutoLoadThisCommand() : base()
        {
            Word = "Auto";
            Description = "Shows how autoload works";
            OnCommand += OnOnCommand;
        }

        private string OnOnCommand(string[] args)
        {
            return "auto command executed";
        }        
    }
}
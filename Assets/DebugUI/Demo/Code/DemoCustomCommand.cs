namespace TatmanGames.DebugUI.Demo
{
    public class DemoCustomCommand : DebugCommand
    {
        public DemoCustomCommand() : base()
        {
            Word = "player";
            Description = "shows how to create a custom command";
            OnCommand += OnOnCommand;
        }

        private string OnOnCommand(string[] args)
        {
            return "player command processed";
        }
    }
}
using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI
{
    [System.Serializable]
    public abstract class DebugCommand : IDebugCommand
    {
        public string Word { get; protected set; }
        public string Description { get; protected set; }
        public event ExecuteCommandEvent OnCommand;
        public bool HasCommandHandler()
        {
            ExecuteCommandEvent commandEvent = OnCommand;
            if (commandEvent != null)
                return true;

            return false;
        }

        public string FireCommandHandler(string[] elements)
        {
            string result = string.Empty;
            ExecuteCommandEvent commandEvent = OnCommand;
            if (commandEvent != null)
                result = commandEvent(elements);

            return result;
        }
    }

    public class GenericCommand : DebugCommand
    {
        public GenericCommand(string word, string desc) : base()
        {
            Word = word;
            Description = desc;
        }
    }
    
}
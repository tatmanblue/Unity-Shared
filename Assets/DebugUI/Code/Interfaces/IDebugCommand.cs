using UnityEngine;

namespace TatmanGames.DebugUI.Interfaces
{
    public delegate string ExecuteCommandEvent(string[] args);
    
    /// <summary>
    /// 
    /// </summary>
    public interface IDebugCommand
    {
        string Word { get; }
        string Description { get;  }

        event ExecuteCommandEvent OnCommand;

        bool HasCommandHandler();
        string FireCommandHandler(string[] elemetns);
    }

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
}
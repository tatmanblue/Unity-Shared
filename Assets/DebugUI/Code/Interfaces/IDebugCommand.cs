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
    
}
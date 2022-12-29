using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TatmanGames.Common.Scene
{
    /// <summary>
    /// a logger to unity debug window
    /// TODO not sure we need this, there is unity logging, need to look into it.
    /// </summary>
    public class DebugLogging : TatmanGames.Common.Interfaces.ILogger
    {
        public void Debug(string statement)
        {
            UnityEngine.Debug.Log(statement);    
        }
        
        public void LogWarning(string statement)
        {
            UnityEngine.Debug.LogWarning(statement);
        }

        public void Log(string statement)
        {
            UnityEngine.Debug.Log(statement);
        }
    }
}
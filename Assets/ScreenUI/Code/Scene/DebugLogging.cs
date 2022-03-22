using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace TatmanGames.ScreenUI.Scene
{
    public class DebugLogging : TatmanGames.Common.Interfaces.ILogger
    {
        public void LogWarning(string statement)
        {
            Debug.LogWarning(statement);
        }

        public void Log(string statement)
        {
            Debug.Log(statement);
        }
    }
}
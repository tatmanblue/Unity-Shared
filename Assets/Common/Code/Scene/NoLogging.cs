using UnityEngine;

namespace TatmanGames.Common.Scene
{
    /// <summary>
    /// Logging is sorta disabled with this implementation
    /// </summary>
    public class NoLogging : TatmanGames.Common.Interfaces.ILogger
    {
        public NoLogging()
        {
            Debug.LogWarning("NoLogging Enabled");
        }

        public void LogWarning(string statement) {}

        public void Log(string statement) {}
    }
}
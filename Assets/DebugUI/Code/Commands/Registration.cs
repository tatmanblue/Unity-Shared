using UnityEngine;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI.Interfaces;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// Automatic registration of commands in this library
    /// </summary>
    public class Registration
    {
        public static void Initialize()
        {
            IDebugEngine engine = null;
            try
            {
                engine = GlobalServicesLocator.Instance.GetService<IDebugEngine>();
            }
            catch
            {
                engine = new CommandEngine();
                GlobalServicesLocator.Instance.AddService<IDebugEngine>(engine);
            }

            IDebugCommand debugLogCommand = new DebugLoggingCommand();
            IDebugCommand noLogCommand = new NoLoggingCommand();
            IDebugCommand playerPositionCommand = new PlayerPositionCommand();
            
            // TODO: there is going to be a point where we do not want this in the application 
            // TODO: such as release mode
            engine.AddCommand(debugLogCommand);
            engine.AddCommand(noLogCommand);
            engine.AddCommand(playerPositionCommand);
            
            debugLogCommand.OnCommand += OnDebugLogCommand;
            noLogCommand.OnCommand += OnNoLogCommand;
            
            (playerPositionCommand as PlayerPositionCommand).RegisterSelfHandler();
        }

        private static string OnNoLogCommand(string[] args)
        {
            GlobalServicesLocator.Instance.AddReplaceService<ILogger>(new NoLogging());
            return "Logging turned off";
        }

        private static string OnDebugLogCommand(string[] args)
        {
            GlobalServicesLocator.Instance.AddReplaceService<ILogger>(new Common.Scene.DebugLogging());
            return "Logging to Unity console";
        }
    }
}
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI.CommandSupport;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;
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
            playerPositionCommand.OnCommand += OnPlayerPositionCommand;
        }

        private static string OnPlayerPositionCommand(string[] args)
        {

            if (1 == args?.Length)      // dirty assumption 1st element is PlayerPos2
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (null == player)
                    return "There is no player object";
                
                if (null != player.GetComponent<PlayerPositionHandler>())
                    return "PositionHandler exists";
                
                player.AddComponent<PlayerPositionHandler>();
                return "PositionHandler added";
            }

            return "PositionHandler command not understood";
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
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI.Commands
{
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

            IDebugCommand debugLogCommand = new DebugLogging();
            IDebugCommand noLogCommand = new NoLoggingCommand();
            
            engine.AddCommand(debugLogCommand);
            engine.AddCommand(noLogCommand);
            
            debugLogCommand.OnCommand += DebugLogCommandOnOnCommand;
            noLogCommand.OnCommand += NoLogCommandOnOnCommand;
        }

        private static string NoLogCommandOnOnCommand(string[] args)
        {
            GlobalServicesLocator.Instance.AddReplaceService<ILogger>(new NoLogging());
            return "Logging turned off";
        }

        private static string DebugLogCommandOnOnCommand(string[] args)
        {
            GlobalServicesLocator.Instance.AddReplaceService<ILogger>(new Common.Scene.DebugLogging());
            return "Logging to Unity console";
        }
    }
}
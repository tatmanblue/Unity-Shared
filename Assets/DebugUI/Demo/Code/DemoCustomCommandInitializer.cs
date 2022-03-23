using System;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI.Commands;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.DebugUI.Demo
{
    public class DemoCustomCommandInitializer : MonoBehaviour
    {
        private void Start()
        {
            IDebugEngine engine = GlobalServicesLocator.Instance.GetService<IDebugEngine>();
            if (null == engine)
            {
                engine = new CommandEngine();
                GlobalServicesLocator.Instance.AddService(engine);
            }
            
            engine.AddCommand(new DemoCustomCommand());
            engine.OnStateChange += EngineOnStateChange;
            
            // initialize built in commands
            Registration.Initialize();
        }

        private void EngineOnStateChange(DebugCommandWindowState state)
        {
            ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
            logger?.Log($"Debug Console state is {state}");
        }
    }
}
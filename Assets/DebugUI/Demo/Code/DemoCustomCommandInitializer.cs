using System;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI.Commands;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.DebugUI.Demo
{
    public class DemoCustomCommandInitializer : MonoBehaviour
    {
        private ServicesLocator services;
        private void Start()
        {
            services = GlobalServicesLocator.Instance;
            services.AddReplaceService<ILogger>(new DebugLogging());
            IDebugEngine engine = services.GetService<IDebugEngine>();
            if (null == engine)
            {
                engine = new CommandEngine();
                services.AddService<IDebugEngine>(engine);
            }
            
            engine.AddCommand(new DemoCustomCommand());
            engine.OnStateChange += EngineOnStateChange;
            
            // initialize built in commands
            Registration.Initialize();
        }

        private void EngineOnStateChange(DebugCommandWindowState state)
        {
            ILogger logger = services.GetService<ILogger>();
            logger?.Log($"Debug Console state is {state}");
        }
    }
}
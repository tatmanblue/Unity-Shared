using System;
using UnityEngine;
using TatmanGames.Common;
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace Common.Demo.Code
{
    public class DemoButtonHandlers : MonoBehaviour
    {
        public void Start()
        {
            Log("DemoButtonHandlers is running");
        }

        public void Go()
        {
            Log("Starting GameTimeManager");
            GameTimeManager mgr = new GameTimeManager(10, 1);
            mgr.OnGameTimeInterval += OnGameTimeInterval;
            GlobalServicesLocator.Instance.AddReplaceService<IGameTimeManager>(mgr);
            mgr.Start();
        }
        
        public void Stop()
        {
            try
            {
                Log("Stopping GameTimeManager");
                IGameTimeManager gameTimeManager = GlobalServicesLocator.Instance.GetService<IGameTimeManager>();
                gameTimeManager.Stop();
            }
            catch (ServiceLocatorException slex)
            {
                // nothing to do
            }

        }
        
        private void OnGameTimeInterval(GameTimeIntervalUpdate data)
        {
            Log($"got message from GameTimeManager");
        }

        private void Log(string message)
        {
            try
            {
                GlobalServicesLocator.Instance.GetService<ILogger>()?.Log(message);
            }
            catch (ServiceLocatorException slex)
            {
                Console.WriteLine($"{message} - no logger defined");
            }
        }
    }
}
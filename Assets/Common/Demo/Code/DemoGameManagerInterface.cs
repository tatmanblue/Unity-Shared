using System;
using UnityEngine;
using TatmanGames.Common;
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using ILogger = TatmanGames.Common.Interfaces.ILogger;


namespace Common.Demo.Code
{
    public class DemoGameManagerInterface : MonoBehaviour
    {
        private void Start()
        {
            GlobalServicesLocator.Instance.AddService<ILogger>(new DebugLogging());
            Log("DemoGameManagerInterface started");
        }

        private void Update()
        {
            try
            {
                IGameTimeManager gameTimeManager = GlobalServicesLocator.Instance.GetService<IGameTimeManager>();
                gameTimeManager.Update();
            }
            catch (ServiceLocatorException)
            {
                // nothing to do
            }
        }
        
        private void Log(string message)
        {
            try
            {
                GlobalServicesLocator.Instance.GetService<ILogger>()?.Log(message);
            }
            catch (ServiceLocatorException)
            {
                Debug.Log($"{message} - no logger defined");
            }
        }
    }
}
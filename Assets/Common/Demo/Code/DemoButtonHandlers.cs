using System;
using UnityEngine;
using TatmanGames.Common;
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TMPro;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace Common.Demo.Code
{
    public class DemoButtonHandlers : MonoBehaviour
    {
        [SerializeField] private TMP_InputField Interval;
        [SerializeField] private TMP_InputField NotificationsPerInterval;
        
        public void Start()
        {
            Log("DemoButtonHandlers is running");
        }

        public void Go()
        {
            Log("Starting GameTimeManager");
            int interval = GetInterval();
            int notificationRate = GetIntervalNotificationRate();
            GameTimeManager mgr = new GameTimeManager(interval, notificationRate);
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

        private int GetInterval()
        {
            return Convert.ToInt32(Interval.text);
        }

        private int GetIntervalNotificationRate()
        {
            return Convert.ToInt32(NotificationsPerInterval.text);
        }
    }
}
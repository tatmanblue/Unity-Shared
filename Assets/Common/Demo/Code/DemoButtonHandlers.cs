using System;
using UnityEngine;
using TatmanGames.Common;
using TatmanGames.Common.Interfaces;
using TatmanGames.Common.Scene;
using TatmanGames.Common.ServiceLocator;
using TMPro;
using UnityEngine.Serialization;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace Common.Demo.Code
{
    public class DemoButtonHandlers : MonoBehaviour
    {
        [SerializeField] private TMP_InputField Interval;
        [SerializeField] private TMP_InputField HeartbeatsPerInterval;
        [SerializeField] private TextMeshProUGUI HeartbeatHelpLabel;

        
        public void Start()
        {
            Log("DemoButtonHandlers is running");
            Interval.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
            HeartbeatsPerInterval.onValueChanged.AddListener(delegate(string arg0) {ValueChangeCheck();  });

            // do this on start to get initial message
            ValueChangeCheck();
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
            catch (ServiceLocatorException)
            {
                // nothing to do
            }

        }

        public void Pause()
        {
            try
            {
                Log("Pausing GameTimeManager");
                IGameTimeManager gameTimeManager = GlobalServicesLocator.Instance.GetService<IGameTimeManager>();
                gameTimeManager.Pause();
            }
            catch (ServiceLocatorException)
            {
                // nothing to do
            }
        }
        
        public void Resume()
        {
            try
            {
                Log("Resume GameTimeManager");
                IGameTimeManager gameTimeManager = GlobalServicesLocator.Instance.GetService<IGameTimeManager>();
                gameTimeManager.Resume();
            }
            catch (ServiceLocatorException)
            {
                // nothing to do
            }
        }
        
        // Invoked when the value of the text field changes.
        private void ValueChangeCheck()
        {
            int interval = GetInterval();
            int notificationRate = GetIntervalNotificationRate();

            string message;
            // theres and equal number of heartbeats in interval
            if (interval % notificationRate == 0)
            {
                message = $"There will be one heartbeat every {interval / notificationRate} seconds";
            }
            else
            {
                message = "There are uneven qty of heartbeats for the game day length. The last heartbeat will have a different time span in a given day";
            }

            HeartbeatHelpLabel.text = message;
        }
        
        private void OnGameTimeInterval(GameTimeIntervalUpdate data)
        {
            Log($"got message from GameTimeManager. state {data.State} type {data.EventType}");
        }

        private void Log(string message)
        {
            try
            {
                GlobalServicesLocator.Instance.GetService<ILogger>()?.Log(message);
            }
            catch (ServiceLocatorException)
            {
                Console.WriteLine($"{message} - no logger defined");
            }
        }

        private int GetInterval()
        {
            try
            {
                return Convert.ToInt32(Interval.text);
            }
            catch (Exception)
            {
                // returning 1 to avoid math errors in this class
                return 1;
            }
        }

        private int GetIntervalNotificationRate()
        {
            try
            {
                return Convert.ToInt32(HeartbeatsPerInterval.text);
            }
            catch (Exception)
            {
                // returning 1 to avoid math errors in this class
                return 1;
            }
        }
    }
}
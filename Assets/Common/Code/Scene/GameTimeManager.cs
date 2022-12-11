using System;
using TatmanGames.Common.Interfaces;

namespace TatmanGames.Common.Scene
{
    /// <summary>
    /// Responsible for figuring out when an "Interval" has been reached
    ///
    /// An Interval is like Day or some concrete time span
    /// </summary>
    public class GameTimeManager : IGameTimeManager
    {
        /// <summary>
        /// EG a game day.  if its 60 then one game day occurs every 60 seconds
        /// </summary>
        public int IntervalInSeconds { get; private set; }
        /// <summary>
        /// how many OnGameTimeInterval events will be fired per interval
        /// eg: if interval is 60 seconds and NotificationPerInterval is 10
        /// then OnGameTimeInterval will be fired every 6 seconds. if the number
        /// notifications cannot be equally divided into the interval, when the interval
        /// expires another event will be fired and the NotificationPerInterval restart
        /// </summary>
        public int NotificationPerInterval { get; private set; }

        public event GameTimeInterval OnGameTimeInterval;
        
        private int intervalId = 0;
        private int intervalNotificationId = 0;
        private bool trackingTime = false;
        private bool checkingTime = false;
        private DateTime startTime;
        private DateTime lastTime;

        public GameTimeManager(int intervalInSeconds, int notificationPerInterval)
        {
            IntervalInSeconds = intervalInSeconds;
            NotificationPerInterval = notificationPerInterval;
        }

        public void Start()
        {
            if (trackingTime == true)
                return;

            intervalId = 1;
            startTime = DateTime.Now;
            lastTime = startTime;
            trackingTime = true;
        }

        public void Pause()
        {
            trackingTime = false;
        }

        public void Resume()
        {
            startTime = new DateTime();
            trackingTime = true;
        }

        public void Stop()
        {
            trackingTime = false;
            FireGameTimeIntervalEvent(GameTimeManagerState.Stopped);
        }

        public void Update()
        {
            try
            {
                if (false == trackingTime)
                    return;
                
                if (true == checkingTime)
                    return;

                checkingTime = true;
                DateTime now = DateTime.Now;
                TimeSpan span = now - lastTime;

                // span.TotalSeconds / IntervalInSeconds tells us if an interval has transpired.
                // if its 0 then the event hasn't transpired yet.
                // if its > 1 the event has occurred
                int computedIntervalId = (int) Math.Floor(span.TotalSeconds / IntervalInSeconds);

                if (computedIntervalId >= 1)
                {
                    lastTime = now;
                    intervalId ++;
                    intervalNotificationId++;
                    // time to fire off an event
                    FireGameTimeIntervalEvent(GameTimeManagerState.Running);
                }
                
            }
            finally
            {
                checkingTime = false;
            }
        }

        private void FireGameTimeIntervalEvent(GameTimeManagerState state)
        {
            TimeSpan totalTime = lastTime - startTime;
            GameTimeIntervalUpdate data = new GameTimeIntervalUpdate((int) totalTime.TotalSeconds, intervalId, intervalNotificationId, state);
            GameTimeInterval interval = OnGameTimeInterval;

            if (null == interval) return;
            
            interval(data);
        }

    }
}
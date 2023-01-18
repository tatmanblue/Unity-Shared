using System;
using TatmanGames.Common.Interfaces;

namespace TatmanGames.Common.Scene
{
    /// <summary>
    /// Responsible for figuring out when an "Interval" has been reached
    ///
    /// An Interval is like Day or some concrete time span
    ///
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
        /// expires another event will be fired and the HeartbeatPerInterval restart
        /// </summary>
        public int HeartbeatPerInterval { get; private set; }

        public GameTimeIntervalUpdate MostRecentUpdate { get; private set; } = null;

        public event GameTimeInterval OnGameTimeInterval;
        
        private int intervalId = 0;
        private int intervalNotificationId = 0;
        private bool trackingTime = false;
        private bool checkingTime = false;
        private DateTime startTime;
        private DateTime lastIntervalTime;          // last time the game interval was noted
        private DateTime lastHeartbeatTime;         // last time the heartbeat was noted

        public GameTimeManager(int intervalInSeconds, int heartbeatPerInterval)
        {
            IntervalInSeconds = intervalInSeconds;
            HeartbeatPerInterval = heartbeatPerInterval;
        }

        public void Start()
        {
            if (trackingTime == true)
                return;

            intervalId = 1;
            startTime = DateTime.Now;
            lastIntervalTime = startTime;
            lastHeartbeatTime = startTime;
            trackingTime = true;
        }

        public void Pause()
        {
            trackingTime = false;
        }

        public void Resume()
        {
            // Need some work here. time is lost when the game time manager is paused between
            // intervals.  time can be "gained" as well if they track time
            // (such as mgt unit finishing work after a given amount of time) and are looking
            // at TotalSeconds value as the source of truth
            DateTime now = DateTime.Now;
            lastIntervalTime = now;
            lastHeartbeatTime = now;
            trackingTime = true;
        }

        public void Stop()
        {
            trackingTime = false;
            FireGameTimeIntervalEvent(GameTimeManagerState.Stopped, GameTimeEventType.StateChange);
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
                TimeSpan span = now - lastIntervalTime;

                // span.TotalSeconds / IntervalInSeconds tells us if an interval has transpired.
                // because we are using floor,
                // if its 0 then the event hasn't transpired yet.
                // if its >= 1 the event has occurred
                int computedIntervalId = (int) Math.Floor(span.TotalSeconds / IntervalInSeconds);

                if (computedIntervalId >= 1)
                {
                    lastHeartbeatTime = now;
                    lastIntervalTime = now;
                    intervalId ++;
                    intervalNotificationId++;
                    // time to fire off an event
                    FireGameTimeIntervalEvent(GameTimeManagerState.Running, GameTimeEventType.GameInterval);
                    return;
                }

                // logic is the same here for figuring out heartbeat interval
                // IntervalSeconds / HeartbeatPerInterval is the number of seconds between
                // Heartbeat events.
                int heartbeatSeconds = IntervalInSeconds / HeartbeatPerInterval;
                span = now - lastHeartbeatTime;
                computedIntervalId = (int) Math.Floor(span.TotalSeconds / heartbeatSeconds);
                if (computedIntervalId >= 1)
                {
                    lastHeartbeatTime = now;
                    intervalNotificationId++;
                    // time to fire off an event
                    FireGameTimeIntervalEvent(GameTimeManagerState.Running, GameTimeEventType.Heartbeat);
                }
                
            }
            finally
            {
                checkingTime = false;
            }
        }

        private void FireGameTimeIntervalEvent(GameTimeManagerState state, GameTimeEventType eventType)
        {
            TimeSpan totalTime = lastIntervalTime - startTime;
            GameTimeIntervalUpdate data = new GameTimeIntervalUpdate((int) totalTime.TotalSeconds, intervalId, intervalNotificationId, state);
            data.EventType = eventType;

            MostRecentUpdate = data;
            
            GameTimeInterval interval = OnGameTimeInterval;

            if (null == interval) return;
            
            interval(data);
        }

    }
}
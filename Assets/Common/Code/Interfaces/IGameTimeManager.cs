using TatmanGames.Common;

namespace TatmanGames.Common.Interfaces
{
    /// <summary>
    /// GameTime or game clock. designed to work in a number of different environments
    ///
    /// Terms:
    ///     Interval is some time space that is meaningful to the player
    ///     Expired means when time lapsed = Interval.
    ///     HeartBeat is a event fired after a fixed time lapse
    ///
    /// PLEASE NOTE:  in future version interface is going have breaking changes:
    ///      IntervalInSeconds -> KeyframeInSeconds
    ///      HeartbeatPerInterval -> FramesPerKeyFrame
    /// </summary>
    public interface IGameTimeManager
    {
        /// <summary>
        /// Interval is span of time
        /// should be something meaningful to the game player such as a game day
        /// 
        /// It is up to the game to convert the interval to something meaningful in the game
        /// </summary>
        int IntervalInSeconds { get; }
        /// <summary>
        /// the number of notifications that should be sent per Interval (above)
        /// Should round down if IntervalInSeconds Mod HeartbeatPerInterval != 0
        /// </summary>
        int HeartbeatPerInterval { get; }
        
        /// <summary>
        /// Allows a process that just started to get this most recent update
        /// Can be null if game time manager hasn't started yet
        /// </summary>
        GameTimeIntervalUpdate MostRecentUpdate { get; }

        /// <summary>
        /// Starts the game time clock, starting fresh thus should
        /// be expected to reset any previous values etc...
        /// </summary>
        void Start();
        
        /// <summary>
        /// puts a pause on the game time, call resume to continue.
        /// </summary>
        void Pause();

        /// <summary>
        /// resumes game time calculations after a Pause().  
        /// </summary>
        void Resume();

        /// <summary>
        /// Stops game time.  There should be one final OnGameTimeInterval fired by this event
        /// expectation is Start() would have to be called to start game clock again
        /// </summary>
        void Stop();

        /// <summary>
        /// Update called at some consistent interval such as
        /// from GameObject (monobehavior) OnUpdate method
        ///
        /// The implementation will compute how much time has passed since the last
        /// Update()
        /// </summary>
        void Update();

        /// <summary>
        /// Fired every HeartbeatPerInterval
        /// </summary>
        event GameTimeInterval OnGameTimeInterval;
    }
}
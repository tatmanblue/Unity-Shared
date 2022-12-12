namespace TatmanGames.Common
{
    public enum GameTimeManagerState
    {
        NotStarted,
        Running,
        Paused,
        Stopped
    }

    /// <summary>
    /// See IGameTimeManager for better understanding between GameInterval and Heartbeat
    /// </summary>
    public enum GameTimeEventType
    {
        Error,              // something bad happened with the game time manager, not likely to see this
        StateChange,        // meaning see GameTimeManagerState for more info
        GameInterval,
        Heartbeat
    }
}
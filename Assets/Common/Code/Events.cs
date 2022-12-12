namespace TatmanGames.Common
{
    public class GameTimeIntervalUpdate
    {
        public int TotalSeconds { get; protected internal set; }
        public int IntervalId { get; protected internal  set; }
        public int IntervalNotificationId { get; protected internal  set; }
        public GameTimeManagerState State { get; protected internal  set; }
        public GameTimeEventType EventType { get; protected internal set; }

        public GameTimeIntervalUpdate(int totalSeconds, int intervalId, int intervalNotificationId, GameTimeManagerState state)
        {
            TotalSeconds = totalSeconds;
            IntervalId = intervalId;
            IntervalNotificationId = intervalNotificationId;
            State = state;
        }
    }
    
    public delegate void GameTimeInterval(GameTimeIntervalUpdate data);
}
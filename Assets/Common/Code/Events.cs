namespace TatmanGames.Common
{
    public class GameTimeIntervalUpdate
    {
        public int TotalSeconds { get; private set; }
        public int IntervalId { get; private set; }
        public int IntervalNotificationId { get; private set; }
        public GameTimeManagerState State { get; private set; }

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
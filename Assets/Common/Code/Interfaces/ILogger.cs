namespace TatmanGames.Common.Interfaces
{
    /// <summary>
    /// this seems like its not needed and need to look into unity ILogger interface
    /// </summary>
    public interface ILogger
    {
        void Debug(string statement);
        void LogWarning(string statement);
        void Log(string statement);
    }
}
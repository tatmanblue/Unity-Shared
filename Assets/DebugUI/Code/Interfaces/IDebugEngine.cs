namespace TatmanGames.DebugUI.Interfaces
{
    public enum DebugCommandWindowState
    {
        Opened,
        Closed
    }
    public delegate void DebugCommandWindowStateChange(DebugCommandWindowState state);
    public interface IDebugEngine
    {
        /// <summary>
        /// Intended to find in the running domain types with the attribute
        /// CommandAutoLoadAttribute attached
        /// </summary>
        void DiscoverCommands();
        
        void AddCommand(IDebugCommand command);
        event DebugCommandWindowStateChange OnStateChange;
    }
}
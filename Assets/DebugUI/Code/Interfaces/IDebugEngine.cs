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
        void AddCommand(IDebugCommand command);
        event DebugCommandWindowStateChange OnStateChange;
    }
}
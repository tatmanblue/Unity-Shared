using TatmanGames.DebugUI.Interfaces;
    
namespace TatmanGames.DebugUI
{
    public class DebugServiceLocator
    {
        public IDebugEngine Engine { get; set; }
        public static DebugServiceLocator Instance { get; private set; } = new DebugServiceLocator();
    }
}
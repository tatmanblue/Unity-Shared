namespace TatmanGames.DebugUI
{
    /// <summary>
    /// Wrapper to make it easy to construct a command without having to implement
    /// DebugCommand.  Create an instance of this and assign handler to OnCommand
    /// </summary>
    public class GenericCommand : DebugCommand
    {
        public GenericCommand(string word, string desc) : base()
        {
            Word = word;
            Description = desc;
        }
    }
}
using System.Collections.Generic;
using TatmanGames.DebugUI.Interfaces;

namespace TatmanGames.DebugUI
{
    public delegate string GlobalCommandEvent(string command, string[] args);
    public class CommandEngine : IDebugEngine
    {
        public List<IDebugCommand> Commands { get; } = new List<IDebugCommand>();
        public event GlobalCommandEvent OnGlobalCommandEvent;

        public string HandleCommand(string input)
        {
            // input is something like "player moveto 9999"
            // the command is "player" and the arguments are "moveto" and "9999"
            string[] elements = input.Split(' ');

            IDebugCommand command = Commands.Find(c => 0 == c.Word.CompareTo(elements[0]));
            string result = string.Empty;
            
            // if the command has not been found in the list do nothing
            if (null == command)
                return $"no command registered: {elements[0]}";

            if (true == command.HasCommandHandler())
            {
                result = command.FireCommandHandler(elements);
            }
            else
            {
                // there is an assumption here that there is one and only one global handler
                // if someone has 2, the result is who knows what
                GlobalCommandEvent global = OnGlobalCommandEvent;
                if (null != global)
                    result = global(elements[0], elements);
            }

            return result;
        }

        public void AddCommand(IDebugCommand command)
        {
            Commands.Add(command);
        }
    }
}
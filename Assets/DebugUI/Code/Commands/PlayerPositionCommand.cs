using TatmanGames.DebugUI.CommandSupport;
using UnityEngine;

namespace TatmanGames.DebugUI.Commands
{
    /// <summary>
    /// Injects PlayerPositionHandler onto the PlayerObject
    ///
    /// The PlayerPositionHandler allows for moving the player object around
    /// </summary>
    public class PlayerPositionCommand :  DebugCommand
    {
        public PlayerPositionCommand() : base()
        {
            Word = "PlayerPos";
            Description = "Manipulates player positions. Type `PlayerPos help` for more information";
        }

        public void RegisterSelfHandler()
        {
            OnCommand += HandleOnCommand;
        }

        private string HandleOnCommand(string[] args)
        {
            if (1 == args?.Length)      // dirty assumption 1st element is PlayerPos2
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (null == player)
                    return "There is no player object";
                
                if (null != player.GetComponent<PlayerPositionHandler>())
                    return "PositionHandler exists";
                
                player.AddComponent<PlayerPositionHandler>();
                return "PositionHandler added";
            }

            if (2 == args?.Length)
                return HandleTwoParams(args);

            if (3 == args?.Length)
                return HandleThreeParams(args);

            return "PositionHandler command not understood";
        }

        private string HandleTwoParams(string[] args)
        {
            // making an assumption there are 2 args, first one is always going to be PlayerPos
            string arg1 = args[1];
            if ("help" == arg1)
            {
                return "PlayerPos arguments:  help, remove, jumpTo, useKeyboard";
            }
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (null == player)
                return "There is no player object";

            PlayerPositionHandler handler = player.GetComponent<PlayerPositionHandler>(); 
            if (null == handler)
                return "PositionHandler is not installed";

            if ("remove" == arg1)
            {
                GameObject.Destroy(player.GetComponent<PlayerPositionHandler>());
                return "PositionHandler removed";
            }

            if ("useKeyboard" == arg1)
            {
                bool useKeyboard = handler.UseKeyboardCommands;
                handler.UseKeyboardCommands = !useKeyboard;
                return $"PlayerHandler UseKeyboard = {!useKeyboard}";
            }
            
            return "PositionHandler command argument not understood";
        }

        private string HandleThreeParams(string[] args)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (null == player)
                return "There is no player object";
            PlayerPositionHandler handler = player.GetComponent<PlayerPositionHandler>(); 
            if (null == handler)
                return "PositionHandler is not installed";
            
            string arg1 = args[1];
            string arg2 = args[2];

            if ("jumpTo" == arg1)
            {
                if ("help" == arg2)
                {
                    return "PlayerPos jumpTo arguments:  home, next, closest";
                }
                
                if ("closest" == arg2)
                {
                    handler.SetJumpToClosest();
                    return "Jumping to closest point";
                }
                if ("home" == arg2)
                {
                    handler.SetJumpToHome();
                    return "Jumping to home";
                }
                if ("next" == arg2)
                {
                    handler.SetJumpToNext();
                    return "Jumping to next point";
                }
            }
            
            return "PositionHandler command argument not understood";
        }
    }
}
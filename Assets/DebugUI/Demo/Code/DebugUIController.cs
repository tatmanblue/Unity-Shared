using System.Collections;
using System.Collections.Generic;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TatmanGames.DebugUI.Demo
{
    
    public class DebugUIController : MonoBehaviour
    {
        private CommandEngine _engine = new CommandEngine();

        [Header("UI Components")] 
        public KeyCode activationKey = KeyCode.BackQuote;
        public Canvas consoleCanvas;        // just the canvas
        public Text inputText;              // where commands are entered
        public Text consoleText;            // command output is placed here
        
        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            _engine.OnGlobalCommandEvent += EngineOnGlobalCommandEvent;
            AddGlobalCommands();
        }
        
        private void Update()
        {
            // on activationKey toggle if the debug window is visible or not
            if(Input.GetKeyDown(activationKey))
            {
                consoleCanvas.gameObject.SetActive(!consoleCanvas.gameObject.activeInHierarchy);
            }

            if(consoleCanvas.gameObject.activeInHierarchy)
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    if(inputText.text != "")
                    {
                        AddMessageToConsole(inputText.text);
                        string result = _engine.HandleCommand(inputText.text);
                        if (!string.IsNullOrEmpty(result))
                            AddMessageToConsole(result);
                        inputText.text = "";
                    }
                }
            }
        }
        
        private string EngineOnGlobalCommandEvent(string command, string[] args)
        {
            if (command.Equals("clear"))
            {
                consoleText.text = "";
                return string.Empty;
            }
            return $"registered command not handled: {command}";
        }

        private void AddGlobalCommands()
        {
            _engine.Commands.Add(new GenericCommand("help", "shows this help"));
            _engine.Commands.Add(new GenericCommand("clear", "clears console"));
            _engine.Commands.Add(new DemoCustomCommand());
        }
        
        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

    }
}
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
        public Canvas consoleCanvas;        // just the canvas
        public Text inputText;              // where commands are entered
        public Text consoleText;            // command output is placed here
        
        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
            _engine.OnGlobalCommandEvent += EngineOnOnGlobalCommandEvent;
            AddGlobalCommands();
        }
        
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.BackQuote))
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
                        inputText.text = string.Empty;
                    }
                }
            }
        }
        
        private string EngineOnOnGlobalCommandEvent(string command, string[] args)
        {
            return $"not handled {command}";
        }

        private void AddGlobalCommands()
        {
            _engine.Commands.Add(new GenericCommand("help", "shows this help"));
            _engine.Commands.Add(new GenericCommand("clear", "clears console"));
        }
        
        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

    }
}
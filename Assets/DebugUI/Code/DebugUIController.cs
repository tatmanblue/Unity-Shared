using System;
using System.Collections.Generic;
using TatmanGames.Common.ServiceLocator;
using TatmanGames.DebugUI;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace TatmanGames.DebugUI
{
    
    public class DebugUIController : MonoBehaviour
    {
        private CommandEngine _engine = new CommandEngine();

        [Header("UI Components")] 
        public KeyCode activationKey = KeyCode.BackQuote;
        public Canvas consoleCanvas;        // just the canvas
        public Text inputText;              // where commands are entered
        public Text consoleText;            // command output is placed here

        private void Awake()
        {
            GlobalServicesLocator.Instance.AddService("CommandEngine", _engine);
            _engine.OnGlobalCommandEvent += EngineOnGlobalCommandEvent;
            AddGlobalCommands();
        }

        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            // on activationKey toggle if the debug window is visible or not
            if(Input.GetKeyDown(activationKey))
            {
                // when activeInHierarchy is false, we are activating it and opening the window
                // and vice versa when activeInHierarchy is true
                bool canvasShowing = consoleCanvas.gameObject.activeInHierarchy;
                consoleCanvas.gameObject.SetActive(!canvasShowing);         // NOTE the ! inverting the value
                _engine.FireStateChange(canvasShowing == true ? DebugCommandWindowState.Closed : DebugCommandWindowState.Opened);
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
            _engine.AddCommand(new GenericCommand("help", "shows this help"));
            _engine.AddCommand(new GenericCommand("clear", "clears console"));
        }
        
        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

    }
}
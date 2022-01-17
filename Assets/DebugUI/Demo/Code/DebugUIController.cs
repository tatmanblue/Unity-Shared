using System.Collections;
using System.Collections.Generic;
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
        
        // Start is called before the first frame update
        private void Start()
        {
            consoleCanvas.gameObject.SetActive(false);
        }

        // Update is called once per frame
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
                        _engine.HandleCommand(inputText.text);
                        inputText.text = string.Empty;
                    }
                }
            }
        }
        
        private void AddMessageToConsole(string msg)
        {
            consoleText.text += msg + "\n";
        }

    }
}
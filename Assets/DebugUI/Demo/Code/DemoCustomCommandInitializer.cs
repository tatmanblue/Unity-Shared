using System;
using TatmanGames.DebugUI.Interfaces;
using UnityEngine;

namespace TatmanGames.DebugUI.Demo
{
    public class DemoCustomCommandInitializer : MonoBehaviour
    {
        private void Start()
        {
            IDebugEngine engine = DebugServiceLocator.Instance.Engine;
            if (null == engine)
                return;
            
            engine.AddCommand(new DemoCustomCommand());
            engine.OnStateChange += EngineOnStateChange;
        }

        private void EngineOnStateChange(DebugCommandWindowState state)
        {
            Debug.Log($"Debug Console state is {state}");
        }
    }
}
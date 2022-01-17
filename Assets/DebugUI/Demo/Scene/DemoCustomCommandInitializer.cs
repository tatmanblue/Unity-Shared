using System;
using UnityEngine;

namespace TatmanGames.DebugUI.Demo
{
    public class DemoCustomCommandInitializer : MonoBehaviour
    {
        private void Start()
        {
            DebugServiceLocator.Instance.Engine?.AddCommand(new DemoCustomCommand());
        }
    }
}
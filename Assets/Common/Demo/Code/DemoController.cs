using System;
using TMPro;
using UnityEngine;

namespace Common.Demo.Code
{
    public class DemoController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI SystemTime;
        
        private void Update()
        {
            SystemTime.text = $"{DateTime.Now:HH:mm:ss}";
        }
    }
}
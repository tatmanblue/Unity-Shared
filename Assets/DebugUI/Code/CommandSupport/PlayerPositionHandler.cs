using TatmanGames.Common;
using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.DebugUI.CommandSupport
{
    /// <summary>
    /// Handles functionality for moving Player object around by command
    /// </summary>
    public class PlayerPositionHandler : MonoBehaviour
    {
        private GameObject[] gos;
        private GameObject player;
        private Vector3 jumpTo = Vector3.zero;
        private int jumpIndex = -1;
        private bool doTeleport = false;

        public string JumpsTag { get; set; } = "JumpPoint";
        public string HomeTag { get; set; } = "StartHere";
        public string PlayerTag { get; set; } = "Player";
        public bool UseKeyboardCommands { get; set; } = true;

        private void Start()
        {
            gos = GameObject.FindGameObjectsWithTag(JumpsTag);
            player = GameObject.FindGameObjectWithTag(PlayerTag);
        }

        private void Update()
        {
            ShowPlayerPosition();
            
            if (true == doTeleport)
                return;

            if (false == UseKeyboardCommands)
                return;
            
            if (true == Keyboard.current.jKey.wasPressedThisFrame)
            {
                SetJumpToClosest();
            }

            if (true == Keyboard.current.nKey.wasPressedThisFrame)
            {
                SetJumpToNext();
            }

            if (true == Keyboard.current.homeKey.wasPressedThisFrame)
            {
                SetJumpToHome();
            }
        }

        private void LateUpdate()
        {
            if (true == doTeleport)
            {
                Log($"performing teleport to x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");
                doTeleport = false;
                SetPlayerEnabled(false);
                player.transform.position = jumpTo;
                SetPlayerEnabled(true);
                jumpTo = Vector3.zero;
            }
        }

        private GameObject FindClosestJumpPoint()
        {
            GameObject closest = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            return closest;
        }

        private void SetPlayerEnabled(bool state)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (null == controller) return;
            controller.enabled = state;
        }

        private void ShowPlayerPosition()
        {
            TMP_Text text = GameObject.Find("PositionText")?.GetComponent<TMP_Text>();
            if (null == text)
                return;
            
            Vector3 location = player.transform.position;
            text.text = $"X: {location.x} Y:{location.y} Z:{location.z}";
        }

        private void Log(string msg)
        {
            try
            {
                ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
                logger?.Log(msg);
            } catch (ServiceLocatorException) {}
        }

        private void LogWarning(string msg)
        {
            try
            {
                ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
                logger?.LogWarning(msg);
            } catch (ServiceLocatorException) {}
            
        }
        
        public void SetJumpToHome()
        {
            GameObject home = GameObject.Find(HomeTag);
            if (null == home)
            {
                LogWarning("Couldn't find home object");
                return;
            }

            jumpTo = home.transform.position;
            doTeleport = true;
            Log($"queuing returning to home x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");
        }

        public void SetJumpToNext()
        {
            if (0 == gos.Length)
            {
                LogWarning("no jumpTo objects found");
                return;
            }
                    
            jumpIndex++;
            if (jumpIndex >= gos.Length)
                jumpIndex = 0;

            jumpTo = gos[jumpIndex].transform.position;
            doTeleport = true;
            Log($"queuing next teleport x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z} {gos[jumpIndex].name}/({jumpIndex})");

        }

        public void SetJumpToClosest()
        {
            if (0 == gos.Length)
            {
                LogWarning("no jumpTo objects found");
                return;
            }

            GameObject closest = FindClosestJumpPoint();
            if (null == closest)
                return;
                
            jumpTo = closest.transform.position;
            doTeleport = true;
            Log($"queuing closest teleport x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");
        }
    }
}
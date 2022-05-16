using TatmanGames.Common.ServiceLocator;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using ILogger = TatmanGames.Common.Interfaces.ILogger;

namespace TatmanGames.DebugUI.CommandSupport
{
    public class PlayerPositionHandler : MonoBehaviour
    {
        private GameObject[] gos;
        private GameObject player;
        private Vector3 jumpTo = Vector3.zero;
        private int jumpIndex = -1;
        private bool doTeleport = false;

        private void Start()
        {
            gos = GameObject.FindGameObjectsWithTag("JumpPoint");
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
            ShowPlayerPosition();
            
            if (true == doTeleport)
                return;

            if (true == Keyboard.current.jKey.wasPressedThisFrame)
            {
                GameObject closest = FindClosestJumpPoint();
                if (null == closest)
                    return;
                
                jumpTo = closest.transform.position;
                doTeleport = true;
                logger?.Log($"queuing closest teleport x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");

            }

            if (true == Keyboard.current.nKey.wasPressedThisFrame)
            {
                GameObject[] allPoints = GameObject.FindGameObjectsWithTag("JumpPoint");
                jumpIndex++;
                if (jumpIndex >= allPoints.Length)
                    jumpIndex = 0;

                jumpTo = allPoints[jumpIndex].transform.position;
                doTeleport = true;
                logger?.Log($"queuing next teleport x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z} {allPoints[jumpIndex].name}/({jumpIndex})");
            }

            if (true == Keyboard.current.homeKey.wasPressedThisFrame)
            {
                GameObject home = GameObject.Find("StartHere");
                if (null == home)
                {
                    Debug.LogWarning("Couldnt find home object");
                    return;
                }

                jumpTo = home.transform.position;
                doTeleport = true;
                logger?.Log($"queuing returning to home x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");
            }
        }

        private void LateUpdate()
        {
            if (true == doTeleport)
            {
                ILogger logger = GlobalServicesLocator.Instance.GetService<ILogger>();
                logger?.Log($"performing teleport to x:{jumpTo.x} y:{jumpTo.y} z:{jumpTo.z}");
                doTeleport = false;
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = jumpTo;
                player.GetComponent<CharacterController>().enabled = true;
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

        private void ShowPlayerPosition()
        {
            TMP_Text text = GameObject.Find("PositionText").GetComponent<TMP_Text>();
            Vector3 location = player.transform.position;
            text.text = $"X: {location.x} Y:{location.y} Z:{location.z}";
        }
    }
}
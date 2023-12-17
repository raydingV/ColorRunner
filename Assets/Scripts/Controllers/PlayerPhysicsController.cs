using Managers;
using Signals;
using Enums;
using UnityEngine;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] PlayerManager manager;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Ramp"))
            {
                // manager.JumpPlayerOnRamp();
                other.gameObject.GetComponent<Collider>().enabled = false;
            }

            if(other.CompareTag("Gate"))
            {
                PlayerSignals.Instance.onChangeAllCollectableColorType(other.GetComponent<GateController>().currentColorType);
            }

            if (other.CompareTag("ExitDroneArea"))
            {
                PlayerSignals.Instance.onPlayerExitDroneArea?.Invoke();
            }

            if (other.CompareTag("TurretArea"))
            {
                PlayerSignals.Instance.onPlayerEnterTurretArea?.Invoke();
                manager.ChangeForwardSpeed(PlayerSpeedState.EnterTurretArea);
            }

            if (other.CompareTag("ExitTurretArea"))
            {
                PlayerSignals.Instance.onPlayerExitTurretArea?.Invoke();
            }
            
            if(other.CompareTag("IdleTrigger"))
            {
                PlayerSignals.Instance.onPlayerEnterIdleArea?.Invoke();
                StackSignals.Instance.onMergeToPLayer?.Invoke();
                PlayerSignals.Instance.onActivateObject?.Invoke();
                other.gameObject.SetActive(false);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("DroneArea")) // change name Drone Area
            {
                ScoreSignals.Instance.onHideScore?.Invoke();
                PlayerSignals.Instance.onPlayerEnterDroneArea?.Invoke();
            }
            
            if (other.CompareTag("ExitTurretArea"))
            {
                manager.ChangeForwardSpeed(PlayerSpeedState.Normal);
            }
        }
    }
}
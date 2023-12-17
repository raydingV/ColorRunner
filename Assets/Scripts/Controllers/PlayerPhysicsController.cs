using System;
using Managers;
using Signals;
using StateMachine;
using DG.Tweening;
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
                Material color = other.GetComponent<MeshRenderer>().material;
                //PlayerSignals.Instance.onChangeMaterial(color); delete that signal when ever refactor code
                PlayerSignals.Instance.onChangeAllCollectableColorType(other.GetComponent<GateController>().currentColorType);
            }
            
            if(other.CompareTag("DroneArea")) // change name Drone Area
            {
                PlayerSignals.Instance.onPlayerEnterDroneArea?.Invoke();
                ScoreSignals.Instance.onHideScore?.Invoke();
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
           
            if(other.CompareTag("IdleTrigger"))
            {
                PlayerSignals.Instance.onTranslateCameraState?.Invoke(new CameraIdleState());
                UISignals.Instance.onOpenPanel?.Invoke(UIPanels.EndGamePrizePanel);
                Debug.Log("idle trigger is done");
            }
            
            if (other.CompareTag("ExitTurretArea"))
            {
                PlayerSignals.Instance.onPlayerExitTurretArea?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ExitTurretArea"))
            {
                manager.ChangeForwardSpeed(PlayerSpeedState.Normal);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("MainBuilding") || other.CompareTag("SideBuilding"))
            {
                string nameOfBuilding = other.GetComponentInParent<BuildingManager>().gameObject.name;
                IdleSignals.Instance.onPlayerEnterBuildingArea?.Invoke(nameOfBuilding, other.name);
            }
        }
    }
}
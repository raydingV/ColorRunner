using System;
using Managers;
using Signals;
using StateMachine;
using DG.Tweening;
using UnityEngine;


namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        [SerializeField] PlayerManager manager;

        private void OnTriggerEnter(Collider other)
        {
            #region Runner Area
            if(other.CompareTag("Ramp"))
            {
                manager.JumpPlayerOnRamp();
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
                //manager.DeactivateMovement();
                PlayerSignals.Instance.onPlayerEnterDroneArea?.Invoke();
            }

            if (other.CompareTag("TurretArea"))
            {
                PlayerSignals.Instance.onPlayerEnterTurretArea?.Invoke();
            }
           
            if(other.CompareTag("IdleTrigger"))
            {
                PlayerSignals.Instance.onTranslateCameraState?.Invoke(new CameraIdleState());
                Debug.Log("idle trigger is done");
            }
            #endregion

            #region Idle Area
            // if (other.CompareTag("MainBuilding") || other.CompareTag("SideBuilding"))
            // {
            //     string nameOfBuilding = other.GetComponentInParent<UmutBuildingManager>().gameObject.name;
            //     IdleSignals.Instance.onPlayerEnterBuildingArea?.Invoke(nameOfBuilding, other.name);
            // }
            #endregion
        }
        private void OnTriggerExit(Collider other)
        {
            #region Runner Area
            if (other.CompareTag("DroneArea"))
            {
                PlayerSignals.Instance.onPlayerExitDroneArea?.Invoke();
            }
            
            if (other.CompareTag("ExitTurretArea"))
            {
                PlayerSignals.Instance.onPlayerExitTurretArea?.Invoke();
            }
            #endregion

            #region Idle Area
            // if (other.CompareTag("MainBuilding") || other.CompareTag("SideBuilding"))
            // {
            //     string nameOfBuilding = other.GetComponentInParent<UmutBuildingManager>().gameObject.name;
            //     IdleSignals.Instance.onPlayerExitBuildingArea?.Invoke(nameOfBuilding, other.name);
            // }
            #endregion
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
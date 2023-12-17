﻿using Managers;
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
                Material color = other.GetComponent<MeshRenderer>().material;
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
                print("IdleTriggered");
                PlayerSignals.Instance.onPlayerEnterIdleArea?.Invoke();
                StackSignals.Instance.onMergeToPLayer?.Invoke();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("DroneArea")) // change name Drone Area
            {
                PlayerSignals.Instance.onPlayerEnterDroneArea?.Invoke();
                ScoreSignals.Instance.onHideScore?.Invoke();
            }
            
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
using System;
using Controllers;
using Enums;
using Signals;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

namespace Managers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion
        
        #region SeriliazeField
        
        [SerializeField] private MatController matControllerLeft;
        [SerializeField] private ColorType leftMatColorType;
        [SerializeField] private MatController matControllerRight;
        [SerializeField] private ColorType rightMatColorType;
        [SerializeField] private DroneController droneController;
        [SerializeField] private bool matLeft, matRight;
        #endregion

        #region Private Variables
        
        #endregion
        #endregion
        
        private void Awake()
        {
            Init();
            SendColorTypeToMats();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onLastCollectableEnterDroneArea += OnLastCollectableEnterDroneArea;
        }

        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onLastCollectableEnterDroneArea -= OnLastCollectableEnterDroneArea;
        }

        #endregion
        
        private void Start()
        {
            SetColorOfMats();
        }

        private void Init()
        {
            
        }

        #region SetMatsColors

        private void SendColorTypeToMats()
        {
            matControllerLeft.GetColorData(leftMatColorType);
            matControllerRight.GetColorData(rightMatColorType);
        }
        
        private void SetColorOfMats()
        {
            matControllerLeft.SetColorData(leftMatColorType);
            matControllerRight.SetColorData(rightMatColorType);
        }

        #endregion
        
        private void DisableMatControllersCollider()
        {
            matControllerLeft.DisableBoxCollider();
            matControllerRight.DisableBoxCollider();
        }

        private void CloseUpMat()
        {
            if(!matLeft)
                matControllerLeft.CloseMat();
            else if(!matRight)
                matControllerRight.CloseMat();
        }
        
        private async void OnLastCollectableEnterDroneArea()
        {
            print("LastCollectable");
            await Task.Delay(1000);
            print("outline changed");
            StackSignals.Instance.onActivateOutlineTrasition?.Invoke(OutlineType.NonOutline);
            await Task.Delay(1000);
            DisableMatControllersCollider();
            CloseUpMat();
            print("MatsAreClosing");
            await Task.Delay(250);
            droneController.StartDroneAnimation();
            print("Drone started");
        }
    }
}
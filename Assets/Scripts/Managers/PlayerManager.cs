using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerData _playerData;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerAnimationController playerAnimationController;


        #region Event Subsicription

        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onPointerDragged += OnInputDragged;
            InputSignals.Instance.onPointerReleased += OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate += OnInputParamsUpdate;
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onPointerDragged -= OnInputDragged;
            InputSignals.Instance.onPointerReleased -= OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate -= OnInputParamsUpdate;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
        }


        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;


        private void SetPlayerDataToControllers()
        {
            playerMovementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnInputDragged()
        {
            playerMovementController.ActivateMovement();
        }

        private void OnInputReleased()
        {
            playerMovementController.DeactivateMovement();
            playerMovementController.SetMovementValues(0, 0);
        }

        private void OnInputParamsUpdate(InputParameters inputParams)
        {
            playerMovementController.GetJoystickValues(inputParams);
        }

        private void OnChangePlayerGradientColor()
        {

        }

        private void OnChanePlayerColor()
        {

        }

        private void ActivateMovement()
        {

        }

        private void DeactivateMovement()
        {

        }

        private void OnReset()
        {

        }

    }
}
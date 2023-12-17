using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class PlayerManager: MonoBehaviour
    {
        #region Variables
        private PlayerData _playerData;
        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerPhysicsController playerPhysicsController;
        [SerializeField] private PlayerMeshController playerMeshController;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        #endregion

        #region Event Subsicription
    
        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            InputSignals.Instance.onPointerDown += OnPointerDown;
            InputSignals.Instance.onPointerDragged += OnInputDragged;
            InputSignals.Instance.onPointerReleased += OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate += OnInputParamsUpdate;
            InputSignals.Instance.onJoystickStateChange += OnJoystickStateChange;
            PlayerSignals.Instance.onPlayerEnterTurretArea += OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea += OnPlayerExitTurretArea;
            PlayerSignals.Instance.onPlayerEnterDroneArea += OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea += OnPlayerExitDroneArea;
        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            InputSignals.Instance.onPointerDown -= OnPointerDown;
            InputSignals.Instance.onPointerDragged -= OnInputDragged;
            InputSignals.Instance.onPointerReleased -= OnInputReleased;
            InputSignals.Instance.onInputParamsUpdate -= OnInputParamsUpdate;
            InputSignals.Instance.onJoystickStateChange -= OnJoystickStateChange;
            PlayerSignals.Instance.onPlayerEnterTurretArea -= OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea -= OnPlayerExitTurretArea;
            PlayerSignals.Instance.onPlayerEnterDroneArea -= OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnPlayerExitDroneArea;
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

        private void OnPlay()
        {
            playerMovementController.ActivateMovement();
        }

        private void OnPointerDown()
        {
            playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
            playerMovementController.JoystickPressState(true, false, false);
        }
        
        private void OnInputDragged()
        {
            // playerMovementController.ActivateMovement();
            playerMovementController.JoystickPressState(false, true, false);
        }
        
        private void OnInputReleased()
        {
            playerMovementController.JoystickPressState(false, false, true);
            playerAnimationController.SetAnimationState(SticmanAnimationType.Idle);
            playerMovementController.SetInputValues(new InputParams() { XValue = 0, YValue = 0,});
        }

        private void OnInputParamsUpdate(InputParams inputParams)
        {
            playerMovementController.SetInputValues(inputParams);
        }      
        
        private void OnJoystickStateChange(JoystickStates joystickState)
        {
            playerMovementController.ChangeMovementType(joystickState);
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
                    break;
                case JoystickStates.Idle:
                    playerAnimationController.SetAnimationState(SticmanAnimationType.Idle);
                    break;
            }
        }

        public void JumpPlayerOnRamp()
        {
            // transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, 25, 25 * Time.deltaTime), transform.position.z);
            // transform.DOMoveY(15, 1f).SetEase(Ease.OutCubic).SetAutoKill();
        }
        
        private void OnChangePlayerGradientColor()
        {
            
        }

        private void OnChangePlayerColor(Color color) { playerMeshController.ChangeMaterialColor(color); }

        private void ActivateMovement() { playerMovementController.ActivateMovement(); }

        public void DeactivateMovement() { playerMovementController.DeactivateMovement(); }

        private void OnPlayerEnterTurretArea()
        { 
            _playerData.playerMovementData.RunnerForwardSpeed = 5f;
            SetPlayerDataToControllers();
            playerAnimationController.SetAnimationState(SticmanAnimationType.SneakWalk);
        }
        
        private void OnPlayerExitTurretArea() 
        { 
            _playerData.playerMovementData.RunnerForwardSpeed = 10f;
            SetPlayerDataToControllers();
            playerAnimationController.SetAnimationState(SticmanAnimationType.Run);
        }
        
        private void OnPlayerEnterDroneArea()
        {
            playerMovementController.DroneAreaMovement(transform);
        }

        private void OnPlayerExitDroneArea()
        {
            playerMovementController.ExitDroneAreaMovement();
        }
        
        private void OnReset()
        {
            
        }
    }
}
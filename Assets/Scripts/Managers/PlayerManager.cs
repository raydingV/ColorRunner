﻿using System;
using System.Threading.Tasks;
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
        #region Self Variables

        #region Public Variables

        

        #endregion Public Variables

        #region Seriliazed Field

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;

        #endregion Seriliazed Field

        #region Private
        
        private PlayerData _playerData;
        private Vector3 exitDroneAreaPosition;
        
        #endregion Private

        #endregion Self Variables

        private void Awake()
        {
            _playerData = GetPlayerData();
            SetPlayerDataToControllers();
        }
        
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;
        
        #region Event Subsicription
    
        void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onChangeGameState += OnGameStateChange;
            
            InputSignals.Instance.onInputTaken += OnPointerDown;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onJoystickDragged += OnJoystickDragged;
            
            PlayerSignals.Instance.onPlayerEnterDroneArea += OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea += OnPlayerExitDroneArea;
            PlayerSignals.Instance.onPlayerEnterTurretArea += OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea += OnPlayerExitTurretArea;
            RunnerSignals.Instance.onDroneAnimationComplated += OnDroneAnimationComplated;

        }
        
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onChangeGameState -= OnGameStateChange;
            
            InputSignals.Instance.onInputTaken -= OnPointerDown;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onJoystickDragged -= OnJoystickDragged;

            PlayerSignals.Instance.onPlayerEnterDroneArea -= OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnPlayerExitDroneArea;
            PlayerSignals.Instance.onPlayerEnterTurretArea -= OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea -= OnPlayerExitTurretArea;
            RunnerSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationComplated;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        
        private void SetPlayerDataToControllers()
        {
            movementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            // _playerData.playerMovementData.RunnerForwardSpeed = 5;
        }
            
        private void OnFailed() => movementController.IsReadyToPlay(false);
        
        private void OnPointerDown()
        {
            ActivateMovement();
            // animationController.SetAnimationState(SticmanAnimationType.Run);
        }
        
        private void OnInputReleased()
        {
            DeactivateMovement();
            // animationController.SetAnimationState(SticmanAnimationType.Idle);
        }
        private void OnInputDragged(RunnerInputParams inputParams) =>  movementController.UpdateRunnerInputValue(inputParams);
        
        private void OnJoystickDragged(IdleInputParams inputParams)  => movementController.UpdateIdleInputValue(inputParams);
   

        private void OnGameStateChange(GameStates gameState) => movementController.ChangeGameStates(gameState);
    
        public void StopVerticalMovement() => movementController.StopVerticalMovement();
        
        private void OnChangePlayerColor(Color color) { meshController.ChangeMaterialColor(color); }

        private void ActivateMovement() { movementController.ActivateMovement(); }

        public void DeactivateMovement() { movementController.DeactivateMovement(); }



        private void OnPlayerEnterDroneArea()
        {
            ChangeForwardSpeed(PlayerSpeedState.EnterDroneArea);
        }
        
        private void OnPlayerExitDroneArea()
        {
            exitDroneAreaPosition = transform.position;
            StopVerticalMovement();
            ChangeForwardSpeed(PlayerSpeedState.Stop);
        }
        
        private void OnDroneAnimationComplated()
        {
            StartVerticalMovement(exitDroneAreaPosition);
        }
        
        private void OnPlayerEnterTurretArea()
        {
            animationController.SetAnimationState(SticmanAnimationType.SneakWalk); // Collected stickmans dinleyecek
            ChangeForwardSpeed(PlayerSpeedState.EnterTurretArea);
        }
        
        private void OnPlayerExitTurretArea() 
        { 
            animationController.SetAnimationState(SticmanAnimationType.Run); // Collected stickmans dinleyecek
            ChangeForwardSpeed(PlayerSpeedState.Normal);
        }

        public void StartVerticalMovement(Vector3 exitPosition) => movementController.OnStartVerticalMovement(exitPosition);
        public void ChangeForwardSpeed(PlayerSpeedState changeSpeedState) => movementController.ChangeVerticalSpeed(changeSpeedState);
        
        private void OnReset()
        {
            Debug.Log("MovementReset");
            movementController.MovementReset();
            gameObject.SetActive(true);
        }
    }
}
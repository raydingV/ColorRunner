using Controllers;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using StateMachine;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables



        #region Seriliazed Field

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] public ParticleSystem particule; 

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

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
            StackSignals.Instance.onGetPlayer?.Invoke();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onChangeGameState += OnGameStateChange;
            CoreGameSignals.Instance.onReset += OnReset;
            
            InputSignals.Instance.onInputTaken += OnPointerDown;
            InputSignals.Instance.onInputReleased += OnInputReleased;
            InputSignals.Instance.onInputDragged += OnInputDragged;
            InputSignals.Instance.onJoystickDragged += OnJoystickDragged;

            PlayerSignals.Instance.onGetPlayerTransfrom += OnGetPlayerTransform;
            PlayerSignals.Instance.onPlayerEnterDroneArea += OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea += OnPlayerExitDroneArea;
            PlayerSignals.Instance.onPlayerEnterTurretArea += OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea += OnPlayerExitTurretArea;
            PlayerSignals.Instance.onPlayerEnterIdleArea += OnPlayerEnterIdleArea;
            PlayerSignals.Instance.onPlayerScaleUp += OnPlayerScaleUp;
            PlayerSignals.Instance.onTranslatePlayerAnimationState += OnTranslatePlayerAnimationState;
            PlayerSignals.Instance.onScaleDown += OnScaleDown;
            PlayerSignals.Instance.onThrowParticule += OnThrowParticule;    

            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            
            RunnerSignals.Instance.onDroneAnimationComplated += OnDroneAnimationComplated;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onChangeGameState -= OnGameStateChange;
            CoreGameSignals.Instance.onReset -= OnReset;

            InputSignals.Instance.onInputTaken -= OnPointerDown;
            InputSignals.Instance.onInputReleased -= OnInputReleased;
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            InputSignals.Instance.onJoystickDragged -= OnJoystickDragged;

            PlayerSignals.Instance.onGetPlayerTransfrom -= OnGetPlayerTransform;
            PlayerSignals.Instance.onPlayerEnterDroneArea -= OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnPlayerExitDroneArea;
            PlayerSignals.Instance.onPlayerEnterTurretArea -= OnPlayerEnterTurretArea;
            PlayerSignals.Instance.onPlayerExitTurretArea -= OnPlayerExitTurretArea;
            PlayerSignals.Instance.onPlayerEnterIdleArea -= OnPlayerEnterIdleArea;
            PlayerSignals.Instance.onPlayerScaleUp -= OnPlayerScaleUp;
            PlayerSignals.Instance.onTranslatePlayerAnimationState -= OnTranslatePlayerAnimationState;
            PlayerSignals.Instance.onScaleDown -= OnScaleDown;
            PlayerSignals.Instance.onThrowParticule -= OnThrowParticule;    

            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            
            RunnerSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationComplated;
        }

        public void OnThrowParticule()
        {
            particule.Play();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subsicription

        private void SetPlayerDataToControllers()
        {
            movementController.SetMovementData(_playerData.playerMovementData);
        }

        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
            // _playerData.playerMovementData.RunnerForwardSpeed = 5;
        }
        
        private void OnPointerDown()

        {
            ActivateMovement();
            if (CoreGameSignals.Instance.onGetGameState() == GameStates.Idle)
            {
                animationController.TranslatePlayerAnimationState(new RunnerAnimationState());
            }
        }

        private void OnInputReleased()
        {
            DeactivateMovement();
            if (CoreGameSignals.Instance.onGetGameState() == GameStates.Idle)
            {
                animationController.TranslatePlayerAnimationState(new IdleAnimationState());
            }
        }

        private void OnInputDragged(RunnerInputParams inputParams) => movementController.UpdateRunnerInputValue(inputParams);

        private void OnJoystickDragged(IdleInputParams inputParams) => movementController.UpdateIdleInputValue(inputParams);

        private void OnGameStateChange(GameStates gameState) => movementController.ChangeGameStates(gameState);

        public void StopVerticalMovement() => movementController.StopVerticalMovement();

        private void OnChangePlayerColor(Color color)
        { meshController.ChangeMaterialColor(color); }

        private void ActivateMovement()
        { movementController.ActivateMovement(); }

        public void DeactivateMovement()
        { movementController.DeactivateMovement(); }

        private void OnPlayerEnterDroneArea()
        {
            exitDroneAreaPosition = transform.position;
            StopVerticalMovement();
            ChangeForwardSpeed(PlayerSpeedState.Stop);
        }

        private void OnPlayerExitDroneArea()
        {
        }
        
        private void OnDroneAnimationComplated()
        {
            StartVerticalMovement(exitDroneAreaPosition);
        }

        private void OnPlayerEnterTurretArea()
        {
            ChangeForwardSpeed(PlayerSpeedState.EnterTurretArea);
            //animationController.SetAnimationState(SticmanAnimationType.SneakWalk); // Collected stickmans dinleyecek
        }

        private void OnPlayerExitTurretArea()
        {
            ChangeForwardSpeed(PlayerSpeedState.Normal);
            //animationController.SetAnimationState(SticmanAnimationType.Run); // Collected stickmans dinleyecek
        }

        private void OnPlayerEnterIdleArea()
        {
            print("Player Mesh Enabled");

            movementController.StopVerticalMovement();
            movementController.ChangeGameStates(GameStates.Idle);
            animationController.gameObject.SetActive(true);
        }

        private void OnPlayerScaleUp()
        {
            if (transform.localScale.x >= _playerData.playerMovementData.MaxSizeValue) return;
            transform.DOScale(transform.localScale + Vector3.one * _playerData.playerMovementData.SizeUpValue, .1f);
        }

        private void OnScaleDown()
        {
            if (transform.localScale.x <= _playerData.playerMovementData.MinSizeValue) return;
            transform.DOScale(transform.localScale + Vector3.one * -_playerData.playerMovementData.SizeUpValue * 2, .1f);
        }

        private void OnTranslatePlayerAnimationState(AnimationStateMachine state)
        {
            animationController.TranslatePlayerAnimationState(state);
        }

        public void StartVerticalMovement(Vector3 exitPosition) => movementController.OnStartVerticalMovement(exitPosition);

        public void ChangeForwardSpeed(PlayerSpeedState changeSpeedState) => movementController.ChangeVerticalSpeed(changeSpeedState);

        private Transform OnGetPlayerTransform() => transform;

        private void OnLevelFailed() => movementController.IsReadyToPlay(false);
        
        private void OnReset()
        {
            ChangeForwardSpeed(PlayerSpeedState.Normal);
            movementController.MovementReset();
            animationController.gameObject.SetActive(false);
            transform.DOScale(Vector3.one, .1f);
            movementController.OnReset();
        }
    }
}
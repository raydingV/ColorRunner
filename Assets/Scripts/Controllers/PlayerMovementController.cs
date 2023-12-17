using System;
using Data.ValueObject;
using Enums;
using Keys;
using Signals;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
using Managers;


namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;

        private PlayerMovementData _playerMovementData;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isReadyToMove;
        private bool _runnerMovement;
        private bool _idleMovement;
        private bool _isPressed, _isDragged, _isReleased;

        #region EventSubscription

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
            PlayerSignals.Instance.onPlayerRotate += OnPlayerRotate;
        }
        
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerRotate -= OnPlayerRotate;
        }

        #endregion
        
        public void SetMovementData(PlayerMovementData movementData)
        {
            _playerMovementData = movementData;
        }

        private void FixedUpdate()
        {
            if (_isReadyToMove)
            {
                if (_runnerMovement)
                {
                    RunnerMove();
                    if (_isDragged)
                    {
                        RunnerRotate();
                    }
                    else if (_isReleased)
                    {
                        RunnerRotateNormal();
                    }
                }
                else if (_idleMovement)
                {
                    if (_isDragged)
                    {
                        IdleMove();
                        IdleRotate();
                    }
                    else if (_isReleased)
                    {
                        Stop();
                    }
                }
            }
            else
                Stop();
        }

        public void ActivateMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactivateMovement()
        {
            _isReadyToMove = false;
        }

        public void JoystickPressState(bool isPressed, bool isDragged, bool isReleased)
        {
            _isPressed = isPressed;
            _isDragged = isDragged;
            _isReleased = isReleased;
        }
        
        private void RunnerMove()
        {
            rigidBody.velocity = new Vector3(_horizontalInput * _playerMovementData.RunnerSidewaySpeed, rigidBody.velocity.y,
                _playerMovementData.RunnerForwardSpeed);
            Clamp();
        }

        private void Clamp()
        {
            var pos = transform.position;
            pos.x =  Mathf.Clamp(transform.position.x, _playerMovementData.ClampValues.x, _playerMovementData.ClampValues.y);
            transform.position = pos;
        }

        private void RunnerRotate()
        {
            Vector3 direction = Vector3.forward + Vector3.right * Mathf.Clamp(_horizontalInput,
                -_playerMovementData.RunnerMaxRotateAngle, _playerMovementData.RunnerMaxRotateAngle);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                _playerMovementData.RunnerTurnSpeed);
        }

        private float OnPlayerRotate()
        {
            return transform.rotation.y;
        }

        private void RunnerRotateNormal()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        
        private void IdleMove()
        {
            rigidBody.velocity = new Vector3(_horizontalInput * _playerMovementData.IdleSpeed, rigidBody.velocity.y,
                 _verticalInput * _playerMovementData.IdleSpeed);
        }

        private void IdleRotate()
        {
            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                Vector3 direction = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction),
                    _playerMovementData.IdleTurnSpeed);
            }
        }
        
        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            rigidBody.angularVelocity = Vector3.zero;
        }
        
        public void SetInputValues(InputParams inputParams)
        {
            _horizontalInput = inputParams.XValue;
            _verticalInput = inputParams.YValue;
        }

        public void DroneAreaMovement(Transform _transform)
        {
            _transform.DOMoveZ(10, 3f).SetRelative().OnComplete(() =>
            {
                _playerMovementData.RunnerForwardSpeed = 0f;
            });
        }

        public void ExitDroneAreaMovement()
        {
            _playerMovementData.RunnerForwardSpeed = 10f;
        }
        
        public void ChangeMovementType(JoystickStates joystickState)
        {
            switch (joystickState)
            {
                case JoystickStates.Runner:
                    _runnerMovement = true;
                    _idleMovement = false;
                    break;
                case JoystickStates.Idle:
                    _runnerMovement = false;
                    _idleMovement = true;
                    break;
            }
        }
    }
}
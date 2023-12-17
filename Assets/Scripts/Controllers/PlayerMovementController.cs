using Enums;
using Keys;
using DG.Tweening;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public
        
        #endregion

        #region Serialized

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private GameStates currentGameState;
        [SerializeField] private PlayerManager manager;
        
        #endregion

        #region Private

        private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay, _isMovingVertical;
        private float _inputValueX;
        private Vector2 _clampValues;
        private Vector3 _movementDirection;
        private PlayerSpeedState _speedState;

        #endregion
        
        #endregion
        
        public void SetMovementData(PlayerMovementData movementData) => _movementData = movementData;
        public void ActivateMovement() => _isReadyToMove = true;
        public void DeactivateMovement() => _isReadyToMove = false;

        public void UpdateRunnerInputValue(RunnerInputParams inputParam)
        {
            _inputValueX = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }
        public void UpdateIdleInputValue(IdleInputParams inputParam) => _movementDirection = inputParam.joystickMovement;
        public void IsReadyToPlay(bool state) => _isReadyToPlay = state;
        public void ChangeGameStates(GameStates currentState) => currentGameState = currentState;

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    if (currentGameState == GameStates.Runner)
                    {
                        RunnerMove();
                    }
                    else if (currentGameState == GameStates.Idle)
                    {
                        IdleMove();
                    }
                }
                else
                {
                    if (currentGameState == GameStates.Runner)
                    {
                        RunnerStopSideways();
                    }
                    else if (currentGameState == GameStates.Idle)
                    {
                        Stop();
                    }
                }
            }
            else
                Stop();
        }
        
        private void RunnerMove()
        {
            Vector3 velocity = rigidBody.velocity;
            velocity = new Vector3(_inputValueX * _movementData.RunnerSidewaySpeed, velocity.y,
                _movementData.RunnerForwardSpeed);
            rigidBody.velocity = velocity;
            Clamp();
            rigidBody.angularVelocity = Vector3.zero;   // Gecici cozum
        }

        private void Clamp()
        {
            Vector3 position = rigidBody.position;
            position = new Vector3(Mathf.Clamp(rigidBody.position.x, _movementData.ClampValues.x, _movementData.ClampValues.y),
                position.y, position.z);
         
            rigidBody.position = position;
        }
        
        private void IdleMove()
        {
            Vector3 velocity = rigidBody.velocity;
            velocity = new Vector3(_movementDirection.x * _movementData.IdleSpeed, velocity.y,
                _movementDirection.z * _movementData.IdleSpeed);
            rigidBody.velocity = velocity;

            if (_movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_movementDirection);
                transform.rotation = toRotation;
                return;
            }
        }
        private void RunnerStopSideways()
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, _movementData.RunnerForwardSpeed);
        }

        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
            
            rigidBody.angularVelocity = Vector3.zero;
        }
        public void StopVerticalMovement()
        {
            ChangeVerticalSpeed(PlayerSpeedState.Stop);

            rigidBody.angularVelocity = Vector3.zero;
        }

        public void OnStartVerticalMovement()
        {
            transform.DOMoveZ(20, .5f).SetRelative(true).SetEase(Ease.OutSine)
                .OnComplete(() => { ChangeVerticalSpeed(PlayerSpeedState.Normal);});
        }

        public void ChangeVerticalSpeed(PlayerSpeedState changeSpeedState)
        {
            _movementData.RunnerForwardSpeed = (int)changeSpeedState;

            _speedState = changeSpeedState;
        }

        public  void MovementReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }

        public void OnReset()
        {
            DOTween.KillAll();
        }
    }
}
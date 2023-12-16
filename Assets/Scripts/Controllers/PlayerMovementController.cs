using System;
using Data.ValueObject;
using Keys;
using Unity.Mathematics;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidBody;

        private PlayerMovementData _playerMovementData;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isReadyToMove;
        
        public void SetMovementData(PlayerMovementData movementData)
        {
            _playerMovementData = movementData;
        }

        private void FixedUpdate()
        {
            if (_isReadyToMove)
            {
                IdleMove();
                IdleRotation();
            }
            else
            {
                Stop();
            }
        }

        public void ActivateMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactivateMovement()
        {
            _isReadyToMove = false;
        }
        
        private void RunnerMove()
        {
            
        }
        
        private void IdleMove()
        {
            rigidBody.velocity = new Vector3(_horizontalInput * _playerMovementData.IdleSpeed, rigidBody.velocity.y,
                 _verticalInput * _playerMovementData.IdleSpeed);
        }

        private void IdleRotation()
        {
            Vector3 direction = Vector3.forward * _verticalInput + Vector3.right * _horizontalInput;
            rigidBody.rotation = Quaternion.Slerp(rigidBody.rotation, Quaternion.LookRotation(direction),
                _playerMovementData.IdleTurnSpeed);
        }

        private void Stop()
        {
            rigidBody.velocity = Vector3.zero;
        }
        
        public void GetJoystickValues(InputParameters inputParams)
        {
            _horizontalInput = inputParams.ValueOfX;
            _verticalInput = inputParams.ValueOfY;
        }
        
        public void SetMovementValues(float horizontalInput, float verticalInput)
        {
            _horizontalInput = horizontalInput;
            _verticalInput = verticalInput;
        }
    }
}
using System;
using Commands;
using DG.Tweening;
using Enums;
using JetBrains.Annotations;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField] [CanBeNull] private Transform[]  movePath;
        [SerializeField] private float[] moveSpeed;
        [SerializeField] private float delay;

        private Sequence _sequence;
        private DroneMovementCommand _droneMovementCommand;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        private void Awake()
        {
            _droneMovementCommand = new DroneMovementCommand(transform, _sequence, ref movePath, ref moveSpeed, delay);
        }
        
        public void StartDroneAnimation()
        {
            _droneMovementCommand.Execute();
        }

        private void OnReset()
        {
            _sequence.Kill();
        }
    }
}
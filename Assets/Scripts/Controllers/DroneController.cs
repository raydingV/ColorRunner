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

        private void Awake()
        {
            _droneMovementCommand = new DroneMovementCommand(transform, _sequence, ref movePath, ref moveSpeed, delay);
        }
        
        public void StartDroneAnimation()
        {
            _droneMovementCommand.Execute();
        }
    }
}
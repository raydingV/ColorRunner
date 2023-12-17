using System;
using System.Collections.Generic;
using Managers;
using Cinemachine;
using UnityEngine;

namespace StateMachine
{
    public abstract class CameraStateMachine
    {
        #region Self Variables

        #region Protected Variables

        protected CameraManager ? _cameraManager { get; set; }
        protected Transform ? _target { get; set; }
        protected CinemachineVirtualCamera ? _runnerCamera { get; set; }
        protected CinemachineVirtualCamera ? _idleCamera { get; set; }
        protected Animator ? _cinamationAnimationStates{ get; set; }

        #endregion

        #endregion

        public void SetContext(CameraManager cameraManager)
        {
            _cameraManager = cameraManager;
            _target = _cameraManager.Player;
            _runnerCamera = _cameraManager.RunnerCam;
            _idleCamera = _cameraManager.IdleCam;
            _cinamationAnimationStates = _cameraManager.StateDrivenCameraAnimator;
        }

        public abstract void ChangeStateCamera();
    }
}

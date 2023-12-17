using System.Collections;
using System.Diagnostics.Contracts;
using StateMachine;
using Cinemachine;
using UnityEngine;
using Signals;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables

        public CinemachineVirtualCamera InitializeCam;
        public CinemachineVirtualCamera RunnerCam;
        public CinemachineVirtualCamera MiniGameCam;
        public CinemachineVirtualCamera IdleCam;
        public Animator StateDrivenCameraAnimator;
        public Transform Player;

        #endregion

        #region Private 

        private CameraStateMachine _state;
        #endregion

        #endregion
        
        private void Awake()
        {
            StateDrivenCameraAnimator = GetComponent<Animator>();
            onTranslateCameraState(new CameraInitializeState());
        }
        
        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            
            PlayerSignals.Instance.onTranslateCameraState += onTranslateCameraState;
        }

        private void UnSubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;

            PlayerSignals.Instance.onTranslateCameraState -= onTranslateCameraState;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnPlay()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            
            InitializeCam.PreviousStateIsValid = false;
            RunnerCam.PreviousStateIsValid = false;
            MiniGameCam.PreviousStateIsValid = false;
            IdleCam.PreviousStateIsValid = false;
            onTranslateCameraState(new CameraRunnerState());
        }

        
        private void onTranslateCameraState(CameraStateMachine state)
        {
            _state = state;
            _state.SetContext(this);
            _state.ChangeStateCamera();
        }

        private void OnReset()
        {
            Player = PlayerSignals.Instance.onGetPlayerTransfrom();
            onTranslateCameraState(new CameraInitializeState());
        }
    }
}
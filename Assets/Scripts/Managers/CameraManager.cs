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
            RunnerCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            MiniGameCam = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            IdleCam = transform.GetChild(2).GetComponent<CinemachineVirtualCamera>();
            StateDrivenCameraAnimator = GetComponent<Animator>();
            onTranslateCameraState(new CameraRunnerState());
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
            PlayerSignals.Instance.onPlayerEnterDroneArea += OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea += OnDroneAnimationComplated;
            // RunnerSignals.Instance.onDroneAnimationComplated += OnDroneAnimationComplated;
        }

        private void UnSubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;

            PlayerSignals.Instance.onTranslateCameraState -= onTranslateCameraState;
            PlayerSignals.Instance.onPlayerEnterDroneArea -= OnPlayerEnterDroneArea;
            PlayerSignals.Instance.onPlayerExitDroneArea -= OnDroneAnimationComplated;
            // RunnerSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationComplated;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void OnPlay()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            RunnerCam.PreviousStateIsValid = false;
            MiniGameCam.PreviousStateIsValid = false;
            IdleCam.PreviousStateIsValid = false;
            onTranslateCameraState(new CameraRunnerState());
        }
        private void OnPlayerEnterDroneArea() => RunnerCam.Follow = null;


        private void OnDroneAnimationComplated() => RunnerCam.Follow = Player;
        
        private void onTranslateCameraState(CameraStateMachine state)
        {
            _state = state;
            _state.SetContext(this);
            _state.ChangeStateCamera();
        }

        private void OnReset()
        {
            Player = PlayerSignals.Instance.onGetPlayerTransfrom();
            onTranslateCameraState(new CameraRunnerState());
        }
    }
}
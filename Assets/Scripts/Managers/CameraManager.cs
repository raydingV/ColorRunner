using System.Collections;
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
        public CinemachineVirtualCamera IdleCam;
        public Animator StateDrivenCameraAnimator;
        public Transform Player;

        #endregion

        #region Private 

        private CameraStateMachine _state;

        #endregion

        #endregion
        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onTranslateCameraState += onTranslateCameraState;
        }

        private void UnSubscribe()
        {
            PlayerSignals.Instance.onTranslateCameraState += onTranslateCameraState;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion

        private void Awake()
        {
            RunnerCam = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            IdleCam = transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
            StateDrivenCameraAnimator = GetComponent<Animator>();
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            //_state = new CameraRunnerState();
            //_state.SetContext(this);
            //_state.ChangeStateCamera();
            onTranslateCameraState(new CameraRunnerState());

        }

        private void onTranslateCameraState(CameraStateMachine state)
        {
            _state = state;
            _state.SetContext(this);
            _state.ChangeStateCamera();
        }
       
    }
}
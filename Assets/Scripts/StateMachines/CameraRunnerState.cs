namespace StateMachine
{
    public class CameraRunnerState : CameraStateMachine
    {
        public override void ChangerStateCamera()
        {
            _runnerCamera.Follow = _target;
            _cinamationAnimationStates.Play("RunnerCam");
        }
    }
}

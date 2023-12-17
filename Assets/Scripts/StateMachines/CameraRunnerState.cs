using Signals;
using Enums;

namespace StateMachine
{
    public class CameraRunnerState : CameraStateMachine
    {
        public override void ChangeStateCamera()
        {
            _runnerCamera.Follow = _target;
            _cinamationAnimationStates.Play("RunnerCam");
            StackSignals.Instance.onActivateOutlineTrasition(OutlineType.Outline);
        }


    }
}
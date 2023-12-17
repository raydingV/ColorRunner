namespace StateMachine
{
    public class CameraIdleState : CameraStateMachine
    {
        public override void ChangeStateCamera()
        {
            _idleCamera.Follow = _target;
            //_idleCamera.LookAt = _target;
            _cinamationAnimationStates.Play("IdleCam");
        }
    }
}
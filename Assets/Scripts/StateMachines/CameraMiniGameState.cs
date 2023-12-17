namespace StateMachine
{
    public class CameraMiniGameState : CameraStateMachine
    {
        public override void ChangeStateCamera()
        {
            _miniGameCamera.Follow = _target;
            _cinamationAnimationStates.Play("MiniGameCam");
        }
    }
}
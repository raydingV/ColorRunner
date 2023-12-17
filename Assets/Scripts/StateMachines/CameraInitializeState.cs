namespace StateMachine
{
    public class CameraInitializeState : CameraStateMachine
    {
        public override void ChangeStateCamera()
        {
            _cinamationAnimationStates.Play("InitCam");
        }
    }
}
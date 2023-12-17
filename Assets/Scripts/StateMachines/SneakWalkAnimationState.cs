namespace StateMachine
{
    public class SneakWalkAnimationState : AnimationStateMachine
    {
        public override void ChangeAnimationState()
        {
            _animator.SetTrigger("SneakWalk");
        }
    }
}

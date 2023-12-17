namespace StateMachine
{
    public class IdleAnimationState : AnimationStateMachine
    {
        public override void ChangeAnimationState()
        {
            _animator.SetTrigger("Idle");
        }
    }
}
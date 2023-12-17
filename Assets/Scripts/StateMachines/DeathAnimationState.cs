namespace StateMachine
{
    public class DeathAnimationState : AnimationStateMachine
    {
        public override void ChangeAnimationState()
        {
            _animator.SetTrigger("Death");
        }
    }
}
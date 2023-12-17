namespace StateMachine
{
    public class RunnerAnimationState :AnimationStateMachine
    {
        public override void ChangeAnimationState()
        {
            _animator.SetTrigger("Run");
        }
    }
}

using Enums;
using Signals;

namespace StateMachine
{
    public class SneakIdleAnimationState : AnimationStateMachine
    {
        public override void ChangeAnimationState()
        {
            _animator.SetTrigger("SneakIdle");
        }
    }
}
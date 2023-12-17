using Enums;
using Signals;

namespace StateMachine
{
    public class SneakIdleAnimationState : AnimationStateMachine
    {
        private bool isPlay;
        
        public override void ChangeAnimationState()
        {
            if (!isPlay)
            {
                _animator.SetTrigger("SneakIdle");
                isPlay = true;
            }
        }
    }
}
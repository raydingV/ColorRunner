using UnityEngine;
using Enums;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public void SetAnimationState(SticmanAnimationType animType)
        {
            switch (animType)
            {
                case SticmanAnimationType.Idle:
                    animator.SetTrigger("Idle");
                    break;
                case SticmanAnimationType.Run:
                    animator.SetTrigger("Run");
                    break;
                case SticmanAnimationType.SneakWalk:
                    animator.SetTrigger("SneakWalk");
                    break;
                case SticmanAnimationType.SneakIdle:
                    animator.SetTrigger("SneakIdle");
                    break;
                case SticmanAnimationType.Die:
                    animator.SetTrigger("Death");
                    break;
                case SticmanAnimationType.MinigameThrow:
                    break;
            }
        }
    }
}
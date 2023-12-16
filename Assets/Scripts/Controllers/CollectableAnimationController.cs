using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {

        #region SelfVariables

        #region Serialize Variables

        [SerializeField] CollectableManager manager;

        #endregion

        #region Private Variavles

        private ParticleSystem _particleSystem;
        private Animator _animator;

        #endregion
        #endregion

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void AnimationStateChange()
        {
            _animator.SetTrigger("Run");
        }

        private void ActivateParticul()
        {

        }
    }
}
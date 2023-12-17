using System;
using System.Collections;
using Managers;
using Signals;
using StateMachine;
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
        private Animator _CollectableAnimator;
        private AnimationStateMachine _CollectableStateMachine;

        #endregion
        #endregion
        #region Subscriptions

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnSubscribe()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        #endregion
        
        
        
        private void Awake()
        {
            _CollectableAnimator = GetComponent<Animator>();
            if (manager.GetTag() == "Collected")
            {
                _CollectableStateMachine = new SneakIdleAnimationState();
            }
            else
            {
                _CollectableStateMachine = new IdleAnimationState();
            }
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }

       

        public void TranslateAnimationState(AnimationStateMachine state)
        {
            _CollectableStateMachine = state;
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }

        private void OnPlay()
        {
            if (manager.GetComponentInChildren<CollectablePhisicController>().CompareTag("Collected"))
            {
                TranslateAnimationState(new RunnerAnimationState());
            }
        }
        
        private void ActivateParticul()
        {

        }

        private void DestroyManager()
        {
            manager.gameObject.SetActive(false);
        }
    }
}
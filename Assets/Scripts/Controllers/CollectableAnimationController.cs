using System;
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
        
        private void Awake()
        {
            _CollectableAnimator = GetComponent<Animator>();
            Initialize();
        }
        
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

        private void Initialize()
        {
            if (manager.GetTag() == "Collected" && !CoreGameSignals.Instance.onIsGameRunning())
            {
                _CollectableStateMachine = new SneakIdleAnimationState();
            }
            else if(manager.GetTag() != "Collected" && !CoreGameSignals.Instance.onIsGameRunning())
            {
                _CollectableStateMachine = new IdleAnimationState();
            }
            
            else if (CoreGameSignals.Instance.onIsGameRunning())
            {
                _CollectableStateMachine = new RunnerAnimationState();
            }
            
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }
        
        public void TranslateCollectableAnimationState(AnimationStateMachine state)
        {
            _CollectableStateMachine = state;
            _CollectableStateMachine.SetContext(ref _CollectableAnimator);
            _CollectableStateMachine.ChangeAnimationState();
        }

        private void DestroyManager()
        {
            manager.gameObject.SetActive(false);
        }
        
        private void OnPlay()
        {
            TranslateCollectableAnimationState(new RunnerAnimationState());
        }
    }
}
using System;
using Managers;
using Signals;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region SelfVariables

        
        #region Serialize Variables

        [SerializeField] private PlayerManager manager;

        #endregion Serialize Variables

        #region Private Variavles

        private ParticleSystem _particleSystem;
        private Animator _playereAnimator;
        private AnimationStateMachine _playerStateMachine;

        #endregion Private Variavles

        #endregion SelfVariables
        private void Awake()
        {
            Initialize();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnterIdleArea += OnPlayerEnterIdleArea;
        }
        
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onPlayerEnterIdleArea -= OnPlayerEnterIdleArea;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        #endregion
        
        
        public void TranslatePlayerAnimationState(AnimationStateMachine state)
        {
            _playerStateMachine = state;
            _playerStateMachine.SetContext(ref _playereAnimator);
            _playerStateMachine.ChangeAnimationState();
        }

        private void OnPlayerEnterIdleArea()
        {
            transform.gameObject.SetActive(true);
        }
        private void Initialize()
        {
            _playereAnimator = GetComponent<Animator>();
            // _playerStateMachine = GetComponent<AnimationStateMachine>();
            TranslatePlayerAnimationState(new IdleAnimationState());
            transform.gameObject.SetActive(false);
        }
    }
}
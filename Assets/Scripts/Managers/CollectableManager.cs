using Controllers;
using Enums;
using Signals;
using StateMachine;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region SelfVariables

        #region Serialize Variables

        [SerializeField] private CollectableAnimationController animatorController;
        [SerializeField] private CollectableMeshController meshController;
        [SerializeField] private CollectablePhisicController physicController;

        #endregion Serialize Variables

        #region Private Variables

        [SerializeField] private bool _isDead;

        #endregion Private Variables

        #region public Variable

        public ColorType currentColorType;

        #endregion public Variable

        #endregion SelfVariables

        #region Subscriptions

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            PlayerSignals.Instance.onChangeMaterial += OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState += OnTranslateAnimationState;
            StackSignals.Instance.onActivateOutlineTrasition += OnActivateOutlineTrasition;
            RunnerSignals.Instance.onDroneAnimationComplated += OnDroneAnimationComplated;
            StackSignals.Instance.onChangeMatarialColor += OnChangeMatarialColor;
        }

        private void UnSubscribe()
        {
            PlayerSignals.Instance.onChangeMaterial -= OnSetCollectableMaterial;
            PlayerSignals.Instance.onTranslateAnimationState -= OnTranslateAnimationState;
            StackSignals.Instance.onActivateOutlineTrasition -= OnActivateOutlineTrasition;
            RunnerSignals.Instance.onDroneAnimationComplated -= OnDroneAnimationComplated;
            StackSignals.Instance.onChangeMatarialColor -= OnChangeMatarialColor;
        }

        #endregion Subscriptions

        public bool IsDead
        { get { return _isDead; } set { _isDead = value; } }

        public void AddCollectableToStackManager(Transform _transform)
        {
            StackSignals.Instance.onAddStack(_transform);
            _transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        public void OnChangeMatarialColor(ColorType type)
        {
            if (physicController.CompareTag("Collected"))
            {
                currentColorType = type;
                meshController.ChangeMatarialColor();
            }
        }

        public string GetTag()
        {
            return physicController.tag;
        }

        public bool CompareColor(ColorType type)
        {
            if (currentColorType == type)
            {
                return true;
            }
            else return false;
        }

        public void OnTranslateAnimationState(AnimationStateMachine state)
        {
            if (physicController.CompareTag("Collected"))
            {
                animatorController.TranslateAnimationState(state);
            }
        }

        public void RemoveCollectableFromStackManager(Transform transform)
        {
            StackSignals.Instance.onRemoveFromStack?.Invoke(transform);
        }

        public void RotateMeshForward()
        {
            StackSignals.Instance.onRemoveFromStack(transform);
        }

        private void OnActivateOutlineTrasition(OutlineType type)
        {
            if (physicController.CompareTag("Collected"))
            {
                meshController.ActivateOutlineTrasition(type);
            }
        }

        private void OnDroneAnimationComplated()
        {
            if (_isDead)
            {
                OnTranslateAnimationState(new DeathAnimationState());
            }
            else
            {
                OnTranslateAnimationState((new RunnerAnimationState()));
                meshController.ActivateOutlineTrasition(OutlineType.Outline);
            }
            StackSignals.Instance.onAddAfterDroneAnimationDone?.Invoke(_isDead, transform);
        }

        private void OnSetCollectableMaterial(Material material)
        {
            if (physicController.CompareTag("Collected"))
            {
                meshController.SetCollectableMatarial(material);
            }
        }
    }
}
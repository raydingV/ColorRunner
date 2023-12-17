using System;
using Managers;
using Signals;
using StateMachine;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhisicController : MonoBehaviour
    {

        #region Self Variables

        #region Serialize Variables

        [SerializeField] CollectableManager _manager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                other.tag = "Collected";
                if (_manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>().currentColorType))
                {
                    _manager.AddCollectableToStackManager(other.transform.parent);
                }
                else if (!_manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>()
                    .currentColorType)) //call from signals
                {
                    _manager.RemoveCollectableFromStackManager(transform.parent);
                    other.transform.parent.gameObject.SetActive(false);
                }
            }

            if (other.CompareTag("Obstacle"))
            {
                _manager.RemoveCollectableFromStackManager(_manager.transform);
                other.gameObject.SetActive(false);
            }
            
            if (other.CompareTag("MatTrigger"))
            {
                StackSignals.Instance.onStackEnterDroneArea?.Invoke(_manager.transform, other.transform);
                if (!_manager.CompareColor(other.GetComponent<MatController>().currentColorType))
                {
                    _manager.IsDead = true;
                }
            }

            if (other.CompareTag("TurretMatTrigger"))
            {
                if (!_manager.CompareColor(other.GetComponent<TurretMatController>().currentColorType))
                {
                    StackSignals.Instance.onWrongTurretMatAreaEntered?.Invoke(_manager.transform);
                }
            }

            if(other.CompareTag("TurretArea"))
            {
                _manager.OnTranslateAnimationState(new SneakWalkAnimationState());
            }

            if (other.CompareTag("ExitTurretArea"))
            {

                _manager.OnTranslateAnimationState(new RunnerAnimationState());
            }
        }

        
    }
}
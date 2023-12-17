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

        [SerializeField] private CollectableManager manager;

        #endregion Serialize Variables

        #endregion Self Variables

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                other.tag = "Collected";
                other.transform.parent.GetComponent<CollectableManager>().OnTranslateAnimationState(new RunnerAnimationState());
                if (manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>().currentColorType))
                {
                    manager.AddCollectableToStackManager(other.transform.parent);
                }
                else if (!manager.CompareColor(other.transform.parent.GetComponent<CollectableManager>()
                    .currentColorType)) //call from signals
                {
                    manager.RemoveCollectableFromStackManager(transform.parent);
                    other.transform.parent.gameObject.SetActive(false);
                }
            }

            if (other.CompareTag("Obstacle"))
            {
                manager.RemoveCollectableFromStackManager(manager.transform);
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("MatTrigger"))
            {
                StackSignals.Instance.onStackEnterDroneArea?.Invoke(manager.transform, other.transform);
                if (!manager.CompareColor(other.GetComponent<MatController>().currentColorType))
                {
                    manager.IsDead = true;
                }
            }

            if (other.CompareTag("TurretMatTrigger"))
            {
                if (!manager.CompareColor(other.GetComponent<TurretMatController>().currentColorType))
                {
                    StackSignals.Instance.onWrongTurretMatAreaEntered?.Invoke(manager.transform);
                    int randomDeath = Random.Range(0, 2);
                    if (randomDeath == 1) return;
                    manager.RemoveCollectableFromStackManager(transform);
                }
            }

            if (other.CompareTag("TurretArea"))
            {
                manager.OnTranslateAnimationState(new SneakWalkAnimationState());
            }

            if (other.CompareTag("ExitTurretArea"))
            {
                manager.OnTranslateAnimationState(new RunnerAnimationState());
            }

            if (other.CompareTag("RainbowArea") && !manager.IsTouchTheGate)
            {
                manager.EnterRainbowGate();
            }
        }
    }
}
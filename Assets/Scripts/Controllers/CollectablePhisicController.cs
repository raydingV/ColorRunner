using Managers;
using Signals;
using StateMachine;
using UnityEngine;
using Random = UnityEngine.Random;

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
                    manager.OnCOllisionWithObstacle(other.transform.parent.gameObject); 
                }
            }

            if (other.CompareTag("Obstacle"))
            {
                manager.OnCOllisionWithObstacle(other.gameObject); 
            }

            if (other.CompareTag("ExitDroneArea"))
            {
                manager.OnTranslateAnimationState(new RunnerAnimationState());
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
                    int randomDeath = Random.Range(0, 3);
                    if (randomDeath != 1) return;
                    manager.RemoveCollectableFromStackManager(manager.transform);
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

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ExitTurretArea"))
            {
                manager.OnTranslateAnimationState(new RunnerAnimationState());
            }

            if (other.CompareTag("ExitDroneArea"))
            {
                manager.OnTranslateAnimationState(new RunnerAnimationState());
            }
        }
    }
}
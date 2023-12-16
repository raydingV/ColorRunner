using Managers;
using Signals;
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
            if(other.CompareTag("Collectable") && CompareTag("Collected"))
            {
                StackSignals.Instance.onAddStack?.Invoke(other.transform);
            }

            //test purposes
            if(other.CompareTag("Player") && CompareTag("Collectable"))
            {
                StackSignals.Instance.onAddStack?.Invoke(transform.parent);
                _manager.RotateMeshForward();
            }

            if(other.CompareTag("Obstical"))
            {
                StackSignals.Instance.OnRemoveFromStack?.Invoke(transform);
                Destroy(other.gameObject); 
            }
        }
    }
}
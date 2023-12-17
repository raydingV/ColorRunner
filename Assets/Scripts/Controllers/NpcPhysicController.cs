using Assets.Scripts.Managers;
using Signals;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class NpcPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private NpcManager manager;

        #endregion Serialize Variables

        #endregion Self Variables

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                ScoreSignals.Instance.onTotalScoreUpdate(1);
                manager.MeshController.gameObject.SetActive(false);
            }
        }
    }
}
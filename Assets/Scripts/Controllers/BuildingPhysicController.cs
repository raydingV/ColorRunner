using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicController : MonoBehaviour
    {
        private float timer;
        private float delay = .1f;
        [SerializeField] private BuildingManager manager;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                timer += Time.deltaTime;
                if (timer >= delay && ScoreSignals.Instance.currentScore() >= 0)
                {
                    manager.OnPlayerEnter();
                    PlayerSignals.Instance.onThrowParticule?.Invoke();
                    timer = 0;
                }
            }
        }
    }
}

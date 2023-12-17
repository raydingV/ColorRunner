using System;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicController : MonoBehaviour
    {

        #region Variables

        #region Serialized

        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private BuildingManager manager;

        #endregion

        #region Private
        
        private float timer;
        private float delay = .1f;

        #endregion

        #endregion
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                particleSystem.Play();
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                timer += Time.deltaTime;
                if (timer >= delay && ScoreSignals.Instance.currentScore() >= 0)
                {
                    manager.OnPlayerEnter();
                    timer = 0;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                particleSystem.Stop();
            }
        }
    }
}
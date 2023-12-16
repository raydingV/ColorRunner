using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private PlayerManager manager;
        
        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        private void SubscribeEvents()
        {
            
        }

        private void UnSubscribeEvents()
        {
            
        }

        #endregion

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0,0,manager.transform.rotation.z * -1.0f);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
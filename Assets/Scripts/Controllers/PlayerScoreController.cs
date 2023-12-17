using System;
using Managers;
using TMPro;
using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerScoreController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private Vector3 followOffset;
        
        private Transform _follow;
        
        
        #endregion
        
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
            StackSignals.Instance.onSetScoreControllerPosition += OnSetPosition;
        }

        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onSetScoreControllerPosition -= OnSetPosition;
        }

        #endregion

        private void Update()
        {
            transform.position = new Vector3(_follow.position.x, _follow.position.y + followOffset.y,
                _follow.position.z + followOffset.z);
        }

        private void OnSetPosition(Transform _transform)
        {
            _follow = _transform;
        }
        
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
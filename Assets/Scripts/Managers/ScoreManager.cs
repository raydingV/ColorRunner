using System;
using Keys;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables

        private int _currentScore;
        private int _totalScore;
        private Transform _target;
        private TextMeshPro _scoreText;

        [SerializeField] private Vector3 followOffset;
        #endregion

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshPro>();
            OnHideScore();
        }

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
            ScoreSignals.Instance.onCurrentLevelScoreUpdate += OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate += OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore += ReturnCurrentScore;
            ScoreSignals.Instance.totalScore += ReturnTotalScore;
            ScoreSignals.Instance.onHideScore += OnHideScore;
            ScoreSignals.Instance.onUpdateScoreAfterDroneArea += OnUpdateScoreAfterDroneArea;

            StackSignals.Instance.onSetScoreControllerPosition += OnSetPosition;

            CoreGameSignals.Instance.onPlay += OnFindFollowTarget;
            CoreGameSignals.Instance.onReset += OnReset;
        }
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onCurrentLevelScoreUpdate -= OnCurrentLevelScoreUpdate;
            ScoreSignals.Instance.onTotalScoreUpdate -= OnTotalScoreUpdate;
            
            ScoreSignals.Instance.currentScore -= ReturnCurrentScore;
            ScoreSignals.Instance.totalScore -= ReturnTotalScore;
            ScoreSignals.Instance.onHideScore -= OnHideScore;
            ScoreSignals.Instance.onUpdateScoreAfterDroneArea += OnUpdateScoreAfterDroneArea;

            StackSignals.Instance.onSetScoreControllerPosition -= OnSetPosition;

            CoreGameSignals.Instance.onPlay -= OnFindFollowTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        #endregion

        private void Update()
        {
            if (_target == null) return;
            transform.position = new Vector3(_target.position.x, _target.position.y + followOffset.y,
                _target.position.z + followOffset.z);
        }

        private void OnFindFollowTarget()
        {
            try
            {
                _target = StackSignals.Instance.onGetFirstCollectable();
                _scoreText.text = _currentScore.ToString();
            }
            catch
            {
                Debug.Log("Player Cant find" , _target);
            }
        }

        private void OnCurrentLevelScoreUpdate()
        {
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
            SaveScoreParams();
        }

        private void OnTotalScoreUpdate(int score)
        {
            _totalScore += score;
            _scoreText.text = _currentScore.ToString();
            SaveScoreParams();
        }

        private void OnSetPosition(Transform _transform)
        {
            _target = _transform;
        }

        public void OnUpdateScoreAfterDroneArea()
        {
            int newScore = _currentScore * 2;
            _scoreText.text = newScore.ToString();
            /*
            for (int i = 0; i < _score; i++) !!write on stackmanager and call from here
            {
                StackSignals.Instance.onAddStack?.Invoke(player);
            }*/
            _currentScore = newScore;
        }

        private void OnHideScore()
        {
            _scoreText.text = "";
        }

        //store total scroe when p;ayer enter drone area

        private void OnReset()
        {
            _currentScore = 0;
        }
        
        #region refactred when saveManager is added
        private void SaveScoreParams()
        {
            //conver ejs version
            new ScoreParams() {currentLevelScore = _currentScore, totalScore = _totalScore };
        }
        
        private int ReturnCurrentScore() {return _currentScore; }
        
        private int ReturnTotalScore() {return _totalScore; }
        #endregion
    }
}